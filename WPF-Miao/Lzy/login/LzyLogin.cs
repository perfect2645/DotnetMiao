using System.Text;

namespace Lzy.login
{
    internal class LzyLogin
    {
        public string Cookie { get; set; }
        public string UserName { get; set; }
        public string Mobile { get; set; }
        public bool IsSource { get; set; }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 用户信息 *********");

            sb.AppendLine($"姓名 - {UserName}");
            sb.AppendLine($"Monile - {Mobile}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
