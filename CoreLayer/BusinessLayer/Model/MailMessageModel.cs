using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.BusinessLayer.Model
{
    public class MailMessageModel
    {
        public MailMessageModel()
        {
            To = new List<string>();
            CC= new List<string>();
            Bcc= new List<string>();
            Attachment = new List<string>();
            AttachmentStream = new List<Tuple<Stream, string>>();
        }
        public List<string> To { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public bool IsBodyHtml { get; set; }
        public List<string> CC { get; set; }
        public List<string> Bcc { get; set; }
        public List<string> Attachment { get; set; }
        public List<Tuple<Stream, string>> AttachmentStream { get; set; }
    }
}
