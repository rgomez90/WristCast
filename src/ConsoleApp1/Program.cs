using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using ListenNotesSearch.NET;
using WristCast.Core.Services;
using Type = ListenNotesSearch.NET.Models.Type;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var o = "/sfsdf/dfgfdg/t.mp3";
            Console.WriteLine(Path.GetDirectoryName(o));
            Console.ReadLine();
        }

        private static void OnP(double obj)
        {
            Console.Out.WriteLine(obj);
        }
    }
}
