using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using randevuburada.BusinessLayer.Abstract;
using randevuburada.DtoLayer.Dtos.CustomerDto;

namespace Randevuburada.WebApi.Controller
{
    [Route("api/v1/test")]
    [ApiController]
    public class testController : ControllerBase
    {

        [HttpGet]
        [Route("get")]
        public IActionResult GetCompanyType()
        {
            return Ok("success");
        }
    }
}
