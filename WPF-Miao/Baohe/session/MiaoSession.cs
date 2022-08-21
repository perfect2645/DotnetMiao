using Base.session;
using Base.viewModel;

namespace Baohe.session
{
    public class MiaoSession : ISession
    {
        public string Key { get; set; }
        public ISessionItem SessionItem { get; set; }
    }
}
