using BusinessLayer.Dto;
using BusinessLayer.Dto.Adress;
using BusinessLayer.Dto.Customer;
using CoreLayer.BusinessLayer.Model;
using CoreLayer.DataAccess.Concrete.DataRequest;
using CoreLayer.Utilities.Result.Concreate;
using EntityLayer.Models.EntityFremework;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICustomerWorkFlow 
    {
        Task<Result> UpdateProfilePhotoAsync(FileModel file);

        Result<ClaimDto> GetClaim();
        Result UpdateProfilePhoto(FileModel file);
        Task<Result> PasswordConfirm(PasswordConfirmDto user);
        Result PasswordChange(PasswordChangeDto passwordChange);
    }
}
