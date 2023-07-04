// <copyright file="ToastModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.Mvc.Toast
{
    public class ToastModel
    {
        public ToastType ToastType { get; set; }

        public string Message { get; set; }
    }
}