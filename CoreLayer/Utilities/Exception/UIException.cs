using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities.Exception
{
    public class UIException<T> : BaseException
    {
        private T _errorData;
        public UIException()
        {

        }

        public UIException(string message) : base(message)
        {

        }
        public UIException(string message, T errorData) : base(message)
        {
            this._errorData = errorData;
        }
        public T ErrorData => this._errorData;
    }
}
