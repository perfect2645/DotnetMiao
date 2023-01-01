using Base.model;
using Base.session;
using Longchi.appointment;
using System.Collections.Generic;

namespace Longchi.session
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
                    AddController($"{user.UserName}|{date.Value}");
                    foreach(var time in timeList)
                    {
                        AddController($"{user.UserName}|{date.Value} {time.Value}");
                    }
                }
            }
        }
    }
}
