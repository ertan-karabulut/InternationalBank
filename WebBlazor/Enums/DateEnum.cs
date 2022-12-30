namespace WebBlazor.Enums
{

    public static class DateEnum
    {
        public enum MonthEnum : ushort
        {
            Ocak = 1,
            Şubat = 2,
            Mart = 3,
            Nisan = 4,
            Mayıs = 5,
            Haziran = 6,
            Temmuz = 7,
            Ağustos = 8,
            Eylül = 9,
            Ekim = 10,
            Kasım = 11,
            Aralık = 12
        }

        public static string GetMonthName(ushort month)
        {
            switch (month)
            {
                case (ushort)MonthEnum.Ocak:
                    return MonthEnum.Ocak.ToString();
                case (ushort)MonthEnum.Şubat:
                    return MonthEnum.Şubat.ToString();
                case (ushort)MonthEnum.Mart:
                    return MonthEnum.Mart.ToString();
                case (ushort)MonthEnum.Nisan:
                    return MonthEnum.Nisan.ToString();
                case (ushort)MonthEnum.Mayıs:
                    return MonthEnum.Mayıs.ToString();
                case (ushort)MonthEnum.Haziran:
                    return MonthEnum.Haziran.ToString();
                case (ushort)MonthEnum.Temmuz:
                    return MonthEnum.Temmuz.ToString();
                case (ushort)MonthEnum.Ağustos:
                    return MonthEnum.Ağustos.ToString();
                case (ushort)MonthEnum.Eylül:
                    return MonthEnum.Eylül.ToString();
                case (ushort)MonthEnum.Ekim:
                    return MonthEnum.Ekim.ToString();
                case (ushort)MonthEnum.Kasım:
                    return MonthEnum.Kasım.ToString();
                case (ushort)MonthEnum.Aralık:
                    return MonthEnum.Aralık.ToString();
                default:
                    return "";
            }
        }
    }
}
