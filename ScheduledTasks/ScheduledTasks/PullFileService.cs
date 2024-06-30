using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScheduledTasks.Interface;

namespace ScheduledTasks.ScheduledTasks
{
    public class PullFileService : IScheduledTask
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment;

        public PullFileService(IServiceScopeFactory scopeFactory, IConfiguration configuration,
            IHostingEnvironment hostingEnvironment)
        {
            _scopeFactory = scopeFactory;
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;

        }
        public string Schedule => "*/15 * * * *";

        public async Task Invoke(CancellationToken cancellationToken)
        {
            await PullFileRecord();
        }

        public async Task PullFileRecord()
        {
            // Start a local transaction.
            
            try
            {
                var sourcePath = HostingEnvironment.ContentRootPath + Configuration.GetSection("Ftp:LocalFolder").Value;
                var targetPath = Configuration.GetSection("Ftp:ServerFolder").Value;
                string host = Configuration.GetSection("Ftp:FtpHost").Value;
                string userId = Configuration.GetSection("Ftp:FtpUser").Value;
                string password = Configuration.GetSection("Ftp:FtpPassword").Value;


                //Get All Files that  exist in local Directory 
                var localDirectories = Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories);
                var localDirectoryFiles = new List<string>();
                foreach (var dir in localDirectories)
                {
                    localDirectoryFiles.Add(Path.GetFileName(dir));
                }
                // Get the object used to communicate with the server.
                FtpWebRequest request =
                    (FtpWebRequest)WebRequest.Create(host + targetPath);
                request.Credentials = new NetworkCredential(userId, password);
                request.Method = WebRequestMethods.Ftp.ListDirectory;

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                string names = await reader.ReadToEndAsync();

                reader.Close();
                response.Close();

                var serverDirectories = names.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

               
                foreach (var fileName in serverDirectories)
                {
                    if (!localDirectoryFiles.Contains(fileName))
                    {
                        FtpWebRequest downloadRequest =
                            (FtpWebRequest)WebRequest.Create(host + targetPath + "/" + fileName);

                        downloadRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                        downloadRequest.Credentials = new NetworkCredential(userId, password);

                        if (!Directory.Exists(sourcePath))
                            Directory.CreateDirectory(sourcePath);

                        using (FtpWebResponse downloadResponse =
                            (FtpWebResponse)downloadRequest.GetResponse())
                        using (Stream sourceStream = downloadResponse.GetResponseStream())
                        using (Stream targetStream = File.Create(sourcePath + "/" + fileName))
                        {
                            byte[] buffer = new byte[10240];
                            int read;
                            while ((read = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                            {
                                await targetStream.WriteAsync(buffer, 0, read);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
