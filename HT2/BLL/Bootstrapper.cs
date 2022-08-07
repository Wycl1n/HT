using System.Reflection;
using Autofac;

namespace BLL;

public static class Bootstrapper
{
	public static void Bootstrap(ContainerBuilder builder)
	{
		RegisterServices(builder);
		DAL.Bootstrapper.Bootstrap(builder);
	}

	private static void RegisterServices(ContainerBuilder builder)
	{
		builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
			.Where(x => x.Name.EndsWith("Service"))
			.AsImplementedInterfaces()
			.InstancePerLifetimeScope();
	}
}
