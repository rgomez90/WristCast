using Autofac;
using WristCast.Core.Data;
using WristCast.Core.Services;
using WristCast.Services;

namespace WristCast
{
    internal class WristCastModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Except<Log>()
                .Except<SecretsService>()
                .AsImplementedInterfaces().AsSelf();

            builder.RegisterType<Log>().As<ILog>().SingleInstance();
            builder.RegisterType<SecretsService>().As<ISecretsService>().SingleInstance();

            builder.Register((c, p) =>
                    new WristCastContext(App.GetDatabasePath()))
                .As<WristCastContext>()
                .SingleInstance();
        }
    }
}