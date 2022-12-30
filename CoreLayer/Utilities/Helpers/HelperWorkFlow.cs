using CoreLayer.Utilities.Enum;
using CoreLayer.Utilities.Helpers.Abstract;
using CoreLayer.Utilities.Ioc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CoreLayer.Utilities.Helpers
{
    public class HelperWorkFlow
    {
        public string CreateToken(List<Claim> claims, string key, TimeEnum timeEnum = TimeEnum.Minute, int duration = 30, DateTime? IssuedAt = null)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] keys = Encoding.UTF8.GetBytes(key);
            IssuedAt = IssuedAt.HasValue ? IssuedAt.Value : DateTime.UtcNow;
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity
                (
                    claims
                ),
                IssuedAt = IssuedAt,
                Expires = DateTime.UtcNow.AddMinutes((UInt32)timeEnum * duration),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(keys)
                    , SecurityAlgorithms.HmacSha256Signature
                )
            };
            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }

        public string GetIpAddress()
        {
            string IpAddress = string.Empty;
            try
            {
                IHttpContextAccessor HttpContext = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
                if (HttpContext != null && HttpContext.HttpContext != null && HttpContext.HttpContext.Connection != null)
                {
                    IpAddress = HttpContext.HttpContext.Connection.RemoteIpAddress.ToString();
                }
            }
            catch (System.Exception)
            {

            }
            return IpAddress;
        }

        public string GetToken()
        {
            string token = string.Empty;
            try
            {
                IHttpContextAccessor HttpContext = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
                if (HttpContext != null && HttpContext.HttpContext != null && HttpContext.HttpContext.Request != null)
                {
                    token = HttpContext.HttpContext.Request.Headers["Authorization"];
                }
            }
            catch (System.Exception)
            {

            }
            return token;
        }

        public void GetTokenClaims(IClaimDto claimModel)
        {
            List<Claim> claims = new List<Claim>();
            try
            {
                IHttpContextAccessor HttpContext = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

                if (HttpContext != null && HttpContext.HttpContext != null && HttpContext.HttpContext.User != null && HttpContext.HttpContext.User.Identities != null)
                {
                    claims = HttpContext.HttpContext.User.Identities.First().Claims.ToList();
                    claimModel.Name = claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault().Value;
                    claimModel.Surname = claims.Where(x => x.Type == ClaimTypes.Surname).FirstOrDefault().Value;
                    claimModel.Id = int.Parse(claims.Where(x => x.Type == "Id").FirstOrDefault().Value);
                    claimModel.Photo = claims.Where(x => x.Type == "Photo").FirstOrDefault().Value;
                    claimModel.RoleList = (from claim in claims.Where(x => x.Type == ClaimTypes.Role).ToList()
                                           select new List<string>()
                                    {
                                        claim.Value
                                    }).FirstOrDefault();
                    claimModel.CustomerNumber = claims.Where(x => x.Type == "CustomerNumber").FirstOrDefault().Value;
                }
            }
            catch (System.Exception)
            {
                claimModel = null;
            }
        }

        public int GetUserId()
        {
            int Id = 0;
            IHttpContextAccessor HttpContext = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            if (HttpContext != null && HttpContext.HttpContext != null && HttpContext.HttpContext.User != null && HttpContext.HttpContext.User.Identities != null)
            {
                var claims = HttpContext.HttpContext.User.Identities.First().Claims.ToList();
                if (!object.Equals(claims, null) && claims.Count > 0)
                {
                    var value = claims.Where(x => x.Type == "Id").FirstOrDefault();
                    if (value != null && value.Value != null)
                        int.TryParse(value.Value, out Id);
                }
            }

            return Id;
        }

        public string GetImageBase64(string path)
        {
            string image = string.Empty;
            try
            {
                if (File.Exists(path))
                {
                    string extension = Path.GetExtension(path).Trim().TrimStart('.');
                    image = $"data:image/{extension};base64,{Convert.ToBase64String(File.ReadAllBytes(path))}";
                }
            }
            catch (System.Exception)
            {

                throw;
            }
            return image;
        }
        public async Task<string> GetFileBase64(string path)
        {
            string file = string.Empty;
            try
            {
                if (File.Exists(path))
                {
                    file = Convert.ToBase64String(await File.ReadAllBytesAsync(path));
                }
            }
            catch (System.Exception)
            {

                throw;
            }
            return file;
        }

        public string CreateFileName(string fileName)
        {
            string extension = Path.GetExtension(fileName);
            fileName = Path.GetFileNameWithoutExtension(fileName);
            fileName = fileName.Count() >= 20 ? $"{fileName.Substring(0, 20)}{extension}" : $"{fileName}{extension}";
            string newFileName = $"{Guid.NewGuid().ToString("N").Substring(0, 6)}{fileName}";
            newFileName = this.ReplacePath(newFileName);
            return newFileName;
        }
        public string SaveFile(string path, string fileName, byte[] file)
        {
            if (!Directory.Exists(path))
            {
                path = this.ReplacePath(path);
                Directory.CreateDirectory(path);
            }
            string fulltPath = string.Empty;
            do
            {
                fileName = this.CreateFileName(fileName);
                fulltPath = Path.Combine(path, fileName);
            } while (File.Exists(fulltPath));

            File.WriteAllBytes(fulltPath, file);
            return fileName;
        }

        public string SaveFileAsync(string path, string fileName, byte[] file)
        {
            if (!Directory.Exists(path))
            {
                path = this.ReplacePath(path);
                Directory.CreateDirectory(path);
            }
            string fulltPath = string.Empty;
            do
            {
                fileName = this.CreateFileName(fileName);
                fulltPath = Path.Combine(path, fileName);
            } while (File.Exists(fulltPath));

            File.WriteAllBytes(fulltPath, file);
            return fileName;
        }
        public string IbanFormat(string iban)
        {
            string _iban = iban;
            iban = string.Empty;
            for (int i = 0; i < _iban.Count(); i++)
            {
                if ((i + 1) % 4 == 0)
                    iban += $"{_iban[i]} ";
                else
                    iban += _iban[i];
            }
            return iban;
        }

        public string SerializeObject(object value)
        {
            return JsonConvert.SerializeObject(value, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        public string Encrypt(string InputText, string SecurityKey)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();
            byte[] PlainText = Encoding.Unicode.GetBytes(InputText);
            byte[] Salt = Encoding.ASCII.GetBytes(SecurityKey.Length.ToString());

            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(SecurityKey, Salt);

            ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));

            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);

            cryptoStream.Write(PlainText, 0, PlainText.Length);

            cryptoStream.FlushFinalBlock();

            byte[] CipherBytes = memoryStream.ToArray();

            memoryStream.Close();
            cryptoStream.Close();

            string EncryptedData = Convert.ToBase64String(CipherBytes);

            return SecureEncryptCharacter(EncryptedData);
        }

        public string Decrypt(string InputText, string SecurityKey)
        {
            InputText = SecureDecryptCharacter(InputText);

            RijndaelManaged RijndaelCipher = new RijndaelManaged();
            byte[] EncryptedData = Convert.FromBase64String(InputText);
            byte[] Salt = Encoding.ASCII.GetBytes(SecurityKey.Length.ToString());

            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(SecurityKey, Salt);
            ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));

            MemoryStream memoryStream = new MemoryStream(EncryptedData);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);

            byte[] PlainText = new byte[EncryptedData.Length];

            int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);

            memoryStream.Close();
            cryptoStream.Close();

            string DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);
            return DecryptedData;
        }

        private string SecureDecryptCharacter(string input)
        {
            return input.Replace('_', '+').Replace('-', '/').Replace('$', '=');
        }
        private string SecureEncryptCharacter(string input)
        {
            return input.Replace('+', '_').Replace('/', '-').Replace('=', '$');
        }

        string ReplacePath(string path)
        {
            return path.Replace(">", "_").Replace("<", "_").Replace(":", "_").Replace(" ", "_")
                .Replace(@"\", "_").Replace("|", "_").Replace("/", "_").Replace("*", "_").Replace("?", "_");
        }
    }
}
