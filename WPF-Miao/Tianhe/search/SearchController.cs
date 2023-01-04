using Tianhe.login;
using HttpProcessor.Container;
using System;
using System.Threading.Tasks;
using Tianhe.session;

namespace Tianhe.search
{
    internal class SearchController
    {
        public SearchController()
        {

        }

        public async Task GetUsersAsync()
        {
            var users = MainSession.Users;
            ; foreach (var user in users)
            {
                var userController = HttpServiceController.GetService<UserController>();
                await userController.GetUserAsync(user);
            }
        }

        public void GetDateAsync()
        {

        }
    }
}
