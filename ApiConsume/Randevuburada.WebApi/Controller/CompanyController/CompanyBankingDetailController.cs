using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using randevuburada.BusinessLayer.Abstract;
using randevuburada.DtoLayer.Dtos.CompanyDto.CompanyBankingDetailDto;
using randevuburada.DtoLayer.Dtos.CompanyDto.CompanyDto;
using randevuburada.DtoLayer.Dtos.CompanyDto.CompanyOwnerInfoDto;
using randevuburada.DtoLayer.Dtos.CustomerDto;
using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using randevuburada.EntityLayer.Concrete.Identity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Randevuburada.WebApi.Controller.CompanyController
{
    [Route("api/v1/company")]
    [ApiController]
    public class CompanyBankingDetailController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICompanyBankingDetailsService _companyBankingDetailsService;
        private readonly ICompanySubscribeService _companySubscribeService;
        private readonly ICompanyPackageService _companyPackageService;
        public CompanyBankingDetailController(ICompanyBankingDetailsService companyBankingDetailsService, IMapper mapper, UserManager<AppUser> userManager, ICompanySubscribeService companySubscribeService, ICompanyPackageService companyPackageService)
        {
            _mapper = mapper;
            _companyBankingDetailsService = companyBankingDetailsService;
            _userManager = userManager;
            _companySubscribeService = companySubscribeService;
            _companyPackageService = companyPackageService;
        }


        [HttpPost]
        [Route("bankingDetail/set")]
        [Authorize(Roles = "Company")]

        public async Task<IActionResult> SetCompanyBankingDetailAsync(CompanyBankingDetailDtoAdd companyBankingDetailDtoAdd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var companyBankingDetail = _mapper.Map<CompanyBankingDetails>(companyBankingDetailDtoAdd);
            var userId = companyBankingDetail.UserId;

            companyBankingDetail.User = await _userManager.FindByIdAsync(userId.ToString());
            if (companyBankingDetail.User == null)
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

            var countBankDetailse = _companyBankingDetailsService.TGetCountCompanyByUserId(userId);
            
            _companyBankingDetailsService.TInsert(companyBankingDetail);

            var returnSuccess = new
            {
                status = "success",
                message = "Banka bilgileri başarıyla kayıt edildi."
            };

            return Ok(returnSuccess);
        }

        [HttpGet]
        [Route("bankingDetail/list/byUserId")]
        [Authorize(Roles = "Company")]

        public async Task<IActionResult> GetCompanyBankingDetailByIdAsync(int userId)
        {

            var User = await _userManager.FindByIdAsync(userId.ToString());
            if (User == null)
            {
                var returnNullUserData = new
                {
                    status = "error",
                    message = "Kullanıcı Bulunamadı!"
                };
                return BadRequest(returnNullUserData);
            }
            
            var companyBankingDetail = _companyBankingDetailsService.TGetByUserID(userId);
            if (!companyBankingDetail.Any())
            {
                var returnData = new
                {
                    status = "error",
                    message = "Kayıt bulunamadı!"
                };
                return BadRequest(returnData);
            }
           
            var successData = new
            {
                status = "success",
                message = "Kullanıcının banka bilgileri başarıyla bulundu!",
                data = companyBankingDetail
            };
            return Ok(successData);
        }

        [HttpPost]
        [Route("bankingDetail/update")]
        [Authorize(Roles = "Company")]

        public async Task<IActionResult> UpdateCompanyBankingDetailAsync(CompanyBankingDetailDtoUpdate companyBankingDetailDtoUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var companyBankingDetails = _mapper.Map<CompanyBankingDetails>(companyBankingDetailDtoUpdate);
            int userId = companyBankingDetails.UserId;
            companyBankingDetails.User = await _userManager.FindByIdAsync(userId.ToString());
            if (companyBankingDetails.User == null)
            {
                var returnData = new
                {
                    status = "error",
                    message = "Kullanıcı bulunamadı!"
                };
                return BadRequest(returnData);
            }
            var control = _companyBankingDetailsService.TGetByID(companyBankingDetails.Id);
            if (control == null)
            {
                var returnData = new
                {
                    status = "error",
                    message = "Güncellenecek kayıt bulunamadı!"
                };
                return BadRequest(returnData);
            }
            companyBankingDetails.UpdatedAt = DateTime.Now;

            _companyBankingDetailsService.TUpdate(companyBankingDetails);

            var successData = new
            {
                status = "success",
                message = "Kullanıcı bilgileri başarıyla güncellendi!"
            };
            return Ok(successData);
        }
    }
}
