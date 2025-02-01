using NSE.WebMvc.APP.Service;

namespace NSE.WebMvc.APP.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddHttpClient<IAutenticationService, AutenticationService>();
    }
}