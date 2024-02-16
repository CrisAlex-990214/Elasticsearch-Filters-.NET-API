using CleanArchitecture.Application.Interfaces;
using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace CleanArchitecture.Persistence
{
    public static class PersistenceInstallers
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("Elastic");
            var connectionSettings = new ConnectionSettings(new Uri("https://localhost:9200"))
                .BasicAuthentication(section["user"], section["secret"])
                .ServerCertificateValidationCallback(CertificateValidations.AllowAll)
                .DefaultIndex("product");

            services.AddSingleton<IElasticClient>(new ElasticClient(connectionSettings));
            services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
        }
    }
}
