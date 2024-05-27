using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using randevuburada.BusinessLayer.Abstract;
using randevuburada.DtoLayer.Dtos.CompanyDto.CompanyBankingDetailDto;
using randevuburada.DtoLayer.Dtos.CompanyDto.CompanyStaffDto;
using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using randevuburada.EntityLayer.Concrete.Identity;

namespace Randevuburada.WebApi.Controller.CompanyController
{
    [Route("api/v1")]
    [ApiController]
    public class CompanyStaffController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICompanySubscribeService _companySubscribeService;
        private readonly ICompanyService _companyService;
        private readonly ICompanyStaffService _staffService;
        private readonly IStaffWorkingPositionService _staffWorkingPositionService;
        private readonly IStaffWorkingStatusService _staffWorkingStatusService;
        public CompanyStaffController(IMapper mapper, UserManager<AppUser> userManager, ICompanySubscribeService companySubscribeService, ICompanyService companyService, ICompanyStaffService staffService, IStaffWorkingPositionService staffWorkingPositionService, IStaffWorkingStatusService staffWorkingStatusService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _companySubscribeService = companySubscribeService;
            _companyService = companyService;
            _staffService = staffService;
            _staffWorkingPositionService = staffWorkingPositionService;
            _staffWorkingStatusService = staffWorkingStatusService;           
        }


        [HttpPost]
        [Route("company/staff/set")]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> SetCompanyStaffAsync(CompanyStaffAddDto companyStaffAddDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var companyStaff = _mapper.Map<CompanyStaff>(companyStaffAddDto);
            var companyId = companyStaff.CompanyId;

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

            _staffService.TInsert(companyStaff);

            var returnSuccess = new
            {
                status = "success",
                message = "İşletme çalışan bilgileri başarıyla kayıt edildi."
            };

            return Ok(returnSuccess);
        }

        [HttpGet]
        [Route("company/staff/list")]
        public async Task<IActionResult> GetCompanyStaffByCompanyIdAsync(int companyId)
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

            var companyStaff = _staffService.TGetByCompanyId(companyId);

            if (!companyStaff.Any())
            {
                var returnData = new
                {
                    status = "error",
                    message = "İşletmenin herhangi bir personel kaydı bulunamadı!"
                };
                return BadRequest(returnData);
            }

            var objectList = companyStaff.Select(item =>
            {
                var workingPositionName = _staffWorkingPositionService.TGetByID(item.StaffWorkingPositionId).PositionName;
                var workingStatusName = _staffWorkingStatusService.TGetByID(item.StaffWorkingStatusId).StatusName;

                return new
                {
                    item.Id,
                    item.CompanyId,
                    item.StaffWorkingPositionId,
                    WorkingPositionName = workingPositionName,
                    WorkingStatusName = workingStatusName,
                    item.StaffWorkingStatusId,
                    item.ProfilPicture,
                    item.FirstName,
                    item.LastName,
                    item.PhoneNumber,
                    item.Email,
                    item.Tc,
                    item.BirthDate
                };
            }).ToList();

            var successData = new
            {
                status = "success",
                data = objectList
            };
            return Ok(successData);
        }

        [HttpGet]
        [Route("company/staff/getById")]
        public IActionResult GetCompanyStaffById(int id)
        {

            var companyStaff = _staffService.TGetByID(id);

            if (companyStaff == null)
            {
                var returnData = new
                {
                    status = "error",
                    message = "İşletmenin ilgili id ile bir personel kaydı bulunamadı!"
                };
                return BadRequest(returnData);
            }

            var successData = new
            {
                status = "success",
                data = companyStaff
            };
            return Ok(successData);
        }

        [HttpPost]
        [Route("company/staff/update")]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> UpdateCompanyStaffAsync(CompanyStaffUpdateDto companyStaffUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var companyStaff = _mapper.Map<CompanyStaff>(companyStaffUpdateDto);
            var company = _companyService.TGetByID(companyStaff.CompanyId);

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
            var control = _staffService.TGetByID(companyStaff.Id);
            if (control == null)
            {
                var returnData = new
                {
                    status = "error",
                    message = "Güncellenecek kayıt bulunamadı!"
                };
                return BadRequest(returnData);
            }
            companyStaff.UpdatedAt = DateTime.Now;
            var update = _staffService.TupdateCompanyStaff(companyStaff);
            if(update == null)
            {
                var returnData = new
                {
                    status = "error",
                    message = "Personel bilgileri güncellenirken bir hata oluştu!"
                };
                return BadRequest(returnData);
            }
            var successData = new
            {
                status = "success",
                message = "Personel bilgileri başarıyla güncellendi!"
            };
            return Ok(successData);
        }

        [HttpPost]
        [Route("company/staff/delete")]
        [Authorize(Roles = "Company")]
        public IActionResult DeleteCompanyStaff(int id)
        {
            var companyStaff = _staffService.TGetByID(id);

            if (companyStaff == null)
            {
                var returnData = new
                {
                    status = "error",
                    message = "Silinecek kayıt bulunamadı!"
                };
                return BadRequest(returnData);
            }

            _staffService.TDelete(companyStaff);

            var successData = new
            {
                status = "success",
                message = "Personel bilgileri başarıyla silindi!"
            };
            return Ok(successData);
        }
    }
}
