using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using randevuburada.BusinessLayer.Abstract;
using randevuburada.DtoLayer.Dtos.CompanyDto.CompanyServiceDto;
using randevuburada.DtoLayer.Dtos.CompanyDto.CompanyWorkingHoursDto;
using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using randevuburada.EntityLayer.Concrete.Identity;
using randevuburada.EntityLayer.Concrete.Other;

namespace Randevuburada.WebApi.Controller.CompanyController
{
    [Route("api/v1")]
    [ApiController]
    public class CompanyWorkingHoursController : ControllerBase
    {
        
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICompanySubscribeService _companySubscribeService;
        private readonly ICompanyService _companyService;
        private readonly ICompanyServiceService _companyServiceService;
        private readonly ICompanyWorkingHoursService _companyWorkingHoursService;
        private readonly IDayService _dayService;
        public CompanyWorkingHoursController(IMapper mapper, UserManager<AppUser> userManager, ICompanySubscribeService companySubscribeService, ICompanyService companyService, ICompanyServiceService companyServiceService, ICompanyWorkingHoursService companyWorkingHoursService, IDayService dayService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _companySubscribeService = companySubscribeService;
            _companyService = companyService;
            _companyServiceService = companyServiceService;
            _companyWorkingHoursService = companyWorkingHoursService;
            _dayService = dayService;
        }


        [HttpPost]
        [Route("company/workingHours/set")]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> SetCompanyWorkingHoursAsync(CompanyWorkingHoursAddDto companyWorkingHoursAddDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var company = _companyService.TGetByID(companyWorkingHoursAddDto.CompanyId);

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

            var userSubscribe = _companySubscribeService.TGetByUserID(user.Id);

            if (userSubscribe == null)
            {
                var returnNullUserSubscribeData = new
                {
                    status = "error",
                    message = "Kullanıcının herhangi bir aboneliği bulunamadı!"
                };
                return BadRequest(returnNullUserSubscribeData);
            }

            foreach (var workingDay in companyWorkingHoursAddDto.CompanyWorkingHours)
            {
                var workingDayData = new CompanyWorkingHours
                {
                    CompanyId = companyWorkingHoursAddDto.CompanyId,
                    DayId = workingDay.DayId,
                    OpenTime = TimeOnly.Parse(workingDay.StartTime),
                    CloseTime = TimeOnly.Parse(workingDay.EndTime),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                var insertOrUpdate = _companyWorkingHoursService.TupdateOrInsertCompanyWorkingHours(workingDayData);
                if(insertOrUpdate == null)
                {
                    var returnError = new
                    {
                        status = "error",
                        message = "İşletme çalışma saati bilgileri eklenirken bir hata oluştu."
                    };
                    return BadRequest(returnError);
                }

            }

            var returnSuccess = new
            {
                status = "success",
                message = "İşletme çalışma saati bilgileri başarıyla eklendi."
            };

            return Ok(returnSuccess);
        }


        [HttpGet]
        [Route("company/workingHours/list")]
        public async Task<IActionResult> GetCompanyWorkingHoursByCompanyIdAsync(int companyId)
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

            var companyWorkingHours = _companyWorkingHoursService.TGetByCompanyId(companyId);

            if (!companyWorkingHours.Any())
            {
                var returnData = new
                {
                    status = "error",
                    message = "İşletmenin herhangi bir çalışma saati kaydı bulunamadı!"
                };
                return BadRequest(returnData);
            }

            var successData = new
            {
                status = "success",
                data = companyWorkingHours
            };
            return Ok(successData);
        }

        [HttpGet]
        [Route("company/workingHours/getById")]
        public IActionResult GetCompanyWorkingHoursById(int id)
        {

            var companyWorkingHours = _companyWorkingHoursService.TGetByID(id);

            if (companyWorkingHours == null)
            {
                var returnData = new
                {
                    status = "error",
                    message = "İşletmenin ilgili id ile bir çalışma saat kaydı bulunamadı!"
                };
                return BadRequest(returnData);
            }

            var successData = new
            {
                status = "success",
                data = companyWorkingHours
            };
            return Ok(successData);
        }

        [HttpPost]
        [Route("company/workingHours/update")]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> UpdateCompanyWorkingHoursAsync(CompanyWorkingHoursUpdateDto companyWorkingHoursUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var companyWorkingHours = _mapper.Map<CompanyWorkingHours>(companyWorkingHoursUpdate);
            var company = _companyService.TGetByID(companyWorkingHours.CompanyId);
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

            var dayIds = companyWorkingHoursUpdate.DayIds;
            foreach (var dayId in companyWorkingHoursUpdate.DayIds)
            {
                var day = _dayService.TGetByID(dayId);
                if (day == null)
                {
                    var returnNullDayData = new
                    {
                        status = "error",
                        message = "Gün Bulunamadı!, Gün id: " + dayId
                    };
                    return BadRequest(returnNullDayData);
                }
            }

            companyWorkingHours.UpdatedAt = DateTime.Now;

            _companyWorkingHoursService.TUpdateByCompanyId(companyWorkingHours.CompanyId, companyWorkingHoursUpdate);

            var successData = new
            {
                status = "success",
                message = "İşletme hizmeti başarıyla güncellendi!"
            };
            return Ok(successData);
        }
    }
    
}
