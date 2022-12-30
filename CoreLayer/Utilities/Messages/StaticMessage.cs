using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities.Messages
{
    public static class StaticMessage
    {
        #region Mesajlar
        public static string DefaultSuccessMessage = "İşleminiz başarılı bir şekilde tamamlanmıştır.";
        public static string DefaultErrorMessage = "Üzgünüz, beklenmedik bir hata oluştu.";
        public static string DefaultValidationMessage = "Gönderilen bilgiler doğrulanamadı.";
        public static string AccountBalanceError = "Hesabınızda bakiye bulunduğu için işleminizi gerçekleştiremiyoruz. Lütfen hesabınızdaki tutarı başka bir hesaba aktararak, kapama işleminizi tekrar deneyin.";
        #region Mail mesajları
        public static string ConfirmPasswordSubject = "Şifre yenileme";
        #endregion
        #endregion

        #region Başarı kodları
        public static short OK = 200;
        public static short Created = 201;
        #endregion

        #region Hata kodları
        public static short DefaultValidationCode = 400;
        public static short DefaultUIMessageCode = 401;
        public static short DefaultErrorCode = 500;
        #endregion
    }
}
