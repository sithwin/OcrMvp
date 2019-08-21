using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json.Linq;
using ServiceStack.Text.Json;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ComputerVision
{
    static class Program
    {
        static void Main()
        {
            FolderWatcher folder = new FolderWatcher(ConfigurationManager.AppSettings["DirectoryPath"].ToString());
            folder.Run();
            Console.ReadLine();
        }
    }
}