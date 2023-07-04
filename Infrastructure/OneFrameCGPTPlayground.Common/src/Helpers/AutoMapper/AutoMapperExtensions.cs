// <copyright file="AutoMapperExtensions.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using System;
using System.Linq.Expressions;

namespace OneFrameCGPTPlayground.Common.Helpers.AutoMapper
{
    public static class AutoMapperExtensions
    {
        public static IMappingExpression<TSource, TDestination> ForTranslateMember<TSource, TDestination>(
       this IMappingExpression<TSource, TDestination> map,
       Expression<Func<TDestination, string>> selector)
        {
            var selectorName = (selector.Body as MemberExpression).Member.Name;

            _ = map.ForMember(selector, opt => opt.MapFrom(new TranslateResolver<TSource, TDestination>(selectorName)));

            return map;
        }

        public static IMappingExpression<TSource, TDestination> ToTimeZone<TSource, TDestination>(this IMappingExpression<TSource, TDestination> map, Expression<Func<TDestination, DateTime?>> selector)
        {
            var selectorName = GetSelectorName<TSource, TDestination>(selector);

            _ = map.ForMember(selector, opt => opt.MapFrom<DateTimeTimeZoneResolver<TSource, TDestination>, DateTime?>(selectorName));

            return map;
        }

        public static IMappingExpression<TSource, TDestination> ToUtc<TSource, TDestination>(this IMappingExpression<TSource, TDestination> map, Expression<Func<TDestination, DateTime?>> selector)
        {
            var selectorName = GetSelectorName<TSource, TDestination>(selector);

            _ = map.ForMember(selector, opt => opt.MapFrom<DateTimeUtcResolver<TSource, TDestination>, DateTime?>(selectorName));

            return map;
        }

        private static string GetSelectorName<TSource, TDestination>(Expression<Func<TDestination, DateTime?>> selector)
        {
            var member = selector.Body as MemberExpression;
            if (member == null)
            {
                if (selector.Body is UnaryExpression unary)
                {
                    member = unary.Operand as MemberExpression;
                }
            }

            var selectorName = member!.Member.Name;
            return selectorName;
        }
    }
}