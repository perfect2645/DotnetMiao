using Base.session;
using Lujiazhen.appointment;

namespace Lujiazhen.session
{
    public class AppointSession : ControllerSession<YuyueController>
    {
        public AppointSession() : base()
        {
            var userList = MainSession.Users;
            foreach(var user in userList)
            {
                AddController($"{user.Name}");
            }
        }
    }
}
