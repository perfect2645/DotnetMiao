using Base.Events;
using System;
using System.Collections.Generic;

namespace Base.viewModel
{
    public interface ISessionItem
    {
        string Key { get; set; }
        string Cookie { get; set; }
        string Referer { get; set; }
        Dictionary<string, object> SessionDic { get; set; }
        Action GetUserSessionAction { get; set; }

        LogEvents PrintLogEvent { get; set; }
    }
}
