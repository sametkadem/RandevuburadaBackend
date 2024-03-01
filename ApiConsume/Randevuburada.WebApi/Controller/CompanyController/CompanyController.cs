using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Randevuburada.WebApi.Controller.CompanyController
{
    [Route("api/v1/company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        [HttpPost]
        [Route("add")]
        public IActionResult SetCompany()
        {
            return Ok();
        }

        [HttpGet]
        [Route("get")]
        public IActionResult GetCompany()
        {
            return Ok();
        }

        [HttpPost]
        [Route("update")]
        public IActionResult UpdateCompany()
        {
            return Ok();
        }
    }
}
