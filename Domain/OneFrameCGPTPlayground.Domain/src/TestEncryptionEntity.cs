// <copyright file="TestEncryptionEntity.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.Data.Relational;
using System.ComponentModel.DataAnnotations;

namespace OneFrameCGPTPlayground.Domain
{
    /// <summary>
    /// /TestEncryptionEntity.
    /// </summary>
    /// <seealso cref="int" />
    public class TestEncryptionEntity : IEntity<int>
    {
        public int Id { get; set; }

        [Encrypted]
        public string Name { get; set; }
    }
}
