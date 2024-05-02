using PDFService.API.Repositories;
using PDFService.API.Repositories.Interface;
using PDFService.API.Services;
using PDFService.API.Services.Interface;

namespace PDFService.API.IoC
{
    public static class IoC
    {
        public static IServiceCollection AddDependency(this IServiceCollection services, IConfiguration configuration)
        {

            // Services
            services.AddScoped<IPdfService, PdfService>();

            // General 
            services.AddScoped<IHttpClienteRepositories, HttpClienteRepositories>();

            return services;
        }
    }
}
