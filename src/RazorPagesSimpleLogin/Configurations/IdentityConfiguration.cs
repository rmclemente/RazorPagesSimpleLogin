using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace RazorPagesSimpleLogin.Configurations
{
    public static class IdentityConfiguration
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(cookieOptions =>
                {
                    cookieOptions.LoginPath = "/Account/Login";
                    cookieOptions.AccessDeniedPath = "/Account/AccessDenied";
                    cookieOptions.ExpireTimeSpan = new TimeSpan(0, 1, 0);
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole("Admin").RequireAuthenticatedUser());
            });

            return services;
        }

        public static IApplicationBuilder UseIdentityConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}
