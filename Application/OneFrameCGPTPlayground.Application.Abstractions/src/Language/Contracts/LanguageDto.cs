// <copyright file="LanguageDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.Data.Relational;
using System;

namespace OneFrameCGPTPlayground.Application.Abstractions.Language.Contracts
{
    public class LanguageDto : IDto<Guid>, IInsertAuditing, IUpdateAuditing, ISoftDelete
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Image { get; set; }

        public bool IsDefault { get; set; }

        public bool IsActive { get; set; }

        public string InsertedUser { get; set; }

        public DateTime? InsertedDate { get; set; }

        public string UpdatedUser { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
