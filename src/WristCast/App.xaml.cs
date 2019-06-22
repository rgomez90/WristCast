using Autofac;
using Tizen.Security;
using Tizen.Wearable.CircularUI.Forms;
using WristCast.Core.IoC;
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

        protected override void OnStart()
        {
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

