using BusinessLayer.Dto;
using BusinessLayer.Dto.Adress;
using BusinessLayer.Dto.Mail;
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
    public interface IEMailWorkFlow 
    {
        #region Async metod
        Task<Result<DataTableResponse<MailDto>>> CustomerMailListAsync(DataTableRequest request);
        Task<Result> UpdateMailAsync(MailUpdateDto mail);
        Task<Result> DeleteMailAsync(MailUpdateDto mail);
        Task<Result<MailDto>> AddMailAsync(MailCreateDto mail);
        #endregion
        Result<DataTableResponse<MailDto>> CustomerMailList(DataTableRequest request);
        Result UpdateMail(MailUpdateDto mail);
        Result DeleteMail(MailUpdateDto mail);
        Result<MailDto> AddMail(MailCreateDto mail);
    }
}
