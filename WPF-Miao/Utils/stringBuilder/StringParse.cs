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
    }
}
