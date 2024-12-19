using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NSE.Identidade.API.Data;

namespace NSE.Identidade.API.Configuration;

public static class ApiConfig
{
    private const string ConexaoIdentity = "IdentityConnection";

    public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString(ConexaoIdentity)));
       
        services.AddDefaultIdentity<IdentityUser>()
            .AddRoles<IdentityRole>()
            //AddErrorDesciber PT-BR in Extension later.
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
    }

    public static void UseApiConfiguration(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.UseSwagger();
        app.UseSwaggerUI();

        app.MapControllers();
        app.UseHttpsRedirection();
    }
}