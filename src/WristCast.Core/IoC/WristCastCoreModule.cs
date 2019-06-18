using Autofac;
using Autofac.Core;
using WristCast.Core.Services;

namespace WristCast.Core.IoC
{
    public class WristCastCoreModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Except<SearchService>()
                .Except<FileService>()
                .Except<DownloadManager>().AsImplementedInterfaces();
            builder.RegisterType<DownloadManager>().AsSelf().SingleInstance();
            builder.RegisterType<FileService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<SearchService>().As<ISearchService>().WithParameter(
                new ResolvedParameter((info, context) => info.ParameterType == typeof(string),
                    ((info, context) => "dad39050909646f9a6976c88fec19a01")));//Secrets.ApiKey)));
        }
    }
}