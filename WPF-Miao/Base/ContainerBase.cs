using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base
{
    public abstract class ContainerBase
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
    }
}
