using HttpProcessor.Container;
using Longchi.appointment;
using Longchi.session;
using System;
using System.Threading.Tasks;

namespace Longchi.search
{
    internal class SearchController
    {
        public SearchController()
        {

        }

        public async Task GetUsersAsync()
        {
            var users = MainSession.Users;
;           foreach(var user in users)
            {
                var userController = HttpServiceController.GetService<UserController>();
                await userController.GetUserAsync(user);
            }
        }
    }
}
