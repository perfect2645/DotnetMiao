using Utils;

namespace WPF_Miao.Platform.yunnan.viewModel
{
    internal class SessionItem : NotifyChanged, ISessionItem
    {
        private string _cookie;
        public string Cookie 
        { 
            get { return _cookie; }
            set
            {
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
                _referer = value;
                NotifyUI(() => Referer);
            }
        }

        public SessionItem()
        {
        }

        public SessionItem(string cookie, string referer)
        {
            Cookie = cookie;
            Referer = referer;
        }
    }
}
