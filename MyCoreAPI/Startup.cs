using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MyCoreAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:12817/Employee",
                                                          "http://localhost:12817")
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod();
                                  });
            });
            services.AddControllers();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddDbContext<EmployeeDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("myconn")));
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddMvc(option => option.EnableEndpointRouting = false);
            //services.AddDbContext<EmployeeDBContext>();
            //MvcOptions.EnableEndPointRouting = false;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(route =>
            {
                route.MapRoute("default", "{controller}/{action}/{id}");

            });

            app.UseCors(MyAllowSpecificOrigins);

            app.UseRouting();
            //app.UseMvcWithDefaultRoute();
            app.UseAuthorization();

            app.UseHttpsRedirection();
            app.UseMvc();
            //app.UseMiddleware<AuthenticationMiddleware>();
            
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
        }
    }
}
