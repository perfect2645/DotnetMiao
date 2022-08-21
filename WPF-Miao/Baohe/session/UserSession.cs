using Base.session;
using Base.viewModel;

namespace Baohe.session
{
    public class UserSession : ISession
    {
        public string Key { get; set; }
        public ISessionItem SessionItem { get; set; }
    }
}
