using System.Threading.Tasks;

namespace WristCast.ViewModels
{
    public interface INavigationService
    {
        Task PushModalAsync<T>() where T : ViewModel;
        Task PushModalAsync<T,TParam>(TParam param) where T : ViewModel<TParam>;
        Task PopModalAsync();
    }
}