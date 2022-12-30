using BusinessLayer.Dto.Card;
using CoreLayer.DataAccess.Concrete.DataRequest;
using CoreLayer.Utilities.Result.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICardWorkFlow
    {
        #region Async metod
        Task<Result<DataTableResponse<CreditCardDto>>> CustomerCrediCardListAsync(DataRequestBase request);
        Task<Result<DataTableResponse<AtmCardDto>>> CustomerAtmCardListAsync(DataRequestBase request);
        #endregion

        Result<DataTableResponse<CreditCardDto>> CustomerCrediCardList(DataRequestBase request);
        Result<DataTableResponse<AtmCardDto>> CustomerAtmCardList(DataRequestBase request);
    }
}
