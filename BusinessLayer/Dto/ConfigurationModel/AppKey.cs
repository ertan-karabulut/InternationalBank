using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dto.ConfigurationModel
{
    public class AppKey
    {
        public string TokenKey { get; set; }
        public string Extensions { get; set; }
        public ulong DefaultFileSize { get; set; }
        public string FileSizeName { get; set; }
        public string RefreshTokenExTime { get; set; }
        public string UploadFileName { get; set; }
        public string SecurityKey { get; set; }
        public string WebAdress { get; set; }
    }
}
