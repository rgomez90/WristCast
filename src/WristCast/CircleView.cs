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
            base.OnAppearing();
            await ViewModel.Init();
        }

        public T ViewModel { get; }
    }
}
