using Base.session;
using Tjzs.appointment;

namespace Tjzs.session
{
    public class AppointSession : ControllerSession<YuyueController>
    {
        public AppointSession() : base()
        {
            var userList = MainSession.Users;
            foreach(var user in userList)
            {
                AddController($"{user.Authorization}");
            }
        }
    }
}
