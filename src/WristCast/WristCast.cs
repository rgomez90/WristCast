using Autofac;
using WristCast.Core.Data;
using WristCast.Core.IoC;
using WristCast.Core.Services;

namespace WristCast
{
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            ElmSharp.Utility.AppendGlobalFontPath(this.DirectoryInfo.Resource);
            IocContainer.Initialize(new WristCastModule());
            IocContainer.Instance.Resolve<ILog>().Info("App starting...");
            LoadApplication(new App());
        }

        static void Main(string[] args)
        {
            var app = new Program();
            global::Xamarin.Forms.Platform.Tizen.Forms.Init(app);
            global::Tizen.Wearable.CircularUI.Forms.Renderer.FormsCircularUI.Init();
            app.Run(args);
        }
    }

    internal class WristCastModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AsImplementedInterfaces().AsSelf();
            builder.Register((c, p) =>
                    new WristCastContext(App.GetDatabasePath()))
                .As<WristCastContext>()
                .SingleInstance();
        }
    }
}
