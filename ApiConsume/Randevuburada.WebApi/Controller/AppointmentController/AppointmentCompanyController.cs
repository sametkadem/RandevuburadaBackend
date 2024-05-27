using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using randevuburada.BusinessLayer.Abstract;
using randevuburada.DtoLayer.Dtos.AppointmentDto.AppointmentCompanyDto;
using randevuburada.EntityLayer.Concrete.Identity;
using Randevuburada.WebApi.Model.MailModel;

namespace Randevuburada.WebApi.Controller.AppointmentController
{
    [Route("api/v1")]
    [ApiController]
    public class AppointmentCompanyController : ControllerBase
    {
        private readonly IGeneralAppointmentService _generalAppointment;
        private readonly IAppointmentCompanyInfoService _appointmentCompanyInfoService;
        private readonly IAppointmentStatusService _appointmentStatusService;
        private readonly ICompanyService _companyService;
        private readonly IPaymentTypeService _paymentTypeService;
        private readonly IAppointmentInfoService _appointmentInfoService;
        private readonly ICompanyWorkingHoursService _companyWorkingHoursService;
        private readonly ICompanyServiceService _companyServiceService;
        private readonly IServiceIntervalHoursService _serviceIntervalHoursService;
        private readonly ICustomerAppointmentInfoService _customerAppointmentInfoService;
        private readonly ICustomerBillingInfoService _customerBillingInfoService;
        private readonly ICustomerService _customerService;
        private readonly ICompanyStaffService _companyStaffService;
        private readonly UserManager<AppUser> _userManager;

        public AppointmentCompanyController(IGeneralAppointmentService generalAppointment, IAppointmentCompanyInfoService appointmentCompanyInfoService, IAppointmentStatusService appointmentStatusService, ICompanyService companyService, IPaymentTypeService paymentTypeService, IAppointmentInfoService appointmentInfoService, ICompanyWorkingHoursService companyWorkingHoursService, ICompanyServiceService companyServiceService, IServiceIntervalHoursService serviceIntervalHoursService, ICustomerAppointmentInfoService customerAppointmentInfoService, ICustomerBillingInfoService customerBillingInfoService, ICustomerService customerService, ICompanyStaffService companyStaffService, UserManager<AppUser> userManager)
        {
            _generalAppointment = generalAppointment;
            _appointmentCompanyInfoService = appointmentCompanyInfoService;
            _appointmentStatusService = appointmentStatusService;
            _companyService = companyService;
            _paymentTypeService = paymentTypeService;
            _appointmentInfoService = appointmentInfoService;
            _companyWorkingHoursService = companyWorkingHoursService;
            _companyServiceService = companyServiceService;
            _serviceIntervalHoursService = serviceIntervalHoursService;
            _customerAppointmentInfoService = customerAppointmentInfoService;
            _customerBillingInfoService = customerBillingInfoService;
            _customerService = customerService;
            _companyStaffService = companyStaffService;
            _userManager = userManager;
        }


        [Route("company/appointment/list")]
        [HttpGet]
        public IActionResult GetCompanyAppointments(int companyId)
        {
            try
            {
                var company = _companyService.TGetByID(companyId);
                if (company == null)
                {
                    var returnNullCompanyData = new
                    {
                        status = "error",
                        message = "İşletme Bulunamadı!"
                    };
                    return NotFound(returnNullCompanyData);
                }
                var appointments = _generalAppointment.TGetByCompanyId(companyId);
                foreach (var appointment in appointments)
                {
                    appointment.AppointmentStatus = _appointmentStatusService.TGetByID(appointment.AppointmentStatusId);
                    appointment.Customer = _customerService.TGetByID(appointment.CustomerId);
                    appointment.CustomerAppointmentInfo = _customerAppointmentInfoService.TGetByID(appointment.CustomerId);
                    appointment.CustomerBillingInfo = _customerBillingInfoService.TGetByID(appointment.CustomerBillingInfoId);
                    appointment.PaymentType = _paymentTypeService.TGetByID(appointment.PaymentTypeId);
                }
                var returnData = new
                {
                    status = "success",
                    data = appointments
                };

                return Ok(returnData);
            }
            catch (System.Exception ex)
            {
                var returnErrorData = new
                {
                    status = "error",
                    message = ex.Message
                };
                return StatusCode(StatusCodes.Status500InternalServerError, returnErrorData);
            }
            
        }

        [Route("company/appointment/detail")]
        [HttpGet]
        public IActionResult GetCompanyAppointmentDetail(int companyId, int appointmentId)
        {
            try
            {
                var company = _companyService.TGetByID(companyId);
                if (company == null)
                {
                    var returnNullCompanyData = new
                    {
                        status = "error",
                        message = "İşletme Bulunamadı!"
                    };
                    return NotFound(returnNullCompanyData);
                }
                var appointment = _appointmentInfoService.TGetAppointmentsByAppointmentId(appointmentId);
                foreach (var appointmentDetail in appointment)
                {
                    appointmentDetail.CompanyService = _companyServiceService.TGetByID(appointmentDetail.CompanyServiceId);
                    appointmentDetail.Staff = _companyStaffService.TGetByID(appointmentDetail.StaffId);
                }
                var returnData = new
                {
                    status = "success",
                    data = appointment
                };

                return Ok(returnData);
            }
            catch (System.Exception ex)
            {
                var returnErrorData = new
                {
                    status = "error",
                    message = ex.Message
                };
                return StatusCode(StatusCodes.Status500InternalServerError, returnErrorData);
            }
        }

        [Route("company/appointment/apprrove")]
        [HttpGet]
        public IActionResult ApproveCompanyAppointment(int companyId, int appointmentId)
        {
            try
            {
                var company = _companyService.TGetByID(companyId);
                if (company == null)
                {
                    var returnNullCompanyData = new
                    {
                        status = "error",
                        message = "İşletme Bulunamadı!"
                    };
                    return NotFound(returnNullCompanyData);
                }
                
                var appointment = _generalAppointment.TGetByID(appointmentId);
                if (appointment == null)
                {
                    var returnNullAppointmentData = new
                    {
                        status = "error",
                        message = "Randevu Bulunamadı!"
                    };
                    return NotFound(returnNullAppointmentData);
                }
                appointment.AppointmentStatusId = 2;
                _generalAppointment.TUpdate(appointment);
                var returnData = new
                {
                    status = "success",
                    message = "Randevu Onaylandı!"
                };

                var customer = _customerService.TGetByID(appointment.CustomerId);
                var user = _userManager.FindByIdAsync(customer.UserId.ToString()).Result;
                var mailDto = new MailDto("noreply@randevuburada.com", "smtKDM110*", user.Email);
                string mailBody = $@"
                    <!DOCTYPE html>
                    <html lang=""en"">
                    <head>
                    <meta charset=""UTF-8"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                    <title>Email Doğrulama</title>
                    <style>
                    body {{
                        font-family: Arial, sans-serif;
                    }}
                    .container {{
                        max-width: 600px;
                        margin: 0 auto;
                        padding: 20px;
                    }}
                    .button {{
                        display: inline-block;
                        background-color: #007bff;
                        color: #fff;
                        padding: 10px 20px;
                        text-decoration: none;
                        border-radius: 5px;
                    }}
                    </style>
                    </head>
                    <body>
                    <div class=""container"">
                        <p>Merhaba,</p>
                        <p>Randevunuz işletme tarafından onaylanmıştır.</p>
                        <p>Randevu Tarihi : ""{appointment.AppointmentDateStart}"" </p>
                    </div>
                    </body>
                    </html>
                    ";
                mailDto.SendMail(mailBody, "Randevuburada - Randevunuz Onaylandı!");

                return Ok(returnData);
            }catch(System.Exception ex)
            {
                var returnErrorData = new
                {
                    status = "error",
                    message = ex.Message
                };
                return StatusCode(StatusCodes.Status500InternalServerError, returnErrorData);
            }
        }

        [Route("company/appointment/cancel")]
        [HttpPost]
        public IActionResult CancelCompanyAppointment(CompanyDeleteAppointmentDto companyDeleteAppointmentDto)
        {
            try
            {
                var appointment = _generalAppointment.TGetByID(companyDeleteAppointmentDto.AppointmentId);
                if (appointment == null)
                {
                    var returnNullAppointmentData = new
                    {
                        status = "error",
                        message = "Randevu Bulunamadı!"
                    };
                    return NotFound(returnNullAppointmentData);
                }
               
                appointment.AppointmentStatusId = 3;
                appointment.Description = companyDeleteAppointmentDto.Description;
                _generalAppointment.TUpdate(appointment);

                var customer = _customerService.TGetByID(appointment.CustomerId);
                var user = _userManager.FindByIdAsync(customer.UserId.ToString()).Result;
                var mailDto = new MailDto("noreply@randevuburada.com", "smtKDM110*", user.Email);
                string mailBody = $@"
                    <!DOCTYPE html>
                    <html lang=""en"">
                    <head>
                    <meta charset=""UTF-8"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                    <title>Email Doğrulama</title>
                    <style>
                    body {{
                        font-family: Arial, sans-serif;
                    }}
                    .container {{
                        max-width: 600px;
                        margin: 0 auto;
                        padding: 20px;
                    }}
                    .button {{
                        display: inline-block;
                        background-color: #007bff;
                        color: #fff;
                        padding: 10px 20px;
                        text-decoration: none;
                        border-radius: 5px;
                    }}
                    </style>
                    </head>
                    <body>
                    <div class=""container"">
                        <p>Merhaba,</p>
                        <p>Randevunuz işletme tarafından iptal edilmiştir!</p>
                        <p>Randevu Tarihi : ""{appointment.AppointmentDateStart}"" </p>
                    </div>
                    </body>
                    </html>
                    ";
                mailDto.SendMail(mailBody, "Randevuburada - Randevunuz İptal Edildi!");

                var returnData = new
                {
                    status = "success",
                    message = "Randevu İptal Edildi!"
                };
                return Ok(returnData);

            }catch(System.Exception ex)
            {
                var returnErrorData = new
                {
                    status = "error",
                    message = ex.Message
                };
                return StatusCode(StatusCodes.Status500InternalServerError, returnErrorData);
            }
        }


        [Route("company/appointment/info/detail")]
        [HttpGet]
        public IActionResult GetDetailAppointmens(int appointmentId)
        {
            try
            {
                var appointmentDetail = _appointmentInfoService.TGetAppointmentsByAppointmentId(appointmentId);
                if(appointmentDetail == null)
                {
                    var returnNullAppointmentData = new
                    {
                        status = "error",
                        message = "Randevu Detayı Bulunamadı!"
                    };
                    return NotFound(returnNullAppointmentData);
                }
                var objectList = appointmentDetail.Select(item =>
                {
                   
                    var appointmentDateStart = item.AppointmentDateStart.ToString("dd.MM.yyyy hh:mm");
                    var appointmentTime = item.AppointmentTime.ToString("mm");
                    var companyService = _companyServiceService.TGetByID(item.CompanyServiceId);
                    var serviceName = companyService.ServiceName;
                    var staff = _companyStaffService.TGetByID(item.StaffId);
                    var staffName = staff.FirstName + " " + staff.LastName;
                    return new
                    {
                        item.Id,
                        appointmentDateStart,
                        item.AppointmentDateEnd,
                        appointmentTime,
                        serviceName,
                        staffName,
                        item.Price
                    };
                }).ToList();

                var returnData = new
                {
                    status = "success",
                    data = objectList
                };
                return Ok(returnData);
            }catch(System.Exception ex)
            {
                var returnErrorData = new
                {
                    status = "error",
                    message = ex.Message
                };
                return StatusCode(StatusCodes.Status500InternalServerError, returnErrorData);
            }
        }

    }
}
