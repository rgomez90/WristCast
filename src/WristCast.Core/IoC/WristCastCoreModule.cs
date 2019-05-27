using Autofac;

namespace WristCast.Core.IoC
{
    public class WristCastCoreModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly).AsImplementedInterfaces();
        }
    }
}