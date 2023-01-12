using Base.model;
using Base.session;
using System.Collections.Generic;
using Xihongmen.appointment;

namespace Xihongmen.session
{
    internal class AppointSession : ControllerSession<YuyueController>
    {
        public AppointSession() : base()
        {
            var dateList = MainSession.PlatformSession["DateList"] as List<DspVal>;
            var userList = MainSession.Users;
            foreach (var user in userList)
            {
                foreach (var date in dateList)
                {
                    AddController($"{user.UserName}|{date.Value}");
                }
            }

        }
    }
}
