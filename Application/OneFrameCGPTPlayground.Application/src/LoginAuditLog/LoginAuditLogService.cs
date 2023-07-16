// <copyright file="LoginAuditLogService.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using KocSistem.OneFrame.Common.Extensions;
using KocSistem.OneFrame.Data;
using KocSistem.OneFrame.Data.Relational;
using KocSistem.OneFrame.DesignObjects.Services;
using KocSistem.OneFrame.I18N;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using OneFrameCGPTPlayground.Application.Abstractions;
using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.Excel.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.LoginAuditLog.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.PdfExport.Contracts;
using OneFrameCGPTPlayground.Application.Constants;
using OneFrameCGPTPlayground.Common.Helpers.ExcelExport;
using OneFrameCGPTPlayground.Common.Helpers.PdfExport;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace OneFrameCGPTPlayground.Application
{
    public class LoginAuditLogService : ApplicationCrudServiceAsync<Domain.LoginAuditLog, LoginAuditLogDto, Guid>, ILoginAuditLogService
    {
        private readonly ILookupNormalizer _keyNormalizer;
        private readonly IKsStringLocalizer<LoginAuditLogService> _localize;
        private readonly IRepository<Domain.LoginAuditLog> _loginAuditLogRepository;
        private readonly IMapper _mapper;
        private readonly IServiceResponseHelper _serviceResponseHelper;
        private readonly IConfiguration _configuration;

        public LoginAuditLogService(IRepository<Domain.LoginAuditLog> loginAuditLogRepository, IMapper mapper, IDataManager dataManager, IServiceResponseHelper serviceResponseHelper, IKsI18N i18N, ILookupNormalizer keyNormalizer, IConfiguration configuration)
            : base(loginAuditLogRepository, mapper, dataManager)
        {
            _loginAuditLogRepository = loginAuditLogRepository;
            _mapper = mapper;
            _serviceResponseHelper = serviceResponseHelper;
            _localize = i18N.GetLocalizer<LoginAuditLogService>();
            _keyNormalizer = keyNormalizer;
            _configuration = configuration;
        }

        /// <summary>
        /// LoginAuditLog tablosuna ait verilerin getirilmesini sağlar sağlar.
        /// </summary>
        /// <param name="pagedRequest">This field pagedRequest.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task<ServiceResponse<PagedResultDto<LoginAuditLogDto>>> GetLoginAuditLogsAsync(PagedRequestDto pagedRequest)
        {
            var query = _loginAuditLogRepository.GetQueryable();

            if (pagedRequest.Orders != null && pagedRequest.Orders.Any())
            {
                query = pagedRequest.Orders.Aggregate(query, (current, order) => order.DirectionDesc ? current.OrderByDescending(order.ColumnName) : current.OrderBy(order.ColumnName));
            }
            else
            {
                query = query.OrderBy(o => o.InsertedDate);
            }

            var loginAuditLogs = await query
                                            .ToPagedListAsync(pagedRequest.PageIndex, pagedRequest.PageSize)
                                            .ConfigureAwait(false);

            var loginAuditLogGetResponse = _mapper.Map<IPagedList<Domain.LoginAuditLog>, PagedResultDto<LoginAuditLogDto>>(loginAuditLogs);
            return _serviceResponseHelper.SetSuccess(loginAuditLogGetResponse);
        }

        /// <summary>
        /// LoginAuditLog tablosuna ait verilerin arama kriterine göre getirilmesini sağlar sağlar.
        /// </summary>
        /// <param name="searchRequest">This field searchRequest.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task<ServiceResponse<PagedResultDto<LoginAuditLogDto>>> SearchAsync(LoginAuditLogSearchDto searchRequest)
        {
            var normalizeName = _keyNormalizer.NormalizeName(searchRequest.Value);

            if (normalizeName.Contains(',', StringComparison.CurrentCulture))
            {
                var dates = normalizeName.Split(',');
                _ = DateTime.TryParse(dates[0], CultureInfo.CurrentCulture, DateTimeStyles.None, out var startDate);
                _ = DateTime.TryParse(dates[1], CultureInfo.CurrentCulture, DateTimeStyles.None, out var endDate);

                if (startDate != default && endDate != default)
                {
                    var loginAuditLogsByDateTime = await _loginAuditLogRepository.GetQueryable().Where(u => u.InsertedDate >= startDate && u.InsertedDate <= endDate)
                           .ToPagedListAsync(searchRequest.PageIndex, searchRequest.PageSize).ConfigureAwait(false);

                    var loginAuditLogGetResponseByDateTime = _mapper.Map<IPagedList<Domain.LoginAuditLog>, PagedResultDto<LoginAuditLogDto>>(loginAuditLogsByDateTime);
                    return _serviceResponseHelper.SetSuccess(loginAuditLogGetResponseByDateTime);
                }
            }

            var loginAuditLogs = await _loginAuditLogRepository.GetQueryable().Where(u => u.RequestUserName.Contains(normalizeName))
                .ToPagedListAsync(searchRequest.PageIndex, searchRequest.PageSize).ConfigureAwait(false);

            var loginAuditLogGetResponse = _mapper.Map<IPagedList<Domain.LoginAuditLog>, PagedResultDto<LoginAuditLogDto>>(loginAuditLogs);
            return _serviceResponseHelper.SetSuccess(loginAuditLogGetResponse);
        }

        /// <summary>
        /// It export the data of the Login Audit Log table to be brought according to the search criteria.
        /// </summary>
        /// <param name="searchRequest">This field searchRequest.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task<ServiceResponse<ExcelExportDto>> SearchForExcelExportAsync(LoginAuditLogFilterDto searchRequest)
        {
            var fileName = FileConstants.ExcelFileName;

            var loginAuditLogsByDateTime = await _loginAuditLogRepository.GetListAsync(u => u.InsertedDate >= searchRequest.StartDate && u.InsertedDate <= searchRequest.EndDate).ConfigureAwait(false);

            var loginAuditLogGetResponseByDateTime = _mapper.Map<List<Domain.LoginAuditLog>, List<LoginAuditLogExcelExportDto>>(loginAuditLogsByDateTime);

            var exportByDateTime = loginAuditLogGetResponseByDateTime.Export(_localize, "Sheet1", true);

            return _serviceResponseHelper.SetSuccess(new ExcelExportDto() { FileByteArray = exportByDateTime, FileName = fileName });
        }

        /// <summary>
        /// It export the data of the Login Audit Log table to be brought according to the search criteria.
        /// </summary>
        /// <param name="searchRequest">This field searchRequest.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task<ServiceResponse<List<LoginAuditLogDto>>> SearchLoginLogsAsync(LoginAuditLogFilterDto searchRequest)
        {
            var loginAuditLogsByDateTime = await _loginAuditLogRepository.GetListAsync(
                    u => (u.InsertedDate >= searchRequest.StartDate) &&
                         (u.InsertedDate <= searchRequest.EndDate))
                .ConfigureAwait(false);

            var loginAuditLogGetResponseByDateTime = _mapper.Map<List<Domain.LoginAuditLog>, List<LoginAuditLogDto>>(loginAuditLogsByDateTime);
            return _serviceResponseHelper.SetSuccess(loginAuditLogGetResponseByDateTime);
        }

        /// <summary>
        /// It export the data of the Login Audit Log table to be brought according to the search criteria.
        /// </summary>
        /// <param name="searchRequest">This field searchRequest.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task<ServiceResponse<PdfExportDto>> SearchForPdfExportAsync(LoginAuditLogFilterDto searchRequest)
        {
            var fileName = FileConstants.PdfFileName;

            var loginAuditLogsByDateTime = await _loginAuditLogRepository.GetListAsync(
                    u => (u.InsertedDate >= searchRequest.StartDate) &&
                         (u.InsertedDate <= searchRequest.EndDate))
                .ConfigureAwait(false);

            var loginAuditLogGetResponseByDateTime = _mapper.Map<List<Domain.LoginAuditLog>, List<LoginAuditLogPdfExport>>(loginAuditLogsByDateTime);
            var pdfSettings = new PdfExportDocumentSettings { Title = FileConstants.PdfTitle, Date = DateTime.UtcNow.ToShortDateString(), PageSize = iText.Kernel.Geom.PageSize.A4.Rotate(), ShowPageNumber = true, EncodingType = _configuration["PdfExport:Encoding"] ?? FileConstants.PdfDefaultEncodingType };
            byte[] exportByDateTime = loginAuditLogGetResponseByDateTime.PdfExport(pdfSettings, _localize);

            return _serviceResponseHelper.SetSuccess(new PdfExportDto() { FileByteArray = exportByDateTime, FileName = fileName });
        }
    }
}