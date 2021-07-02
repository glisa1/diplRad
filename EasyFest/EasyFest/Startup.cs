using GraphQLDataAccess.Schema;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HotChocolate;
using Microsoft.Extensions.Options;
using EasyFest.Models;
using Storage;
using Storage.Models;
using Storage.Services.MongoDbConnectService;
using Storage.Services.FestivalService;
using Storage.Services.FestivalLocationsService;
using Storage.Services.CommentsService;
using Storage.Services.AuthenticationService;
using Storage.Services.UserService;

namespace EasyFest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.Configure<FestDatabaseSettings>(
                Configuration.GetSection(nameof(FestDatabaseSettings)));

            services.AddSingleton<IFestDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<FestDatabaseSettings>>().Value);

            services.AddSingleton<IMongoDbConnectService, MongoDbConnectService>();

            services.AddSingleton<IFestivalService, FestivalService>();
            services.AddSingleton<IFestivalLocationsService, FestivalLocationsService>();
            services.AddSingleton<ICommentsService, CommentsService>();
            services.AddSingleton<IUserService, UserService>();

            services.AddHttpContextAccessor();
            services.AddSingleton<IAuthenticationService, AuthenticationService>();

            //services.AddSingleton<IStorageService, StorageService>();

            //set default authentication schemes
            var authenticationBuilder = services.AddAuthentication(options =>
            {
                options.DefaultScheme = FestAuthenticationDefaults.AuthenticationScheme;
            });

            //add main cookie authentication
            authenticationBuilder.AddCookie(FestAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.Cookie.Name = $"{FestAuthenticationDefaults.Prefix}{FestAuthenticationDefaults.AuthenticationCookie}";
                options.Cookie.HttpOnly = true;
                options.LoginPath = FestAuthenticationDefaults.LoginPath;
                options.AccessDeniedPath = FestAuthenticationDefaults.AccessDeniedPath;
            });

            services
                .AddGraphQLServer()
                .AddQueryType<Query>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app
            .UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
        }
    }
}
