using BusinessLayer.Abstract;
using BusinessLayer.Dto;
using BusinessLayer.Dto.Adress;
using BusinessLayer.Dto.Customer;
using BusinessLayer.Validation;
using CoreLayer.BusinessLayer.Model;
using CoreLayer.DataAccess.Concrete.DataRequest;
using CoreLayer.Utilities.Filters;
using InternationalBankApi.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternationalBankApi.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly ICustomerWorkFlow _customerWorkFlow;

        public CustomerController(ICustomerWorkFlow customerWorkFlow)
        {
            this._customerWorkFlow = customerWorkFlow;
        }

        [HttpGet,Route("GetNameAndImage"), Authorize(Roles = "Customer")]
        public IActionResult GetNameAndImage()
        {
            var result = this._customerWorkFlow.GetClaim();
            return Ok(result);
        }

        [HttpPost, Route("UpdateProfilePhoto"), Authorize(Roles = "Customer")]
        public IActionResult UpdateProfilePhoto([FromBody]FileModel file)
        {
            var result = this._customerWorkFlow.UpdateProfilePhoto(file);
            return Ok(result);
        }
        [HttpPost, Route("PasswordConfirm"), AllowAnonymous]
        public async Task<IActionResult> PasswordConfirm([FromBody] PasswordConfirmDto user)
        {
            var result = await this._customerWorkFlow.PasswordConfirm(user);
            return Ok(result);
        }
        [HttpPost, Route("PasswordChange"), AllowAnonymous]
        public IActionResult PasswordChange([FromBody] PasswordChangeDto model)
        {
            var result = this._customerWorkFlow.PasswordChange(model);
            return Ok(result);
        }
    }
}
