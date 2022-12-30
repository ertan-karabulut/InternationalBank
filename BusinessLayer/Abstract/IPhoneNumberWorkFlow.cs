using BusinessLayer.Dto;
using BusinessLayer.Dto.Adress;
using BusinessLayer.Dto.Mail;
using BusinessLayer.Dto.PhpneNumber;
using CoreLayer.DataAccess.Concrete.DataRequest;
using CoreLayer.Utilities.Result.Concreate;
using EntityLayer.Models.EntityFremework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IPhoneNumberWorkFlow 
    {
        #region async metod
        Task<Result<DataTableResponse<PhoneNumberDto>>> CustomerPhoneListAsync(DataTableRequest request);
        Task<Result> UpdatePhoneNumberAsync(PhoneNumberUpdateDto phone);
        Task<Result<PhoneNumberDto>> AddPhoneNumberAsync(PhoneNumberCreateDto phone);
        Task<Result> DeletePhoneNumberAsync(PhoneNumberUpdateDto phone);
        #endregion

        Result<DataTableResponse<PhoneNumberDto>> CustomerPhoneList(DataTableRequest request);
        Result UpdatePhoneNumber(PhoneNumberUpdateDto phone);
        Result<PhoneNumberDto> AddPhoneNumber(PhoneNumberCreateDto phone);
        Result DeletePhoneNumber(PhoneNumberUpdateDto phone);
    }
}
