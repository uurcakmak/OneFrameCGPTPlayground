// <copyright file="TimeZoneResponseExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.DesignObjects.Services;
using OneFrameCGPTPlayground.WebAPI.Model.Configuration;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Response
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    internal class TimeZoneResponseExample : IExamplesProvider<ServiceResponse<List<TimeZoneResponse>>>
    {
        public ServiceResponse<List<TimeZoneResponse>> GetExamples()
        {
            var data = new List<TimeZoneResponse>
            {
               new () { Id = "Pacific/Niue", DisplayName = "(UTC-11:00) Niue Time" },
               new () { Id = "Pacific/Midway", DisplayName = "(UTC-11:00) Samoa Standard Time (Midway)" },
               new () { Id = "Pacific/Pago_Pago", DisplayName = "(UTC-11:00) Samoa Standard Time (Pago Pago)" },
               new () { Id = "Pacific/Rarotonga", DisplayName = "(UTC-10:00) Cook Islands Standard Time (Rarotonga)" },
               new () { Id = "America/Adak", DisplayName = "(UTC-10:00) Hawaii-Aleutian Time (Adak)" },
               new () { Id = "Pacific/Honolulu", DisplayName = "(UTC-10:00) Hawaii-Aleutian Time (Honolulu)" },
               new () { Id = "Pacific/Tahiti", DisplayName = "(UTC-10:00) Tahiti Time" },
               new () { Id = "Pacific/Marquesas", DisplayName = "(UTC-09:30) Marquesas Time" },
               new () { Id = "America/Anchorage", DisplayName = "(UTC-09:00) Alaska Time (Anchorage)" },
               new () { Id = "America/Juneau", DisplayName = "(UTC-09:00) Alaska Time (Juneau)" },
            };

            return new ServiceResponse<List<TimeZoneResponse>>(data);
        }
    }
}