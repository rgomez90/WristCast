using System.Threading.Tasks;

namespace WristCast.Core.Services
{
    public interface IDownloadService
    {
        Task<byte[]> DownloadFileAsync(string url);
    }
}