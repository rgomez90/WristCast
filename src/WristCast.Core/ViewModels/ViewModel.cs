using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Transactions;
using Xamarin.Forms;

namespace WristCast.Core.ViewModels
{
    public abstract class ViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual Task Init() { return Task.CompletedTask;}
    }

    public abstract class ViewModel<T> : ViewModel
    {
        public abstract void Prepare(T parameter);
    }
}