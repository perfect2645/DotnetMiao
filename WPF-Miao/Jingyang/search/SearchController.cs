using HttpProcessor.Container;
using Jingyang.session;
using System;
using System.Threading.Tasks;

namespace Jingyang.search
{
    internal class SearchController
    {
        private UserController userController;
        public SearchController()
        {
            //miaoController = HttpServiceController.GetService<GetMiaoController>();
            userController = HttpServiceController.GetService<UserController>();
        }
    }
}
