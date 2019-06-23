using System;
using System.IO;
using System.Linq;
using Tizen.Network.Connection;
using Tizen.System;
using WristCast.Core.Model;
using WristCast.Core.Services;
using WristCast.ViewModels;

namespace WristCast.Views
{
    public partial class HomePage : CircleView<HomeViewModel>
    {
        public HomePage()
        {
            InitializeComponent();
        }
    }
}