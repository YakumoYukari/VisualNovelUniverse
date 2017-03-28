using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace DependencyInjection
{
    public static class IoC
    {
        private static readonly StandardKernel Kernel;

        static IoC()
        {
            Kernel = new StandardKernel();
        }


    }
}
