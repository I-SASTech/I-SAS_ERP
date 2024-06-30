using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScheduledTasks.Interface;

namespace ScheduledTasks.ScheduledTasks
{
    public class PushFileService : IScheduledTask
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment;

        public PushFileService(IServiceScopeFactory scopeFactory, IConfiguration configuration,
            IHostingEnvironment hostingEnvironment)
        {
            _scopeFactory = scopeFactory;
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;

        }
        public string Schedule => "*/15 * * * *";

        public async Task Invoke(CancellationToken cancellationToken)
        {
            await PushFileRecord();
        }
        
        public async Task PushFileRecord()
        {
            // Start a local transaction.

            string sourcePath = HostingEnvironment.ContentRootPath+ Configuration.GetSection("Ftp:LocalFolder").Value; ;
            string targetPath = Configuration.GetSection("Ftp:ServerFolder").Value + "/";
            string host = Configuration.GetSection("Ftp:FtpHost").Value;
            string userId = Configuration.GetSection("Ftp:FtpUser").Value;
            string password = Configuration.GetSection("Ftp:FtpPassword").Value;

            try
            {
                //var result = createFolder(host, targetPath, userId, password);
                var localDirectories = Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories);
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
                
                foreach (var path in localDirectories)
                {
                    if (!serverDirectories.Contains(Path.GetFileName(path)))
                    {
                        var filename = Path.GetFileName(path);

                        FtpWebRequest uploadRequest =
                        (FtpWebRequest)WebRequest.Create(host + targetPath + filename);
                        uploadRequest.Credentials = new NetworkCredential(userId, password);
                        uploadRequest.Method = WebRequestMethods.Ftp.UploadFile;

                        using (Stream fileStream = System.IO.File.OpenRead(path))
                        using (Stream ftpStream = uploadRequest.GetRequestStream())
                        {
                            await fileStream.CopyToAsync(ftpStream);
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
