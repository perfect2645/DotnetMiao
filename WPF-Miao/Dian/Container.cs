using Base;
using Microsoft.Extensions.DependencyInjection;

namespace Dian
{
    internal class Container : ContainerBase
    {


        static Container()
        {
            _serviceCollection = new ServiceCollection();
        }

        private Container()
        {
        }

        public static void BuildServiceProvider()
        {
            _serviceProvider = ServiceCollection.BuildServiceProvider();
        }
    }
}
