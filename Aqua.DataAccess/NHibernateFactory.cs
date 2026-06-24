using AquaApp.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace Aqua.DataAccess;

public class NHibernateFactory
{
	private static ISessionFactory _sessionFactory;

	public static ISessionFactory GetSessionFactory(string connectionString)
	{
		if (_sessionFactory != null)
			return _sessionFactory;

		_sessionFactory = Fluently.Configure()
			.Database(
				MySQLConfiguration.Standard
					.ConnectionString(connectionString)
					.Driver<NHibernate.Driver.MySqlDataDriver>()
					.Dialect<NHibernate.Dialect.MySQLDialect>()
			)
			.Mappings(m => m.FluentMappings.AddFromAssemblyOf<EmployeeMap>())
			.ExposeConfiguration(cfg =>
			{
				cfg.SetProperty(NHibernate.Cfg.Environment.UseProxyValidator, "false");

				new SchemaValidator(cfg).Validate();
			})
			.BuildSessionFactory();

		return _sessionFactory;
	}
}