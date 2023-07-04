// <copyright file="EmailTemplate.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Domain.TranslationBase;
using KocSistem.OneFrame.Data.Relational;
using System;
using System.Collections.Generic;

namespace OneFrameCGPTPlayground.Domain
{
    public class EmailTemplate : IEntity<Guid>, IInsertAuditing, IUpdateAuditing, ISoftDelete, IMainTableTranslation<EmailTemplateTranslation>
    {
        public string Bcc { get; set; }

        public string Cc { get; set; }

        public Guid Id { get; set; }

        public DateTime? InsertedDate { get; set; }

        public string InsertedUser { get; set; }

        public bool IsDeleted { get; set; }

        public string Name { get; set; }

        public string To { get; set; }

        public virtual List<EmailTemplateTranslation> Translations { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string UpdatedUser { get; set; }
    }
}