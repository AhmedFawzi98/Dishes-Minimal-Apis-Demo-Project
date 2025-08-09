using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace DishesMinimalApi.OpenApiConfigurations;
public class DocumentInfoTransformer(IApiVersionDescriptionProvider _provider) : IOpenApiDocumentTransformer
{

    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        var versionDescription = _provider.ApiVersionDescriptions
           .FirstOrDefault(desc => desc.GroupName == context.DocumentName);

        if (versionDescription != null)
        {
            document.Info = new OpenApiInfo
            {
                Version = $"version {versionDescription.ApiVersion}",
                Title = "Dishes API",
                Description = "a minimal Web API using .net core 9",
                Contact = new OpenApiContact
                {
                    Name = "Ahmed Fawzi",
                    Email = "ahmedfawzielarabi98@gmail.com",
                }
            };
        }

        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes = new Dictionary<string, OpenApiSecurityScheme>
        {
            ["ApiKeyScehme"] = new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Name = "Api-Key",
                Description = "enter Api-Key needed to access the endpoints. API-KEY: your-api-key"
            }
        };

        // Add global security requirement
        document.SecurityRequirements.Add(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "ApiKeyScehme"
                    }
                },
                Array.Empty<string>()
            }
        });

        return Task.CompletedTask;
    }
}