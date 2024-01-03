using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
//using Swashbuckle.AspNetCore.SwaggerGen;


namespace CandidatePortal.Server.Extensions
{
    public class ConfigureSwaggerOptions
    : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider provider;

        public ConfigureSwaggerOptions(
            IApiVersionDescriptionProvider provider)
        {
            this.provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            // add swagger document for every API version discovered
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                    description.GroupName,
                    CreateVersionInfo(description));
            }
        }

        public void Configure(string name, SwaggerGenOptions options)
        {
            Configure(options);
        }

        private OpenApiInfo CreateVersionInfo(
                ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "Radix API",
                Version = description.ApiVersion.ToString()
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }
    }

    //public static class SwaggerConfiguration 
    //{
    //    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    //    {
    //        if (services == null)
    //        {
    //            throw new ArgumentNullException(nameof(services));
    //        }

    //        services.AddVersionedApiExplorer(x =>
    //        {
    //            x.AssumeDefaultVersionWhenUnspecified = true;
    //            x.DefaultApiVersion = new ApiVersion(1, 0);
    //            x.GroupNameFormat = "v'VVV";
    //            x.SubstituteApiVersionInUrl = true;

    //        });

    //        services.AddApiVersioning(x =>
    //        {
    //            x.ReportApiVersions = true;
    //            x.AssumeDefaultVersionWhenUnspecified = true;
    //            x.DefaultApiVersion = new ApiVersion(1, 0);
    //        });

    //        //string serviceDescription = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "ServiceDescription.md"));

    //        services.AddSwaggerGen(c =>
    //        {
    //            c.EnableAnnotations();

    //            c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

    //            var provider =
    //                services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

    //            foreach (ApiVersionDescription desc in provider.ApiVersionDescriptions)
    //            {
    //                c.SwaggerDoc(desc.GroupName, CreateInfoForApiVersion(desc));
    //            }
    //            //c.SwaggerDoc("v1", new OpenApiInfo { Title = "FeedbackService", Version = "v1", Description = serviceDescription});
    //            string xmlFile = $"{typeof(SwaggerConfiguration).Assembly.GetName().Name}.xml";

    //            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
    //            c.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["controller"]}_{e.ActionDescriptor.RouteValues["action"]}");
    //        });

    //        return services;
    //    }

    //    public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
    //    {

    //        if (app == null)
    //        {
    //            throw new ArgumentNullException(nameof(app));
    //        }

    //        app.UseSwagger(x => x.RouteTemplate = $"swagger/{ApiConstants.ServiceName}/{{documentName}}/swagger.json");

    //        app.UseSwaggerUI(c =>
    //        {
    //            c.RoutePrefix = $"swagger/{ApiConstants.ServiceName}";

    //            foreach (ApiVersionDescription desc in provider.ApiVersionDescriptions)
    //            {
    //                c.SwaggerEndpoint($"{desc.GroupName}/swagger.json", desc.GroupName.ToUpperInvariant());
    //            }
    //            //c.SwaggerEndpoint("/swagger/v1/swagger.json", "FeedbackService.Api v1")});
    //        });

    //        return app;
    //    }

    //    private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    //    {
    //        //string serviceDescription = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "ServiceDescription.md"));

    //        var info = new OpenApiInfo
    //        {
    //            //Title = "Six Ladders API",
    //            Title = $"{ApiConstants.FriendlyServiceName} API {description.ApiVersion}",
    //            Version = description.ApiVersion.ToString(),
    //            //Description = serviceDescription
    //        };

    //        if (description.IsDeprecated)
    //        {
    //            info.Description += $"{Environment.NewLine} This API version has been deprecated";
    //        }

    //        return info;
    //    }
    //}
}
