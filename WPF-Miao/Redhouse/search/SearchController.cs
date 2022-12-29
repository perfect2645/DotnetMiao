using HttpProcessor.Client;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using HttpProcessor.HtmlAnalysis;
using HttpProcessor.Response;
using Redhouse.session;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Utils;

namespace Redhouse.search
{
    internal class SearchController
    {
        public async Task GetUserInfo()
        {
            var userController = HttpServiceController.GetService<UserController>();
            await userController.GetUserAsync();
        }
    }
}
