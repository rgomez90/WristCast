using System;
using System.Collections.Generic;
using System.Text;
using ListenNotesSearch.NET;
using MvvmCross;
using MvvmCross.ViewModels;
using WristCast.Core.ViewModels;

namespace WristCast.Core
{
    public class App:MvxApplication
    {
        public override void Initialize()
        {
            Mvx.IoCProvider.RegisterType<IListenNodeSearchClient, ListenNodeSearchClient>();
            RegisterAppStart<HomeViewModel>();
        }
    }
}
