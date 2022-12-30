using BusinessLayer.Dto;
using CoreLayer.BusinessLayer.Model;

using CoreLayer.Utilities.Result.Concreate;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ILogonWorkFlow
    {
        Result<TokenResponseDto> Logon(TokenRequestDto tokenModel);
        Result<TokenResponseDto> RefreshToken(RefreshTokenRequestDto model);
        Task<Result<TokenResponseDto>> LogonAsync(TokenRequestDto tokenModel);
    }
}
