using System.Threading.Tasks;

namespace WristCast.Core.Services
{
    public interface IFileService
    {
        Task<bool> CheckIfMediaFileExists(string fileName);
        Task<string[]> GetAllMediaFiles();
    }
}