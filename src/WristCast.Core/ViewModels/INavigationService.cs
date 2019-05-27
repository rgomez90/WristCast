using System.Threading.Tasks;

namespace WristCast.Core.ViewModels
{
    public interface INavigationService
    {
        Task PushModalAsync<T>(params object[] parameters) where T : ViewModel;
        Task PopModalAsync();
    }
}