using System.IO;
using System.Threading.Tasks;

namespace WristCast.Core.Services
{
    public class FileService : IFileService
    {
        public FileService()
        {
        }

        public Task<bool> CheckIfMediaFileExists(string fileName)
        {
            return Task.Run(() => File.Exists(Path.Combine(StorageProvider.Current.MediaFolderPath, fileName)));
        }

        public Task<string[]> GetAllMediaFiles()
        {
            return Task.Run(() => Directory.GetFiles(StorageProvider.Current.MediaFolderPath));
        }
    }
}