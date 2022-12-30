using CoreLayer.Utilities.Messages;
using CoreLayer.Utilities.Result.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities.Result.Concreate
{
    public class Result<T> : Result, IResult<T>
    {
        public Result(bool resultStatus) : base(resultStatus)
        {
        }

        public Result()
        {
            base.ResultMessage = StaticMessage.DefaultErrorMessage;
            base.ResultCode = StaticMessage.DefaultErrorCode;
            base.ResultStatus = false;
            base.ResultInnerMessage = string.Empty;
        }

        Result(string resultMessage, short resultCode, bool resultStatus, string resultInnerMessage)
        {
            base.ResultMessage = resultMessage;
            base.ResultCode = resultCode;
            base.ResultStatus = resultStatus;
            base.ResultInnerMessage = resultInnerMessage;
        }

        public T ResultObje { get; set; }
    }
    public class Result : IResult
    {
        public void SetTrue()
        {
            this.ResultMessage = StaticMessage.DefaultSuccessMessage;
            this.ResultCode = StaticMessage.OK;
            this.ResultStatus = true;
        }

        public void SetTrue(string resultMessage)
        {
            this.ResultMessage = resultMessage;
            this.ResultCode = StaticMessage.OK;
            this.ResultStatus = true;
        }

        public void SetTrue(short resultCode)
        {
            this.ResultMessage = StaticMessage.DefaultSuccessMessage;
            this.ResultCode = resultCode;
            this.ResultStatus = true;
        }

        public void SetTrue(string resultMessage, short resultCode)
        {
            this.ResultMessage = resultMessage;
            this.ResultCode = resultCode;
            this.ResultStatus = true;
        }

        public void SetFalse()
        {
            this.ResultMessage = StaticMessage.DefaultErrorMessage;
            this.ResultCode = StaticMessage.DefaultErrorCode;
            this.ResultStatus = false;
        }

        public void SetFalse(string resultMessage)
        {
            this.ResultMessage = resultMessage;
            this.ResultCode = StaticMessage.DefaultErrorCode;
            this.ResultStatus = false;
        }

        public void SetFalse(short resultCode)
        {
            this.ResultMessage = StaticMessage.DefaultErrorMessage;
            this.ResultCode = resultCode;
            this.ResultStatus = false;
        }

        public void SetFalse(string resultMessage, short resultCode)
        {
            this.ResultMessage = resultMessage;
            this.ResultCode = resultCode;
            this.ResultStatus = false;
        }

        public Result() : this(StaticMessage.DefaultErrorMessage, StaticMessage.DefaultErrorCode, false, string.Empty)
        {

        }

        public Result(bool resultStatus) :
            this
            (
                resultStatus ? StaticMessage.DefaultSuccessMessage : StaticMessage.DefaultErrorMessage,
                resultStatus ? StaticMessage.OK : StaticMessage.DefaultErrorCode,
                resultStatus, string.Empty
            )
        {

        }
        Result(string resultMessage, short resultCode, bool resultStatus, string resultInnerMessage)
        {
            ResultMessage = resultMessage;
            ResultCode = resultCode;
            ResultStatus = resultStatus;
            ResultInnerMessage = resultInnerMessage;
        }

        #region Property
        /// <summary>
        /// Result message
        /// </summary>
        public string ResultMessage { get; set; }
        /// <summary>
        /// Result code 
        /// </summary>
        public short ResultCode { get; set; }
        /// <summary>
        /// Result status
        /// </summary>
        public bool ResultStatus { get; set; }
        /// <summary>
        /// Detay mesajı
        /// </summary>
        public string ResultInnerMessage { get; set; }
        #endregion
    }
}
