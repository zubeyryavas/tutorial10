namespace tutorial10.Middlewares
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext http)
        {
            try
            {
                await _next(http);
            }
            catch
            {
                http.Response.StatusCode = 500;
                await http.Response.WriteAsync("Unexpected Error");
            }
        }
    }
}
