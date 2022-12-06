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

        public async Task GetUserInfoAsync()
        {
            var tokenController = HttpServiceController.GetService<AppletTokenController>();
            var tokenContent = new AppletTokenContent("https://ymglfw.care4u.cn/npApii/auth/getAppletToken");
            tokenController.BuildClientHeaders(tokenContent);
            await tokenController.ProcessAsync(tokenContent);

            var userController = HttpServiceController.GetService<UserController>();
            var userContent = new UserContent("https://ymglfw.care4u.cn/npApii/vaccineDisPark/selectInfo");
            userController.BuildClientHeaders(tokenContent);
            await userController.ProcessAsync(userContent);

            BuildOrder();
        }

        private void BuildOrder()
        {
            var order = new Order()
            {
                daid = MainSession.PlatformSession.GetString(Constants.UserId),
                UserName = MainSession.PlatformSession.GetString(Constants.UserName),
                disparkId = MainSession.DisparkId,
                jgcommid = "10",
                jgid = "4",
                yyjg = MainSession.PlatformSession.GetString("yyjg").ToInt(),
                yysj = "2022-12-10",
                yysjd = "08:30-16:20",
                yyymid = MainSession.PlatformSession.GetString("yyymid"),
            };
        }
    }
}
