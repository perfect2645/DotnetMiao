using Base.session;
using Longchi.appointment;
using System.Collections.Generic;

namespace Longchi.session
{
    public class AppointSession : ControllerSession<YuyueController>
    {
        public AppointSession() : base()
        {
            var dateList = MainSession.TimeIdList;
            var userList = MainSession.Users;
            foreach(var user in userList)
            {
                AddController($"{user.Cookie}");
            }
        }
    }
}
