using Microsoft.Extensions.DependencyInjection;

namespace WPF_Miao.Platform.yunnan
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

        private Container()
        {
        }

        public static void BuildServiceProvider()
        {
            _serviceProvider = ServiceCollection.BuildServiceProvider();
        }
    }
}
