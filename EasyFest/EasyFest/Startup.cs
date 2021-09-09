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
using Storage.Services.CommentsService;
using Storage.Services.AuthenticationService;
using Storage.Services.UserService;
using Storage.Services.RateService;
using GraphQL.Server.Ui.Voyager;
using GraphQLDataAccess.Schema.Types;
using EasyFest.Factories;
using Storage.Services.TagService;

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

            services.AddSingleton<IHttpClientFactory, HttpClientFactory>();

            services.AddSingleton<IFestivalService, FestivalService>();
            services.AddSingleton<ICommentsService, CommentsService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IRateService, RateService>();
            services.AddSingleton<ITagService, TagService>();
            services.AddSingleton<IQuery, Query>();

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
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                //.AddType<QueryType>()
                .AddType<FestivalType>()
                .AddType<CommentType>()
                .AddType<RateType>()
                .AddType<UserType>()
                //.AddMutationType<UserMutation>()
                //.AddMutationType<CommentMutation>()
                //.AddMutationType<RateMutation>()
                //.AddMutationType<FestivalLocationMutation>()
                //.AddMutationType<FestivalMutation>()
                //.AddType<UserMutationType>()
                //.AddType<UserMutationType>()
                //.AddType<RateMutationType>()
                //.AddType<FestivalLocationMutationType>()
                //.AddType<FestivalMutationType>()
                .AddProjections()
                .AddFiltering()
                .AddSorting();
                //.AddQueryType<CommentQLQuery>();
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
                // /graphql endpoint
                endpoints.MapGraphQL();
            });

            app.UseGraphQLVoyager(new VoyagerOptions()
            {
                GraphQLEndPoint = "/graphql"
            }, "/graphql-voyager");

        }
    }
}
