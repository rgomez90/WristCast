using WristCast.ViewModels;

namespace WristCast.Core
{
    public interface IView<out T> where T:ViewModel
    {
        T ViewModel { get; }
        object BindingContext { get; }
    }
}