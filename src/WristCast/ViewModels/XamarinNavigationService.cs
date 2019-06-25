using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using WristCast.Core;
using WristCast.Core.IoC;
using Xamarin.Forms;

namespace WristCast.ViewModels
{
    public class XamarinNavigationService : INavigationService
    {
        private readonly Lazy<INavigation> _navigation = new Lazy<INavigation>(GetNavigation);

        public IReadOnlyList<IView<ViewModel>> Stack => _navigation.Value.NavigationStack.Cast<IView<ViewModel>>().ToList();

        public IReadOnlyList<IView<ViewModel>> ModalStack => _navigation.Value.ModalStack.Cast<IView<ViewModel>>().ToList();

        public Task PopAsync(bool animated = false)
        {
            return _navigation.Value.PopAsync(animated);
        }

        public Task PopModalAsync(bool animated = false)
        {
            return _navigation.Value.PopModalAsync(animated);
        }

        public Task PushAsync<T>(bool animated = false) where T : ViewModel
        {
            var view = (Page)IocContainer.Instance.Resolve<IView<T>>();
            return _navigation.Value.PushAsync(view, animated);
        }

        public async Task PushAsync<T, TParam>(TParam param, bool animated = false) where T : ViewModel<TParam>
        {
            var t = IocContainer.Instance.Resolve<IView<T>>();
            t.ViewModel.Prepare(param);
            await _navigation.Value.PushAsync((Page)t, animated);
        }

        public Task PushModalAsync<T>(bool animated = false) where T : ViewModel
        {
            var view = (Page)IocContainer.Instance.Resolve<IView<T>>();
            return _navigation.Value.PushModalAsync(view, animated);
        }

        public async Task PushModalAsync<T, TParam>(TParam param, bool animated = false) where T : ViewModel<TParam>
        {
            var t = IocContainer.Instance.Resolve<IView<T>>();
            t.ViewModel.Prepare(param);
            await _navigation.Value.PushModalAsync((Page)t, animated);
        }

        private static INavigation GetNavigation()
        {
            return Application.Current.MainPage.Navigation;
        }
    }
}