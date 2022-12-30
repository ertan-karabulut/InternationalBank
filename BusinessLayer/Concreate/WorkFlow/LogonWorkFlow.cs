using BusinessLayer.Abstract;
using CoreLayer.Security;

using DataAccessLayer.Abstract;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BusinessLayer.Concreate.Component;
using BusinessLayer.Dto;
using Microsoft.Extensions.Options;
using BusinessLayer.Dto.ConfigurationModel;
using CoreLayer.Utilities.Result.Concreate;

namespace BusinessLayer.Concreate.WorkFlow
{
    public class LogonWorkFlow : LogonComponen , ILogonWorkFlow
    {
        #region Variables
        private readonly AppKey _appKey;
        #endregion

        #region constructor
        public LogonWorkFlow(IUnitOfWork unow, IOptions<AppKey> option) : base(unow)
        {
            this._appKey= option.Value;
        }
        #endregion
        public async Task<Result<TokenResponseDto>> LogonAsync(TokenRequestDto tokenModel)
        {
            Result<TokenResponseDto> result = new Result<TokenResponseDto>();
            result.ResultObje = new TokenResponseDto();
            string saltPassword = Hashing.CreateSHA512(tokenModel.Password);
            string logMessage = string.Empty;
            var claimResult = await base.GetUserPasswordAsync(tokenModel.User, saltPassword);
            if (claimResult.ResultStatus)
            {
                logMessage += "Kullanıcı adı şifre doğru";
                string token = this.CretateToken(claimResult.ResultObje);
                if (!string.IsNullOrEmpty(token))
                {
                    logMessage = "Token oluşturma başarılı.";
                    result.ResultObje.AccessToken = token;
                    result.ResultObje.RefreshToken = Hashing.CreateSHA512($"{token}{this._appKey.TokenKey}");
                    result.SetTrue();
                }
            }
            else
            {
                result.SetFalse();
                result.ResultInnerMessage = "Kullanıcı adı veya şifre hatalı.";
                logMessage = "Kullanıcı adı veya şifre hatalı.";
            }

            base.LogMessage.InsertLog(logMessage, "LogonAsync", "LogonWorkFlow.cs");
            return result;
        }
        public Result<TokenResponseDto> RefreshToken(RefreshTokenRequestDto model)
        {
            Result<TokenResponseDto> result = new Result<TokenResponseDto>();
            result.ResultObje = new TokenResponseDto();
            StringBuilder logText = new StringBuilder();
            logText.AppendLine($"RefreshToken işlemi başladı.");
            if (model.RefreshToken == Hashing.CreateSHA512($"{model.AccessToken}{this._appKey.TokenKey}"))
            {
                logText.AppendLine("RefreshToken doğrulandı.");
                JwtSecurityTokenHandler toJwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken toSecurityToken = toJwtSecurityTokenHandler.ReadJwtToken(model.AccessToken);
                string refreshTokenExTime = this._appKey.RefreshTokenExTime;
                double extTime;
                double.TryParse(refreshTokenExTime, out extTime);
                if (toSecurityToken.IssuedAt.ToLocalTime().AddMinutes(extTime) >= DateTime.Now)
                {
                    var claims = toSecurityToken.Claims.ToList();
                    string key = this._appKey.TokenKey;
                    result.ResultObje.AccessToken = base.helperWorkFlow.CreateToken(claims: claims, key: key, IssuedAt: toSecurityToken.IssuedAt);
                    result.ResultObje.RefreshToken = Hashing.CreateSHA512($"{result.ResultObje.AccessToken}{this._appKey.TokenKey}");
                    result.SetTrue();
                }
                else
                    logText.AppendLine("RefreshToken süresi sonlanmış.");
            }
            else
                logText.AppendLine("RefreshToken doğrulanamadı.");
            base.LogMessage.InsertLog(logText.ToString(), "RefreshTokenAsync", "LogonWorkFlow.cs");
            return result;
        }
        public Result<TokenResponseDto> Logon(TokenRequestDto tokenModel)
        {
            Result<TokenResponseDto> result = new Result<TokenResponseDto>();
            result.ResultObje = new TokenResponseDto();
            string saltPassword = Hashing.CreateSHA512(tokenModel.Password);
            string logMessage = string.Empty;
            var claimResult = base.GetUserPassword(tokenModel.User, saltPassword);
            if (claimResult.ResultStatus)
            {
                logMessage += "Kullanıcı adı şifre doğru";
                string token = this.CretateToken(claimResult.ResultObje);
                if (!string.IsNullOrEmpty(token))
                {
                    logMessage = "Token oluşturma başarılı.";
                    result.ResultObje.AccessToken = token;
                    result.ResultObje.RefreshToken = Hashing.CreateSHA512($"{token}{this._appKey.TokenKey}");
                    result.SetTrue();
                }
            }
            else
            {
                result.SetFalse();
                result.ResultInnerMessage = "Kullanıcı adı veya şifre hatalı.";
                logMessage = "Kullanıcı adı veya şifre hatalı.";
            }

            base.LogMessage.InsertLog(logMessage, "Logon", "LogonWorkFlow.cs");
            return result;
        }
        #region TokenOperation
        string CretateToken(ClaimDto claim, DateTime? IssuedAt = null)
        {
            string key = this._appKey.TokenKey;
            List<Claim> claims = new List<Claim>()
            {
                        new Claim(ClaimTypes.Name,claim.Name),
                        new Claim(ClaimTypes.Surname,claim.Surname),
                        new Claim("Id",claim.Id.ToString()),
                        new Claim("Photo",claim.Photo),
                        new Claim("CustomerNumber",claim.CustomerNumber)
            };
            foreach (var item in claim.RoleList)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }
            return base.helperWorkFlow.CreateToken(claims: claims,key: key);
        }
        #endregion
    }
}
