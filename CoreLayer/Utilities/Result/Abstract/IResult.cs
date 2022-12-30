using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities.Result.Abstract
{
    public interface IResult<T> : IResult
    {
        public T ResultObje { get; set; }
    }

    public interface IResult
    {
        void SetTrue();
        void SetTrue(string resultMessage);
        void SetTrue(short resultCode);
        void SetTrue(string resultMessage, short resultCode);
        void SetFalse();
        void SetFalse(string resultMessage);
        void SetFalse(short resultCode);
        void SetFalse(string resultMessage, short resultCode);
        public string ResultMessage { get; set; }
        public short ResultCode { get; set; }
        public bool ResultStatus { get; set; }
        public string ResultInnerMessage { get; set; }
    }
}
