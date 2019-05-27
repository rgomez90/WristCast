using System;
using Autofac;
using WristCast.Core.IoC;
using WristCast.Core.ViewModels;

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
            builder.RegisterType<XamarinNavigationService>().As<INavigationService>();
        }
    }
}
