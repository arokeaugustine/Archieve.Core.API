using Microsoft.AspNetCore.Authentication.JwtBearer;
using Archieve.Core.Contracts.TransferObjects.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Archieve.Domain.Helpers.Authorizations;
using Archieve.Domain.Interfaces;
using Archieve.Domain.Services;

namespace Archieve.Domain.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection ConfigureAuthorizationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtConfig>(configuration.GetSection("JwtConfig"));

            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true,
                ValidIssuer = "austin",
                ValidAudience = "austin",
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("hsdkjskfdjoieoelkmdskl783ksjn909742knsld")),
                ClockSkew = TimeSpan.Zero,

            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = tokenValidationParameters;
                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            var accessToken = context.SecurityToken as JwtSecurityToken;
                            if (accessToken != null)
                            {
                                ClaimsIdentity identity = context.Principal.Identity as ClaimsIdentity;
                                if (identity != null)
                                {
                                    identity.AddClaim(new Claim("access_token", accessToken.RawData));
                                }
                            }
                            return Task.CompletedTask;
                        },
                        OnChallenge = context =>
                        {

                            return Task.CompletedTask;
                        },
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];
                            if (!string.IsNullOrEmpty(accessToken))
                            {
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };



                });


            services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();


            services.AddScoped<JwtConfig>();

            return services;
        }

    }
}
