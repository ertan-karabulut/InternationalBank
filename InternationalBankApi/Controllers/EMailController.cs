using BusinessLayer.Abstract;
using BusinessLayer.Dto.Mail;
using CoreLayer.DataAccess.Concrete.DataRequest;
using InternationalBankApi.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace InternationalBankApi.Controllers
{
    public class EMailController : BaseController
    {
        #region Variables
        private readonly IEMailWorkFlow _mailWorkFlow;
        #endregion
        #region Constructor
        public EMailController(IEMailWorkFlow mailWorkFlow)
        {
            this._mailWorkFlow = mailWorkFlow;
        }
        #endregion

        [HttpPost, Route("CustomerMailList"), Authorize(Roles = "Customer")]
        public IActionResult CustomerMailList([FromBody] DataTableRequest request)
        {
            var result = this._mailWorkFlow.CustomerMailList(request);
            return Ok(result);
        }

        [HttpPost, Route("UpdateMail")]
        public IActionResult UpdateMail([FromBody] MailUpdateDto mail)
        {
            var result = this._mailWorkFlow.UpdateMail(mail);
            return Ok(result);
        }

        [HttpPost, Route("DeleteMail")]
        public IActionResult DeleteMail([FromBody] MailUpdateDto mail)
        {
            var result = this._mailWorkFlow.DeleteMail(mail);
            return Ok(result);
        }

        [HttpPost, Route("AddMail")]
        public IActionResult AddMail([FromBody] MailCreateDto mail)
        {
            var result = this._mailWorkFlow.AddMail(mail);
            return Ok(result);
        }
    }
}
