using BusinessLayer.Dto.Adress;
using BusinessLayer.Dto.ConfigurationModel;
using BusinessLayer.Dto.Customer;
using CoreLayer.BusinessLayer.Concreate.Component;
using CoreLayer.BusinessLayer.Model;
using CoreLayer.DataAccess.Concrete.DataRequest;
using CoreLayer.Utilities.Enum;
using CoreLayer.Utilities.Messages;

using CoreLayer.Utilities.Result.Concreate;
using DataAccessLayer.Abstract;
using EntityLayer.Models.EntityFremework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concreate.Component
{
    public class CustomerComponent : ComponentBase
    {
        #region Variables
        protected readonly IUnitOfWork _unow;
        protected readonly AppKey _appKey;
        #endregion
        #region Constructor
        public CustomerComponent(IHostingEnvironment env, IUnitOfWork unow, IOptions<AppKey> options) : base(env)
        {
            this._unow = unow;
            this._appKey = options.Value;
        }
        #endregion
        #region Protected metod
        protected Result DeleteProfilePhoto(string fileName)
        {
            var result = new Result();
            string folder = FolderEnum.UserPhotos.ToString();
            string path = Path.Combine(this.Env.ContentRootPath, this._appKey.UploadFileName, folder, fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
                result.SetTrue();
            }
            return result;
        }
        protected Result<string> SaveProfilPhoto(FileModel file)
        {
            var result = new Result<string>();
            string path = Path.Combine(this.Env.ContentRootPath, this._appKey.UploadFileName, FolderEnum.UserPhotos.ToString());
            string fileName = base.helperWorkFlow.SaveFileAsync(path, file.FileName, Convert.FromBase64String(file.Data));
            if (!string.IsNullOrEmpty(fileName))
            {
                result.ResultObje = fileName;
                result.SetTrue();
            }
            return result;
        }
        protected async Task<Result<Customer>> GetUserInfo(string user, string phoneNumber)
        {
            var result = new Result<Customer>();
            var query = this._unow.CustomerRepository.Get(x => true);
            query = query.Include(x => x.EMails.Where(y=>y.IsActive && y.IsFavorite));
            query = query.Where(x => x.EMails.Any(y=>y.IsFavorite && y.IsActive) && x.IsActive && (x.IdentityNumber == user || x.CustomerNumber == user) && x.PhoneNumbers.Any(z=> z.PhoneNumber1 == phoneNumber));
            var data = await query.FirstOrDefaultAsync();
            if (data != null)
            {
                result.ResultObje = data;
                result.SetTrue();
            }
            return result;
        }
        #endregion
    }
}
