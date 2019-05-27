using System.Linq;
using System.Threading.Tasks;
using Autofac;
using WristCast.Core.IoC;
using Xamarin.Forms;

namespace WristCast.Core.ViewModels
{
    public class XamarinNavigationService : INavigationService
    {
        private readonly INavigation _navigation;

        public XamarinNavigationService(INavigation navigation)
        {
            _navigation = navigation;
        }

        public Task PushModalAsync<T>(params object[] parameters) where T : ViewModel
        {
            using (var con = IocContainer.Instance.BeginLifetimeScope())
            {
                var view = con.Resolve<IView<T>>(parameters.Select(x => new TypedParameter(x.GetType(), x)));
                return _navigation.PushModalAsync((Page)view);
            }
        }

        public Task PopModalAsync()
        {
            return _navigation.PopModalAsync();
        }
    }
}