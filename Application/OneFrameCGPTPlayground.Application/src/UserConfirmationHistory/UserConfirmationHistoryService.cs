// <copyright file="UserConfirmationHistoryService.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using KocSistem.OneFrame.Data;
using KocSistem.OneFrame.Data.Relational;
using KocSistem.OneFrame.DesignObjects.Services;
using KocSistem.OneFrame.I18N;
using Microsoft.AspNetCore.Http;
using OneFrameCGPTPlayground.Application.Abstractions.UserConfirmationHistory;
using OneFrameCGPTPlayground.Application.Abstractions.UserConfirmationHistory.Contract;
using System;
using System.Threading.Tasks;

namespace OneFrameCGPTPlayground.Application.UserConfirmationHistory
{
    public class UserConfirmationHistoryService : ApplicationCrudServiceAsync<Domain.UserConfirmationHistory, UserConfirmationHistoryDto, Guid>, IUserConfirmationHistoryService
    {
        private readonly IRepository<Domain.UserConfirmationHistory> _userConfirmationRepository;
        private readonly IMapper _mapper;
        private readonly IServiceResponseHelper _serviceResponseHelper;
        private readonly IKsStringLocalizer<UserConfirmationHistoryService> _localize;

        public UserConfirmationHistoryService(IRepository<Domain.UserConfirmationHistory> userConfirmationRepository, IMapper mapper, IDataManager dataManager, IServiceResponseHelper serviceResponseHelper, IKsI18N i18N)
            : base(userConfirmationRepository, mapper, dataManager)
        {
            _userConfirmationRepository = userConfirmationRepository;
            _mapper = mapper;
            _serviceResponseHelper = serviceResponseHelper;
            _localize = i18N.GetLocalizer<UserConfirmationHistoryService>();
        }

        /// <summary>
        /// Creates confirmation code async.
        /// </summary>
        /// <param name="confirmationHistoryDto">The confiramtion code object is type of UserConfirmationHistoryDto.</param>
        /// <returns></returns>
        public async Task<ServiceResponse<UserConfirmationHistoryDto>> CreateCodeAsync(UserConfirmationHistoryDto confirmationHistoryDto)
        {
            var confirmationHistory = new Domain.UserConfirmationHistory()
            {
                UserId = confirmationHistoryDto.UserId,
                CodeType = confirmationHistoryDto.CodeType,
                Email = confirmationHistoryDto.Email,
                PhoneNumber = confirmationHistoryDto.PhoneNumber,
                Code = confirmationHistoryDto.Code,
                ExpiredDate = confirmationHistoryDto.ExpiredDate,
                IsSent = confirmationHistoryDto.IsSent,
                SentDate = confirmationHistoryDto.SentDate,
                IsUsed = confirmationHistoryDto.IsUsed,
                UsedDate = confirmationHistoryDto.UsedDate,
            };

            await _userConfirmationRepository.AddAsync(confirmationHistory).ConfigureAwait(false);
            var result = _mapper.Map<Domain.UserConfirmationHistory, UserConfirmationHistoryDto>(confirmationHistory);

            return _serviceResponseHelper.SetSuccess(result);
        }

        /// <summary>
        /// Checks confirmation code as sent.
        /// </summary>
        /// <param name="id">The confirmation code id is type of guid.</param>
        /// <returns></returns>
        public async Task<ServiceResponse> CheckCodeAsSentAsync(Guid id)
        {
            var confirmationHistory = await _userConfirmationRepository.GetFirstOrDefaultAsync(predicate: x => x.Id == id).ConfigureAwait(false);

            if (confirmationHistory == null)
            {
                return _serviceResponseHelper.SetError(_localize["ConfirmationCodeNotFound"], StatusCodes.Status400BadRequest, true);
            }

            confirmationHistory.IsSent = true;
            confirmationHistory.SentDate = DateTime.UtcNow;

            await _userConfirmationRepository.UpdateAsync(confirmationHistory).ConfigureAwait(false);

            return _serviceResponseHelper.SetSuccess();
        }

        /// <summary>
        /// Confirms phone number confirmation code.
        /// </summary>
        /// <param name="confirmationHistoryDto">The confiramtion code object is type of UserConfirmationHistoryDto.</param>
        /// <returns></returns>
        public async Task<ServiceResponse> ConfirmCodeAsync(UserConfirmationHistoryDto confirmationHistoryDto)
        {
            var confirmationHistory = await _userConfirmationRepository.GetFirstOrDefaultAsync(predicate:
                x => x.Id == confirmationHistoryDto.Id &&
                x.Code == confirmationHistoryDto.Code &&
                x.ExpiredDate > DateTime.UtcNow &&
                !x.IsUsed).ConfigureAwait(false);

            if (confirmationHistory == null)
            {
                return _serviceResponseHelper.SetError(_localize["InvalidConfirmationCode"], StatusCodes.Status400BadRequest, true);
            }

            confirmationHistory.IsUsed = true;
            confirmationHistory.UsedDate = DateTime.UtcNow;
            await _userConfirmationRepository.UpdateAsync(confirmationHistory).ConfigureAwait(false);

            return _serviceResponseHelper.SetSuccess();
        }

        /// <summary>
        /// Gets a specific user's active confirmation code.
        /// </summary>
        /// <param name="userId">The user id is type of guid.</param>
        /// <param name="phoneNumber">The phone number is type of string.</param>
        /// <returns></returns>
        public async Task<ServiceResponse<UserConfirmationHistoryDto>> GetActiveCodeAsync(Guid userId, string phoneNumber)
        {
            var confirmationHistory = await _userConfirmationRepository.GetFirstOrDefaultAsync(predicate:
                x => x.UserId == userId &&
                x.PhoneNumber == phoneNumber &&
                x.ExpiredDate > DateTime.UtcNow &&
                !x.IsUsed).ConfigureAwait(false);

            var mappingDto = _mapper.Map<Domain.UserConfirmationHistory, UserConfirmationHistoryDto>(confirmationHistory);

            return _serviceResponseHelper.SetSuccess(mappingDto);
        }
    }
}