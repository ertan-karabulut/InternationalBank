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
    public class CardComponent : ComponentBase
    {
        #region Variables
        protected readonly IUnitOfWork _unow;
        #endregion
        #region Constructor
        public CardComponent(IUnitOfWork unow)
        {
            _unow = unow;
        }
        #endregion
        #region protected method
        #region Async metod
        protected async Task<Result<DataTableResponse<CreditCard>>> CustomerCrediCardListComponentAsync(DataRequestBase request)
        {
            var result = new Result<DataTableResponse<CreditCard>>();
            result.ResultObje = new DataTableResponse<CreditCard>();
            var query = this._unow.CreditCardRespository.Get(x => true);
            query = query.Include(x=>x.CreditCardNavigation);
            query = SetFilter(request.Filter, query);

            int count = await query.CountAsync();
            if (count > 0)
            {
                var data = await query.AsQueryable().ToListAsync();

                result.ResultObje.Data = data;
                result.ResultObje.TotalCount = count;
                result.SetTrue();
            }
            return result;
        }

        protected async Task<Result<DataTableResponse<AtmCard>>> CustomerAtmCardListComponentAsync(DataRequestBase request)
        {
            var result = new Result<DataTableResponse<AtmCard>>();
            result.ResultObje = new DataTableResponse<AtmCard>();
            var query = this._unow.AtmCardRespository.Get(x => true);
            query = query.Include(x => x.AtmCardNavigation);

            #region Filter
            if (request.Filter.Any(x => x.Name == "Id") && request.Filter.FirstOrDefault(x => x.Name == "Id").Value != null)
            {
                string value = request.Filter.FirstOrDefault(x => x.Name == "Id").Value;
                int Id;
                int.TryParse(value, out Id);
                query = query.Where(x => x.AtmCardNavigation.Id == Id);
            }
            if (request.Filter.Any(x => x.Name == "CardNumber") && request.Filter.FirstOrDefault(x => x.Name == "CardNumber").Value != null)
            {
                string value = request.Filter.FirstOrDefault(x => x.Name == "CardNumber").Value;
                query = query.Where(x => x.AtmCardNavigation.CardNumber == value);
            }
            if (request.Filter.Any(x => x.Name == "CustomerId") && request.Filter.FirstOrDefault(x => x.Name == "CustomerId").Value != null)
            {
                string value = request.Filter.FirstOrDefault(x => x.Name == "CustomerId").Value;
                int CustomerId;
                int.TryParse(value, out CustomerId);
                query = query.Where(x => x.AtmCardNavigation.CustomerId == CustomerId);
            }
            if (request.Filter.Any(x => x.Name == "IsActive") && request.Filter.FirstOrDefault(x => x.Name == "IsActive").Value != null)
            {
                string value = request.Filter.FirstOrDefault(x => x.Name == "IsActive").Value;
                bool IsActive;
                bool.TryParse(value, out IsActive);
                query = query.Where(x => x.AtmCardNavigation.IsActive == IsActive);
            }
            #endregion

            int count = await query.CountAsync();
            if (count > 0)
            {
                var data = await query.AsQueryable().ToListAsync();

                result.ResultObje.Data = data;
                result.ResultObje.TotalCount = count;
                result.SetTrue();
            }
            return result;
        }
        #endregion
        protected Result<DataTableResponse<CreditCard>> CustomerCrediCardListComponent(DataRequestBase request)
        {
            var result = new Result<DataTableResponse<CreditCard>>();
            result.ResultObje = new DataTableResponse<CreditCard>();
            var query = this._unow.CreditCardRespository.Get(x => true);
            query = query.Include(x => x.CreditCardNavigation);
            query = SetFilter(request.Filter, query);

            int count = query.Count();
            if (count > 0)
            {
                var data = query.AsQueryable().ToList();

                result.ResultObje.Data = data;
                result.ResultObje.TotalCount = count;
                result.SetTrue();
            }
            return result;
        }

        protected Result<DataTableResponse<AtmCard>> CustomerAtmCardListComponent(DataRequestBase request)
        {
            var result = new Result<DataTableResponse<AtmCard>>();
            result.ResultObje = new DataTableResponse<AtmCard>();
            var query = this._unow.AtmCardRespository.Get(x => true);
            query = query.Include(x => x.AtmCardNavigation);

            #region Filter
            if (request.Filter.Any(x => x.Name == "Id") && request.Filter.FirstOrDefault(x => x.Name == "Id").Value != null)
            {
                string value = request.Filter.FirstOrDefault(x => x.Name == "Id").Value;
                int Id;
                int.TryParse(value, out Id);
                query = query.Where(x => x.AtmCardNavigation.Id == Id);
            }
            if (request.Filter.Any(x => x.Name == "CardNumber") && request.Filter.FirstOrDefault(x => x.Name == "CardNumber").Value != null)
            {
                string value = request.Filter.FirstOrDefault(x => x.Name == "CardNumber").Value;
                query = query.Where(x => x.AtmCardNavigation.CardNumber == value);
            }
            if (request.Filter.Any(x => x.Name == "CustomerId") && request.Filter.FirstOrDefault(x => x.Name == "CustomerId").Value != null)
            {
                string value = request.Filter.FirstOrDefault(x => x.Name == "CustomerId").Value;
                int CustomerId;
                int.TryParse(value, out CustomerId);
                query = query.Where(x => x.AtmCardNavigation.CustomerId == CustomerId);
            }
            if (request.Filter.Any(x => x.Name == "IsActive") && request.Filter.FirstOrDefault(x => x.Name == "IsActive").Value != null)
            {
                string value = request.Filter.FirstOrDefault(x => x.Name == "IsActive").Value;
                bool IsActive;
                bool.TryParse(value, out IsActive);
                query = query.Where(x => x.AtmCardNavigation.IsActive == IsActive);
            }
            #endregion

            int count = query.Count();
            if (count > 0)
            {
                var data = query.AsQueryable().ToList();

                result.ResultObje.Data = data;
                result.ResultObje.TotalCount = count;
                result.SetTrue();
            }
            return result;
        }
        #endregion
        #region private method
        private IQueryable<CreditCard> SetOrder(OrderModel order, IQueryable<CreditCard> query)
        {
            switch (order.Column)
            {
                case "Id":
                    if (order.Short.ToUpper() == "DESC")
                        query = query.OrderByDescending(x => x.CreditCardNavigation.Id);
                    else
                        query = query.OrderBy(x => x.CreditCardNavigation.Id);
                    break;
                case "CreditCardLimit":
                    if (order.Short.ToUpper() == "DESC")
                        query = query.OrderByDescending(x => x.CreditCardLimit);
                    else
                        query = query.OrderBy(x => x.CreditCardLimit);
                    break;
                case "CreditCardName":
                    if (order.Short.ToUpper() == "DESC")
                        query = query.OrderByDescending(x => x.CreditCardName);
                    else
                        query = query.OrderBy(x => x.CreditCardName);
                    break;
                case "CustomerId":
                    if (order.Short.ToUpper() == "DESC")
                        query = query.OrderByDescending(x => x.CreditCardNavigation.CustomerId);
                    else
                        query = query.OrderBy(x => x.CreditCardNavigation.CustomerId);
                    break;
                case "CardNumber":
                    if (order.Short.ToUpper() == "DESC")
                        query = query.OrderByDescending(x => x.CreditCardNavigation.CardNumber);
                    else
                        query = query.OrderBy(x => x.CreditCardNavigation.CardNumber);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreditCardNavigation.CreateDate);
                    break;
            }
            return query;
        }
        private IQueryable<CreditCard> SetFilter(List<NameValuePair> valuePairs, IQueryable<CreditCard> query)
        {
            if (valuePairs.Any(x => x.Name == "Id") && valuePairs.FirstOrDefault(x => x.Name == "Id").Value != null)
            {
                string value = valuePairs.FirstOrDefault(x => x.Name == "Id").Value;
                int Id;
                int.TryParse(value, out Id);
                query = query.Where(x => x.CreditCardNavigation.Id == Id);
            }
            if (valuePairs.Any(x => x.Name == "CreditCardName") && valuePairs.FirstOrDefault(x => x.Name == "CreditCardName").Value != null)
            {
                string value = valuePairs.FirstOrDefault(x => x.Name == "CreditCardName").Value;
                query = query.Where(x => x.CreditCardName == value);
            }
            if (valuePairs.Any(x => x.Name == "CreditCardLimit") && valuePairs.FirstOrDefault(x => x.Name == "CreditCardLimit").Value != null)
            {
                string value = valuePairs.FirstOrDefault(x => x.Name == "CreditCardLimit").Value;
                decimal CreditCardLimit;
                decimal.TryParse(value, out CreditCardLimit);
                query = query.Where(x => x.CreditCardLimit == CreditCardLimit);
            }
            if (valuePairs.Any(x => x.Name == "CustomerId") && valuePairs.FirstOrDefault(x => x.Name == "CustomerId").Value != null)
            {
                string value = valuePairs.FirstOrDefault(x => x.Name == "CustomerId").Value;
                int CustomerId;
                int.TryParse(value, out CustomerId);
                query = query.Where(x => x.CreditCardNavigation.CustomerId == CustomerId);
            }
            if (valuePairs.Any(x => x.Name == "CardNumber") && valuePairs.FirstOrDefault(x => x.Name == "CardNumber").Value != null)
            {
                string value = valuePairs.FirstOrDefault(x => x.Name == "CardNumber").Value;
                query = query.Where(x => x.CreditCardNavigation.CardNumber == value);
            }
            if (valuePairs.Any(x => x.Name == "IsActive") && valuePairs.FirstOrDefault(x => x.Name == "IsActive").Value != null)
            {
                string value = valuePairs.FirstOrDefault(x => x.Name == "IsActive").Value;
                bool IsActive;
                bool.TryParse(value, out IsActive);
                query = query.Where(x => x.CreditCardNavigation.IsActive == IsActive);
            }

            return query;
        }
        #endregion
    }
}
