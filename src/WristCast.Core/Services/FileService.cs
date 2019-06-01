using System.IO;
using System.Threading.Tasks;
using WristCast.Core.ViewModels;

namespace WristCast.Core.Services
{
    public class FileService : IFileService
    {
        private readonly IStorageProvider _storageProvider;

        public FileService(IStorageProvider storageProvider)
        {
            _storageProvider = storageProvider;
        }

        public Task<bool> CheckIfMediaFileExists(string fileName)
        {
            return Task.Run(() => File.Exists(Path.Combine(_storageProvider.MediaFolderPath, fileName)));
        }

        public Task<string[]> GetAllMediaFiles()
        {
            return Task.Run(() => Directory.GetFiles(_storageProvider.MediaFolderPath));
        }
    }
}