using HttpProcessor.Container;
using System;
using System.Threading.Tasks;

namespace suiyang.search
{
    internal class SearchController
    {
        private GetMiaoController miaoController;
        private UserController userController;

        public SearchController()
        {
            //miaoController = HttpServiceController.GetService<GetMiaoController>();
            userController = HttpServiceController.GetService<UserController>();
        }

        public async Task SearchAsync()
        {
            await userController.GetUserAsync();
        }
    }
}
