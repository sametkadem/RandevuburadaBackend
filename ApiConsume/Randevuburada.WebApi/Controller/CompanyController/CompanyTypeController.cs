using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using randevuburada.BusinessLayer.Abstract;
using randevuburada.DtoLayer.Dtos.CompanyDto.CompanyTypeDto;
using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using randevuburada.EntityLayer.Concrete.Identity;

namespace Randevuburada.WebApi.Controller.CompanyController
{
    [Route("api/v1/")]
    [ApiController]
    public class CompanyTypeController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly ICompanyTypeService _companyTypeService;

        public CompanyTypeController(ICompanyTypeService companyTypeService, IMapper mapper)
        {
            _companyTypeService = companyTypeService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("admin/company/type/set")]
        public IActionResult SetCompanyType(CompanyTypeAddDto companyTypeAddDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var companyType = _mapper.Map<CompanyType>(companyTypeAddDto);

            _companyTypeService.TInsert(companyType);

            var returnData = new
            {
                status = "success",
                message = "Kayıt başarıyla eklendi."
            };
            return Ok(returnData);
        }

        [HttpGet]
        [Route("company/type/get")]
        [Authorize(Roles = "Company")]
        public IActionResult GetCompanyType()
        {
            var companyType = _companyTypeService.TGetList();
            var returnData = new
            {
                status = "success",
                data = companyType
            };

            return Ok(returnData);
        }
    }
}
