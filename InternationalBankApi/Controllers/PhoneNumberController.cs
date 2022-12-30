using BusinessLayer.Abstract;
using BusinessLayer.Dto.PhpneNumber;
using CoreLayer.DataAccess.Concrete.DataRequest;
using InternationalBankApi.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InternationalBankApi.Controllers
{
    public class PhoneNumberController : BaseController
    {
        #region Variables
        private readonly IPhoneNumberWorkFlow _phoneNumberWorkFlow;
        #endregion
        #region Constructor
        public PhoneNumberController(IPhoneNumberWorkFlow phoneNumberWorkFlow)
        {
            this._phoneNumberWorkFlow = phoneNumberWorkFlow;
        }
        #endregion

        [HttpPost, Route("CustomerPhoneList"), Authorize(Roles = "Customer")]
        public IActionResult CustomerPhoneList([FromBody] DataTableRequest request)
        {
            var result = this._phoneNumberWorkFlow.CustomerPhoneList(request);
            return Ok(result);
        }

        [HttpPost, Route("UpdatePhoneNumber"), Authorize(Roles = "Customer")]
        public IActionResult UpdatePhoneNumber([FromBody] PhoneNumberUpdateDto request)
        {
            var result = this._phoneNumberWorkFlow.UpdatePhoneNumber(request);
            return Ok(result);
        }

        [HttpPost, Route("AddPhoneNumber"), Authorize(Roles = "Customer")]
        public IActionResult AddPhoneNumber([FromBody] PhoneNumberCreateDto phone)
        {
            var result = this._phoneNumberWorkFlow.AddPhoneNumber(phone);
            return Ok(result);
        }

        [HttpPost, Route("DeletePhoneNumber"), Authorize(Roles = "Customer")]
        public IActionResult DeletePhoneNumber([FromBody] PhoneNumberUpdateDto phone)
        {
            var result = this._phoneNumberWorkFlow.DeletePhoneNumber(phone);
            return Ok(result);
        }
    }
}
