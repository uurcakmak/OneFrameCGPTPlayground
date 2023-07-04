// <copyright file="ViewLocationExpander.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using Microsoft.AspNetCore.Mvc.Razor;

namespace OneFrameCGPTPlayground.Mvc
{
    public class ViewLocationExpander : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            // {1} is controller,{0} is the action
            var locations = new string[] { "src/Views/{1}/{0}.cshtml", "src/Views/Shared/{0}.cshtml" };

            return locations.Union(viewLocations); // Add mvc default locations after ours
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            context.Values["customviewlocation"] = nameof(ViewLocationExpander);
        }
    }
}