using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBlazor.Models
{
    class Result<T> : Result
    {
        public T ResultObje { get; set; }
    }
    class Result
    {
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
