namespace challenge2.UserKeyValidatorsMiddleware
{
    public class UserKeyValidatorsMiddleware
    {
        private readonly RequestDelegate _next;
        public UserKeyValidatorsMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.Keys.Contains("x-authentication") || context.Request.Headers["x-authentication"] != "Vizion-Test")
            {
                context.Response.StatusCode = 401; //UnAuthorized                
                await context.Response.WriteAsync("Header Not Allow");
                return;
            }
            await _next.Invoke(context);
        }
    }
}