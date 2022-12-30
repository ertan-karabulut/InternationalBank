using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dto.ConfigurationModel
{
    public class RebbitmqSetting
    {
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string MailQueueName { get; set; }
    }
}
