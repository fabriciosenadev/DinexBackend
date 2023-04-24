using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dinex.Backend.WebApi.Controllers
{
    [EnableCors("_myAllowSpecificOrigins")]
    [ApiController]
    public class MainController : ControllerBase
    {
    }
}
