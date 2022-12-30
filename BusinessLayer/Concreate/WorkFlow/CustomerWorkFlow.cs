using BusinessLayer.Abstract;

using CoreLayer.Utilities.Result.Concreate;
using DataAccessLayer.Abstract;
using Microsoft.AspNetCore.Hosting;
using System.Text;
using BusinessLayer.Concreate.Component;
using BusinessLayer.Dto;
using AutoMapper;
using CoreLayer.Utilities.Aspect;
using BusinessLayer.Validation;
using Microsoft.EntityFrameworkCore;
using CoreLayer.BusinessLayer.Model;
using CoreLayer.Utilities.Messages;
using BusinessLayer.Dto.ConfigurationModel;
using Microsoft.Extensions.Options;
using CoreLayer.Utilities.Enum;
using System.Net.Mail;
using StackExchange.Redis;
using MassTransit;
using BusinessLayer.Dto.Customer;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using EntityLayer.Models.EntityFremework;
using CoreLayer.Security;

namespace BusinessLayer.Concreate.WorkFlow
{
    public class CustomerWorkFlow : CustomerComponent, ICustomerWorkFlow
    {
        #region Variables
        protected readonly AppKey _appKey;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly RebbitmqSetting _rebbitmqSetting;
        #endregion
        #region Constructor
        public CustomerWorkFlow(IHostingEnvironment env, IUnitOfWork unow, IOptions<AppKey> options, ISendEndpointProvider sendEndpointProvider, IOptions<RebbitmqSetting> rebbitmqSetting) : base(env, unow, options)
        {
            this._appKey = options.Value;
            this._sendEndpointProvider = sendEndpointProvider;
            this._rebbitmqSetting = rebbitmqSetting.Value;
        }
        #endregion
        #region Async metod
        [ValidatorAspect(typeof(FileModelValidation))]
        public async Task<Result> UpdateProfilePhotoAsync(FileModel file)
        {
            string newFileName = string.Empty;
            try
            {
                StringBuilder logText = new StringBuilder();
                var result = new Result();
                var nameResult = base.SaveProfilPhoto(file);
                if (nameResult.ResultStatus)
                {
                    logText.AppendLine($"Profil resmi kaydedildi. Dosya adı : {nameResult.ResultObje}");
                    int userId = base.helperWorkFlow.GetUserId();
                    var customer = await base._unow.CustomerRepository.Get(x => x.Id == userId, QueryTrackingBehavior.TrackAll).FirstOrDefaultAsync();
                    if (customer != null)
                    {
                        logText.AppendLine("Kullanıcı bilgileri bulundu.");
                        string fileName = customer.Photo;
                        newFileName = nameResult.ResultObje;
                        customer.Photo = nameResult.ResultObje;
                        await base._unow.BeginTransactionAsync();
                        if (base._unow.CustomerRepository.Update(customer).ResultStatus && await base._unow.SaveChangesAsync())
                        {
                            if (base.DeleteProfilePhoto(fileName).ResultStatus)
                                logText.AppendLine("Eski profil resmi silindi.");
                            logText.AppendLine("Kullanıcı güncellendi. İşlem Başarılı.");
                            result.SetTrue();
                            await base._unow.CommitAsync();
                        }
                        else
                        {
                            await base._unow.RollbackAsync();
                            if (base.DeleteProfilePhoto(nameResult.ResultObje).ResultStatus)
                                logText.AppendLine("Gönderilen dosya silindi.");
                            logText.AppendLine("Kullanıcı bilgileri güncellenemedi. İşlemler geri alındı.");
                        }
                    }
                }

                base.LogMessage.InsertLog(logText.ToString(), "UpdateProfilePhoto", "CustomerWorkFlow.cs");
                return result;
            }
            catch (Exception ex)
            {
                base.DeleteProfilePhoto(newFileName);
                await base._unow.RollbackAsync();
                throw ex;
            }
        }
        [ValidatorAspect(typeof(PasswordConfirmValidation))]
        public async Task<Result> PasswordConfirm(PasswordConfirmDto model)
        {
            var result = new Result();
            StringBuilder logText = new StringBuilder();
            logText.AppendLine(" Kullanıcı bilgileri kontrol ediliyor.");
            var emailResult = await base.GetUserInfo(model.User, model.PhoneNumber);
            if (emailResult.ResultStatus)
            {
                logText.AppendLine($" Kullanıcı bilgileri başarıyla alındı. ResultObje: {base.helperWorkFlow.SerializeObject(emailResult.ResultObje)}");
                var send = await this._sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{_rebbitmqSetting.MailQueueName}"));
                var mailMessage = new MailMessageModel();
                mailMessage.Subject = StaticMessage.ConfirmPasswordSubject;
                mailMessage.IsBodyHtml = true;
                string contentPath = Path.Combine(this.Env.ContentRootPath, this._appKey.UploadFileName, FolderEnum.Mailing.ToString(), "ConfirmPassword.html");
                string body = File.ReadAllText(contentPath);
                string link = $"{this._appKey.WebAdress}/PasswordChange/{base.helperWorkFlow.Encrypt(emailResult.ResultObje.Id.ToString(), this._appKey.SecurityKey)}/{base.helperWorkFlow.Encrypt(emailResult.ResultObje.CustomerNumber, this._appKey.SecurityKey)}";
                mailMessage.Body = body.Replace("{AdSoyad}", $"{emailResult.ResultObje.Name} {emailResult.ResultObje.Surname}").Replace("{link}", link);
                mailMessage.To.Add(emailResult.ResultObje.EMails.FirstOrDefault().EMail1);
                await send.Send<MailMessageModel>(mailMessage);
                logText.AppendLine(" Şifre sıfırlama maili kullanıcıya gönderildi.");
                result.SetTrue();
            }
            else
            {
                result.SetFalse("Kullanıcı bulunamadı.");
                logText.AppendLine(" Kullanıcı bulunamadı.");
            }
            base.LogMessage.InsertLog(logText.ToString(), "PasswordConfirm", "CustomerWorkFlow.cs");
            return result;
        }
        #endregion
        public Result<ClaimDto> GetClaim()
        {
            StringBuilder logText = new StringBuilder();
            Result<ClaimDto> result = new Result<ClaimDto>();
            result.ResultObje = new ClaimDto();
            logText.AppendLine("Claims okuma işlemi başladı.");
            base.helperWorkFlow.GetTokenClaims(result.ResultObje);
            if (!object.Equals(result.ResultObje, null))
            {
                string fileName = Path.Combine(this._appKey.UploadFileName, FolderEnum.UserPhotos.ToString(), result.ResultObje.Photo);
                string filePath = base.GetUserPhoto(fileName);
                if (!string.IsNullOrEmpty(filePath))
                {
                    result.ResultObje.Photo = filePath;
                    result.SetTrue();
                }
            }
            logText.AppendLine("İşlem sonucu " + (result.ResultStatus ? "başarılı." : "başarısız."));
            base.LogMessage.InsertLog(logText.ToString(), "GetClaim", "CustomerWorkFlow.cs");
            return result;
        }
        [ValidatorAspect(typeof(FileModelValidation))]
        public Result UpdateProfilePhoto(FileModel file)
        {
            string newFileName = string.Empty;
            try
            {
                StringBuilder logText = new StringBuilder();
                var result = new Result();
                var nameResult = base.SaveProfilPhoto(file);
                if (nameResult.ResultStatus)
                {
                    logText.AppendLine($"Profil resmi kaydedildi. Dosya adı : {nameResult.ResultObje}");
                    int userId = base.helperWorkFlow.GetUserId();
                    var customer = base._unow.CustomerRepository.Get(x => x.Id == userId, QueryTrackingBehavior.TrackAll).FirstOrDefault();
                    if (customer != null)
                    {
                        logText.AppendLine("Kullanıcı bilgileri bulundu.");
                        string fileName = customer.Photo;
                        newFileName = nameResult.ResultObje;
                        customer.Photo = nameResult.ResultObje;
                        base._unow.BeginTransaction();
                        if (base._unow.CustomerRepository.Update(customer).ResultStatus && base._unow.SaveChanges())
                        {
                            if (base.DeleteProfilePhoto(fileName).ResultStatus)
                                logText.AppendLine("Eski profil resmi silindi.");
                            logText.AppendLine("Kullanıcı güncellendi. İşlem Başarılı.");
                            result.SetTrue();
                            base._unow.Commit();
                        }
                        else
                        {
                            base._unow.Rollback();
                            if (base.DeleteProfilePhoto(nameResult.ResultObje).ResultStatus)
                                logText.AppendLine("Gönderilen dosya silindi.");
                            logText.AppendLine("Kullanıcı bilgileri güncellenemedi. İşlemler geri alındı.");
                        }
                    }
                }

                base.LogMessage.InsertLog(logText.ToString(), "UpdateProfilePhoto", "CustomerWorkFlow.cs");
                return result;
            }
            catch (Exception ex)
            {
                base.DeleteProfilePhoto(newFileName);
                base._unow.Rollback();
                throw ex;
            }
        }
        [ValidatorAspect(typeof(PasswordChangeValidation))]
        public Result PasswordChange(PasswordChangeDto passwordChange)
        {
            var result = new Result();
            string decryptId = base.helperWorkFlow.Decrypt(passwordChange.CodeId, this._appKey.SecurityKey);
            int Id = 0;
            int.TryParse(decryptId, out Id);
            string customerNumber = base.helperWorkFlow.Decrypt(passwordChange.CodeCustomerNumber, this._appKey.SecurityKey);

            var data = base._unow.InternetPasswordRespository.Get(x => x.CustomerId == Id && x.Customer.CustomerNumber == customerNumber).FirstOrDefault();
            if (data != null)
            {
                data.IsActive = false;
                var update = base._unow.InternetPasswordRespository.Update(data);
                var passwordEntity = new InternetPassword();
                passwordEntity.Password = Hashing.CreateSHA512(passwordChange.Password);
                passwordEntity.IsActive = true;
                passwordEntity.CustomerId = Id;
                passwordEntity.CreateDate= DateTime.Now;
                var save = base._unow.InternetPasswordRespository.Add(passwordEntity);
                if (update.ResultStatus && save.ResultStatus && base._unow.SaveChanges())
                    result.SetTrue();
            }
            return result;
        }
    }
}
