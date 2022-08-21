using Base.Events;
using System;
using System.Collections.Generic;
using Utils;

namespace Base.viewModel
{
    public class SessionItem : NotifyChanged, ISessionItem
    {
        #region Properties
        public string Key { get; set; }
        private string _cookie;
        public string Cookie
        {
            get { return _cookie; }
            set
            {
                if (_cookie == value)
                {
                    return;
                }
                _cookie = value;
                NotifyUI(() => Cookie);
            }
        }

        private string _referer;

        public string Referer
        {
            get { return _referer; }
            set
            {
                if (_referer == value)
                {
                    return;
                }
                _referer = value;
                NotifyUI(() => Referer);
            }
        }
        public Dictionary<string, object> SessionDic { get; set; }

        public LogEvents PrintLogEvent { get; set; } = new LogEvents();

        #endregion Properties

        public SessionItem()
        {
            SessionDic = new Dictionary<string, object>();
        }

        public SessionItem(string key)
        {
            Key = key;
            SessionDic = new Dictionary<string, object>();
        }

        public void PrintLog(Dictionary<string, object> body)
        {

        }
    }
}
