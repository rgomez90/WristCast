using System.Dynamic;
using Tizen.Wearable.CircularUI.Forms;
using WristCast.Core;
using WristCast.Core.ViewModels;

namespace WristCast
{
    public abstract class CircleView<T> : CirclePage, IView<T> where T:ViewModel 
    {
        public CircleView(T viewModel):base()
        {
           ViewModel = viewModel;
           BindingContext = ViewModel;
        }

        public T ViewModel { get; }
    }
}
