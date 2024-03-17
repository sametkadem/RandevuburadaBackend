using AutoMapper;
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
        public async Task<IActionResult> SetCompanyWorkingHoursAsync(CompanyWorkingHoursAddDto companyWorkingHoursAddDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var companyWorkingHours = _mapper.Map<CompanyWorkingHours>(companyWorkingHoursAddDto);
            var companyId = companyWorkingHours.CompanyId;

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
            var userControl = await _userManager.GetUserAsync(HttpContext.User);

            /*
            if (user != userControl)
            {
                var returnNullUserData = new
                {
                    status = "error",
                    message = "Yetkisiz işlem!"
                };
                return BadRequest(returnNullUserData);
            }
            */

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

            var control = _companyWorkingHoursService.THasCompanyWorkingHours(companyId, companyWorkingHoursAddDto.DayIds);

            if(control != null || !control.Any())
            {
                var controlResultDays = new List<Dictionary<string, string>>();

                foreach (var item in control)
                {
                    var day = _dayService.TGetByID(item);
                    var dayAdd = new Dictionary<string, string>
                    {
                        { "dayName", day.DayName },
                        { "dayId", day.Id.ToString() }
                    };
                    controlResultDays.Add(dayAdd);

                }
                var returnData = new
                {
                    status = "error",
                    message = "İşletmenin ilgili günler için çalışma saatleri zaten tanımlı!",
                    data = controlResultDays
                };

            }

            companyWorkingHours.CreatedAt = DateTime.Now;
            companyWorkingHours.UpdatedAt = DateTime.Now;
            companyWorkingHours.Company = company;
            foreach (var dayId in companyWorkingHoursAddDto.DayIds)
            {
                companyWorkingHours.DayId = dayId;
                companyWorkingHours.Day = _dayService.TGetByID(dayId);
                _companyWorkingHoursService.TInsert(companyWorkingHours);
            }

            var returnSuccess = new
            {
                status = "success",
                message = "İşletme hizmet bilgileri başarıyla eklendi."
            };

            return Ok(returnSuccess);
        }

        [HttpGet]
        [Route("company/workingHours/list")]
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
        [Route("company/workingHours/getById")]
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
        [Route("company/workingHours/update")]
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
    }
}
