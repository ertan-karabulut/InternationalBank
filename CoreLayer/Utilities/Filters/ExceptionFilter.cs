using CoreLayer.DataAccess.Abstract;
using CoreLayer.Utilities.Exception;
using CoreLayer.Utilities.Ioc;
using CoreLayer.Utilities.Logger;
using CoreLayer.Utilities.Messages;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using CoreLayer.Utilities.Result.Concreate;
using Microsoft.Extensions.DependencyInjection;
using CoreLayer.Utilities.Result.Abstract;

namespace CoreLayer.Utilities.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        ILogDto _logDto;
        ILogRepository _logRepository;
        public ExceptionFilter(Type logRepository, Type logDto)
        {
            if (!typeof(ILogRepository).IsAssignableFrom(logRepository))
                throw new System.Exception($"{logRepository.Name} bir ILogger değil!");
            this._logRepository = (ILogRepository)Activator.CreateInstance(logRepository);
            if (!typeof(ILogDto).IsAssignableFrom(logDto))
                throw new System.Exception($"{logDto.Name} bir ILogger değil!");
            this._logDto = (ILogDto)Activator.CreateInstance(logDto);
        }
        public override void OnException(ExceptionContext context)
        {
            IResult<object> result = ResultInjection.Result<object>();
            try
            {
                LogMessage logMessage = ServiceTool.ServiceProvider.GetService<LogMessage>();
                result.SetFalse();

                if (context.Exception.GetType() == typeof(CustomValidationException))
                {
                    CustomValidationException exception = (CustomValidationException)context.Exception;
                    result.ResultObje = exception.ValidateMessage;
                    result.SetFalse(StaticMessage.DefaultValidationMessage, StaticMessage.DefaultValidationCode);
                }
                else if (context.Exception.GetType() == typeof(UIException<>))
                {

                }
                _logDto.Message = $"{logMessage.GetLogText()} Hata : {context.Exception.ToString()}";
                _logDto.Method = context.RouteData.Values["action"].ToString();
                _logDto.Page = context.RouteData.Values["controller"].ToString();
                _logDto.Token = context.HttpContext.Request.Headers["Authorization"];
                _logDto.Ip = context.HttpContext.Connection.RemoteIpAddress.ToString();
                _logDto.CreateDate = DateTime.Now;

                this._logRepository.AddLog(_logDto);
                result.ResultInnerMessage = context.Exception.ToString();
                context.Result = new OkObjectResult(result);
            }
            catch (System.Exception exception)
            {
                result.ResultInnerMessage = $"Uygulama hatası : {context.Exception.ToString()}. Loglama yapılırken oluşan hata : {exception.ToString()}";
                context.Result = new OkObjectResult(result);
            }

            base.OnException(context);
        }
    }
}
