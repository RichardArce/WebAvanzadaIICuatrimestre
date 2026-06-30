using System.Net;

namespace WebAvanzadaIICuatrimestre.Middleware
{
    public class MiddlewareGlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MiddlewareGlobalExceptionHandler> _logger;

        public MiddlewareGlobalExceptionHandler(RequestDelegate next, ILogger<MiddlewareGlobalExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); //DONDE SE EJECUTA EL CONTROLADOR, SI OCURRE UN ERROR SE CAPTURA EN EL CATCH
            }
            catch (Exception ex)
            {              
                await HandleExceptionAsync(context, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "Ocurrió un error en la aplicación."); // Queda registrado en LOGS para analisis posterior

            ExceptionResponse response = exception switch
            {
                ArgumentNullException => new ExceptionResponse
                (
                    HttpStatusCode.BadRequest,
                    "El argumento proporcionado es nulo."
                ),
                UnauthorizedAccessException => new ExceptionResponse
                (
                    HttpStatusCode.Unauthorized,
                    "No tiene permiso para acceder a este recurso."
                ),
                NotImplementedException => new ExceptionResponse
                (
                    HttpStatusCode.NotImplemented,
                    "No se ha implementado esta funcionalidad"
                ),
                _ => new ExceptionResponse
                (
                    HttpStatusCode.InternalServerError,
                    "Ocurrió un error interno en el servidor."
                )
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)response.StatusCode;
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
