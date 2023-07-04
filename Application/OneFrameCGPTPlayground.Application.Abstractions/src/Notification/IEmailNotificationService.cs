// <copyright file="IEmailNotificationService.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;
using KocSistem.OneFrame.DesignObjects;
using KocSistem.OneFrame.DesignObjects.Services;
using System;
using System.Threading.Tasks;

namespace OneFrameCGPTPlayground.Application.Abstractions.Notification
{
    /// <summary>
    /// IEmailNotificationService.
    /// </summary>
    public interface IEmailNotificationService : IApplicationService
    {
        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <param name="content">The content.</param>
        /// <param name="toEmailAddress">To email address.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task SendEmailAsync(string subject, string content, string toEmailAddress);

        /// <summary>
        ///  Get Email Notification Items.
        /// </summary>
        /// <param name="pagedRequest">This field pagedRequest.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        Task<ServiceResponse<PagedResultDto<EmailNotificationDto>>> GetEmailNotificationsAsync(PagedRequestDto pagedRequest);

        /// <summary>
        /// Get Email Notification Items by search criteria.
        /// </summary>
        /// <param name="searchRequest">This field searchRequest.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        Task<ServiceResponse<PagedResultDto<EmailNotificationDto>>> SearchNotificationAsync(EmailNotificationSearchRequestDto searchRequest);

        /// <summary>
        /// Get Email Notification Item by Id.
        /// </summary>
        /// <returns>
        /// <param name="id">This field id.</param>
        /// </returns>
        Task<ServiceResponse<bool>> SendEmailByIdAsync(Guid id);
    }
}