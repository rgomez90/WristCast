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
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage((CirclePage)IocContainer.Instance.Resolve<IView<HomeViewModel>>());
        }

        public static string GetDatabasePath()
        {
            return IocContainer.Instance.Resolve<IStorageProvider>().DatabasePath;
        }

        protected override async void OnStart()
        {
            if (!File.Exists(GetDatabasePath()))
                await IocContainer.Instance.Resolve<WristCastContext>().Create();
            raw.SetProvider(new SQLite3Provider_sqlite3());
            PrivacyPrivilegeManager.RequestPermission("http://tizen.org/privilege/mediastorage");
            PrivacyPrivilegeManager.RequestPermission("http://tizen.org/privilege/externalstorage");
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

