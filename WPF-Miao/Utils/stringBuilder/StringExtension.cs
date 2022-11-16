namespace Utils
{
    public static class StringExtension
    {
        public static bool ContainsAny(this string source, params string[] targets)
        {
            if (string.IsNullOrEmpty(source))
            {
                return false;
            }

            foreach (var target in targets)
            {
                if (string.IsNullOrEmpty(target))
                {
                    continue;
                }
                if (source.Contains(target))
                {
                    return true;
                }
            }

            return false;
        }

        public static List<string> SplitToList(this string str, string spliter)
        {
            if (str == null)
            {
                return null;
            }

            var strList = new List<string>();
            if (string.IsNullOrEmpty(spliter))
            {
                strList.Add(str);
                return strList;
            }

            return str.Split(spliter).ToList();
        }
    }
}
