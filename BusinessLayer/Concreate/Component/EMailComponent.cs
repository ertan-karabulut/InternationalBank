using BusinessLayer.Dto.Mail;
using CoreLayer.BusinessLayer.Concreate.Component;
using CoreLayer.DataAccess.Concrete.DataRequest;

using CoreLayer.Utilities.Result.Concreate;
using DataAccessLayer.Abstract;
using EntityLayer.Models.EntityFremework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concreate.Component
{
    public class EMailComponent : ComponentBase
    {
        #region Variables
        protected readonly IUnitOfWork _unow;
        #endregion
        #region Constructor
        public EMailComponent(IUnitOfWork unow)
        {
            this._unow = unow;
        }
        #endregion
        #region protected method
        #region async metod
        protected async Task<Result<DataTableResponse<EMail>>> CustomerMailListComponentAsync(DataTableRequest request)
        {
            var result = new Result<DataTableResponse<EMail>>();
            result.ResultObje = new DataTableResponse<EMail>();
            var query = this._unow.MailRespository.Get(x => true);
            query = SetFilter(request.Filter, query);
            query = SetOrder(request.Order, query);

            int count = await query.CountAsync();
            if (count > 0)
            {
                var data = await query.Skip(request.Skip).Take(request.Take).AsQueryable().ToListAsync();

                result.ResultObje.Data = data;
                result.ResultObje.TotalCount = count;
                result.SetTrue();
            }
            return result;
        }

        protected async Task<Result> UpdateMailComponentAsync(MailUpdateDto mail)
        {
            Result result = new Result();
            var mailEntity = await this._unow.MailRespository.Get(x => x.Id == mail.Id).FirstOrDefaultAsync();

            if (mailEntity != null)
            {
                this.MailUpdateMapping(mailEntity, mail);
                if (this._unow.MailRespository.Update(mailEntity).ResultStatus && await this._unow.SaveChangesAsync())
                    result.SetTrue();
            }

            return result;
        }

        protected async Task<Result> DeleteMailComponentAsync(MailUpdateDto mail)
        {
            Result result = new Result();
            var mailEntity = await this._unow.MailRespository.Get(x => x.Id == mail.Id).FirstOrDefaultAsync();

            if (mailEntity != null)
            {
                mailEntity.IsActive = false;
                var update = this._unow.MailRespository.Update(mailEntity);
                if (update.ResultStatus && await this._unow.SaveChangesAsync())
                    result.SetTrue();
            }

            return result;
        }

        protected async Task<Result<EMail>> AddMailComponentAsync(EMail mail)
        {
            var result = new Result<EMail>();
            var addResult = await this._unow.MailRespository.AddAsync(mail);
            if (addResult.ResultStatus && await this._unow.SaveChangesAsync())
            {
                result.ResultObje = addResult.ResultObje;
                result.SetTrue();
            }

            return result;
        }
        #endregion
        protected Result<DataTableResponse<EMail>> CustomerMailListComponent(DataTableRequest request)
        {
            var result = new Result<DataTableResponse<EMail>>();
            result.ResultObje = new DataTableResponse<EMail>();
            var query = this._unow.MailRespository.Get(x => true);
            query = SetFilter(request.Filter, query);
            query = SetOrder(request.Order, query);

            int count = query.Count();
            if (count > 0)
            {
                var data = query.Skip(request.Skip).Take(request.Take).AsQueryable().ToList();

                result.ResultObje.Data = data;
                result.ResultObje.TotalCount = count;
                result.SetTrue();
            }
            return result;
        }
        protected Result UpdateMailComponent(MailUpdateDto mail)
        {
            Result result = new Result();
            var mailEntity = this._unow.MailRespository.Get(x => x.Id == mail.Id).FirstOrDefault();

            if (mailEntity != null)
            {
                this.MailUpdateMapping(mailEntity, mail);
                if (this._unow.MailRespository.Update(mailEntity).ResultStatus && this._unow.SaveChanges())
                    result.SetTrue();
            }

            return result;
        }
        protected Result DeleteMailComponent(MailUpdateDto mail)
        {
            Result result = new Result();
            var mailEntity = this._unow.MailRespository.Get(x => x.Id == mail.Id).FirstOrDefault();

            if (mailEntity != null)
            {
                mailEntity.IsActive = false;
                var update = this._unow.MailRespository.Update(mailEntity);
                if (update.ResultStatus && this._unow.SaveChanges())
                    result.SetTrue();
            }

            return result;
        }
        protected Result<EMail> AddMailComponent(EMail mail)
        {
            var result = new Result<EMail>();
            var addResult = this._unow.MailRespository.Add(mail);
            if (addResult.ResultStatus && this._unow.SaveChanges())
            {
                result.ResultObje = addResult.ResultObje;
                result.SetTrue();
            }

            return result;
        }
        #endregion
        #region private method
        private IQueryable<EMail> SetOrder(OrderModel order, IQueryable<EMail> query)
        {
            switch (order.Column)
            {
                case "EMail":
                    if (order.Short.ToUpper() == "DESC")
                        query = query.OrderByDescending(x => x.EMail1);
                    else
                        query = query.OrderBy(x => x.EMail1);
                    break;
                case "CustomerId":
                    if (order.Short.ToUpper() == "DESC")
                        query = query.OrderByDescending(x => x.CustomerId);
                    else
                        query = query.OrderBy(x => x.CustomerId);
                    break;
                case "IsFavorite":
                    if (order.Short.ToUpper() == "DESC")
                        query = query.OrderByDescending(x => x.IsFavorite);
                    else
                        query = query.OrderBy(x => x.IsFavorite);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreateDate);
                    break;
            }
            return query;
        }
        private IQueryable<EMail> SetFilter(List<NameValuePair> valuePairs, IQueryable<EMail> query)
        {
            if (valuePairs.Any(x => x.Name == "Id") && valuePairs.FirstOrDefault(x => x.Name == "Id").Value != null)
            {
                string value = valuePairs.FirstOrDefault(x => x.Name == "Id").Value;
                int Id;
                int.TryParse(value, out Id);
                query = query.Where(x => x.Id == Id);
            }
            if (valuePairs.Any(x => x.Name == "CustomerId") && valuePairs.FirstOrDefault(x => x.Name == "CustomerId").Value != null)
            {
                string value = valuePairs.FirstOrDefault(x => x.Name == "CustomerId").Value;
                int CustomerId;
                int.TryParse(value, out CustomerId);
                query = query.Where(x => x.CustomerId == CustomerId);
            }
            if (valuePairs.Any(x => x.Name == "EMail") && valuePairs.FirstOrDefault(x => x.Name == "EMail").Value != null)
            {
                string value = valuePairs.FirstOrDefault(x => x.Name == "EMail").Value;
                query = query.Where(x => value.Contains(x.EMail1));
            }
            if (valuePairs.Any(x => x.Name == "IsActive") && valuePairs.FirstOrDefault(x => x.Name == "IsActive").Value != null)
            {
                string value = valuePairs.FirstOrDefault(x => x.Name == "IsActive").Value;
                bool IsActive;
                bool.TryParse(value, out IsActive);
                query = query.Where(x => x.IsActive == IsActive);
            }

            return query;
        }
        private void MailUpdateMapping(EMail mail, MailUpdateDto updateDto)
        {
            if (!string.IsNullOrEmpty(updateDto.EMail))
                mail.EMail1 = updateDto.EMail;
            if (updateDto.IsFavorite.HasValue)
                mail.IsFavorite = updateDto.IsFavorite.Value;
            if (updateDto.IsActive.HasValue)
                mail.IsActive = updateDto.IsActive.Value;
        }
        #endregion
    }
}
