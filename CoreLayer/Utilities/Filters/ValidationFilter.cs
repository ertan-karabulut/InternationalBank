using CoreLayer.Utilities.Exception;
using CoreLayer.Utilities.Messages;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities.Filters
{
    public class ValidationFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                List<string> errorMessageList = new List<string>();
                foreach (var modelState in context.ModelState)
                {
                    string key = modelState.Key;
                    foreach (var error in modelState.Value.Errors)
                    {
                        errorMessageList.Add(error.ErrorMessage);
                    }
                }
                throw new CustomValidationException(StaticMessage.DefaultValidationMessage, errorMessageList);
            }
        }
    }
}
