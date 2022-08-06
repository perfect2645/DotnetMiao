using System;
using System.Collections.Generic;
using Utils;

namespace Base.viewModel
{
    public class SessionItem : NotifyChanged, ISessionItem
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
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
        public Action GetUserSessionAction { get; set; }
    }
}
