using Autofac;
using AutoMapper;
using BLL;
using Common;
using Common.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using WebAPI.Infrastructure.AutoMapper;

namespace WebAPI;

public class Startup
{
	public Startup(IConfiguration configuration)
	{
		Configuration = configuration;

		Config.SecretKey = Configuration.GetSection("Config:SecretKey").Get<string>();
	}

	public IConfiguration Configuration { get; }

	public void ConfigureContainer(ContainerBuilder builder)
	{
		builder.RegisterBuildCallback(ctx => IoC.Container = ctx.Resolve<ILifetimeScope>());

		builder.RegisterInstance(Configuration);

		var cfg = new MapperConfigurationExpression
		{
			SourceMemberNamingConvention = new ExactMatchNamingConvention(),
			DestinationMemberNamingConvention = new ExactMatchNamingConvention()
		};

		BLL.Bootstrapper.Bootstrap(builder);
	}

	// This method gets called by the runtime. Use this method to add services to the container.
	public void ConfigureServices(IServiceCollection services)
	{
		services.AddControllers();

		services.AddCors(option =>
		{
			option.AddDefaultPolicy(builder =>
			{
				builder.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader();
			});
		});

		services.AddHttpLogging(log =>
		{
			log.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
		});

		#region Init Mapper Profiles

		var mapperConfig = new MapperConfiguration(mc =>
		{
			mc.AddProfile(new BookProfile());
			mc.AddProfile(new ReviewProfile());
			mc.AddProfile(new RatingProfile());
		});

		var mapper = mapperConfig.CreateMapper();
		services.AddSingleton(mapper);

		#endregion
	}

	// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		app.UseHttpLogging();

		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}

		app.Use(HandleError);

		app.UseHttpsRedirection();

		app.UseRouting();
		app.UseCors();

		app.UseAuthentication();
		app.UseAuthorization();

		app.UseEndpoints(endpoints =>
		{
			endpoints.MapControllers();
		});
	}

    private async Task HandleError(HttpContext httpContext, Func<Task> next)
    {
        try
        {
            await next();
        }
        catch (Exception ex)
        {
            var errorType = ApiErrorTypeEnum.Unknown;
            string message = null;
            object data = null;

            switch (ex)
            {
                case UnauthorizedAccessException _:
                    errorType = ApiErrorTypeEnum.Forbidden;
                    message = ex.Message;
                    data = ex.Data;
                    break;
            }

            var error = new JsonResultData<JsonErrorResult>(new JsonErrorResult
            {
                ErrorType = errorType,
                Message = message,
                Error = data
            });

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(error, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                IgnoreNullValues = true
            }));
            await httpContext.Response.CompleteAsync();
        }
    }
}
