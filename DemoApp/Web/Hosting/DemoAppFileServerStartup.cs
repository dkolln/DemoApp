using System;
using Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;

namespace DemoApp.Web.Hosting
{
    public class DemoAppFileServerStartup
    {
        public void Initialize(IAppBuilder appBuilder, string path)
        {
            var physicalFileSystem = new PhysicalFileSystem(path);
            var options = new FileServerOptions
            {
                EnableDefaultFiles = true,
                FileSystem = physicalFileSystem
            };
            options.StaticFileOptions.FileSystem = physicalFileSystem;
            options.StaticFileOptions.ServeUnknownFileTypes = true;
            options.DefaultFilesOptions.DefaultFileNames = new[]
            {
                "index.html"
            };

            appBuilder.UseFileServer(options);
        }
    }
}