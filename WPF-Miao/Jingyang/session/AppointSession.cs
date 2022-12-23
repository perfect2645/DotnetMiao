using Base.session;
using Jingyang.appointment;
using System.Collections.Generic;

namespace Jingyang.session
{
    public class AppointSession : ControllerSession<YuyueController>
    {
        public AppointSession() : base()
        {
            var dateList = MainSession.TimeIdList;
            var userList = MainSession.Users;
            foreach(var user in userList)
            {
                foreach (var date in dateList)
                {
                    AddController($"{user.Fid}|{date}");
                }
            }
        }
    }
}
