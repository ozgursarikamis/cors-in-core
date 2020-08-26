using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CorsInCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var allowedOrigins = Configuration.GetValue<string>("AllowedOrigins")
                ?.Split(",") ?? new string[0];

            services.AddCors(options =>
                {
                    //options.AddPolicy("AllowEverything", corsPolicyBuilder =>
                    //{
                    //    corsPolicyBuilder.WithOrigins(allowedOrigins).AllowCredentials();
                    //    corsPolicyBuilder.WithExposedHeaders("PageNo", "PageSize", "PageCount", "PageTotalRecords");
                    //});

                    //options.AddPolicy("PublicApi", builder => 
                    //    builder.AllowAnyOrigin()
                    //        .WithMethods("GET")
                    //        .WithHeaders("Content-Type")
                    //    );

                    options.AddPolicy("AllowSubDomains", builder =>
                    {
                        builder.WithOrigins("http://*.somedomain.com");
                        builder.SetIsOriginAllowedToAllowWildcardSubdomains();
                    });
                }
            );
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
             
            // use middleware:
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("HeaderType", "HeaderValue");
                await next();
            });

            app.UseRouting();
            app.UseCors("AllowEverything");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
