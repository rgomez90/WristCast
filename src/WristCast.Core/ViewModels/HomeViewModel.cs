using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.ViewModels;

namespace WristCast.Core.ViewModels
{
    public class HomeViewModel : MvxViewModel
    {
        public string Text { get; set; } = "Hey there!";
    }
}
