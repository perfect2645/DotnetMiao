using HttpProcessor.Container;

namespace Xihongmen.search
{
    internal class SearchController
    {

        private readonly UserController userController;

        public SearchController()
        {
            userController = HttpServiceController.GetService<UserController>();
        }

        public void SearchAsync()
        {
            userController.GetUserAsync();
        }
    }
}
