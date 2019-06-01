using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using WristCast.Core.IoC;
using WristCast.Core.ViewModels;

namespace WristCast.Core.Services
{
    public class DownloadService : IDownloadService
    {
        public async Task<FileInfo> DownloadFileAsync(string sourceUrl, string filePath, CancellationToken ct, IProgress<double> progress)
        {
            using (var client = new HttpClient())
            using (var response = await client.GetAsync(sourceUrl, HttpCompletionOption.ResponseHeadersRead, ct))
            {
                using (var fs = new FileStream(filePath, FileMode.CreateNew))
                {
                    var t = response.Content.Headers.ContentLength;
                    using (var content = await response.Content.ReadAsStreamAsync())
                    {
                        byte[] buffer = new byte[16 * 1024];
                        int bytesRead = 0;
                        int read;
                        while ((read = await content.ReadAsync(buffer, 0, buffer.Length, ct)) > 0)
                        {
                            await fs.WriteAsync(buffer, 0, read, ct);
                            bytesRead += read;
                            if (t.HasValue)
                            {
                                var p = (bytesRead / (double)t.Value) * 100;
                                progress.Report(p);
                            }
                        }
                    }
                }

                return new FileInfo(filePath);
            }
        }

        public async Task<FileInfo> DownloadFileAsync(string sourceUrl, string filePath, CancellationToken ct)
        {
            using (var client = new HttpClient())
            using (var response = await client.GetStreamAsync(sourceUrl))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                using (var fs = new FileStream(filePath, FileMode.CreateNew))
                {
                    response.CopyTo(fs);
                }

                return new FileInfo(filePath);
            }
        }

       
    }

    public enum DownloadState
    {
        Pending,
        Cancelled,
        Downloading,
        Completed,
        Error
    }

}
