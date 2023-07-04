// <copyright file="TranslateResolver.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using OneFrameCGPTPlayground.Common.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace OneFrameCGPTPlayground.Common.Helpers.AutoMapper
{
    public class TranslateResolver<TSource, TDestination> : IValueResolver<TSource, TDestination, string>, ITranslateResolver
    {
        private readonly LanguageType _defaultLanguage;
        private readonly string _propName;

        public TranslateResolver(string propName)
        {
            _propName = propName;
            var lang = CultureInfo.DefaultThreadCurrentCulture?.TwoLetterISOLanguageName;
            if (string.IsNullOrEmpty(lang))
            {
                _defaultLanguage = LanguageType.en;
            }
            else
            {
                _defaultLanguage = (LanguageType)Enum.Parse(typeof(LanguageType), lang);
            }
        }

        public TranslateResolver(string propName, LanguageType defaultLanguage)
        {
            _propName = propName;
            _defaultLanguage = defaultLanguage;
        }

        public void Recursive(List<PropertyDictionary> list, out PropertyDictionary propertyDictionary)
        {
            var propertiesMap = new List<PropertyDictionary>();

            propertyDictionary = null;

            foreach (var prop in list)
            {
                var propValue = prop.Value.GetValue(prop.Key, null);

                var propProperties = propValue.GetType().GetProperties();

                var translations = propProperties.FirstOrDefault(i => i.Name == "Translations");

                if (translations != null)
                {
                    propertyDictionary = new PropertyDictionary() { Key = propValue, Value = translations };
                    break;
                }

                foreach (var item in propProperties.Where(i => i.PropertyType != typeof(string)).ToList())
                {
                    propertiesMap.Add(new PropertyDictionary() { Key = propValue, Value = item });
                }
            }

            if (propertiesMap.Count > 0 && propertyDictionary == null)
            {
                Recursive(propertiesMap, out propertyDictionary);
            }
        }

        public string Resolve(TSource source, TDestination destination, string destMember, ResolutionContext context)
        {
            return GetPropertyValue(source, _propName);
        }

        private string GetPropertyValue(object src, string propName)
        {
            var properties = src.GetType().GetProperties();

            var propertiesMap = new List<PropertyDictionary>();

            foreach (var item in properties)
            {
                if (item.PropertyType != typeof(string))
                {
                    propertiesMap.Add(new PropertyDictionary() { Key = src, Value = item });
                }
            }

            var tranlations = propertiesMap.FirstOrDefault(i => i.Value.Name == "Translations");

            var result = tranlations;

            if (tranlations == null)
            {
                Recursive(propertiesMap, out result);
            }

            var translationValueList = (IEnumerable)result.Value.GetValue(result.Key, null);

            var value = string.Empty;
            var valueDefault = string.Empty;

            foreach (var item in translationValueList)
            {
                var itemLanguage = item.GetType().GetProperty("Language").GetValue(item, null)?.ToString();
                if (itemLanguage == CultureInfo.CurrentCulture.TwoLetterISOLanguageName)
                {
                    value = item.GetType().GetProperty(propName).GetValue(item, null)?.ToString();
                }
                else if (itemLanguage == _defaultLanguage.ToString())
                {
                    valueDefault = item.GetType().GetProperty(propName).GetValue(item, null)?.ToString();
                }
            }

            if (!string.IsNullOrEmpty(value))
            {
                return value;
            }
            else if (!string.IsNullOrEmpty(valueDefault))
            {
                return valueDefault;
            }

            return string.Empty;
        }
    }
}