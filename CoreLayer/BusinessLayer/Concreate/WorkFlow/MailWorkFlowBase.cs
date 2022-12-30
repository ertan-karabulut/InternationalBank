using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.Utilities.Result.Concreate;
using CoreLayer.BusinessLayer.Model;

namespace CoreLayer.BusinessLayer.Concreate.WorkFlow
{
    public class MailWorkFlowBase
    {
        public Result SendMail(SmtpClientModel smtpClientModel, MailMessageModel mailMessageModel)
        {
            var result = new Result();
            try
            {
                using (var smtpClient = new SmtpClient(smtpClientModel.Host))
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    smtpClient.UseDefaultCredentials = smtpClientModel.UseDefaultCredentials;
                    smtpClient.EnableSsl = smtpClientModel.EnableSsl;
                    smtpClient.DeliveryMethod = (SmtpDeliveryMethod)smtpClientModel.DeliveryMethod;
                    smtpClient.Port = smtpClientModel.Port;
                    smtpClient.Credentials = new NetworkCredential(smtpClientModel.NetworkCredentialUserName, smtpClientModel.NetworkCredentialPassword);

                    MailMessage message = new MailMessage();

                    message.From = new System.Net.Mail.MailAddress(smtpClientModel.From);
                    foreach (var item in mailMessageModel.To)
                        message.To.Add(item);
                    message.Body = mailMessageModel.Body;
                    message.Subject = mailMessageModel.Subject;
                    message.IsBodyHtml = mailMessageModel.IsBodyHtml;
                    foreach (var item in mailMessageModel.CC)
                        message.CC.Add(item);
                    foreach (var item in mailMessageModel.Bcc)
                        message.Bcc.Add(item);
                    foreach (var item in mailMessageModel.Attachment)
                        message.Attachments.Add(new Attachment(item));
                    foreach (var item in mailMessageModel.AttachmentStream)
                        message.Attachments.Add(new Attachment(item.Item1, item.Item2));
                    smtpClient.Send(message);
                    result.SetTrue();
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
