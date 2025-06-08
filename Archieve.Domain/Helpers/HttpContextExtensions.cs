using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Archieve.Domain.Helpers
{
    public static class HttpContextExtensions
    {
        private static readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };


        public static async ValueTask WriteAccessDeniedResponse(
            this HttpContext context, string? title = null,
            int? statusCode = null,
            CancellationToken cancellationToken = default)
        {

            var problem = new ProblemDetails
            {
                Instance = context.Request.Path,
                Title = title ?? "Access denied",
                Status = statusCode ?? StatusCodes.Status403Forbidden
            };

            context.Response.StatusCode = problem.Status.Value;
            await context.Response.WriteAsync(JsonSerializer.Serialize(problem, jsonSerializerOptions), cancellationToken);
        }
    }
}
