using Autofac;
using DAL.DbContexts;
using DAL.UnitOfWorks;
using System.Reflection;

namespace DAL;

public static class Bootstrapper
{
	public static void Bootstrap(ContainerBuilder builder)
	{
		RegisterDbContext(builder);
		RegisterRepositories(builder);
	}

	private static void RegisterDbContext(ContainerBuilder builder)
	{
		builder.RegisterType(typeof(DbContextBase))
			.AsSelf()
			.As<DbContextBase>()
			.InstancePerLifetimeScope();

		builder.RegisterType(typeof(UnitOfWork))
			.AsSelf()
			.As<UnitOfWork>()
			.InstancePerLifetimeScope();
	}

	private static void RegisterRepositories(ContainerBuilder builder)
	{
		builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
			.Where(x => x.Name.EndsWith("Repository"))
			.AsImplementedInterfaces()
			.InstancePerLifetimeScope();
	}
}
