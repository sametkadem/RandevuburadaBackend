using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using randevuburada.BusinessLayer.Abstract;
using randevuburada.DtoLayer.Dtos.CompanyDto.CompanyDto;
using randevuburada.DtoLayer.Dtos.CompanyDto.CompanyOwnerInfoDto;
using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using randevuburada.EntityLayer.Concrete.Identity;
using System.Net.NetworkInformation;

namespace Randevuburada.WebApi.Controller.CompanyController
{

    [Route("api/v1/company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICompanySubscribeService _companySubscribeService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICompanyPackageService _companyPackageService;
        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService, ICompanyPackageService companyPackageService, IMapper mapper, UserManager<AppUser> userManager, ICompanySubscribeService companySubscribeService)
        {
            _mapper = mapper;
            _companyService = companyService;
            _companyPackageService = companyPackageService;
            _userManager = userManager;
            _companySubscribeService = companySubscribeService;
        }

        [HttpPost]
        [Route("set")]
        public async Task<IActionResult> SetCompanyAsync(CompanyAddDto companyAddDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var company = _mapper.Map<Company>(companyAddDto);
            int userId = company.UserId;

            company.User = await _userManager.FindByIdAsync(userId.ToString());
            if (company.User == null)
            {
                var returnNullUserData = new
                {
                    status = "error",
                    message = "Kullanıcı Bulunamadı!"
                };
                return BadRequest(returnNullUserData);
            }

            var userSubscribe = _companySubscribeService.TGetByUserID(userId);
            
            if(userSubscribe == null)
            {
                var returnNullUserSubscribeData = new
                {
                    status = "error",
                    message = "Kullanıcının herhangi bir aboneliği bulunamadı!"
                };
                return BadRequest(returnNullUserSubscribeData);
            }

            var countCompany = _companyService.TGetCountCompanyByUserId(userId);

            var package = _companyPackageService.TGetByID(userSubscribe.Id);
            if (package == null)
            {
                var returnError1 = new
                {
                    status = "error",
                    message = "Destek ile iletişime geçiniz!"
                };
                return BadRequest(returnError1);
            }

            if(countCompany > package.MaxBranch)
            {
                var returnError2 = new
                {
                    status = "error",
                    message = "Mevcut aboneliğinizde bulunan işletme sayısına ulaştınız!"
                };
                return BadRequest(returnError2);
            }

            company.CreatedAt = DateTime.Now;
            company.UpdatedAt = company.CreatedAt;
            company.CompanyStatus = true;
            company.CompanyVisibility = true;

            _companyService.TInsert(company);

            var returnSuccess = new
            {
                status = "success",
                message = "İşletme başarıyla kayıt edildi."
            };
            return Ok(returnSuccess);
        }

        [HttpGet]
        [Route("list/byUserId")]
        public async Task<IActionResult> GetCompanyByUserIdAsync(int userId)
        {
            var company = _companyService.TGetByUserID(userId);
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                var returnNullUserData = new
                {
                    status = "error",
                    message = "Kullanıcı Bulunamadı!"
                };
                return BadRequest(returnNullUserData);
            }
            if (company == null || !company.Any())
            {
                var returnNullCompanyData = new
                {
                    status = "error",
                    message = "İşletme Bulunamadı!"
                };
                return BadRequest(returnNullCompanyData);
            }
            var returnData = new
            {
                status = "success",
                data = company
            };
            return Ok(returnData);
        }

        [HttpGet]
        [Route("list/location/cityAndDistrict")]
        public async Task<IActionResult> GetCompanyBycityAndDistrictAsync(int cityId = 0, int districtId = 0)
        {
            int countryId = 1; // Türkiye
            var company = _companyService.TGetCountryCityDistrictCompany(countryId, cityId, districtId);
            if (company == null || !company.Any())
            {
                var returnNullCompanyData = new
                {
                    status = "error",
                    message = "İşletme Bulunamadı!"
                };
                return BadRequest(returnNullCompanyData);
            }
            var returnData = new
            {
                status = "success",
                data = company
            };
            return Ok(returnData);
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateCompanyAsync(CompanyUpdateDto companyUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var company = _mapper.Map<Company>(companyUpdateDto);
            int userId = company.UserId;

            company.User = await _userManager.FindByIdAsync(userId.ToString());
            if (company.User == null)
            {
                var returnNullUserData = new
                {
                    status = "error",
                    message = "Kullanıcı Bulunamadı!"
                };
                return BadRequest(returnNullUserData);
            }

            var userSubscribe = _companySubscribeService.TGetByUserID(userId);

            if (userSubscribe == null)
            {
                var returnNullUserSubscribeData = new
                {
                    status = "error",
                    message = "Kullanıcının herhangi bir aboneliği bulunamadı!"
                };
                return BadRequest(returnNullUserSubscribeData);
            }


            var package = _companyPackageService.TGetByID(userSubscribe.Id);
            if (package == null)
            {
                var returnError1 = new
                {
                    status = "error",
                    message = "Destek ile iletişime geçiniz!"
                };
                return BadRequest(returnError1);
            }

          
            company.UpdatedAt = company.CreatedAt;
            company.CompanyStatus = true;
            company.CompanyVisibility = true;

            var control = _companyService.TGetByID(company.Id);
            if (control == null)
            {
                var returnData = new
                {
                    status = "error",
                    message = "İşletmenin mevcutta kaydı yoktur!"
                };
                return BadRequest(returnData);
            }

            company.UpdatedAt = DateTime.Now;
            _companyService.TUpdate(company);
            var successData = new
            {
                status = "success",
                message = "İşletmenin mevcutta bilgileri başarıyla güncellendi!"
            };
            return Ok(successData);
        }

        [HttpGet]
        [Route("delete")]
        public async Task<IActionResult> DeleteCompany(int userId, int companyId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userControl = await _userManager.FindByIdAsync(userId.ToString());

            if(user != userControl)
            {
                var returnNullUserData = new
                {
                    status = "error",
                    message = "Yetkisiz işlem!"
                };
                return BadRequest(returnNullUserData);
            }

            if (userControl == null)
            {
                var returnNullUserData = new
                {
                    status = "error",
                    message = "Kullanıcı Bulunamadı!"
                };
                return BadRequest(returnNullUserData);
            }

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

            company.CompanyStatus = false;
            company.CompanyVisibility = false;
            company.UpdatedAt = DateTime.Now;

            _companyService.TUpdate(company);

            var returnData = new
            {
                status = "success",
                message = "İşletme başarıyla silindi!"
            };
           
            return Ok(returnData);
        }


    }
}
