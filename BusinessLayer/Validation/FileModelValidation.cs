using BusinessLayer.Dto.ConfigurationModel;
using CoreLayer.BusinessLayer.Model;
using CoreLayer.Utilities.Enum;
using CoreLayer.Utilities.Ioc;
using CoreLayer.Utilities.Messages;
using FluentValidation;
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
    public class FileModelValidation : AbstractValidator<FileModel>
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
        private string fileSizeName { 
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
        public FileModelValidation()
        {
            this._appKey = ServiceTool.ServiceProvider.GetService<IOptions<AppKey>>().Value;

            RuleFor(x=>x.FileName).NotEmpty().WithMessage("{PropertyName} boş bırakılamaz.").Must(IsExtension).WithMessage("Dosya isminde dosya uzantısı bulunmalıdır.");

            RuleFor(x => x).Must(x=> Path.GetExtension(x.FileName).TrimStart('.') == x.Type).WithMessage("Belirtilen tipe uygun dosya girilmelidir.");

            RuleFor(x => x.Data).NotEmpty().WithMessage("{PropertyName} boş bırakılamaz.").
                Must(IsBase64).WithMessage("{PropertyName} base64 tipinde olmalıdır.")
                .Must(IsFileSize).WithMessage("{PropertyName} " + this.fileSize + " "+ this.fileSizeName.ToUpper(this._culture) + " boyutundan büyük olamaz.");

            RuleFor(x => x.Type).NotNull().WithMessage("{PropertyName} boş bırakılamaz.")
                .Must(Extension)
                .WithMessage($"Dosya uzantısı {string.Join(",", this.fileExtension)} uzantılarından farklı olamaz.");
        }
        bool IsExtension(string fileName)
        {
            if (!string.IsNullOrEmpty(Path.GetExtension(fileName)))
                return true;
            else
                return false;
        }
        bool IsBase64(string data)
        {
            try
            {
                Convert.FromBase64String(data);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        bool IsFileSize(string data)
        {
            try
            {
                byte[] file = Convert.FromBase64String(data);
                FileSizesEnum fileSizesEnum = this.GetFileSizeName();

                if ((ulong)file.Length > fileSize * (ulong)fileSizesEnum)
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
        bool Extension(string extension)
        {
            if (fileExtension.Where(x => x.ToUpper(this._culture) == extension.ToUpper(this._culture)).Any())
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
    }
}
