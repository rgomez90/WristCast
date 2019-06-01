using System;
using System.IO;
using System.Linq;
using Autofac;
using Autofac.Core;
using Tizen.Network.Connection;
using Tizen.System;
using WristCast.Core;
using WristCast.Core.IoC;
using WristCast.Core.ViewModels;
using WristCast.Views;
using Xamarin.Forms;

namespace WristCast
{
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();

            LoadApplication(new App());
        }

        static void Main(string[] args)
        {
            var app = new Program();
            global::Xamarin.Forms.Platform.Tizen.Forms.Init(app);
            global::Tizen.Wearable.CircularUI.Forms.Renderer.FormsCircularUI.Init();
            IocContainer.Initialize(new WristCastWearableModule());
            app.Run(args);
        }

    }

    internal class WristCastWearableModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly).AsImplementedInterfaces();
            builder.RegisterType<XamarinNavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<SecretsService>().As<ISecretsService>().SingleInstance();
            builder.RegisterType<Log>().As<ILog>().SingleInstance();
            builder.RegisterType<TizenStorageProvider>().As<IStorageProvider>().SingleInstance();
        }
    }
}
