using System;
using System.Collections.Generic;

namespace Base.viewModel
{
    public interface ISessionItem
    {
        string UserName { get; set; }
        string UserId { get; set; }
        string Cookie { get; set; }
        string Referer { get; set; }
        Dictionary<string, object> SessionDic { get; set; }
        Action GetUserSessionAction { get; set; }
    }
}
