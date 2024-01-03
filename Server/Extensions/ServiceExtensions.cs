
using CandidatePortal.Server.DB;
using CandidatePortal.Server.HealthCheck;
using CandidatePortal.Server.Repositories;
using CandidatePortal.Server.Services;
using CandidatePortal.Shared.PortalUtilities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CandidatePortal.Server.Extensions
{
    public static class ServiceExtensions
    {
        public static void ServiceValidation(this IServiceCollection services, IConfiguration Configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (Configuration == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }


        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient(typeof(IPortalUtilities), typeof(PortalUtility));
            services.AddTransient(typeof(ICandidateService), typeof(CandidateService));
            services.AddTransient(typeof(IMasterService), typeof(MasterService));
            services.AddTransient(typeof(IJobService), typeof(JobService));
            services.AddTransient(typeof(ILoggerService), typeof(LoggerService));
            services.AddTransient(typeof(IReturnResponseService), typeof(ReturnResponseService));
            services.AddTransient(typeof(IUploadService), typeof(UploadService));
            services.AddTransient(typeof(IApplicationService), typeof(ApplicationService));
            services.AddTransient(typeof(IApplicationCommunicationService), typeof(ApplicationCommunicationService));
            services.AddTransient(typeof(IApplicationCheckListService), typeof(ApplicationCheckListService));
            services.AddTransient(typeof(IApplicationQuestionnairesService), typeof(ApplicationQuestionnairesService));

            //services.AddScoped<ICandidateService, CandidateService>();
            services.AddDbContext<PortalDbContext>();
        }

        
        public static void ConfigureMSSqlContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContextPool<PortalDbContext>
                (opt => opt.UseSqlServer(config.GetConnectionString("DbConnection")));
        }

        public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration config)
        {
            //var connectionString = config["mysqlconnection:connectionString"];
            //services.AddDbContext<RepositoryContext>
            //    (o => o.UseMySql(connectionString, MySqlServerVersion.LatestSupportedServerVersion));
        }

        public static void ConfigurePostgresSqlContext(this IServiceCollection services, IConfiguration config)
        {
            //services.AddDbContextPool<RepositoryDbContext>(builder =>
            //{
            //    var connectionString = Configuration.GetConnectionString("DevConnection");

            //    //builder.UseNpgsql(connectionString);
            //});
        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            //services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                    .AddSqlServer(configuration["ConnectionStrings:Feedback"], healthQuery: "select 1", name: "SQL Server", failureStatus: HealthStatus.Unhealthy, tags: new[] { "Feedback", "Database" })
                    .AddCheck<RemoteHealthCheck>("Remote endpoints Health Check", failureStatus: HealthStatus.Unhealthy)
                    .AddCheck<MemoryHealthCheck>("Feedback Service Memory Health Check", failureStatus: HealthStatus.Unhealthy, tags: new[] { "Feedback Service" })
                    .AddUrlGroup(new Uri("https://localhost:5001/api/feedbackservice/v1/healthbeat/ping"), name: "base URL", failureStatus: HealthStatus.Unhealthy);

            services.AddHealthChecksUI(opt =>
            {
                opt.SetEvaluationTimeInSeconds(10);
                opt.MaximumHistoryEntriesPerEndpoint(60);
                opt.SetApiMaxActiveRequests(1);
                opt.AddHealthCheckEndpoint("feedback api", "/api/health");
            }).AddInMemoryStorage();
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            //var builder = services.AddIdentity<User, IdentityRole>(o =>
            //{
            //    o.Password.RequireDigit = true;
            //    o.Password.RequireLowercase = false;
            //    o.Password.RequireUppercase = false;
            //    o.Password.RequireNonAlphanumeric = false;
            //    o.Password.RequiredLength = 10;
            //    o.User.RequireUniqueEmail = true;
            //})
            //.AddEntityFrameworkStores<PortalDbContext>()
            //.AddDefaultTokenProviders();
        }

        public static void CustomMapping(this IApplicationBuilder app)
        {
            //TypeAdapterConfig<UserForRegistrationDto, User>
            //    .NewConfig()
            //    .Map(dest => dest.UserName, src => src.Email);
        }

        
    }
}
