using CoreLayer.DataAccess.Abstract;
using CoreLayer.Utilities.Ioc;
using CoreLayer.Utilities.Logger;
using CoreLayer.Utilities.Messages;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities.Filters
{
    public class LogFilter : Attribute, IActionFilter
    {
        ILogDto _logDto;
        ILogRepository _logRepository;
        public LogFilter(Type logRepository, Type logDto)
        {
            if (!typeof(ILogRepository).IsAssignableFrom(logRepository))
                throw new System.Exception($"{logRepository.Name} bir ILogger değil!");
            this._logRepository = (ILogRepository)Activator.CreateInstance(logRepository);
            if (!typeof(ILogDto).IsAssignableFrom(logDto))
                throw new System.Exception($"{logDto.Name} bir ILogger değil!");
            this._logDto = (ILogDto)Activator.CreateInstance(logDto);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            LogMessage logMessage = ServiceTool.ServiceProvider.GetService<LogMessage>();

            _logDto.Message = $"Inline log : {logMessage.GetLogText()} Result : {JsonConvert.SerializeObject(context.Result)}";
            _logDto.Method = context.RouteData.Values["action"].ToString() + " OnActionExecuted";
            _logDto.Page = context.RouteData.Values["controller"].ToString();
            _logDto.Token = context.HttpContext.Request.Headers["Authorization"];
            _logDto.Ip = context.HttpContext.Connection.RemoteIpAddress.ToString();
            _logDto.CreateDate = DateTime.Now;

            this._logRepository.AddLog(_logDto);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logDto.Message = JsonConvert.SerializeObject(context.ActionArguments);
            _logDto.Method = context.RouteData.Values["action"].ToString() + " OnActionExecuting";
            _logDto.Page = context.RouteData.Values["controller"].ToString();
            _logDto.Token = context.HttpContext.Request.Headers["Authorization"];
            _logDto.Ip = context.HttpContext.Connection.RemoteIpAddress.ToString();
            _logDto.CreateDate = DateTime.Now;

            this._logRepository.AddLog(_logDto);
        }
    }
}
