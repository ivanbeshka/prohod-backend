using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Prohod.WebApi.Configuration;

public static class SwaggerRegistrar
{
    public static IServiceCollection AddSwagger(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSwaggerGen(ConfigureSwaggerGenOptions);
    }

    private static void ConfigureSwaggerGenOptions(SwaggerGenOptions options)
    {
        var jwtSecurityScheme = new OpenApiSecurityScheme
        {
            Scheme = "Bearer",
            BearerFormat = "JWT",
            Name = "JWT Authentication",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Description = "Put your JWT Bearer token in textbox **(without quotes)**",

            Reference = new OpenApiReference
            {
                Id = JwtBearerDefaults.AuthenticationScheme,
                Type = ReferenceType.SecurityScheme
            }
        };

        options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            { jwtSecurityScheme, Array.Empty<string>() }
        });
        
        options.SupportNonNullableReferenceTypes();
    }
}