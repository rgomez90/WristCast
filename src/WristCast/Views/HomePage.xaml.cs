using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Autofac;
using Tizen.Network.Connection;
using Tizen.System;
using Tizen.Wearable.CircularUI.Forms;
using WristCast.Core.IoC;
using WristCast.Core.Services;
using WristCast.Core.ViewModels;

namespace WristCast.Views
{
    public partial class HomePage : CircleView<HomeViewModel>
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
                var st = StorageManager.Storages;
                var root = st.FirstOrDefault(x => x.StorageType == StorageArea.Internal);
                var rootPath = root.GetAbsolutePath(DirectoryType.Sounds);
                var d = new Download("https://www.listennotes.com/e/p/a424661cab384b43bc9657962fa6e4b2/",Path.Combine(rootPath, "a424661cab384b43bc9657962fa6e4b2.mp3"));
                await d.Execute();
                var ss = new
                {
                    Cellular = ConnectionManager.CellularState,
                    Current = ConnectionManager.CurrentConnection,
                };
            }
    }

    public class TizenStorageProvider : IStorageProvider
    {
        private IEnumerable<Storage> _storages;
        private Storage _internalStorage;


        public TizenStorageProvider()
        {
            _storages = StorageManager.Storages;
            _internalStorage = _storages.SingleOrDefault(x => x.StorageType == StorageArea.Internal);
            MediaFolderPath = Path.Combine(_internalStorage.GetAbsolutePath(DirectoryType.Music),"WristCast");
        }

        public string MediaFolderPath{get;}
    }
}