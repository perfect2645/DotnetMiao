using System.Collections.Generic;

namespace Base.session
{
    public interface IUserSession
    {
        Dictionary<string, object> Users { get;}
        void AddUser(string key, object user);

        public object this[string key]
        {
            get
            {
                if (Users.ContainsKey(key))
                {
                    return Users[key];
                }

                return null;
            }
        }
    }
}
