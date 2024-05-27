using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using randevuburada.BusinessLayer.Abstract;
using randevuburada.DtoLayer.Dtos.CompanyDto.CompanyDto;
using randevuburada.DtoLayer.Dtos.CompanyDto.CompanyOwnerInfoDto;
using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using randevuburada.EntityLayer.Concrete.Identity;
using randevuburada.EntityLayer.Concrete.Location;
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
        private readonly ICityService _cityService;
        private readonly IDistrictService _districtService;
        private readonly ICompanyTypeService _companyTypeService;

        public CompanyController(ICompanyService companyService, ICompanyPackageService companyPackageService, IMapper mapper, UserManager<AppUser> userManager, ICompanySubscribeService companySubscribeService, ICityService cityService, IDistrictService districtService, ICompanyTypeService companyTypeService)
        {
            _mapper = mapper;
            _companyService = companyService;
            _companyPackageService = companyPackageService;
            _userManager = userManager;
            _companySubscribeService = companySubscribeService;
            _cityService = cityService;
            _districtService = districtService;
            _companyTypeService = companyTypeService;
        }

        [HttpPost]
        [Route("set")]
        [Authorize(Roles = "Company")]
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

            var package = _companyPackageService.TGetByID(userSubscribe.companyPackageId);
            if (package == null)
            {
                var returnError1 = new
                {
                    status = "error",
                    message = "Destek ile iletişime geçiniz!"
                };
                return BadRequest(returnError1);
            }

            if(countCompany >= package.MaxBranch)
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

            foreach (var item in company)
            {
                var city = new City { CityName = _cityService.TGetByID(item.CityId)?.CityName };
                item.City = city;

                var district = new District { DistrictName = _districtService.TGetByID(item.DistrictId)?.DistrictName };
                item.District = district;
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
        public IActionResult GetCompanyBycityAndDistrict(int cityId = 0, int districtId = 0)
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
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> UpdateCompanyAsync(CompanyUpdateDto companyUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var company = _companyService.TGetByID(companyUpdateDto.Id);
            if (company == null)
            {
                var returnData = new
                {
                    status = "error",
                    message = "İşletmenin kaydı bulunamadı!"
                };
                return BadRequest(returnData);
            }

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

            var package = _companyPackageService.TGetByID(userSubscribe.companyPackageId);
            if (package == null)
            {
                var returnError1 = new
                {
                    status = "error",
                    message = "Destek ile iletişime geçiniz!"
                };
                return BadRequest(returnError1);
            }

            company.UpdatedAt = DateTime.Now;

            var updateCompany = _companyService.TupdateCompany(company);
            if (updateCompany == null)
            {
                var returnError2 = new
                {
                    status = "error",
                    message = "İşletme güncellenirken bir hata oluştu!"
                };
                return BadRequest(returnError2);
            }

            var successData = new
            {
                status = "success",
                message = "İşletmenin mevcut bilgileri başarıyla güncellendi!"
            };
            return Ok(successData);
        }

        [HttpGet]
        [Route("delete")]
        [Authorize(Roles = "Company")]
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

        [HttpGet]
        [Route("list/byCompanyId")]
        [Authorize(Roles = "Company")]
        public IActionResult GetCompanyById(int companyId)
        {
            try
            {
                var company = _companyService.TGetByID(companyId);
                if (company == null)
                {
                    var returnData = new
                    {
                        status = "error",
                        message = "İşletme bulunamadı!"
                    };
                    return BadRequest(returnData);
                }
                var returnsData = new
                {
                    status = "success",
                    data = company
                };
                return Ok(returnsData);
            }
            catch (Exception ex)
            {
                var returnData = new
                {
                    status = "error",
                    message = ex.Message
                };
                return BadRequest(returnData);
            }
        }
    }


}
