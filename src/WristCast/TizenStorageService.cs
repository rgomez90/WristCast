using System.IO;
using System.Threading.Tasks;

namespace WristCast.Core.Services
{
    public class TizenStorageService : IStorageService
    {
        public Task SaveToFile(string fileName, string data)
        {
            return File.WriteAllTextAsync(fileName, data);
        }

        public Task SaveToFile(string fileName, byte[] data)
        {
            return File.WriteAllBytesAsync(fileName, data);
        }

        public Task<byte[]> LoadFile(string fileName)
        {
            return File.ReadAllBytesAsync(fileName);
        }

        public Task<string> ReadFile(string fileName)
        {
            return File.ReadAllTextAsync(fileName);
        }
    }
}