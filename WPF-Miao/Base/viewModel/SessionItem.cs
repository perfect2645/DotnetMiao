using System;
using System.Collections.Generic;

namespace Base.viewModel
{
    public class SessionItem : ISessionItem
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string Cookie { get; set; }
        public string Referer { get; set; }
        public Dictionary<string, object> SessionDic { get; set; }
        public Action GetUserSessionAction { get; set; }
    }
}
