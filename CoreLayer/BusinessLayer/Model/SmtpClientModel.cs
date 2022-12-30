using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.BusinessLayer.Model
{
    public class SmtpClientModel
    {
        public string Host { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public bool EnableSsl { get; set; }
        public short DeliveryMethod { get; set; }
        public int Port { get; set; }
        public string NetworkCredentialUserName { get; set; }
        public string NetworkCredentialPassword { get; set;}
        public string From { get; set; }
    }
}
