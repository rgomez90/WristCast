using System.Threading.Tasks;
using WristCast.Core.ViewModels;

namespace WristCast.Core
{
    public interface IView<out T> where T:ViewModel
    {
        T ViewModel { get; }
        object BindingContext { get; }
    }
}