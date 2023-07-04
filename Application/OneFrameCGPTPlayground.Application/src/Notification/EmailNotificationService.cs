// <copyright file="EmailNotificationService.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using OneFrameCGPTPlayground.Application.Abstractions;
using OneFrameCGPTPlayground.Application.Abstractions.ApplicationSetting;
using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.Notification;
using OneFrameCGPTPlayground.Common.Constants;
using KocSistem.OneFrame.Common.Extensions;
using KocSistem.OneFrame.Data.Relational;
using KocSistem.OneFrame.DesignObjects.Services;
using KocSistem.OneFrame.I18N;
using KocSistem.OneFrame.Notification.Email;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace OneFrameCGPTPlayground.Application.Notification
{
    /// <summary>
    /// EmailNotificationService.
    /// </summary>
    /// <seealso cref="IEmailNotificationService" />
    public class EmailNotificationService : ApplicationServiceBase<Domain.EmailNotification>, IEmailNotificationService
    {
        private readonly IRepository<Domain.EmailNotification> _emailNotificationRepository;
        private readonly IEmailNotification _emailNotification;
        private readonly IConfiguration _configuration;
        private readonly ILookupNormalizer _keyNormalizer;
        private readonly IKsStringLocalizer<EmailNotificationService> _localize;
        private readonly ILogger<EmailNotificationService> _logger;
        private readonly IMapper _mapper;
        private readonly IServiceResponseHelper _serviceResponseHelper;
        private readonly IApplicationSettingService _applicationSettingService;

        public EmailNotificationService(
            IServiceProvider serviceProvider,
            IRepository<Domain.EmailNotification> emailNotificationRepository,
            IEmailNotification emailNotification,
            IConfiguration configuration,
            ILookupNormalizer keyNormalizer,
            IKsStringLocalizer<EmailNotificationService> localize,
            ILogger<EmailNotificationService> logger,
            IMapper mapper,
            IServiceResponseHelper serviceResponseHelper,
            IApplicationSettingService applicationSettingService)
            : base(serviceProvider)
        {
            _emailNotificationRepository = emailNotificationRepository;
            _emailNotification = emailNotification;
            _configuration = configuration;
            _keyNormalizer = keyNormalizer;
            _localize = localize;
            _logger = logger;
            _mapper = mapper;
            _serviceResponseHelper = serviceResponseHelper;
            _applicationSettingService = applicationSettingService;
        }

        /// <summary>
        /// Get Email Notification Items.
        /// </summary>
        /// <param name="pagedRequest">This field pagedRequest.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task<ServiceResponse<PagedResultDto<EmailNotificationDto>>> GetEmailNotificationsAsync(PagedRequestDto pagedRequest)
        {
            var query = _emailNotificationRepository.GetQueryable();

            if (pagedRequest.Orders != null && pagedRequest.Orders.Any())
            {
                query = pagedRequest.Orders.Aggregate(query, (current, order) => order.DirectionDesc ? current.OrderByDescending(order.ColumnName) : current.OrderBy(order.ColumnName));
            }
            else
            {
                query = query.OrderBy(o => o.InsertedDate);
            }

            var emails = await query.ToPagedListAsync(pagedRequest.PageIndex, pagedRequest.PageSize)
                                                      .ConfigureAwait(false);
            var emailResponse = _mapper.Map<IPagedList<Domain.EmailNotification>, PagedResultDto<EmailNotificationDto>>(emails);

            return _serviceResponseHelper.SetSuccess(emailResponse);
        }

        /// <summary>
        /// Get Email Notification Items by search criteria.
        /// </summary>
        /// <param name="searchRequest">This field searchRequest.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task<ServiceResponse<PagedResultDto<EmailNotificationDto>>> SearchNotificationAsync(EmailNotificationSearchRequestDto searchRequest)
        {
            var normalizeName = _keyNormalizer.NormalizeName(searchRequest.Value);

            var emailsByEmailAddress = await _emailNotificationRepository.GetQueryable().Where(u => u.To.Contains(normalizeName))
                   .ToPagedListAsync(searchRequest.PageIndex, searchRequest.PageSize).ConfigureAwait(false);

            var emailsGetResponseByEmailAddress = _mapper.Map<IPagedList<Domain.EmailNotification>, PagedResultDto<EmailNotificationDto>>(emailsByEmailAddress);
            return _serviceResponseHelper.SetSuccess(emailsGetResponseByEmailAddress);
        }

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <param name="content">The content.</param>
        /// <param name="toEmailAddress">To email address.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task SendEmailAsync(string subject, string content, string toEmailAddress)
        {
            var applicationSettingResponse = await _applicationSettingService.GetByKeyAsync(ConfigurationConstant.NotificationEmailIsActive).ConfigureAwait(false);

            if (applicationSettingResponse.IsSuccessful && Convert.ToBoolean(applicationSettingResponse.Result.Value))
            {
                var response = await this.SendEmailAsync(subject, content, new List<string> { toEmailAddress }).ConfigureAwait(false);

                var entity = new Domain.EmailNotification
                {
                    From = _configuration["NotificationSettings:Email:From"],
                    To = toEmailAddress,
                    Subject = subject,
                    Body = content,
                    IsSent = response.IsSuccessful,
                    SentDate = DateTime.UtcNow,
                    RetryCount = 0
                };

                await _emailNotificationRepository.AddAsync(entity).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Get Email Notification Item by Id.
        /// </summary>
        /// <returns>
        /// <param name="id">This field id.</param>
        /// </returns>
        public async Task<ServiceResponse<bool>> SendEmailByIdAsync(Guid id)
        {
            var notificationEntity = await _emailNotificationRepository.GetFirstOrDefaultAsync(predicate: x => x.Id == id).ConfigureAwait(false);

            if (notificationEntity == null)
            {
                return _serviceResponseHelper.SetError<bool>(false, _localize["EmailNotificationNotFound"], StatusCodes.Status404NotFound, true);
            }

            var emailSendResponse = await this.SendEmailAsync(notificationEntity.Subject, notificationEntity.Body, new List<string> { notificationEntity.To }).ConfigureAwait(false);
            if (notificationEntity.IsSent)
            {
                notificationEntity.Id = Guid.NewGuid();
                notificationEntity.IsSent = emailSendResponse.IsSuccessful;
                notificationEntity.SentDate = DateTime.UtcNow;
                notificationEntity.RetryCount = 0;

                await _emailNotificationRepository.AddAsync(notificationEntity).ConfigureAwait(false);
            }
            else
            {
                notificationEntity.IsSent = emailSendResponse.IsSuccessful;
                notificationEntity.RetryCount++;
                notificationEntity.SentDate = DateTime.UtcNow;

                await _emailNotificationRepository.UpdateAsync(notificationEntity).ConfigureAwait(false);
            }

            return _serviceResponseHelper.SetSuccess(true);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "for unknown exceptions")]
        private async Task<EmailNotificationResponse> SendEmailAsync(string subject, string content, List<string> toList, List<string> ccList = null, List<string> bccList = null)
        {
            var userList = new List<KocSistem.OneFrame.Notification.Email.User>();
            if (toList != null && toList.Any())
            {
                userList.AddRange(toList.Select(s => new KocSistem.OneFrame.Notification.Email.User { EmailAddress = s, ReceiverType = ReceiverType.TO }));
            }

            if (ccList != null && ccList.Any())
            {
                userList.AddRange(ccList.Select(s => new KocSistem.OneFrame.Notification.Email.User { EmailAddress = s, ReceiverType = ReceiverType.CC }));
            }

            if (bccList != null && bccList.Any())
            {
                userList.AddRange(bccList.Select(s => new KocSistem.OneFrame.Notification.Email.User { EmailAddress = s, ReceiverType = ReceiverType.BCC }));
            }

            var trackId = Guid.NewGuid();
            try
            {
                var response = await _emailNotification.SendAsync(
                    new EmailContent { TrackId = trackId, Subject = subject, Content = content, },
                    userList,
                    CancellationToken.None).ConfigureAwait(false);

                return response;
            }
            catch (SocketException ex)
            {
                _logger.LogError(ex, ex.Message);
                return new EmailNotificationResponse(trackId, ex.Message, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new EmailNotificationResponse(trackId, ex.Message, false);
            }
        }
    }
}