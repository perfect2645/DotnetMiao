using System.Text;

namespace Utils.stringBuilder
{
    public static class SbHelper
    {
        public static void BuildKeyValue(this StringBuilder sb, string key, string value)
        {
            sb.Append(key).Append("=").Append(value).Append("; ");
        }
    }
}
