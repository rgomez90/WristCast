using System.Dynamic;
using System.Threading.Tasks;

namespace WristCast.ViewModels
{
    public interface INavigationService
    {
        Task PushModalAsync<T>(bool animated = false) where T : ViewModel;
        Task PopAsync(bool animated = false);
        Task PushAsync<T>(bool animated = false) where T : ViewModel;
        Task PushAsync<T, TParam>(TParam param, bool animated = false) where T : ViewModel<TParam>;
        Task PushModalAsync<T, TParam>(TParam param, bool animated = false) where T : ViewModel<TParam>;
        Task PopModalAsync(bool animated = false);
    }
}