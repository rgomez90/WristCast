using Autofac;
using Tizen.Wearable.CircularUI.Forms;
using WristCast.Core.IoC;
using WristCast.ViewModels;

namespace WristCast
{
    public abstract class CircleView<T> : CirclePage, IView<T> where T : ViewModel
    {
        protected CircleView()
        {
            ViewModel = IocContainer.Instance.Resolve<T>();
            BindingContext = ViewModel;
        }

        protected override async void OnAppearing()
        {
            await ViewModel.Init();
        }

        protected override async void OnDisappearing()
        {
            await ViewModel.Clean();
        }

        public T ViewModel { get; }
    }
}
