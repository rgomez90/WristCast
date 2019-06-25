using Autofac;
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
}
