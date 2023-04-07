using System.Net.Mime;
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

        public static string GetStringContent(Dictionary<string, object> source)
        {
            if (source  == null)
            {
                return string.Empty;
            }
            var sb = new StringBuilder();
            foreach (var item in source)
            {
                sb.Append($"{item.Key}={item.Value}&");
            }
            return  sb.ToString().TrimEnd('&');
        }
    }
}
