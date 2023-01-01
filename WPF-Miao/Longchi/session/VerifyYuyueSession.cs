using Base.model;
using Base.session;
using Longchi.appointment;
using System.Collections.Generic;

namespace Longchi.session
{
    internal class VerifyYuyueSession : ControllerSession<VerifyYuyueController>
    {
        public VerifyYuyueSession() : base()
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
                        AddController($"{user.UserName}|{date.Value} {time.Value}");
                    }
                }
            }
        }
    }
}
