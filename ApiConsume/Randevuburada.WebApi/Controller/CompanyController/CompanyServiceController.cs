using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using randevuburada.BusinessLayer.Abstract;
using randevuburada.DtoLayer.Dtos.CompanyDto.CompanyServiceDto;
using randevuburada.DtoLayer.Dtos.CompanyDto.CompanyStaffDto;
using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using randevuburada.EntityLayer.Concrete.Identity;

namespace Randevuburada.WebApi.Controller.CompanyController
{
    [Route("api/v1")]
    [ApiController]
    public class CompanyServiceController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICompanySubscribeService _companySubscribeService;
        private readonly ICompanyService _companyService;
        private readonly ICompanyStaffService _staffService;
        private readonly ICompanyServiceService _companyServiceService;
        public CompanyServiceController(IMapper mapper, UserManager<AppUser> userManager, ICompanySubscribeService companySubscribeService, ICompanyService companyService, ICompanyStaffService staffService, ICompanyServiceService companyServiceService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _companySubscribeService = companySubscribeService;
            _companyService = companyService;
            _staffService = staffService;
            _companyServiceService = companyServiceService;
        }


        [HttpPost]
        [Route("company/service/set")]
        public async Task<IActionResult> SetCompanyServiceAsync(CompanyServiceAddDto companyServiceAddDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var companyService = _mapper.Map<CompanyService>(companyServiceAddDto);
            var companyId = companyService.CompanyId;

            var company = _companyService.TGetByID(companyId);

            if (company == null)
            {
                var returnNullCompanyData = new
                {
                    status = "error",
                    message = "İşletme Bulunamadı!"
                };
                return BadRequest(returnNullCompanyData);
            }

            var user = await _userManager.FindByIdAsync(company.UserId.ToString());

            if (user == null)
            {
                var returnNullUserData = new
                {
                    status = "error",
                    message = "Kullanıcı Bulunamadı!"
                };
                return BadRequest(returnNullUserData);
            }

            var userSubscribe = _companySubscribeService.TGetByUserID(company.UserId);
            if (userSubscribe == null)
            {
                var returnNullUserSubscribeData = new
                {
                    status = "error",
                    message = "Kullanıcının herhangi bir aboneliği bulunamadı!"
                };
                return BadRequest(returnNullUserSubscribeData);
            }

            _companyServiceService.TInsert(companyService);

            var returnSuccess = new
            {
                status = "success",
                message = "İşletme hizmet bilgileri başarıyla eklendi."
            };

            return Ok(returnSuccess);
        }

        [HttpGet]
        [Route("company/service/list")]
        public async Task<IActionResult> GetCompanyServiceByCompanyIdAsync(int companyId)
        {
            var company = _companyService.TGetByID(companyId);

            if (company == null)
            {
                var returnNullCompanyData = new
                {
                    status = "error",
                    message = "İşletme Bulunamadı!"
                };
                return BadRequest(returnNullCompanyData);
            }

            var user = await _userManager.FindByIdAsync(company.UserId.ToString());

            if (user == null)
            {
                var returnNullUserData = new
                {
                    status = "error",
                    message = "Kullanıcı Bulunamadı!"
                };
                return BadRequest(returnNullUserData);
            }

            var userSubscribe = _companySubscribeService.TGetByUserID(company.UserId);
            if (userSubscribe == null)
            {
                var returnNullUserSubscribeData = new
                {
                    status = "error",
                    message = "Kullanıcının herhangi bir aboneliği bulunamadı!"
                };
                return BadRequest(returnNullUserSubscribeData);
            }

            var companyService = _companyServiceService.TGetByCompanyId(companyId);

            if (!companyService.Any())
            {
                var returnData = new
                {
                    status = "error",
                    message = "İşletmenin herhangi bir hizmet kaydı bulunamadı!"
                };
                return BadRequest(returnData);
            }

            var successData = new
            {
                status = "success",
                data = companyService
            };
            return Ok(successData);
        }

        [HttpGet]
        [Route("company/service/getById")]
        public IActionResult GetCompanyServiceById(int id)
        {

            var companyService = _companyServiceService.TGetByID(id);

            if (companyService == null)
            {
                var returnData = new
                {
                    status = "error",
                    message = "İşletmenin ilgili id ile bir hizmet kaydı bulunamadı!"
                };
                return BadRequest(returnData);
            }

            var successData = new
            {
                status = "success",
                data = companyService
            };
            return Ok(successData);
        }

        [HttpPost]
        [Route("company/service/update")]
        public async Task<IActionResult> UpdateCompanyServiceAsync(CompanyServiceUpdateDto companyServiceUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var companyService = _mapper.Map<CompanyService>(companyServiceUpdateDto);
            var company = _companyService.TGetByID(companyService.CompanyId);
            if (company == null)
            {
                var returnData = new
                {
                    status = "error",
                    message = "İşletme bulunamadı!"
                };
                return BadRequest(returnData);
            }

            var user = await _userManager.FindByIdAsync(company.UserId.ToString());
            if (user == null)
            {
                var returnData = new
                {
                    status = "error",
                    message = "Kullanıcı bulunamadı!"
                };
                return BadRequest(returnData);
            }
            var control = _companyServiceService.TGetByID(companyService.Id);
            if (control == null)
            {
                var returnData = new
                {
                    status = "error",
                    message = "Güncellenecek kayıt bulunamadı!"
                };
                return BadRequest(returnData);
            }
            companyService.UpdatedAt = DateTime.Now;

            _companyServiceService.TUpdate(companyService);

            var successData = new
            {
                status = "success",
                message = "İşletme hizmeti başarıyla güncellendi!"
            };
            return Ok(successData);
        }

        [HttpPost]
        [Route("company/service/delete")]
        public IActionResult DeleteCompanyStaff(int id)
        {
            var companyStaff = _companyServiceService.TGetByID(id);

            if (companyStaff == null)
            {
                var returnData = new
                {
                    status = "error",
                    message = "Silinecek kayıt bulunamadı!"
                };
                return BadRequest(returnData);
            }

            _companyServiceService.TDelete(companyStaff);

            var successData = new
            {
                status = "success",
                message = "İşletme hizmeti başarıyla silindi!"
            };
            return Ok(successData);
        }
    }
}
