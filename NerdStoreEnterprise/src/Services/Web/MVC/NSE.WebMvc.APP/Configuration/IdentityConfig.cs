using Microsoft.AspNetCore.Authentication.Cookies;

namespace NSE.WebMvc.APP.Configuration;

public static class IdentityConfig
{
    public static void AddIdentityConfig (this IServiceCollection services)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/login";
                options.AccessDeniedPath = "/denied-access";
            });
        services.AddAuthorization();
    }

    public static void UseIdentityConfiguration(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}