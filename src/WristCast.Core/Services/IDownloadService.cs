using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace WristCast.Core.Services
{
    public interface IDownloadService
    {
        Task<FileInfo> DownloadFileAsync(string sourceUrl, string filePath, CancellationToken ct, IProgress<long> progress);
        Task<FileInfo> DownloadFileAsync(string sourceUrl, string filePath, CancellationToken ct);
        event EventHandler<HttpContentHeaders> DownloadContentHeaderReady;
    }
}