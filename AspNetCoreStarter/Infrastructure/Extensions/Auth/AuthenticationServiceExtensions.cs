using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AspNetCoreStarter.Infrastructure.Extensions.Auth
{
    public static class AuthenticationServiceExtensions
    {
        public static IServiceCollection AddDomainAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var symmetricKeyAsBase64 = configuration["Secret"];
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);

            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = configuration["Issuer"],

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = configuration["Audience"],

                // Validate the token expiry
                ValidateLifetime = true,

                ClockSkew = TimeSpan.FromMinutes(1) // 1 minute tolerance for the expiration date
            };

            services.AddAuthentication()
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = true;
                    o.SaveToken = true;
                    o.TokenValidationParameters = tokenValidationParameters;
                });

            return services;
        }
    }
}
