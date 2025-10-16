using MongoConnection;

namespace CoreBackend
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddSingleTones(this IServiceCollection services, IConfigurationRoot configFile)
        {
            MongoSettings mongoSettings = configFile.GetRequiredSection(nameof(MongoSettings)).Get<MongoSettings>();
            MongoContext mongoContext = new MongoContext(mongoSettings);
            services.AddSingleton(mongoContext);

            return services;
        }
    }
}
