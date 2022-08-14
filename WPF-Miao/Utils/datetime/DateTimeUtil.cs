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
    }
}
