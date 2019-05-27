using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Autofac.Core;

namespace WristCast.Core.IoC
{
    public class IocContainer
    {
        public static IContainer Instance;

        public static void Initialize(Module module)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(module)
                .RegisterModule<WristCastCoreModule>();
            Instance=builder.Build();
        }
    }
}
