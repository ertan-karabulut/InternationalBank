using CoreLayer.Utilities.Exception;
using CoreLayer.Utilities.Messages;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities.Validation
{
    public class ValidationTool
    {
        public static void Validate(IValidator validator, object entity)
        {
            List<Dictionary<string, List<string>>> dictionaryList = new List<Dictionary<string, List<string>>>();
            var context = new ValidationContext<object>(entity);
            var validation = validator.Validate(context);
            if (!validation.IsValid)
            {
                List<string> errorList = new List<string>();
                foreach (var item in validation.Errors)
                {
                    errorList.Add(item.ErrorMessage);
                }
                throw new CustomValidationException(StaticMessage.DefaultValidationMessage, errorList);
            }
        }
    }
}
