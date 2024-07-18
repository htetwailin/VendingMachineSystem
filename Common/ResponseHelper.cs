using Microsoft.AspNetCore.Mvc;

namespace VendingMachineSystem.Common
{
    public static class ResponseHelper
    {
        public static IActionResult CustomMessage(this ControllerBase controller, string message, int statusCode)
        {
            return new ObjectResult(message)
            {
                StatusCode = statusCode
            };
        }
    }
}
