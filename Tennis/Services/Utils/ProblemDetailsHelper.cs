using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

namespace Tennis.Services.Utils
{
    public static class ProblemDetailsHelper
    {
        public static ProblemDetails CreateProblemDetails(HttpContext httpContext, string detailMessage, int? httpStatusCode = null)
        {
            var problemDetails = new ProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                Title = "One or more validation errors occurred.",
                Status = httpStatusCode ?? httpContext.Response.StatusCode,
                Detail = detailMessage,
                Instance = httpContext.Request.Path
            };

            if (Activity.Current != null && Activity.Current.Id != null)
            {
                problemDetails.Extensions["traceId"] = Activity.Current.Id;
            }
            else
            {
                problemDetails.Extensions["traceId"] = "Trace ID not available";
            }

            return problemDetails;
        }
    }
}
