using IControlIT.Connect;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) // Define nível mínimo para logs de bibliotecas Microsoft
            .Enrich.FromLogContext()
            .WriteTo.Console()  // Log para o console
            .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)  // Log para arquivo, com rotação diária
            .CreateLogger();

        try
        {
            Log.Information("Starting up the application");
            CreateHostBuilder(args).Build().Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "The application failed to start correctly");
            throw;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog()  // Use Serilog no lugar do logger padrão do .NET
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
