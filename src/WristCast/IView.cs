using WristCast.ViewModels;

namespace WristCast
{
    public interface IView<out T> where T:ViewModel
    {
        T ViewModel { get; }
        object BindingContext { get; }
    }
}