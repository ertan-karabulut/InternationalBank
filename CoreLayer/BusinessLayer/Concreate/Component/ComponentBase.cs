using CoreLayer.Utilities.Enum;
using CoreLayer.Utilities.Helpers;
using CoreLayer.Utilities.Ioc;
using CoreLayer.Utilities.Messages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.BusinessLayer.Concreate.Component
{
    public class ComponentBase
    {
        #region Constructor
        protected ComponentBase()
        {
            LogMessage = ServiceTool.ServiceProvider.GetService<LogMessage>();
            helperWorkFlow = new HelperWorkFlow();
        }

        protected ComponentBase(IHostingEnvironment env) : this()
        {
            Env = env;
        }
        #endregion
        #region ProtectedMethod
        protected string GetUserPhoto(string fileName)
        {
            string result = string.Empty;
            StringBuilder logText = new StringBuilder();
            logText.AppendLine($"İşlem ekli parametrelerle başladı. fileName : {fileName}");
            try
            {
                string folder = FolderEnum.UserPhotos.ToString();
                string filePath = Path.Combine(Env.ContentRootPath, fileName);
                logText.AppendLine($"Kullanıcı profil resim yolu. {filePath}");
                result = helperWorkFlow.GetImageBase64(filePath);
                logText.AppendLine($"Dönen değer. result : {result}");
            }
            catch (Exception ex)
            {
                logText.AppendLine($"Hata : {ex.ToString()}");
            }
            LogMessage.InsertLog(logText.ToString(), "GetUserPhoto", "WorkFlowBase.cs");
            return result;
        }
        #endregion
        #region ProtectedField
        protected HelperWorkFlow helperWorkFlow;
        protected IHostingEnvironment Env;
        protected LogMessage LogMessage;
        #endregion
    }
}
