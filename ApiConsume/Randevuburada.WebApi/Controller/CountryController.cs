using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Randevuburada.WebApi.Model.AuthenticationModel;

namespace Randevuburada.WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Action()
        {
            return Ok("Merhaba");
        }
    }
}
