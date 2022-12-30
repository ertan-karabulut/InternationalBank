using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.Concreate.Component;
using BusinessLayer.Dto.PhpneNumber;
using BusinessLayer.Validation;
using BusinessLayer.Validation.PhoneNumberValidation;
using CoreLayer.DataAccess.Concrete.DataRequest;
using CoreLayer.Utilities.Aspect;

using CoreLayer.Utilities.Result.Concreate;
using DataAccessLayer.Abstract;
using EntityLayer.Models.EntityFremework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concreate.WorkFlow
{
    public class PhoneNumberWorkFlow : PhoneNumberComponent, IPhoneNumberWorkFlow
    {
        #region Variables
        private readonly IMapper _mapper;
        #endregion
        #region Constructor
        public PhoneNumberWorkFlow(IUnitOfWork unow, IMapper mapper) : base(unow)
        {
            this._mapper = mapper;
        }
        #endregion

        #region async metod
        [ValidatorAspect(typeof(DataTableValidation))]
        public async Task<Result<DataTableResponse<PhoneNumberDto>>> CustomerPhoneListAsync(DataTableRequest request)
        {
            var result = new Result<DataTableResponse<PhoneNumberDto>>();

            if (!request.Filter.Any(x => x.Name == "CustomerId"))
            {
                int customerId = base.helperWorkFlow.GetUserId();
                request.Filter.Add(new NameValuePair { Name = "CustomerId", Value = customerId.ToString() });
            }

            if (!request.Filter.Any(x => x.Name == "IsActive"))
                request.Filter.Add(new NameValuePair { Name = "IsActive", Value = "True" });

            if (request.Order.Column != "IsFavorite")
            {
                request.Order.Column = "IsFavorite";
                request.Order.Short = "desc";
            }

            var mailList = await base.CustomerPhoneNumberComponentAsync(request);
            if (mailList.ResultStatus)
            {
                result.ResultObje = new DataTableResponse<PhoneNumberDto>();

                result.ResultObje.Data = this._mapper.Map<List<PhoneNumberDto>>(mailList.ResultObje.Data);
                result.ResultObje.TotalCount = mailList.ResultObje.TotalCount;
                result.SetTrue();
            }

            return result;
        }

        [ValidatorAspect(typeof(UpdatePhoneNumberValidation))]
        public async Task<Result> UpdatePhoneNumberAsync(PhoneNumberUpdateDto phone)
        {
            base._unow.BeginTransaction();
            if (phone.IsFavorite.HasValue && phone.IsFavorite.Value)
            {
                var favoritePhone = await base._unow.PhoneNumberRepository.Get(x => x.CustomerId == phone.CustomerId && x.IsFavorite).FirstOrDefaultAsync();
                if (favoritePhone != null && favoritePhone.Id != phone.Id)
                {
                    favoritePhone.IsFavorite = false;
                    base._unow.PhoneNumberRepository.Update(favoritePhone);
                }
            }
            var updatePhone = await base.UpdatePhoneNumberComponentAsync(phone);
            if (updatePhone.ResultStatus)
                await base._unow.CommitAsync();
            else
                await base._unow.RollbackAsync();
            return updatePhone;
        }

        [ValidatorAspect(typeof(CreatePhoneNumberValidation))]
        public async Task<Result<PhoneNumberDto>> AddPhoneNumberAsync(PhoneNumberCreateDto phone)
        {
            var result = new Result<PhoneNumberDto>();
            phone.CustomerId = base.helperWorkFlow.GetUserId();

            if (phone.IsFavorite)
            {
                var favoritePhone = await base._unow.PhoneNumberRepository.Get(x => x.CustomerId == phone.CustomerId && x.IsActive && x.IsFavorite).FirstOrDefaultAsync();
                if (favoritePhone != null)
                {
                    favoritePhone.IsFavorite = false;
                    if (!(base._unow.PhoneNumberRepository.Update(favoritePhone).ResultStatus && await base._unow.SaveChangesAsync()))
                    {
                        result.SetFalse();
                    }
                }
            }

            var phoneEntity = this._mapper.Map<PhoneNumber>(phone);
            var phoneAdd = await base.AddPhoneNumberComponentAsync(phoneEntity);
            if (phoneAdd.ResultStatus)
            {
                result.SetTrue();
                result.ResultObje = this._mapper.Map<PhoneNumberDto>(phoneAdd.ResultObje);
            }
            return result;
        }

        [ValidatorAspect(typeof(UpdatePhoneNumberValidation))]
        public async Task<Result> DeletePhoneNumberAsync(PhoneNumberUpdateDto phone)
        {
            Result result = new Result();
            bool IsPhone = base._unow.PhoneNumberRepository.Get(x => true).Any(x => x.CustomerId == phone.CustomerId && x.IsActive);

            if (IsPhone)
            {
                result = await base.DeletePhoneNumberComponentAsync(phone);
            }

            return result;
        }
        #endregion

        [ValidatorAspect(typeof(DataTableValidation))]
        public Result<DataTableResponse<PhoneNumberDto>> CustomerPhoneList(DataTableRequest request)
        {
            var result = new Result<DataTableResponse<PhoneNumberDto>>();

            if (!request.Filter.Any(x => x.Name == "CustomerId"))
            {
                int customerId = base.helperWorkFlow.GetUserId();
                request.Filter.Add(new NameValuePair { Name = "CustomerId", Value = customerId.ToString() });
            }

            if (!request.Filter.Any(x => x.Name == "IsActive"))
                request.Filter.Add(new NameValuePair { Name = "IsActive", Value = "True" });

            if (request.Order.Column != "IsFavorite")
            {
                request.Order.Column = "IsFavorite";
                request.Order.Short = "desc";
            }

            var mailList = base.CustomerPhoneNumberComponent(request);
            if (mailList.ResultStatus)
            {
                result.ResultObje = new DataTableResponse<PhoneNumberDto>();

                result.ResultObje.Data = this._mapper.Map<List<PhoneNumberDto>>(mailList.ResultObje.Data);
                result.ResultObje.TotalCount = mailList.ResultObje.TotalCount;
                result.SetTrue();
            }

            return result;
        }

        [ValidatorAspect(typeof(UpdatePhoneNumberValidation))]
        public Result UpdatePhoneNumber(PhoneNumberUpdateDto phone)
        {
            base._unow.BeginTransaction();
            if (phone.IsFavorite.HasValue && phone.IsFavorite.Value)
            {
                var favoritePhone = base._unow.PhoneNumberRepository.Get(x => x.CustomerId == phone.CustomerId && x.IsFavorite).FirstOrDefault();
                if (favoritePhone != null && favoritePhone.Id != phone.Id)
                {
                    favoritePhone.IsFavorite = false;
                    base._unow.PhoneNumberRepository.Update(favoritePhone);
                }
            }
            var updatePhone = base.UpdatePhoneNumberComponent(phone);
            if (updatePhone.ResultStatus)
                base._unow.Commit();
            else
                base._unow.Rollback();
            return updatePhone;
        }

        [ValidatorAspect(typeof(CreatePhoneNumberValidation))]
        public Result<PhoneNumberDto> AddPhoneNumber(PhoneNumberCreateDto phone)
        {
            var result = new Result<PhoneNumberDto>();
            phone.CustomerId = base.helperWorkFlow.GetUserId();

            if (phone.IsFavorite)
            {
                var favoritePhone = base._unow.PhoneNumberRepository.Get(x => x.CustomerId == phone.CustomerId && x.IsActive && x.IsFavorite).FirstOrDefault();
                if (favoritePhone != null)
                {
                    favoritePhone.IsFavorite = false;
                    if (!(base._unow.PhoneNumberRepository.Update(favoritePhone).ResultStatus && base._unow.SaveChanges()))
                    {
                        result.SetFalse();
                    }
                }
            }

            var phoneEntity = this._mapper.Map<PhoneNumber>(phone);
            var phoneAdd = base.AddPhoneNumberComponent(phoneEntity);
            if (phoneAdd.ResultStatus)
            {
                result.SetTrue();
                result.ResultObje = this._mapper.Map<PhoneNumberDto>(phoneAdd.ResultObje);
            }
            return result;
        }

        [ValidatorAspect(typeof(UpdatePhoneNumberValidation))]
        public Result DeletePhoneNumber(PhoneNumberUpdateDto phone)
        {
            Result result = new Result();
            bool IsPhone = base._unow.PhoneNumberRepository.Get(x => true).Any(x => x.CustomerId == phone.CustomerId && x.IsActive);

            if (IsPhone)
            {
                result = base.DeletePhoneNumberComponent(phone);
            }

            return result;
        }
    }
}
