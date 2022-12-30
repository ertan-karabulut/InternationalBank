using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.Dto;
using BusinessLayer.Dto.Account;
using CoreLayer.DataAccess.Concrete.DataRequest;
using CoreLayer.Utilities.Filters;

using CoreLayer.Utilities.Result.Concreate;
using EntityLayer.Models;
using InternationalBankApi.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternationalBankApi.Controllers
{
    public class AccountController : BaseController
    {
        #region Variables
        private readonly IAccountWorkFlow _accountWorkFlow;
        #endregion
        #region constructor
        public AccountController(IAccountWorkFlow accountWorkFlow)
        {
            this._accountWorkFlow = accountWorkFlow;
        }
        #endregion

        [HttpPost, Route("MyAccount"), Authorize(Roles = "Customer")]
        public IActionResult MyAccount([FromBody] DataTableRequest request)
        {
            var result = this._accountWorkFlow.GetMyAccountList(request);
            return Ok(result);
        }

        [HttpPost, Route("AddAccount")]
        public IActionResult AddAccount([FromBody] AccountCreatDto model)
        {
            var result = _accountWorkFlow.Add(model);
            return Ok(result);
        }

        [HttpPost, Route("AddAccountList")]
        public IActionResult AddAccountList([FromBody] AccountCreatDtoList model)
        {
            var result = _accountWorkFlow.Add(model);
            return Ok(result);
        }

        [HttpPost, Route("DeleteAccount")]
        public IActionResult DeleteAccount([FromBody] AccountUpdateDto model)
        {
            var result = _accountWorkFlow.Delete(model);
            return Ok(result);
        }

        [HttpPost, Route("DeleteAccounList")]
        public IActionResult DeleteAccounList([FromBody] AccountUpdateDtoList model)
        {
            var result = _accountWorkFlow.Delete(model);
            return Ok(result);
        }

        [HttpPost, Route("UpdateAccount")]
        public IActionResult UpdateAccount([FromBody] AccountUpdateDto model)
        {
            var result = _accountWorkFlow.Update(model);
            return Ok(result);
        }

        [HttpPost, Route("UpdateAccountList")]
        public IActionResult UpdateAccountList([FromBody] AccountUpdateDtoList model)
        {
            var result = _accountWorkFlow.Update(model);
            return Ok(result);
        }

        [HttpGet, Route("AccountDetail"), Authorize(Roles = "Customer")]
        public IActionResult AccountDetail(int accountId)
        {
            var result = _accountWorkFlow.AccountDetail(accountId);
            return Ok(result);
        }

        [HttpPost, Route("CloseAccount"), Authorize(Roles = "Customer")]
        public IActionResult CloseAccount([FromBody] AccountUpdateDto request)
        {
            var result = _accountWorkFlow.CloseAccount(request);
            return Ok(result);
        }

        [HttpPost, Route("AccountHistory"), Authorize(Roles = "Customer")]
        public IActionResult AccountHistory([FromBody] DataTableRequest request)
        {
            var result = _accountWorkFlow.AccountHistory(request);
            return Ok(result);
        }
    }
}
