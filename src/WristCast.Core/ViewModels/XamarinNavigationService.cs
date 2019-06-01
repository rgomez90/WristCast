using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using WristCast.Core.IoC;
using Xamarin.Forms;

namespace WristCast.Core.ViewModels
{
    public class XamarinNavigationService : INavigationService
    {
        private readonly Lazy<INavigation> _navigation = new Lazy<INavigation>(GetNavigation);

        private static INavigation GetNavigation()
        {
            return Application.Current.MainPage.Navigation;
        }

        public Task PushModalAsync<T>() where T : ViewModel
        {
            var view=(Page)IocContainer.Instance.Resolve<IView<T>>();
            return _navigation.Value.PushModalAsync(view);
        }

        public async Task PushModalAsync<T, TParam>(TParam param) where T : ViewModel<TParam>
        {
            var t = IocContainer.Instance.Resolve<IView<T>>();
            t.ViewModel.Prepare(param);
            await _navigation.Value.PushModalAsync((Page) t);
        }

        public Task PopModalAsync()
        {
            return _navigation.Value.PopModalAsync();
        }
    }
}