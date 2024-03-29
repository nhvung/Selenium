using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using VSSystem.Extensions.Hosting;

namespace VSSystem.Service.TestService
{
    public class VSStartup : AStartup
    {
        protected override void _ConfigureMiddleware(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<Middlewares.ProxyMiddleware>();
        }
        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.Configure(app, env);
            string webRootPath = env.WebRootPath ?? $"{env.ContentRootPath}/wwwroot";
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(webRootPath),
                RequestPath = "/autotest"

            });
        }
    }

}