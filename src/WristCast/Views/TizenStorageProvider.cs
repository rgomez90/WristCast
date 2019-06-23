using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tizen.System;
using WristCast.Core;
using WristCast.Core.Services;

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
            MediaFolderPath = Path.Combine(_internalStorage.GetAbsolutePath(DirectoryType.Music), "WristCast");
            AppDataDirectory = Tizen.Applications.Application.Current.DirectoryInfo.Data;
            DatabasePath = Path.Combine(AppDataDirectory, "wristcast.db");
        }

        public string AppDataDirectory { get; }

        public string DatabasePath { get; }

        public string MediaFolderPath { get; }
    }
}