using Microsoft.AspNetCore.Mvc;

namespace TechCorp.ContentHub.API.Controllers;

public class ErrorController : Controller
{
    [Route("Error/{statusCode}")]
    public IActionResult HttpStatusCodeHandler(int statusCode)
    {
        switch (statusCode)
        {
            case 404:
                return View("NotFound");
            case 500:
                return View("Error");
            default:
                return View("Error");
        }
    }
}
