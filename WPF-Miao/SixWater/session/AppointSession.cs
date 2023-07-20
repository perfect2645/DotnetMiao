using Base.session;
using SixWater.appointment;

namespace SixWater.session
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
