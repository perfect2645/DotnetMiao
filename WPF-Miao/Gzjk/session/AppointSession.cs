using Base.model;
using Base.session;
using Gzjk.appointment;
using System.Collections.Generic;

namespace Gzjk.session
{
    public class AppointSession : ControllerSession<YuyueController>
    {
        public AppointSession() : base()
        {
            var userList = MainSession.Users;
            foreach(var user in userList)
            {
                AddController($"{user.UserName}");
            }
        }
    }
}
