using HT_1.Models;
using HT_1.Services.Abstraction;
using HT_1.Services.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
 
namespace HT_1;

internal class Program
{
	static void Main(string[] args)
	{
		var host = Host.CreateDefaultBuilder()
			.ConfigureServices((context, services) => ConfigureServices(context, services))
			.Build();

		var app = ActivatorUtilities.CreateInstance<Startup>(host.Services);
		app.Run();
	}

	public static IServiceCollection ConfigureServices(HostBuilderContext context, IServiceCollection services)
	{
		services.AddSingleton<IEtlService, EtlService>();
		services.AddScoped<Parser, CsvParser>();
		services.AddScoped<Parser, TxtParser>();

		services.AddScoped<ILogger, ConsoleLogger>();
		services.AddScoped<Startup>();

		Config.InputPath = context.Configuration.GetSection("Config:InputPath").Get<string>();
		Config.OutputPath = context.Configuration.GetSection("Config:OutputPath").Get<string>();

		return services;
	}
}