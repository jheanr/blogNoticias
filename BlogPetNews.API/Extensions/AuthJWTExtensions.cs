using BlogPetNews.API.Domain.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace BlogPetNews.API.Extensions;

public static class AuthJWTExtensions
{
    public static IServiceCollection AddAuthJWT(this IServiceCollection services, byte[] key)
    {
        services.AddAuthentication(opts =>
        {
            opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opts =>
        {
            opts.RequireHttpsMetadata = false;
            opts.SaveToken = true;
            opts.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy => policy.RequireRole(RolesUser.Admin.ToString()));
            options.AddPolicy("User", policy => policy.RequireRole(RolesUser.User.ToString()));
        });

        return services;
    }
}
