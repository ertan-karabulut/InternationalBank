using CoreLayer.Utilities.Cache.Abstract;
using InternationalBankApi.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternationalBankApi.Controllers
{
    public class CacheRemoveController : BaseController
    {
        #region Variables
        private readonly ICacheWorkFlow _cacheWorkFlow;
        private readonly CoreLayer.Utilities.Result.Abstract.IResult _result;
        #endregion
        #region constructor
        public CacheRemoveController(ICacheWorkFlow cacheWorkFlow, CoreLayer.Utilities.Result.Abstract.IResult result)
        {
            this._cacheWorkFlow = cacheWorkFlow;
            this._result = result;
        }
        #endregion

        [HttpPost, Route("RemoveByPatternList")]
        public IActionResult RemoveByPatternList([FromBody] List<string> keyList)
        {
            foreach (var item in keyList)
            {
                _cacheWorkFlow.RemoveByPattern(item);
            }
            _result.SetTrue();
            return Ok(_result);
        }
        [HttpPost, Route("RemoveByPattern")]
        public IActionResult RemoveByPattern([FromBody] string pattern)
        {
            _cacheWorkFlow.RemoveByPattern(pattern);
            _result.SetTrue();
            return Ok(_result);
        }
        [HttpPost, Route("RemoveAll")]
        public IActionResult RemoveAll()
        {
            _cacheWorkFlow.Clear();
            _result.SetTrue();
            return Ok(_result);
        }
    }
}
