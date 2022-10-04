using StaffServiceBLL.Exceptions;
using System.Net;
using System.Security.Authentication;

namespace StaffServiceAPI.CustomExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next) 
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (AuthenticationException ex) 
            {
                await HandleExceptionAsync(httpContext, HttpStatusCode.BadRequest, ex);
            }
            catch (UnauthorizedException ex)
            {
                await HandleExceptionAsync(httpContext, HttpStatusCode.Unauthorized, ex);
            }
            catch (EmployeeNotFoundException ex)
            {
                await HandleExceptionAsync(httpContext, HttpStatusCode.NotFound, ex);
            }
            catch (NotManagerException ex)
            {
                await HandleExceptionAsync(httpContext, HttpStatusCode.BadRequest, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, HttpStatusCode.InternalServerError, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsync(new ErrorDetails() 
            { 
                StatusCode = context.Response.StatusCode,
                Message = exception.Message 
            }.ToString()!);
        }
    }
}
