using StaffServiceBLL.Exceptions;
using System.Net;

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
            catch (EmployeeNotFoundException ex)
            {
                await HandleExceptionAsync(httpContext, ex);

            }
            catch (NotManagerException ex) 
            {
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var statusCode = exception switch
            {
                EmployeeNotFoundException => (int)HttpStatusCode.NotFound,
                NotManagerException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };

            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsync(new ErrorDetails() 
            { 
                StatusCode = context.Response.StatusCode,
                Message = exception.Message 
            }.ToString()!);
        }
    }
}
