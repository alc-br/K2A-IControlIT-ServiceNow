using IControlIT.Connect.Integrations;
using IControlIT.Connect.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;

namespace IControlIT.Connect
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Registrar o serviço IExternalIntegration com sua implementação ServiceNowIntegration
            services.AddScoped<IExternalIntegration, ServiceNowIntegration>();

            services.AddControllers();

            // Configuração do CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            // Configuração do Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IControlIT API", Version = "v1" });
                var xmlFile = $"{AppContext.BaseDirectory}{Path.DirectorySeparatorChar}{typeof(Startup).Assembly.GetName().Name}.xml";
                if (File.Exists(xmlFile))
                {
                    c.IncludeXmlComments(xmlFile);
                }
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // Use o CORS configurado
            app.UseCors("AllowAllOrigins");

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "IControlIT API V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
