using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Forms.Views;
using MvvmCross.ViewModels;
using MvvmCross.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.Wearable.CircularUI.Forms;

namespace WristCast.Tizen.Wearable
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : CirclePage
    {
        public HomePage()
        {
            InitializeComponent();
        }
    }

    public class MvxCirclePage<T> : CirclePage, IMvxPage<T> where T : class, IMvxViewModel
    {
        public object DataContext { get; set; }
        IMvxViewModel IMvxView.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (T)value;
        }

        public IMvxBindingContext BindingContext { get; set; }
        public T ViewModel { get; set; }
    }
}