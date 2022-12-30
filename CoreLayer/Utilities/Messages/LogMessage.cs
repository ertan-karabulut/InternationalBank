using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities.Messages
{
    public class LogMessage
    {
        StringBuilder logText;
        public LogMessage()
        {
            this.logText = new StringBuilder();
        }
        public void InsertLog(string logText, string logMethod, string logPage)
        {
            this.logText.AppendLine($" LogMesaj : {logText} LogMethod : {logMethod} LogPage : {logPage}");
        }
        public string GetLogText()
        {
            return this.logText.ToString();
        }

        public void EmptyLog()
        {
            this.logText.Clear();
        }
    }
}
