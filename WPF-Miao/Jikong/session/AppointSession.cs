using Base.model;
using Base.session;
using Jikong.appointment;
using System.Collections.Generic;

namespace Jikong.session
{
    public class AppointSession : ControllerSession<YuyueController>
    {
        public AppointSession() : base()
        {
            var dateList = MainSession.PlatformSession["DateList"] as List<DspVal>;
            var timeList = MainSession.PlatformSession["TimeList"] as List<DspVal>;
            var userList = MainSession.Users;
            foreach(var user in userList)
            {
                foreach(var date in dateList)
                {
                    foreach(var time in timeList)
                    {
                        AddController($"{user.UserName}|{date.Value}{time.Value}");
                    }
                }
            }
        }
    }
}
