using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.Concreate.Component;
using BusinessLayer.Dto.Mail;
using BusinessLayer.Validation;
using BusinessLayer.Validation.MailValidation;
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
    public class EMailWorkFlow : EMailComponent, IEMailWorkFlow
    {
        #region Variables
        private readonly IMapper _mapper;
        #endregion
        #region Constructor
        public EMailWorkFlow(IUnitOfWork unow, IMapper mapper) : base(unow)
        {
            this._mapper = mapper;
        }
        #endregion

        #region Async metod
        [ValidatorAspect(typeof(DataTableValidation))]
        public async Task<Result<DataTableResponse<MailDto>>> CustomerMailListAsync(DataTableRequest request)
        {
            var result = new Result<DataTableResponse<MailDto>>();

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

            var mailList = await base.CustomerMailListComponentAsync(request);
            if (mailList.ResultStatus)
            {
                result.ResultObje = new DataTableResponse<MailDto>();

                result.ResultObje.Data = this._mapper.Map<List<MailDto>>(mailList.ResultObje.Data);
                result.ResultObje.TotalCount = mailList.ResultObje.TotalCount;
                result.SetTrue();
            }

            return result;
        }

        [ValidatorAspect(typeof(UpdateMailValidation))]
        public async Task<Result> UpdateMailAsync(MailUpdateDto mail)
        {
            base._unow.BeginTransaction();
            if (mail.IsFavorite.HasValue && mail.IsFavorite.Value)
            {
                var favoriteMail= await base._unow.MailRespository.Get(x => x.CustomerId == mail.CustomerId && x.IsFavorite).FirstOrDefaultAsync();
                if (favoriteMail != null && favoriteMail.Id != mail.Id)
                {
                    favoriteMail.IsFavorite = false;
                    base._unow.MailRespository.Update(favoriteMail);
                }
            }
            var updateMail = await base.UpdateMailComponentAsync(mail);
            if (updateMail.ResultStatus)
                await base._unow.CommitAsync();
            else
                await base._unow.RollbackAsync();
            return updateMail;
        }

        [ValidatorAspect(typeof(UpdateMailValidation))]
        public async Task<Result> DeleteMailAsync(MailUpdateDto mail)
        {
            Result result = new Result();
            bool IsMail = base._unow.MailRespository.Get(x => true).Any(x => x.CustomerId == mail.CustomerId && x.IsActive);

            if (IsMail)
            {
                result = await base.DeleteMailComponentAsync(mail);
            }

            return result;
        }

        [ValidatorAspect(typeof(CreateMailValidation))]
        public async Task<Result<MailDto>> AddMailAsync(MailCreateDto mail)
        {
            Result<MailDto> result = new Result<MailDto>();
            mail.CustomerId = base.helperWorkFlow.GetUserId();

            if (mail.IsFavorite)
            {
                var favoriteMail = await base._unow.MailRespository.Get(x => x.CustomerId == mail.CustomerId && x.IsActive && x.IsFavorite).FirstOrDefaultAsync();
                if (favoriteMail != null)
                {
                    favoriteMail.IsFavorite = false;
                    if (!(base._unow.MailRespository.Update(favoriteMail).ResultStatus && await base._unow.SaveChangesAsync()))
                    {
                        result.SetFalse();
                    }
                }
            }

            var mailEntity = this._mapper.Map<EMail>(mail);
            var mailAdd = await base.AddMailComponentAsync(mailEntity);
            if (mailAdd.ResultStatus)
            {
                result.SetTrue();
                result.ResultObje = this._mapper.Map<MailDto>(mailAdd.ResultObje);
            }
            return result;
        }
        #endregion
        [ValidatorAspect(typeof(DataTableValidation))]
        public Result<DataTableResponse<MailDto>> CustomerMailList(DataTableRequest request)
        {
            var result = new Result<DataTableResponse<MailDto>>();

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

            var mailList = base.CustomerMailListComponent(request);
            if (mailList.ResultStatus)
            {
                result.ResultObje = new DataTableResponse<MailDto>();

                result.ResultObje.Data = this._mapper.Map<List<MailDto>>(mailList.ResultObje.Data);
                result.ResultObje.TotalCount = mailList.ResultObje.TotalCount;
                result.SetTrue();
            }

            return result;
        }

        [ValidatorAspect(typeof(UpdateMailValidation))]
        public Result UpdateMail(MailUpdateDto mail)
        {
            base._unow.BeginTransaction();
            if (mail.IsFavorite.HasValue && mail.IsFavorite.Value)
            {
                var favoriteMail = base._unow.MailRespository.Get(x => x.CustomerId == mail.CustomerId && x.IsFavorite).FirstOrDefault();
                if (favoriteMail != null && favoriteMail.Id != mail.Id)
                {
                    favoriteMail.IsFavorite = false;
                    base._unow.MailRespository.Update(favoriteMail);
                }
            }
            var updateMail = base.UpdateMailComponent(mail);
            if (updateMail.ResultStatus)
                base._unow.Commit();
            else
                base._unow.Rollback();
            return updateMail;
        }

        [ValidatorAspect(typeof(UpdateMailValidation))]
        public Result DeleteMail(MailUpdateDto mail)
        {
            Result result = new Result();
            bool IsMail = base._unow.MailRespository.Get(x => true).Any(x => x.CustomerId == mail.CustomerId && x.IsActive);

            if (IsMail)
            {
                result = base.DeleteMailComponent(mail);
            }

            return result;
        }

        [ValidatorAspect(typeof(CreateMailValidation))]
        public Result<MailDto> AddMail(MailCreateDto mail)
        {
            Result<MailDto> result = new Result<MailDto>();
            mail.CustomerId = base.helperWorkFlow.GetUserId();

            if (mail.IsFavorite)
            {
                var favoriteMail = base._unow.MailRespository.Get(x => x.CustomerId == mail.CustomerId && x.IsActive && x.IsFavorite).FirstOrDefault();
                if (favoriteMail != null)
                {
                    favoriteMail.IsFavorite = false;
                    if (!(base._unow.MailRespository.Update(favoriteMail).ResultStatus && base._unow.SaveChanges()))
                    {
                        result.SetFalse();
                    }
                }
            }

            var mailEntity = this._mapper.Map<EMail>(mail);
            var mailAdd = base.AddMailComponent(mailEntity);
            if (mailAdd.ResultStatus)
            {
                result.SetTrue();
                result.ResultObje = this._mapper.Map<MailDto>(mailAdd.ResultObje);
            }
            return result;
        }
    }
}
