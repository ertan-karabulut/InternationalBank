using BusinessLayer.Dto.Account;
using BusinessLayer.Dto.ConfigurationModel;
using CoreLayer.BusinessLayer.Concreate.WorkFlow;
using CoreLayer.BusinessLayer.Model;
using CoreLayer.Utilities.Result.Concreate;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SendMail.Consumers
{
    public class MailConsumer : IConsumer<MailMessageModel>
    {
        SmtpClientModel _smtpClientModel;
        //IConfiguration _configuration;
        public MailConsumer()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory).AddJsonFile("appsettings.json", true, true).AddJsonFile($"appsettings.{environment}.json", true, true).Build();

            var hostBuilder = Host.CreateDefaultBuilder().ConfigureServices(service =>
            {
                service.Configure<SmtpClientModel>(configuration.GetSection("MailSettings"));
            }).Build();

            var smtpClientModel = hostBuilder.Services.GetRequiredService<IOptions<SmtpClientModel>>().Value;

            #region Smtp configuretion
            _smtpClientModel = new SmtpClientModel();
            _smtpClientModel.From = smtpClientModel.From;
            _smtpClientModel.Port = smtpClientModel.Port;
            _smtpClientModel.Host = smtpClientModel.Host;
            _smtpClientModel.UseDefaultCredentials = smtpClientModel.UseDefaultCredentials;
            _smtpClientModel.DeliveryMethod = smtpClientModel.DeliveryMethod;
            _smtpClientModel.NetworkCredentialUserName = smtpClientModel.NetworkCredentialUserName;
            _smtpClientModel.NetworkCredentialPassword = smtpClientModel.NetworkCredentialPassword;
            _smtpClientModel.EnableSsl = smtpClientModel.EnableSsl;
            #endregion
        }

        public async Task Consume(ConsumeContext<MailMessageModel> context)
        {
            StringBuilder logText = new StringBuilder();
            try
            {
                string data = JsonConvert.SerializeObject(context.Message, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                logText.AppendLine($" İşlem ekli parametrelerle başladı. context: {data}");
                MailWorkFlowBase mailWorkFlow = new MailWorkFlowBase();
                Result mailResult = new Result();
                await Task.Run(() =>
                {
                    mailResult = mailWorkFlow.SendMail(_smtpClientModel, context.Message);
                });
                logText.AppendLine(mailResult.ResultStatus ? " Mail gönderim işlemi başarılı." : " Mail gönderimiz başarısız.");
            }
            catch (Exception ex)
            {
                logText.AppendLine($" Mail gönderilirken bir hata oluştu. Hata :{ex.ToString()}");
            }
            this.AddLog(logText);
        }

        private void AddLog(StringBuilder logText)
        {
            DateTime date = DateTime.Now;
            string folderName = Path.Combine(Environment.CurrentDirectory, "Logs", $"{date.Day.ToString().PadLeft(2, '0')}.{date.Month.ToString().PadLeft(2, '0')}.{date.Year}");
            string fileName = $"{date.Hour.ToString().PadLeft(2, '0')} 00.txt";
            if (!Directory.Exists(folderName))
                Directory.CreateDirectory(folderName);
            if (!File.Exists(Path.Combine(folderName, fileName)))
                File.Create(Path.Combine(folderName, fileName)).Close();
            StreamWriter streamWriter = File.AppendText(Path.Combine(folderName, fileName));
            streamWriter.WriteLine(DateTime.Now);
            streamWriter.WriteLine(logText.ToString());
            streamWriter.WriteLine("---------------------------");
            streamWriter.WriteLine("");
            streamWriter.WriteLine("");
            streamWriter.Close();
        }
    }
}
