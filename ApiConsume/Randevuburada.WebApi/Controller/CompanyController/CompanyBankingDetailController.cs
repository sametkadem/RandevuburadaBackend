using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Randevuburada.WebApi.Controller.CompanyController
{
    [Route("api/company")]
    [ApiController]
    public class CompanyBankingDetailController : ControllerBase
    {

       
        [HttpPost]
        [Route("{companyId}/add/bankingDetail/")]
        public IActionResult SetCompanyBankingDetail()
        {
            return Ok();
        }

        [HttpGet]
        [Route("{companyId}/get/bankingDetail/")]
        public IActionResult GetCompanyBankingDetailById()
        {
            return Ok();
        }

        [HttpPost]
        [Route("{companyId}/update/bankingDetail")]
        public IActionResult UpdateCompanyBankingDetail()
        {
            return Ok();
        }


    }

}
