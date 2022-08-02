using Base;
using Microsoft.Extensions.DependencyInjection;

namespace Dian
{
    internal class Container
    {
        public static ServiceCollection ServiceCollection
        {
            get
            {
                return _serviceCollection;
            }
        }

        private static readonly ServiceCollection _serviceCollection;

        public static ServiceProvider ServiceProvider
        {
            get
            {
                return _serviceProvider;
            }
        }

        private static ServiceProvider? _serviceProvider = null;


        static Container()
        {
            _serviceCollection = new ServiceCollection();
        }

        public static void BuildServiceProvider()
        {
            _serviceProvider = ServiceCollection.BuildServiceProvider();
        }
    }
}
