using Base.model;
using Base.session;
using Lzy.appointment;
using System.Collections.Generic;

namespace Lzy.session
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
