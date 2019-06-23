using System;
using System.IO;
using Autofac;
using SQLitePCL;
using Tizen.Security;
using Tizen.Wearable.CircularUI.Forms;
using WristCast.Core;
using WristCast.Core.Data;
using WristCast.Core.IoC;
using WristCast.Core.Services;
using WristCast.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WristCast
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        private static ILog log;

        public App()
        {
            InitializeComponent();
            AppDomain.CurrentDomain.UnhandledException += OnException;
            MainPage = new NavigationPage((CirclePage)IocContainer.Instance.Resolve<IView<HomeViewModel>>());
        }

        private void OnException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            if (ex == null) return;
            log.Error((ex).Message,ex);
        }

        public static string GetDatabasePath()
        {
            var dbPath = IocContainer.Instance.Resolve<IStorageProvider>().DatabasePath;
            log.Info("DbPath: " + dbPath);
            return dbPath;
        }

        protected override async void OnStart()
        {
            log = IocContainer.Instance.Resolve<ILog>();
            raw.SetProvider(new SQLite3Provider_sqlite3());
            if (!File.Exists(GetDatabasePath()))
            {
                log.Info("Creating database");
                await IocContainer.Instance.Resolve<WristCastContext>().Create();
            }
            else
            {
                log.Info("Database file found...no need to create");
            }
            PrivacyPrivilegeManager.RequestPermission("http://tizen.org/privilege/mediastorage");
            PrivacyPrivilegeManager.RequestPermission("http://tizen.org/privilege/externalstorage");

            //var mPermission =PrivacyPrivilegeManager.CheckPermission("http://tizen.org/privilege/mediastorage");
            //var ePermission =PrivacyPrivilegeManager.CheckPermission("http://tizen.org/privilege/externalstorage");
            //if (!(mPermission==CheckResult.Allow && ePermission==CheckResult.Allow))
            //{
            //    log.Error("Permission not granted");
            //}
            log.Info("Permissions granted");
            await PodcastManager.Init();
            log.Info("Podcast manager initialized...");
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

