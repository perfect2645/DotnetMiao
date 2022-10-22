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
    }
}
