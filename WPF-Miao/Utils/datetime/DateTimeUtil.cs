using Utils.stringBuilder;

namespace Utils.datetime
{
    public static class DateTimeUtil
    {
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            var tsStr = Convert.ToInt64(ts.TotalMilliseconds).ToString();
            return tsStr;
        }

        public static bool IsEqualOrGreaterDate(string str1, string str2, string spliter = "-")
        {
            var darr1 = str1?.Split(spliter);
            if (darr1 == null || darr1.Length != 3)
            {
                return false;
            }

            var darr2 = str2?.Split(spliter);
            if (darr1 == null || darr1.Length != 3)
            {
                return true;
            }
            var d1 = new DateTime(darr1[0].ToInt(), darr1[1].ToInt(), darr1[2].ToInt());
            var d2 = new DateTime(darr2[0].ToInt(), darr2[1].ToInt(), darr2[2].ToInt());

            return d1 >= d2;
        }

        public static bool IsEqualOrGreaterThanToday(string dateStr)
        {
            var today = DateTime.Today.ToString("yyyy-MM-dd");
            return IsEqualOrGreaterDate(dateStr, today);
        }
    }
}
