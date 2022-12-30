using BusinessLayer.Abstract;
using CoreLayer.Utilities.Aspect;
using InternationalBankApi.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InternationalBankApi.Controllers
{
    public class BranchController : BaseController
    {
        private readonly IBranchWorkFlow _branchWorkFlow;
        public BranchController(IBranchWorkFlow branchWorkFlow)
        {
            this._branchWorkFlow = branchWorkFlow;
        }

        [HttpGet, Route("GetBranchList")]
        public IActionResult GetBranchList()
        {
            var result = this._branchWorkFlow.BranchList();
            return Ok(result);
        }
    }
}
