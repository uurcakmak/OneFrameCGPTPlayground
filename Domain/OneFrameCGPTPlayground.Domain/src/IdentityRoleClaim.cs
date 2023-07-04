// -----------------------------------------------------------------------
// <copyright file="IdentityRoleClaim.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using KocSistem.OneFrame.Data.Relational;
using System;

namespace OneFrameCGPTPlayground.Domain
{
    public class IdentityRoleClaim : Microsoft.AspNetCore.Identity.IdentityRoleClaim<Guid>, IEntity
    {
    }
}
