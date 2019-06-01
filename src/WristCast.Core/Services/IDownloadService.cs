using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace WristCast.Core.Services
{
    public interface IDownloadService
    {
        Task<FileInfo> DownloadFileAsync(string sourceUrl, string filePath, CancellationToken ct, IProgress<double> progress);
        Task<FileInfo> DownloadFileAsync(string sourceUrl, string filePath, CancellationToken ct);
    }
}