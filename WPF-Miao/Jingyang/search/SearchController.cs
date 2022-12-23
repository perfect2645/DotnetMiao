using HttpProcessor.Container;
using Jingyang.session;
using System;
using System.Threading.Tasks;

namespace Jingyang.search
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

        public async Task GetUserAsync()
        {
            await userController.GetUserAsync();
        }

        public void GetMiaoAsync()
        {
            var miaoControList = MainSession.SearchSession.ControllerCache;
            foreach(var pair in miaoControList)
            {
                var controller = pair.Value;
                controller.BuildContent(pair.Key);
                controller.BuildClientHeaders(controller.Content);
                Task.Factory.StartNew(() => GetMiao(controller));
            }
        }

        private void GetMiao(GetMiaoController controller)
        {
            controller.GetMiaoAsync();
        }
    }
}
