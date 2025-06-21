using System.Net;
using System.Text.Json;
using Domain.Execptions;
using Microsoft.AspNetCore.Http;
using Services;
using Shared.ErrorModels;

namespace ECommerce.Web
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate Next)
        {
            _next = Next;
        }
        // 400 and upper => frontend errors
        // 500 and upper => backend errors

        // pass obj of ILoggerto handle the exception
        public async Task InvokeAsync(HttpContext httpContext, ILogger<CustomExceptionHandlerMiddleware> _logger)
        {

            try
            {
                await _next.Invoke(httpContext);// invoke the next middle ware 
                                                // i will check if response returned with 404 status  which is not exception thrown  
                await HandleNotFoundEndpointAsync(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Happend");
                await HandleExceptionAsync(httpContext, ex);

            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            //1
            //set status code for  error 
            //   httpContext.Response.StatusCode = 500;
            // httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;// here need casting as it is enum so we convert it to int 
            //httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var badRequestResponse = new ErrorToReturn()
            {
                ErrorMessage = ex.Message
            };
            httpContext.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,// NotFoundException class or ayn one inheret from 
                UnauthorizedAccessException =>StatusCodes.Status401Unauthorized,
                BadRequestException badRequestException  =>GetBadRequestException(badRequestException, badRequestResponse),
                _ => StatusCodes.Status500InternalServerError// as default (any thing other throw internal server error  )
            };


                 var Response= new ErrorToReturn()
                 {
                     StatusCode = httpContext.Response.StatusCode,

                     ErrorMessage = ex.Message
                 };
            //2
            //set content type for response 
            //   httpContext.Response.ContentType = "application/json";

            //3
            //response object dynamic 

            //4
            //return object as json (serialize)
            // var ResponseToReturn = JsonSerializer.Serialize(Response);
            //await  httpContext.Response.WriteAsync(ResponseToReturn);

            await httpContext.Response.WriteAsJsonAsync(Response);
        }

        private static int GetBadRequestException(BadRequestException badRequestException, ErrorToReturn response)
        {
            response.StatusCode = StatusCodes.Status400BadRequest;
            response.Errors=badRequestException.Errors;

            return StatusCodes.Status400BadRequest;
        }

        private static async Task HandleNotFoundEndpointAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var Response = new ErrorToReturn()
                {
                    StatusCode =httpContext.Response.StatusCode,
                    ErrorMessage = $"the EndPoint {httpContext.Request.Path} is Not Found "//Request.Path : return path after the url 
                };
                await httpContext.Response.WriteAsJsonAsync(Response);
            }
        }
    }
}
