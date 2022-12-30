using BusinessLayer.Dto;
using BusinessLayer.Dto.ConfigurationModel;
using CoreLayer.Utilities.Enum;
using CoreLayer.Utilities.Ioc;
using CoreLayer.Utilities.Messages;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validation
{
    public class IFormFileValidation : AbstractValidator<IFormFile>
    {
        private readonly AppKey _appKey;
        private CultureInfo _culture = CultureInfo.GetCultureInfo("en-GB");
        private ulong fileSize
        {
            get
            {
                ulong value = 0;
                try
                {
                    
                    value = this._appKey.DefaultFileSize;
                }
                catch (Exception)
                {
                    return 0;
                }
                return value;
            }
        }
        private List<string> fileExtension
        {
            get
            {
                List<string> result = new List<string>();
                try
                {
                    result.AddRange(this._appKey.Extensions.Trim().Trim(',').ToUpper(this._culture).Split(','));
                }
                catch (Exception)
                {
                    return new List<string>();
                }
                return result;
            }
        }
        private string fileSizeName
        {
            get
            {
                try
                {
                    return this._appKey.FileSizeName;
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
        }

        bool IsFileSize(long length)
        {
            try
            {
                FileSizesEnum fileSizesEnum = this.GetFileSizeName();

                if ((ulong)length > fileSize * (ulong)fileSizesEnum)
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        bool Extension(string fileName)
        {
            string extension = Path.GetExtension(fileName).Trim('.').ToUpper(this._culture);
            if (fileExtension.Where(x => x.ToUpper(this._culture) == extension).Any())
                return true;
            return false;
        }
        FileSizesEnum GetFileSizeName()
        {
            string fileSizeName = this.fileSizeName.ToUpper(this._culture);
            if (fileSizeName == FileSizesEnum.Byte.ToString().ToUpper(this._culture))
                return FileSizesEnum.Byte;
            else if (fileSizeName == FileSizesEnum.Kb.ToString().ToUpper(this._culture))
                return FileSizesEnum.Kb;
            else if (fileSizeName == FileSizesEnum.Mb.ToString().ToUpper(this._culture))
                return FileSizesEnum.Mb;
            else if (fileSizeName == FileSizesEnum.Gb.ToString().ToUpper(this._culture))
                return FileSizesEnum.Gb;
            else if (fileSizeName == FileSizesEnum.Tb.ToString().ToUpper(this._culture))
                return FileSizesEnum.Tb;
            else
                return FileSizesEnum.Pb;
        }
        public IFormFileValidation()
        {
            this._appKey = ServiceTool.ServiceProvider.GetService<IOptions<AppKey>>().Value;

            RuleFor(x => x.FileName).NotEmpty().WithMessage("Dosya adı boş bırakılamaz.").Must(Extension)
    .WithMessage($"Dosya uzantısı {string.Join(",", this.fileExtension)} uzantılarından farklı olamaz.");

            RuleFor(x => x.Length).GreaterThan(0).WithMessage("Dosya boyutu sıfırdan büyük olmalıdır.").Must(IsFileSize).WithMessage($"Dosya boyutu {this.fileSize} {this.fileSizeName.ToUpper(this._culture)} boyutundan büyük olamaz.");
        }
    }
}
