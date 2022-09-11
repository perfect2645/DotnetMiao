using Base.session;
using Base.viewModel;

namespace Baohe.session
{
    public class UserSession : ISession
    {
        public string Key { get; set; }
        public ISessionItem SessionItem { get; set; }

        public UserSession(string key)
        {
            Key = key;
            SessionItem = new SessionItem(key);
        }

        public object this[string key]
        {
            get
            {
                if (SessionItem.SessionDic.ContainsKey(key))
                {
                    return SessionItem.SessionDic[key];
                }

                return null;
            }
        }
    }
}
