using Azure;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EWallet.API.Swagger.Filters;

public class RequiredHeaderParameter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter()
        {
            Name = "X-UserId",
            In = ParameterLocation.Header,
            Description = "user id",
            Schema = new OpenApiSchema() { Type = "string" },
            Required = false
        });
        
        operation.Parameters.Add(new OpenApiParameter()
        {
            Name = "X-Digest",
            In = ParameterLocation.Header,
            Description = "HMAC hash of the content",
            Schema = new OpenApiSchema() { Type = "string" },
            Required = false
        });
    }
}