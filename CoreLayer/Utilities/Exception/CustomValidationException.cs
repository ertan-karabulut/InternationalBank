using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities.Exception
{
    public class CustomValidationException : BaseException
    {
        private List<string> _validateMessage;
        public CustomValidationException(string message) : base(message: message) { }
        public CustomValidationException(string message, List<string> validateMessage) : this(message)
        {
            this._validateMessage = validateMessage;
        }
        public CustomValidationException() { }
        public List<string> ValidateMessage => this._validateMessage;
    }
}
