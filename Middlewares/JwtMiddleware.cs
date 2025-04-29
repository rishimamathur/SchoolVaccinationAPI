namespace SchoolVaccinationAPI.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path.ToString();
            if (path.StartsWith("/api") && !path.Contains("login"))
            {
                var token = context.Request.Headers["Authorization"].ToString();
                if (token != "Bearer fake-jwt-token")
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Unauthorized");
                    return;
                }
            }
            await _next(context);
        }
    }
}
