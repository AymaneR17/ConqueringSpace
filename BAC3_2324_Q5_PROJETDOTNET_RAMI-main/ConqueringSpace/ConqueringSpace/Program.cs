using ConqueringSpace.Interfaces;
using ConqueringSpace.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace ConqueringSpace
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            string uri = builder.Configuration.GetValue<string>("UserAPI");
            string uri2 = builder.Configuration.GetValue<string>("ConqueringSpaceAPI");


            builder.Services.AddScoped<IUsersService, UsersService>(x => {
                var value = new HttpClient()
                {
                    BaseAddress = new Uri(uri)
                 
                };

                return new UsersService(value);
            
            });
            builder.Services.AddScoped<ICelestialObjectService, celestialObjectService>(x => {
                var value = new HttpClient()
                {
                    BaseAddress = new Uri(uri2)
                };

                return new celestialObjectService(value);

            });

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.MapRazorPages();  // Configurer les routes RazorPages
            app.MapControllerRoute(
               name: "default",
               pattern: "{controller=CelestialObject}/{action=Index}/");

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

            app.UseAuthentication();

            app.UseAuthorization();

            app.Run();
        }
    }
}