using Base.session;
using Base.viewModel;

namespace Baohe.session
{
    internal class UserSession : ISession
    {
        public string Key { get; set; }
        public ISessionItem SessionItem { get; set; }
    }
}
