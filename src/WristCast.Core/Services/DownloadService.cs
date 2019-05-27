using System;
using System.Net;
using System.Threading.Tasks;

namespace WristCast.Core.Services
{
    public class DownloadService:IDownloadService
    {
        public async Task<byte[]> DownloadFileAsync(string url)
        {
            using (var client = new WebClient())
            {
                return await client.DownloadDataTaskAsync(url);
            }
        }
    }
}
