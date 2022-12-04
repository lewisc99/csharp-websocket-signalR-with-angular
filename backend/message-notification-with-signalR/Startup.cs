using message_notification_with_signalR.Hub;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;


namespace message_notification_with_signalR
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "message_notification_with_signalR", Version = "v1" });
            });


            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
           {
               builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");
               builder.AllowAnyHeader()
             .AllowAnyMethod()
             .SetIsOriginAllowed((host) => true)
             .AllowCredentials();
           }));

            services.AddSignalR();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "message_notification_with_signalR v1"));
            }


            app.UseHttpsRedirection();

          

            app.UseRouting();

            app.UseCors("CorsPolicy");


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapHub<BroadcastHub>("/notify");
                endpoints.MapControllers();
            });
        }
    }
}
