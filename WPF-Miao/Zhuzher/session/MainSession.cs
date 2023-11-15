using Base.Events;
using Base.session;
using System.Windows.Documents;
using Zhuzher.search;

namespace Zhuzher.session
{
    internal class MainSession : ISessionContainer
    {
        public static string Cookie { get; set; }
        public static string ActivityId { get; set; }
        public static string InviteCode { get; set; }
        public static LogEvents PrintLogEvent { get; set; }

        public static UserProjectList UserProjectList { get;}

        static MainSession()
        {
            UserProjectList = new UserProjectList();
        }
    }
}
