namespace Utils.stringBuilder
{
    public class StringUtil
    {
        public static bool AnyEmpty(params string[] strs)
        {
            if (strs == null)
            {
                return true;
            }
            return strs.Any(str => !string.IsNullOrEmpty(str));
        }

        public static bool NotEmpty(params string[] strs)
        {
            if (strs == null)
            {
                return false;
            }
            return strs.All(str => !string.IsNullOrEmpty(str));
        }
    }
}
