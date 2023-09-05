using Base.model;
using Base.session;
using Jian.appointment;
using System.Collections.Generic;

namespace Jian.session
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
