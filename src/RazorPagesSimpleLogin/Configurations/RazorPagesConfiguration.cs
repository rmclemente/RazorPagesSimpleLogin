using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RazorPagesSimpleLogin.Configurations
{
    public static class RazorPagesConfiguration
    {
        public static IServiceCollection AddRazorPagesConfiguration(this IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;

            });

            services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizeFolder("/admin", "RequireAdministratorRole");
            });

            return services;
        }
        public static IApplicationBuilder UseRazorPagesConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();
            app.UseIdentityConfiguration(env);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

            return app;
        }
    }
}
