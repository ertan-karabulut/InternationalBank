using CoreLayer.Utilities.Filters;
using CoreLayer.Utilities.Helpers;
using CoreLayer.Utilities.Ioc;
using DataAccessLayer.Concreate;
using EntityLayer.Models.Mongo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InternationalBankApi.Base
{
    [Route("[controller]")]
    [ExceptionFilter(typeof(ExceptionLogRepository), typeof(ExceptionLog))]
    [Authorize]
    [LogFilter(typeof(InformationLogRepository), typeof(InformationLog))]
    public class BaseController : ControllerBase
    {
        private readonly HelperWorkFlow helperWorkFlow;

        public BaseController()
        {
            this.helperWorkFlow = new HelperWorkFlow();
        }

        protected int GetUserId()
        {
            return this.helperWorkFlow.GetUserId();
        }
    }
}
