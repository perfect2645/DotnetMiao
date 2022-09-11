using Base.viewModel;

namespace Base.session
{
    public interface ISession
    {
        string Key { get; set; }
        ISessionItem SessionItem { get; set; }

        object this[string key] { get; }
    }
}
