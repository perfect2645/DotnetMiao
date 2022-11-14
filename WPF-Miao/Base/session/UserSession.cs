using System.Collections.Generic;
using Utils;

namespace Base.session
{
    public class UserSession : IUserSession
    {
        public Dictionary<string, object> Users { get; }

        public UserSession()
        {
            Users = new Dictionary<string, object>();
        }

        public void AddUser(string key, object user)
        {
            Users.AddOrUpdate(key, user);
        }
    }
}
