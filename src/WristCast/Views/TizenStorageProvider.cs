using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tizen.Applications;
using Tizen.System;
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
            _internalStorage = _storages.SingleOrDefault(x => 
                                   x.StorageType == StorageArea.Internal)
                               ?? throw new ApplicationException("Could not get internal storage path");
            MediaFolderPath = Path.Combine(_internalStorage.GetAbsolutePath(DirectoryType.Music), "WristCast");
            AppDataDirectory = Application.Current.DirectoryInfo.Data;
            SharedAppDataDirectory = Application.Current.DirectoryInfo.SharedData;
            CacheDirectory = Application.Current.DirectoryInfo.Cache;
            DatabasePath = Path.Combine(AppDataDirectory, "wristcast.db");
        }

        public string AppDataDirectory { get; }

        public string SharedAppDataDirectory { get; }

        public string DatabasePath { get; }

        public string CacheDirectory { get; }

        public string MediaFolderPath { get; }
    }
}