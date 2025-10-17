using CoreBackend.Features.auth;

namespace CoreBackend.Features.middleWare
{
    public class JwtMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly JwtService _jwtService;

        public JwtMiddleWare(RequestDelegate next,JwtService jwtService)
        {
            _next = next;
            _jwtService = jwtService;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine($"[Middleware] Request: {context.Request.Method} {context.Request.Path}");

            // Extract token from Authorization header
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
            var token = authHeader?.StartsWith("Bearer ") == true
                ? authHeader.Substring("Bearer ".Length).Trim()
                : null;

            if (!string.IsNullOrEmpty(token))
            {
                // Validate token
                var principal = _jwtService.ValidateToken(token);
                if (principal != null)
                {
                    // Store the validated user (claims) in HttpContext
                    context.User = principal;
                }
            }
            Console.WriteLine(token);
            // Continue to next middleware
            await _next(context);

            Console.WriteLine($"[Middleware] Response: {context.Response.StatusCode}");
        }

    }
}
