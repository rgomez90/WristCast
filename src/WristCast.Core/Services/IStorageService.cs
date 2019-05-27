using System.Threading.Tasks;

namespace WristCast.Core.Services
{
    public interface IStorageService
    {
        Task SaveToFile(string fileName, string data);
        Task SaveToFile(string fileName, byte[] data);
        Task<byte[]> LoadFile(string fileName);
        Task<string> ReadFile(string fileName);
    }
}
