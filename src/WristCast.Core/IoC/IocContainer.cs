using Autofac;

namespace WristCast.Core.IoC
{
    public class IocContainer
    {
        public static IContainer Instance;

        public static void Initialize(Module module = null)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<WristCastCoreModule>();
            if (module!=null)
            {
                builder.RegisterModule(module);
            }
            Instance = builder.Build();
        }
    }
}
