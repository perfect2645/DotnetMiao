using HttpProcessor.Container;
using System;
using System.Threading.Tasks;

namespace suiyang.search
{
    internal class SearchController
    {
        private GetMiaoController miaoController;

        public SearchController()
        {
            miaoController = HttpServiceController.GetService<GetMiaoController>();
        }

        public void SearchAsync()
        {
            miaoController.GetMiaoAsync();
        }
    }
}
