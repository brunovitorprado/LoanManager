﻿using AutoMapper;
using FluentValidation;
using LoanManager.Application.Interfaces.AppServices;
using LoanManager.Application.Models.DTO;
using LoanManager.Application.Properties;
using LoanManager.Application.Shared;
using LoanManager.Domain;
using LoanManager.Domain.Entities;
using LoanManager.Domain.Exceptions;
using LoanManager.Domain.Interfaces.DomainServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoanManager.Application.AppServices
{
    public class LoanAppService : ILoanAppService
    {
        private readonly ILoanDomainService _loanDomainService;
        private readonly IFriendDomainService _friendDomainService;
        private readonly IGameDomainService _gameDomainService;
        private readonly IMapper _mapper;

        public LoanAppService(
            ILoanDomainService loanDomainService,
            IFriendDomainService friendDomainService,
            IGameDomainService gameDomainService,
            IMapper mapper
            )
        {
            _loanDomainService = loanDomainService;
            _friendDomainService = friendDomainService;
            _gameDomainService = gameDomainService;
            _mapper = mapper;
        }

        #region CRUD operations
        public async Task<Response<object>> Create(LoanDto loan)
        {
            var response = new Response<Object>();
            try
            {
                var loanEntity = _mapper.Map<Loan>(loan);
                var result = await _loanDomainService.CreateAsync(loanEntity);
                return response.SetResult(new { Id = result });
            }
            catch (ValidationException ex)
            {
                return response.SetRequestValidationError(ex);
            }
            catch (EntityNotExistsException ex)
            {
                return response.SetNotFound(ex.Message);
            }
            catch (GameIsOnLoanException ex)
            {
                return response.SetBadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return response.SetInternalServerError(Resources.UnexpectedErrorWhileGeneratingLoan);
            }
        }

        public async Task<Response<LoanDto>> Get(Guid id)
        {
            var response = new Response<LoanDto>();
            try
            {
                var result = await _loanDomainService.ReadAsync(id);
                return response.SetResult(_mapper.Map<LoanDto>(result));
            }
            catch (EntityNotExistsException)
            {
                return response.SetNotFound(Resources.CantFounLoanWithGivenId);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return response.SetInternalServerError(Resources.UnexpectedErrorWhileGettingLoan);
            }
        }

        public async Task<Response<IEnumerable<LoanDto>>> GetAll(int offset, int limit)
        {
            var response = new Response<IEnumerable<LoanDto>>();
            try
            {
                var result = await _loanDomainService.ReadAllAsync(offset, limit);
                return response.SetResult(_mapper.Map<IEnumerable<LoanDto>>(result));
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return response.SetInternalServerError(Resources.UnexpectedErrorWhileGettingLoan);
            }
        }

        public async Task<Response<bool>> Delete(Guid id)
        {
            var response = new Response<bool>();
            try
            {
                // Deletting entity
                await _loanDomainService.DeleteAsync(id);
                return response.SetResult(true);
            }
            catch (EntityNotExistsException)
            {
                return response.SetNotFound(Resources.CantFounLoanWithGivenId);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return response.SetInternalServerError(Resources.UnexpectedErrorWhileDeletingLoan);
            }
        }
        #endregion

        public async Task<Response<IEnumerable<LoanDto>>> ReadLoanByFriendNameAsync(string name, int offset, int limit)
        {
            var response = new Response<IEnumerable<LoanDto>>();
            try
            {
                // Validating if name is null or empty
                if (String.IsNullOrEmpty(name))
                    throw new ValidationException(message:Resources.FriendNameIsMandatory);

                var result = await _loanDomainService.ReadLoanByFriendNameAsync(name, offset, limit);
                return response.SetResult(_mapper.Map<IEnumerable<LoanDto>>(result));
            }
            catch (ValidationException ex)
            {
                return response.SetRequestValidationError(ex);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return response.SetInternalServerError(Resources.UnexpectedErrorWhileGettingLoan);
            }
        }

        public async Task<Response<bool>> EndLoan(Guid id)
        {
            var response = new Response<bool>();
            try
            {
                await _loanDomainService.EndLoan(id);
                return response.SetResult(true);
            }
            catch (EntityNotExistsException)
            {
                return response.SetNotFound(Resources.CantFounLoanWithGivenId);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return response.SetInternalServerError(Resources.UnexpectedErrorWhileDeletingLoan);
            }           
        }

        public async Task<Response<IEnumerable<LoanDto>>> ReadLoanHistoryByGameAsync(Guid id, int offset, int limit)
        {
            var response = new Response<IEnumerable<LoanDto>>();
            try
            {
                var result = await _loanDomainService.ReadLoanHistoryByGameAsync(id, offset, limit);
                return response.SetResult(_mapper.Map<IEnumerable<LoanDto>>(result));
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return response.SetInternalServerError(Resources.UnexpectedErrorWhileGettingLoan);
            }
        }
    }
}
