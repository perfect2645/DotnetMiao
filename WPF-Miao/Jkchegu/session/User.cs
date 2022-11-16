using System.Text;

namespace Jkchegu.session
{
    public class User
    {
        public string? Session { get; set; }
        public string Etid { get; set; }
        public string Name { get; set; }
        public bool IsActive
        {
            get
            {
                return !string.IsNullOrEmpty(Session);
            }
        }

        public string this[string etid]
        {
            get
            {
                if (etid == Etid)
                {
                    return Name;
                }
                return null;
            }
        }

        public User(string etid, string name)
        {
            Etid = etid;
            Name = name;
        }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 用户信息 *********");

            sb.AppendLine($"Etid - {Etid}");
            sb.AppendLine($"Name - {Name}");
            sb.AppendLine($"Session - {Session}");

            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
