using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using randevuburada.BusinessLayer.Abstract;
using randevuburada.DtoLayer.Dtos.CompanyDto.CompanyOwnerInfoDto;
using randevuburada.DtoLayer.Dtos.CompanyDto.CompanySubscribeDto;
using randevuburada.DtoLayer.Dtos.CustomerDto;
using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using randevuburada.EntityLayer.Concrete.Identity;

namespace Randevuburada.WebApi.Controller.CompanyController
{
    [Route("api/v1/company")]
    [ApiController]
    
    public class CompanyOwnerInfoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICompanyOwnerInfoService _companyOwnerInfoService;
        private readonly UserManager<AppUser> _userManager;

        public CompanyOwnerInfoController(ICompanyOwnerInfoService companyOwnerInfoService, IMapper mapper, UserManager<AppUser> userManager)
        {
            _companyOwnerInfoService = companyOwnerInfoService;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("ownerInfo/set")]
        public async Task<IActionResult> SetCompanyOwnerInfo(CompanyOwnerInfoAddDto companyOwnerInfoAddDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var companyOwner = _mapper.Map<CompanyOwnerInfo>(companyOwnerInfoAddDto);
            int userId = companyOwner.UserId;

            companyOwner.User = await _userManager.FindByIdAsync(userId.ToString());
            if (companyOwner.User == null)
            {
                var returnNullUserData = new
                {
                    status = "error",
                    message = "Kullanıcı Bulunamadı!"
                };
                return BadRequest(returnNullUserData);
            }
            var control = _companyOwnerInfoService.TCheckCompany(userId);
            if (control)
            {
                var returnDataControl = new
                {
                    status = "error",
                    message = "Bu kullanıcının mevcutta kaydı vardır."
                };
                return BadRequest(returnDataControl);
            }
            
            companyOwner.CreatedAt = DateTime.Now;
            companyOwner.UpdatedAt = companyOwner.CreatedAt;
            _companyOwnerInfoService.TInsert(companyOwner);

            var returnData = new
            {
                status = "success",
                message = "Kayıt başarıyla eklendi."
            };
            return Ok(returnData);
        }

        [HttpPost]
        [Route("ownerInfo/update")]
        public async Task<IActionResult> UpdateCompanyOwnerInfo(CompanyOwnerInfoUpdateDto companyOwnerInfoUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var companyOwnerInfo = _mapper.Map<CompanyOwnerInfo>(companyOwnerInfoUpdateDto);

            int userId = companyOwnerInfoUpdateDto.UserId;
            companyOwnerInfo.User = await _userManager.FindByIdAsync(userId.ToString());

            if (companyOwnerInfo.User == null)
            {
                var returnDataNull = new
                {
                    status = "error",
                    message = "Kullanıcı bulunamadı"
                };
                return BadRequest(returnDataNull);
            }
            var control = _companyOwnerInfoService.TCheckCompany(userId);
            if (!control)
            {
                var returnData = new
                {
                    status = "error",
                    message = "İşletmenin mevcutta kaydı yoktur!"
                };
                return BadRequest(returnData);
            }

            companyOwnerInfo.UpdatedAt = DateTime.Now;
            _companyOwnerInfoService.TUpdate(companyOwnerInfo);
            var successData = new
            {
                status = "success",
                message = "İşletmenin bilgileri başarıyla güncellendi!"
            };
            return Ok(successData);
        }

        [HttpGet]
        [Route("ownerInfo/get/byUserId")]
        public IActionResult GetCompanySubscribe(int userId)
        {
            var companyOwnerInfo = _companyOwnerInfoService.TGetByUserID(userId);
            if (companyOwnerInfo == null)
            {
                var failedData = new
                {
                    status = "error",
                    message = "Kullanıcı bulunamadı!"
                };
                return BadRequest(failedData);
            }
            var companyOwnerInfoDto = _mapper.Map<CompanyOwnerInfoUpdateDto>(companyOwnerInfo);

            if (companyOwnerInfoDto == null)
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
                data = companyOwnerInfoDto
            };
            return Ok(successData);
        }


    }
}
