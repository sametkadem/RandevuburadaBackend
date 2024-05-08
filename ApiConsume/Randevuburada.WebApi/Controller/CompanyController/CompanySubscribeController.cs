using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using randevuburada.BusinessLayer.Abstract;
using randevuburada.DtoLayer.Dtos.CompanyDto.CompanySubscribeDto;
using randevuburada.DtoLayer.Dtos.CustomerDto;
using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using randevuburada.EntityLayer.Concrete.CustomerConcrete;
using randevuburada.EntityLayer.Concrete.Identity;

namespace Randevuburada.WebApi.Controller.CompanyController
{
    [Route("api/v1/company")]
    [ApiController]
    public class CompanySubscribeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICompanySubscribeService _companySubscribeService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICompanyPackageService _companyPackageService;

        public CompanySubscribeController(ICompanySubscribeService companySubscribeService, IMapper mapper, UserManager<AppUser> userManager, ICompanyPackageService companyPackageService)
        {
            _companySubscribeService = companySubscribeService;
            _mapper = mapper;
            _userManager = userManager;
            _companyPackageService = companyPackageService;
        }

        [HttpPost]
        [Route("subscribe/set")]
        public async Task<IActionResult> SetCompanySubscribeAsync(CompanySubscribeAddDto companySubscribeAddDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var companySubscribe = _mapper.Map<CompanySubscribe>(companySubscribeAddDto);

            int userId = companySubscribeAddDto.UserId;
            companySubscribe.User = await _userManager.FindByIdAsync(userId.ToString());
            
            if (companySubscribe.User == null)
            {
                var returnDataNull = new
                {
                    status = "error",
                    message = "Kullanıcı bulunamadı"
                };
                return BadRequest(returnDataNull);
            }

            var control = _companySubscribeService.TCheckCompany(userId);
            if(control)
            {
                var returnDataControl = new
                {
                    status = "error",
                    message = "Bu kullanıcının mevcutta kaydı vardır."
                };
                return BadRequest(returnDataControl);
            }

            var packageId = companySubscribe.companyPackageId;

            companySubscribe.Package = _companyPackageService.TGetByID(packageId);
            if (companySubscribe.Package == null)
            {
                var returnFailedPackage = new
                {
                    status = "error",
                    message = "İlgili yazılım paket bulunamamıştır."
                };

                return BadRequest(returnFailedPackage);
            }
            companySubscribe.CreatedAt = DateTime.Now;
            companySubscribe.UpdatedAt = companySubscribe.CreatedAt;
            companySubscribe.ExpirationDate = companySubscribe.CreatedAt.AddDays(30);
            _companySubscribeService.TInsert(companySubscribe);

            var returnData = new
            {
                status = "success",
                message = "Kayıt başarıyla eklendi."
            };
            return Ok(returnData);
        }

        [HttpPost]
        [Route("subscribe/update")]
        public async Task<IActionResult> UpdateCompanySubscribe(CompanySubscribeUpdateDto companySubscribeUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var companySubscribe = _mapper.Map<CompanySubscribe>(companySubscribeUpdateDto);

            int userId = companySubscribeUpdateDto.UserId;
            companySubscribe.User = await _userManager.FindByIdAsync(userId.ToString());

            if (companySubscribe.User == null)
            {
                var returnDataNull = new
                {
                    status = "error",
                    message = "Kullanıcı bulunamadı"
                };
                return BadRequest(returnDataNull);
            }
            var control = _companySubscribeService.TCheckCompany(userId);
            if (!control)
            {
                var returnData = new
                {
                    status = "error",
                    message = "İşletmenin mevcutta kaydı yoktur!"
                };
                return BadRequest(returnData);
            }

            var packageId = companySubscribeUpdateDto.companyPackageId;
            var controlPackage = companySubscribe.companyPackageId;

            companySubscribe.Package = _companyPackageService.TGetByID(packageId);
            if (companySubscribe.Package == null)
            {
                var returnFailedPackage = new
                {
                    status = "error",
                    message = "İlgili yazılım paket bulunamamıştır."
                };

                return BadRequest(returnFailedPackage);
            }
            companySubscribe.UpdatedAt = DateTime.Now;

            if (companySubscribe.companyPackageId == controlPackage)
            {
                companySubscribe.ExpirationDate = companySubscribe.ExpirationDate.AddDays(companySubscribe.Package.PackageDay);
            }
            else
            {
                companySubscribe.ExpirationDate = companySubscribe.UpdatedAt.AddDays(companySubscribe.Package.PackageDay);
            }

            _companySubscribeService.TUpdate(companySubscribe);
            var successData = new
            {
                status = "success",
                message = "İşletmenin bilgileri başarıyla güncellendi!"
            };
            return Ok(successData);
        }

        [HttpGet]
        [Route("subscribe/get")]
        public IActionResult GetCompanySubscribe(int userId)
        {
            var companySubscribe = _companySubscribeService.TGetByUserID(userId);
            if (companySubscribe == null)
            {
                var failedData = new
                {
                    status = "error",
                    message = "Kullanıcı bulunamadı!"
                };
                return BadRequest(failedData);
            }
            var companySubscribeDto = _mapper.Map<CompanySubscribeUpdateDto>(companySubscribe);

            if (companySubscribeDto == null)
            {
                var nullData = new
                {
                    status = "error",
                    message = "İlgili Id'ye göre herhangi bir kayıt bulunamadı."
                };
                return BadRequest(nullData);
            }

            var successData = new
            {
                status = "success",
                message = "Kullanıcının abonelik bilgileri başarıyla bulundu!",
                data = companySubscribeDto
            };
            return Ok(successData);
        }
    }
}
