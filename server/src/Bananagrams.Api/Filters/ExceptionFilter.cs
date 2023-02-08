using System.Diagnostics.CodeAnalysis;
using System.Net;
using Bananagrams.Service.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MimeTypes = System.Net.Mime.MediaTypeNames;

namespace Bananagrams.Api.Filters;

[ExcludeFromCodeCoverage]
public class ExceptionFilter : IExceptionFilter
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ExceptionFilter(IWebHostEnvironment webHostEnvironment) =>
        (_webHostEnvironment) = (webHostEnvironment);

    public void OnException(ExceptionContext context)
    {
        if (context == null) return;

        var response = context.HttpContext.Response;
        response.StatusCode = (int)RetrieveStatusCodeForException(context.Exception);
        response.ContentType = MimeTypes.Application.Json;
        context.Result = new JsonResult(new
        {
            error = new[] { context.Exception.Message },
            stackTrace = _webHostEnvironment.IsDevelopment() ? context.Exception.StackTrace : string.Empty,
        });
    }
        
    private static HttpStatusCode RetrieveStatusCodeForException(System.Exception exception)
    {
        if (exception is ArgumentException) return HttpStatusCode.BadRequest;
        if (exception is NotFoundException) return HttpStatusCode.NotFound; 

        return HttpStatusCode.InternalServerError;
    }
}