using AutoMapper;
using BusinessLayer.Dto.Account;
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
    public class AccountComponent : ComponentBase
    {
        #region Variables
        protected readonly IUnitOfWork _unow;
        #endregion

        #region constructor
        public AccountComponent(IUnitOfWork unow)
        {
            this._unow = unow;
        }
        #endregion

        #region protected methots
        protected Result<DataTableResponse<Account>> GetMyAccountListComponent(DataTableRequest request)
        {
            Result<DataTableResponse<Account>> result = new Result<DataTableResponse<Account>>();
            result.ResultObje = new DataTableResponse<Account>();
            var query = this._unow.AccountRepository.Get(x => true);
            query = query.Include(x => x.Branch).Include(x => x.Type).Include(x => x.AccountBalances.Where(y => y.IsActive)).Include(x => x.AdditionalAccounts.Where(y => y.IsActive)).Include(x => x.CurrencyUnit);

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
        protected Result<Account> AccountDetailComponent(int accountId)
        {
            Result<Account> result = new Result<Account>();
            var query = this._unow.AccountRepository.Get(x => x.Id == accountId);
            query = query.Include(x => x.Customer).Include(x => x.Branch).Include(x => x.Type).Include(x => x.CurrencyUnit).Include(x => x.AccountBalances.Where(y => y.IsActive)).Include(x => x.AdditionalAccounts.Where(y => y.IsActive));

            var account = query.FirstOrDefault();
            if (account != null)
            {
                result.ResultObje = account;
                result.SetTrue();
            }
            return result;
        }
        protected Result<DataTableResponse<AccountBalanceHistory>> AccountHistoryComponent(DataTableRequest request)
        {
            var result = new Result<DataTableResponse<AccountBalanceHistory>>();
            var query = this._unow.AccountBalanceHistoryRepository.Get(x => true);


            if (request.Filter.Any(x => x.Name == "AccountId") && request.Filter.FirstOrDefault(x => x.Name == "AccountId").Value != null)
            {
                string value = request.Filter.FirstOrDefault(x => x.Name == "AccountId").Value;
                int AccountId;
                int.TryParse(value, out AccountId);
                query = query.Where(x => x.AccountId == AccountId);
            }

            if (request.Filter.Any(x => x.Name == "CreateDate") && request.Filter.FirstOrDefault(x => x.Name == "CreateDate").Value != null)
            {
                string value = request.Filter.FirstOrDefault(x => x.Name == "CreateDate").Value;
                DateTime CreateDate;
                DateTime.TryParse(value, out CreateDate);
                query = query.Where(x => x.CreateDate >= CreateDate);
            }

            if (request.Filter.Any(x => x.Name == "Today") && request.Filter.FirstOrDefault(x => x.Name == "Today").Value != null)
            {
                string value = request.Filter.FirstOrDefault(x => x.Name == "Today").Value;
                bool Today;
                bool.TryParse(value, out Today);
                if (Today)
                {
                    query = query.Where(x => x.CreateDate >= DateTime.Today);
                }
            }

            if (request.Filter.Any(x => x.Name == "Yesterday") && request.Filter.FirstOrDefault(x => x.Name == "Yesterday").Value != null)
            {
                string value = request.Filter.FirstOrDefault(x => x.Name == "Yesterday").Value;
                bool Yesterday;
                bool.TryParse(value, out Yesterday);
                if (Yesterday)
                {
                    query = query.Where(x => x.CreateDate >= DateTime.Today.AddDays(-1) && x.CreateDate < DateTime.Today);
                }
            }

            if (request.Filter.Any(x => x.Name == "Explanation") && request.Filter.FirstOrDefault(x => x.Name == "Explanation").Value != null)
            {
                string value = request.Filter.FirstOrDefault(x => x.Name == "Explanation").Value;
                query = query.Where(x => value.Contains(x.Explanation));
            }

            switch (request.Order.Column)
            {
                case "Id":
                    if (request.Order.Short.ToUpper() == "DESC")
                        query = query.OrderByDescending(x => x.Id);
                    else
                        query = query.OrderBy(x => x.Id);
                    break;
                case "Amount":
                    if (request.Order.Short.ToUpper() == "DESC")
                        query = query.OrderByDescending(x => x.Amount);
                    else
                        query = query.OrderBy(x => x.Amount);
                    break;
                case "Explanation":
                    if (request.Order.Short.ToUpper() == "DESC")
                        query = query.OrderByDescending(x => x.Explanation);
                    else
                        query = query.OrderBy(x => x.Explanation);
                    break;
                case "CreateDate":
                    if (request.Order.Short.ToUpper() == "DESC")
                        query = query.OrderByDescending(x => x.CreateDate);
                    else
                        query = query.OrderBy(x => x.CreateDate);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreateDate);
                    break;
            }

            int count = query.Count();
            if (count > 0)
            {
                result.ResultObje = new DataTableResponse<AccountBalanceHistory>();
                result.ResultObje.Data = query.Skip(request.Skip).Take(request.Take).ToList();
                result.ResultObje.TotalCount = count;
                result.SetTrue();
            }
            return result;
        }
        protected Result<Account> CloseAccountComponent(AccountUpdateDto dto)
        {
            var result = new Result<Account>();
            var query = this._unow.AccountRepository.Get(x => true);
            query = query.Where(x => x.Id == dto.Id);
            query = query.Include(x => x.AccountBalances.Where(y => y.IsActive));

            var account = query.FirstOrDefault();
            if (account != null)
            {
                result.SetTrue();
                result.ResultObje = account;
            }

            return result;
        }

        #region Async metod
        protected async Task<Result<DataTableResponse<Account>>> GetMyAccountListComponentAsync(DataTableRequest request)
        {
            Result<DataTableResponse<Account>> result = new Result<DataTableResponse<Account>>();
            result.ResultObje = new DataTableResponse<Account>();
            var query = this._unow.AccountRepository.Get(x => true);
            query = query.Include(x => x.Branch).Include(x => x.Type).Include(x => x.AccountBalances.Where(y => y.IsActive)).Include(x => x.AdditionalAccounts.Where(y => y.IsActive)).Include(x => x.CurrencyUnit);

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

        protected async Task<Result<Account>> AccountDetailComponentAsync(int accountId)
        {
            Result<Account> result = new Result<Account>();
            var query = this._unow.AccountRepository.Get(x => x.Id == accountId);
            query = query.Include(x => x.Customer).Include(x => x.Branch).Include(x => x.Type).Include(x => x.CurrencyUnit).Include(x => x.AccountBalances.Where(y => y.IsActive)).Include(x => x.AdditionalAccounts.Where(y => y.IsActive));

            var account = await query.FirstOrDefaultAsync();
            if (account != null)
            {
                result.ResultObje = account;
                result.SetTrue();
            }
            return result;
        }

        protected async Task<Result<DataTableResponse<AccountBalanceHistory>>> AccountHistoryComponentAsync(DataTableRequest request)
        {
            var result = new Result<DataTableResponse<AccountBalanceHistory>>();
            var query = this._unow.AccountBalanceHistoryRepository.Get(x => true);


            if (request.Filter.Any(x => x.Name == "AccountId") && request.Filter.FirstOrDefault(x => x.Name == "AccountId").Value != null)
            {
                string value = request.Filter.FirstOrDefault(x => x.Name == "AccountId").Value;
                int AccountId;
                int.TryParse(value, out AccountId);
                query = query.Where(x => x.AccountId == AccountId);
            }

            if (request.Filter.Any(x => x.Name == "CreateDate") && request.Filter.FirstOrDefault(x => x.Name == "CreateDate").Value != null)
            {
                string value = request.Filter.FirstOrDefault(x => x.Name == "CreateDate").Value;
                DateTime CreateDate;
                DateTime.TryParse(value, out CreateDate);
                query = query.Where(x => x.CreateDate >= CreateDate);
            }

            if (request.Filter.Any(x => x.Name == "Today") && request.Filter.FirstOrDefault(x => x.Name == "Today").Value != null)
            {
                string value = request.Filter.FirstOrDefault(x => x.Name == "Today").Value;
                bool Today;
                bool.TryParse(value, out Today);
                if (Today)
                {
                    query = query.Where(x => x.CreateDate >= DateTime.Today);
                }
            }

            if (request.Filter.Any(x => x.Name == "Yesterday") && request.Filter.FirstOrDefault(x => x.Name == "Yesterday").Value != null)
            {
                string value = request.Filter.FirstOrDefault(x => x.Name == "Yesterday").Value;
                bool Yesterday;
                bool.TryParse(value, out Yesterday);
                if (Yesterday)
                {
                    query = query.Where(x => x.CreateDate >= DateTime.Today.AddDays(-1) && x.CreateDate < DateTime.Today);
                }
            }

            if (request.Filter.Any(x => x.Name == "Explanation") && request.Filter.FirstOrDefault(x => x.Name == "Explanation").Value != null)
            {
                string value = request.Filter.FirstOrDefault(x => x.Name == "Explanation").Value;
                query = query.Where(x => value.Contains(x.Explanation));
            }

            switch (request.Order.Column)
            {
                case "Id":
                    if (request.Order.Short.ToUpper() == "DESC")
                        query = query.OrderByDescending(x => x.Id);
                    else
                        query = query.OrderBy(x => x.Id);
                    break;
                case "Amount":
                    if (request.Order.Short.ToUpper() == "DESC")
                        query = query.OrderByDescending(x => x.Amount);
                    else
                        query = query.OrderBy(x => x.Amount);
                    break;
                case "Explanation":
                    if (request.Order.Short.ToUpper() == "DESC")
                        query = query.OrderByDescending(x => x.Explanation);
                    else
                        query = query.OrderBy(x => x.Explanation);
                    break;
                case "CreateDate":
                    if (request.Order.Short.ToUpper() == "DESC")
                        query = query.OrderByDescending(x => x.CreateDate);
                    else
                        query = query.OrderBy(x => x.CreateDate);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreateDate);
                    break;
            }

            int count = await query.CountAsync();
            if (count > 0)
            {
                result.ResultObje = new DataTableResponse<AccountBalanceHistory>();
                result.ResultObje.Data = await query.Skip(request.Skip).Take(request.Take).ToListAsync();
                result.ResultObje.TotalCount = count;
                result.SetTrue();
            }
            return result;
        }

        protected async Task<Result<Account>> CloseAccountComponentAsync(AccountUpdateDto dto)
        {
            var result = new Result<Account>();
            var query = this._unow.AccountRepository.Get(x => true);
            query = query.Where(x => x.Id == dto.Id);
            query = query.Include(x => x.AccountBalances.Where(y => y.IsActive));

            var account = await query.FirstOrDefaultAsync();
            if (account != null)
            {
                result.SetTrue();
                result.ResultObje = account;
            }

            return result;
        }
        #endregion
        #endregion

        #region private methots
        IQueryable<Account> SetOrder(OrderModel order, IQueryable<Account> query)
        {
            switch (order.Column)
            {
                case "BranchId":
                    if (order.Short.ToUpper() == "DESC")
                        query = query.OrderByDescending(x => x.BranchId);
                    else
                        query = query.OrderBy(x => x.BranchId);
                    break;
                case "CustomerId":
                    if (order.Short.ToUpper() == "DESC")
                        query = query.OrderByDescending(x => x.CustomerId);
                    else
                        query = query.OrderBy(x => x.CustomerId);
                    break;
                case "TypeId":
                    if (order.Short.ToUpper() == "DESC")
                        query = query.OrderByDescending(x => x.TypeId);
                    else
                        query = query.OrderBy(x => x.TypeId);
                    break;
                case "BranchNumber":
                    if (order.Short.ToUpper() == "DESC")
                        query = query.OrderByDescending(x => x.Branch.BranchNumber);
                    else
                        query = query.OrderBy(x => x.Branch.BranchNumber);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreateDate);
                    break;
            }
            return query;
        }

        IQueryable<Account> SetFilter(List<NameValuePair> valuePairs, IQueryable<Account> query)
        {
            if (valuePairs.Any(x => x.Name == "Id") && valuePairs.FirstOrDefault(x => x.Name == "Id").Value != null)
            {
                string value = valuePairs.FirstOrDefault(x => x.Name == "Id").Value;
                int Id;
                int.TryParse(value, out Id);
                query = query.Where(x => x.Id == Id);
            }
            if (valuePairs.Any(x => x.Name == "BranchId") && valuePairs.FirstOrDefault(x => x.Name == "BranchId").Value != null)
            {
                string value = valuePairs.FirstOrDefault(x => x.Name == "BranchId").Value;
                int BranchId;
                int.TryParse(value, out BranchId);
                query = query.Where(x => x.BranchId == BranchId);
            }
            if (valuePairs.Any(x => x.Name == "CustomerId") && valuePairs.FirstOrDefault(x => x.Name == "CustomerId").Value != null)
            {
                string value = valuePairs.FirstOrDefault(x => x.Name == "CustomerId").Value;
                int CustomerId;
                int.TryParse(value, out CustomerId);
                query = query.Where(x => x.CustomerId == CustomerId);
            }
            if (valuePairs.Any(x => x.Name == "Iban") && valuePairs.FirstOrDefault(x => x.Name == "Iban").Value != null)
            {
                string value = valuePairs.FirstOrDefault(x => x.Name == "Iban").Value;
                query = query.Where(x => x.Iban == value);
            }
            if (valuePairs.Any(x => x.Name == "AccountNumber") && valuePairs.FirstOrDefault(x => x.Name == "AccountNumber").Value != null)
            {
                string value = valuePairs.FirstOrDefault(x => x.Name == "AccountNumber").Value;
                query = query.Where(x => x.AccountNumber == value);
            }
            if (valuePairs.Any(x => x.Name == "TypeId") && valuePairs.FirstOrDefault(x => x.Name == "TypeId").Value != null)
            {
                string value = valuePairs.FirstOrDefault(x => x.Name == "TypeId").Value;
                int TypeId;
                int.TryParse(value, out TypeId);
                query = query.Where(x => x.TypeId == TypeId);
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
        #endregion
    }
}
