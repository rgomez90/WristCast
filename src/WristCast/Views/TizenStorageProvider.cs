using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tizen.System;
using WristCast.Core;

namespace WristCast.Views
{
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