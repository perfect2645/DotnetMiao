namespace Utils.stringBuilder
{
    public static class StringParse
    {
        public static long ToLong(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            return Convert.ToInt64(str);
        }

        public static int ToInt(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            return Convert.ToInt32(str);
        }

        public static string NotNullString(this object? source)
        {
            var strSource = source?.ToString();
            return strSource ?? string.Empty;
        }
    }
}
