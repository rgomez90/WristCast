using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Autofac;
using SQLitePCL;
using Tizen.Wearable.CircularUI.Forms;
using WristCast.Core;
using WristCast.Core.Data;
using WristCast.Core.IoC;
using WristCast.Core.Services;
using WristCast.Services;
using WristCast.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Application = Xamarin.Forms.Application;

namespace WristCast
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        private Dictionary<string, bool> _permissions;
        private static ILog _log;

        public App()
        {
            InitializeComponent();
            AppDomain.CurrentDomain.UnhandledException += OnException;
            MainPage = new NavigationPage((CirclePage)IocContainer.Instance.Resolve<IView<HomeViewModel>>());
        }
        public static string GetDatabasePath()
        {
            var databasePath = StorageProvider.Current.DatabasePath;
            _log.Info("DbPath: " + databasePath);
            return databasePath;
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override async void OnStart()
        {
            _log = IocContainer.Instance.Resolve<ILog>();
            raw.SetProvider(new SQLite3Provider_sqlite3());
            if (!File.Exists(GetDatabasePath()))
            {
                _log.Info("Creating database");
                await IocContainer.Instance.Resolve<WristCastContext>().Create();
            }
            else
            {
                _log.Info("Database file found...no need to create");
            }

            await RequestPermissions();
            if (!_permissions.All(x => x.Value))
            {
                ExitApp();
                return;
            }
            _log.Info("Permissions granted");
            CreateFolderStructure();
            await PodcastManager.Init();
            _log.Info("Podcast manager initialized...");
        }

        private void CreateFolderStructure()
        {
            Directory.CreateDirectory(StorageProvider.Current.MediaFolderPath);
        }

        private void ExitApp()
        {
            Toast.DisplayText("Permission is required for the app to work. Please change this on device settings.\n" +
                              "You can also uninstall & reinstall and you'll be asked again.");
            var timer = new Timer(5000) { AutoReset = false, Enabled = true };
            timer.Elapsed += (s, e) =>
            {
                _log.Fatal("App closed due insufficient permissions");
                Quit();
            };
        }

        private void OnException(object sender, UnhandledExceptionEventArgs e)
        {
            if (!(e.ExceptionObject is Exception ex)) return;
            _log.Error(ex.Message, ex);
        }

        private async Task RequestPermissions()
        {
            using (var con = IocContainer.Instance.BeginLifetimeScope())
            {
                _permissions = con.Resolve<IAppPrivacyPermissions>().Permissions.ToDictionary(x => x, x => false);
                var service = con.Resolve<IPrivacyPermissionService>();
                for (var i = 0; i < _permissions.Count; i++)
                {
                    var permission = _permissions.ElementAt(i).Key;
                    _permissions[permission] = await service.GetPermission(permission);
                }
            }
        }
    }
}

