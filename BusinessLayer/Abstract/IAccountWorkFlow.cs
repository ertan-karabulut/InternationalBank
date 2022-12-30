using BusinessLayer.Dto;
using BusinessLayer.Dto.Account;
using CoreLayer.DataAccess.Concrete;
using CoreLayer.DataAccess.Concrete.DataRequest;
using CoreLayer.Utilities.Result.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IAccountWorkFlow
    {
        #region Ekleme metodları
        #region Async metod
        Task<Result<List<AccountDto>>> AddAsync(AccountCreatDtoList entity);
        Task<Result<AccountDto>> AddAsync(AccountCreatDto entity);
        #endregion

        Result<List<AccountDto>> Add(AccountCreatDtoList entity);
        Result<AccountDto> Add(AccountCreatDto entity);
        #endregion

        #region Güncelleme metodları
        # region Async metod
        Task<Result> UpdateAsync(AccountUpdateDto entity);
        Task<Result> UpdateAsync(AccountUpdateDtoList entity);
        Task<Result> CloseAccountAsync(AccountUpdateDto account);
        #endregion

        Result Update(AccountUpdateDto entity);
        Result Update(AccountUpdateDtoList entity);
        Result CloseAccount(AccountUpdateDto account);
        #endregion

        #region Silme medotları
        #region Async metod
        Task<Result> DeleteAsync(AccountUpdateDtoList entity);
        Task<Result> DeleteAsync(AccountUpdateDto entity);
        #endregion

        Result Delete(AccountUpdateDtoList entity);
        Result Delete(AccountUpdateDto entity);
        #endregion

        #region Listeleme metodları
        #region Async metod
        Task<Result<DataTableResponse<MyAccountDto>>> GetMyAccountListAsync(DataTableRequest request);
        Task<Result<AccountDetailDto>> AccountDetailAsync(int accountId);
        Task<Result<DataTableResponse<AccountHistoryDto>>> AccountHistoryAsync(DataTableRequest request);
        #endregion
        Result<DataTableResponse<MyAccountDto>> GetMyAccountList(DataTableRequest request);
        Result<AccountDetailDto> AccountDetail(int accountId);
        Result<DataTableResponse<AccountHistoryDto>> AccountHistory(DataTableRequest request);
        #endregion
    }
}
