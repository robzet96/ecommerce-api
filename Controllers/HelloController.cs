using Microsoft.AspNetCore.Mvc;

namespace ecommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HelloController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Hello, world!");
    }
}
