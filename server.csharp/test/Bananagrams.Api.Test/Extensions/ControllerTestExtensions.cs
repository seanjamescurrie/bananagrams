using System.Diagnostics.CodeAnalysis;
using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Bananagrams.Api.Test.Extensions;

public static class ControllerTestExtensions
{
    public static T AssertObjectResult<T, TU>(this ActionResult<T> @this) where TU : ActionResult
    {
        @this.Result.Should().BeOfType<TU>();
        return (T)((ObjectResult)@this.Result).Value;
    }
        
    public static void AssertResult<T, TU>(this ActionResult<T> @this) where TU : ActionResult
    {
        @this.Result.Should().BeOfType<TU>();
        @this.Result.Should().NotBeAssignableTo<ObjectResult>();
    }

    public static void AssertResult<T>(this IActionResult @this, HttpStatusCode? statusCode = null) 
    {
        @this.Should().BeOfType<T>();
        @this.Should().NotBeAssignableTo<ObjectResult>();
        if (statusCode.HasValue && @this is StatusCodeResult result)
        {
            result.StatusCode.Should().Be((int)statusCode);
        }
    }
}