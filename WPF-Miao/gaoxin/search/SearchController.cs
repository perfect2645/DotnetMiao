using gaoxin.appointment;
using gaoxin.session;
using HttpProcessor.Container;
using System;
using System.Threading.Tasks;
using Utils;
using Utils.timerUtil;

namespace gaoxin.search
{
    internal class SearchController
    {
        public SearchController()
        {

        }

        public async Task GetUserInfoAsync(GaoxinLogin loginInfo)
        {
            //var tokenController = HttpServiceController.GetService<AppletTokenController>();
            //var tokenContent = new AppletTokenContent("https://ymglfw.care4u.cn/npApii/auth/getAppletToken");
            //tokenController.BuildClientHeaders(tokenContent);
            //await tokenController.ProcessAsync(tokenContent);

            var userController = HttpServiceController.GetService<UserController>();
            var userContent = new UserContent(loginInfo);
            userController.BuildClientHeaders(userContent);
            await userController.ProcessAsyncTask(userContent);
        }
    }
}
