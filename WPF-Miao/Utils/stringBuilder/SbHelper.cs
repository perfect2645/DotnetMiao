using System.Text;

namespace Utils.stringBuilder
{
    public static class SbHelper
    {
        public static void BuildKeyValue(this StringBuilder sb, string key, object value)
        {
            sb.Append(key).Append("=").Append(value).Append("; ");
        }

        public static void AppendField(this StringBuilder sb, string key, object value)
        {
            sb.Append(key).Append(":").Append(value).AppendLine();
        }
    }
}
