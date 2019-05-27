using WristCast.Core.ViewModels;

namespace WristCast.Core
{
    public interface IView<T> where T:ViewModel
    {
        T ViewModel { get; }
        object BindingContext { get; }
    }
}