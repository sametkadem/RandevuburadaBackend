using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using randevuburada.DtoLayer.Dtos.CompanyDto.CompanyPackageDto;
using randevuburada.DtoLayer.Dtos.CustomerDto;
using AutoMapper;
using randevuburada.BusinessLayer.Abstract;
using randevuburada.EntityLayer.Concrete.CompanyConcrete;
namespace Randevuburada.WebApi.Controller.CompanyController
{
    [Route("api/admin")]
    [ApiController]
    public class CompanyPackageController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICompanyPackageService _companyPackageService;
        
        public CompanyPackageController(IMapper mapper, ICompanyPackageService companyPackageService)
        {
            _mapper = mapper;
            _companyPackageService = companyPackageService;
        }

        [HttpPost]
        [Route("add/package")]
        public IActionResult SetCompanyPackage(CompanyPackageAddDto companyPackageAddDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var companyPackage = _mapper.Map<CompanyPackage>(companyPackageAddDto);
            
            _companyPackageService.TInsert(companyPackage);
            return Ok();
        }

        [HttpGet]
        [Route("get/package")]
        public IActionResult GetCompanyPackage()
        {
            var companyPackage = _companyPackageService.TGetList();
            return Ok(companyPackage);
        }
    }
}
