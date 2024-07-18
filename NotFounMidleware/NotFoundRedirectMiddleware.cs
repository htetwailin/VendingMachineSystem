namespace VendingMachineSystem.NotFounMidleware
{
    public class NotFoundRedirectMiddleware
    {
        private readonly RequestDelegate _next;

        public NotFoundRedirectMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            // Check if the response status is 404 (Not Found)
            if (context.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                // Redirect to the login page or another appropriate URL
                context.Response.Redirect("/Login/Index");
            }
        }
    }
}
