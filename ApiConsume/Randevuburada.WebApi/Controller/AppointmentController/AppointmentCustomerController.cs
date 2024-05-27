using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using randevuburada.BusinessLayer.Abstract;
using randevuburada.DtoLayer.Dtos.AppointmentDto.CustomerAppointmentAvailableDto;
using randevuburada.DtoLayer.Dtos.AppointmentDto.CustomerAppointmentDto;
using randevuburada.EntityLayer.Concrete.AppointmentConcrete;
using randevuburada.EntityLayer.Concrete.Identity;
using Randevuburada.WebApi.Model.MailModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Randevuburada.WebApi.Controller.AppointmentController
{
    [Route("api/v1")]
    [ApiController]
    public class AppointmentCustomerController : ControllerBase
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
        public AppointmentCustomerController(IGeneralAppointmentService generalAppointment, IAppointmentCompanyInfoService appointmentCompanyInfoService, IAppointmentStatusService appointmentStatusService, ICompanyService companyService, IPaymentTypeService paymentTypeService, IAppointmentInfoService appointmentInfoService, ICompanyWorkingHoursService companyWorkingHoursService, ICompanyServiceService companyServiceService, IServiceIntervalHoursService serviceIntervalHoursService, ICustomerAppointmentInfoService customerAppointmentInfoService, ICustomerBillingInfoService customerBillingInfoService, ICustomerService customerService, ICompanyStaffService companyStaffService, UserManager<AppUser> userManager)
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


        private string GetTurkishDayOfWeek(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    return "Pazartesi";
                case DayOfWeek.Tuesday:
                    return "Salı";
                case DayOfWeek.Wednesday:
                    return "Çarşamba";
                case DayOfWeek.Thursday:
                    return "Perşembe";
                case DayOfWeek.Friday:
                    return "Cuma";
                case DayOfWeek.Saturday:
                    return "Cumartesi";
                case DayOfWeek.Sunday:
                    return "Pazar";
                default:
                    return "";
            }
        }


        /* Müşteriye mevcut çalışma tarihlerine göre statik 90 günlük bir takvim gösterilir. */
        [Route("customer/appointment/available/dates")]
        [HttpGet]
        public IActionResult GetAvailableDayForCustomer(int companyId)
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

                var workingHours = _companyWorkingHoursService.TGetByCompanyId(companyId);
                if (workingHours == null || company.CompanyVisibility == false || company.CompanyStatus == false)
                {
                    var returnNullCompanyData = new
                    {
                        status = "error",
                        message = "İşletme kapalı!"
                    };
                    return NotFound(returnNullCompanyData);
                }

                var currentDate = DateTime.Today;
                var maxDay = 90;
                var appointmentDates = new List<object>();

                for (int i = 0; i < maxDay; i++)
                {
                    var date = currentDate.AddDays(i);
                    var dayOfWeek = date.DayOfWeek;
                    int dayId = (int)dayOfWeek;
                    var notWorkingControl = false;
                    foreach (var workingHour in workingHours)
                    {
                        if (workingHour.DayId == dayId)
                        {
                            notWorkingControl = true;
                            appointmentDates.Add(new
                            {
                                date = date,
                                dateString = date.ToString("yyyy-MM-dd"),
                                dayId = dayId,
                                dayName = GetTurkishDayOfWeek(dayOfWeek),
                                text = $"{date.ToString("dd MMMM dddd")}",
                                status = "available"
                            });
                        }
                    }
                    if (!notWorkingControl)
                    {
                        appointmentDates.Add(new
                        {
                            date = date,
                            dateString = date.ToString("yyyy-MM-dd"),
                            dayId = dayId,
                            dayName = GetTurkishDayOfWeek(dayOfWeek),
                            text = $"{date.ToString("dd MMMM dddd")} (Kapalı)",
                            status = "decline"
                        });
                    }
                }

                var returnData = new
                {
                    status = "success",
                    data = appointmentDates
                };
                return Ok(returnData);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        /* Müşteriye ilgili tarihte ilgili çalışana göre mevcut randevu saatleri gösterilir. */
        [Route("customer/appointment/available/times")]
        [HttpPost]
        public IActionResult GetAvailableAppointmentTimesForCustomer(CustomerAppointmentAvailableGetDto appointmentDto)
        {
            try
            {
                var company = _companyService.TGetByID(appointmentDto.companyId);
                if (company == null)
                {
                    var returnNullCompanyData = new
                    {
                        status = "error",
                        message = "İşletme Bulunamadı!"
                    };
                    return NotFound(returnNullCompanyData);
                }

                DayOfWeek dayOfWeek = appointmentDto.AppointmentDate.DayOfWeek;
                int dayId = (int)dayOfWeek;
                var workingHours = _companyWorkingHoursService.TGetByCompanyIdAndDayId(appointmentDto.companyId, dayId);
                if (workingHours == null || company.CompanyVisibility == false || company.CompanyStatus == false)
                {
                    var returnNullCompanyData = new
                    {
                        status = "error",
                        message = "İşletme kapalı!"
                    };
                    return NotFound(returnNullCompanyData);
                }
                var staffIds = new List<int>();
                var serviceIds = new List<int>();
                var appointmentArray = new List<Dictionary<string, object>>();
                var totalIntervalMinutes = 0;
                var staffId = 0;
                foreach (var appointment in appointmentDto.AppointmentAvailables)
                {
                    var services = _companyServiceService.TGetByCompanyIdAndServiceId(appointmentDto.companyId, appointment.ServiceId);
                    if (services == null)
                    {
                        var returnNullCompanyData = new
                        {
                            status = "error",
                            message = "Hizmet bulunamadı!",
                            companyId = appointmentDto.companyId,
                            serviceId = appointment.ServiceId,
                            staffId = appointment.StaffId,
                        };
                        return NotFound(returnNullCompanyData);
                    }
                    else
                    {
                        if(!services.CompanyStaffIds.Contains(appointment.StaffId))
                        {
                            var returnNullCompanyData = new
                            {
                                status = "error",
                                message = "Personel bulunamadı!",
                                companyId = appointmentDto.companyId,
                                serviceId = appointment.ServiceId,
                                staffId = appointment.StaffId,
                            };
                            return NotFound(returnNullCompanyData);
                        }
                        else
                        {
                            var intervalHoursService = _serviceIntervalHoursService.TGetByID(services.ServiceIntervalHoursId);
                            var appointmentData = new Dictionary<string, object>();
                            appointmentData["intervalHours"] = intervalHoursService.intervalTime.Minute;
                            appointmentData["staffId"] = appointment.StaffId;
                            appointmentData["serviceId"] = appointment.ServiceId;
                            totalIntervalMinutes += intervalHoursService.intervalTime.Minute;
                            staffId = appointment.StaffId;
                            if(!staffIds.Contains(appointment.StaffId))
                            {
                                staffIds.Add(appointment.StaffId);
                            }
                            if (!serviceIds.Contains(appointment.ServiceId))
                            {
                                serviceIds.Add(appointment.ServiceId);
                            }
                            appointmentArray.Add(appointmentData);
                        }
                    }
                
                }
                var appointmentAvailableArray = new List<Dictionary<string, object>>();
                if (staffIds.Count() == 1)
                {
                    var staffAppointmentInfo = _appointmentInfoService.TGetAppointmentInfoByCompanyAndStaffIdAndDate(appointmentDto.companyId, staffId, appointmentDto.AppointmentDate); // İŞLETMEDEKİ ÇALIŞANIN O GÜNKÜ RANDEVULARI
                    var openTime = workingHours.OpenTime.ToTimeSpan(); // TimeOnly değerini TimeSpan'e dönüştür
                    var closeTime = workingHours.CloseTime.ToTimeSpan(); // TimeOnly değerini TimeSpan'e dönüştür
                    var appointmentTimes = new List<object>();
                    var openDateTime = new DateTime(appointmentDto.AppointmentDate.Year, appointmentDto.AppointmentDate.Month, appointmentDto.AppointmentDate.Day, openTime.Hours, openTime.Minutes, openTime.Seconds); // Sadece saat, dakika ve saniye bilgilerini kullanarak DateTime oluştur // workingHours.OpenTime'i bugünkü tarihin saat bilgisiyle birleştir
                    var closeDateTime = new DateTime(appointmentDto.AppointmentDate.Year, appointmentDto.AppointmentDate.Month, appointmentDto.AppointmentDate.Day, closeTime.Hours, closeTime.Minutes, closeTime.Seconds); // Sadece saat, dakika ve saniye bilgilerini kullanarak DateTime oluştur
                    if (openDateTime < DateTime.Now)
                    {
                        openDateTime = DateTime.Now.AddHours(2); // Şu anki zamanı 2 saat ileri al
                        openDateTime = new DateTime(openDateTime.Year, openDateTime.Month, openDateTime.Day, openDateTime.Hour, 0, 0); // Yeni saat bilgisini al, dakika ve saniyeyi 0 olarak ayarla
                        closeDateTime = new DateTime(openDateTime.Year, openDateTime.Month, openDateTime.Day, closeTime.Hours, 0, 0); // Yeni saat bilgisini al, dakika ve saniyeyi 0 olarak ayarla
                    }
                    while (openDateTime < closeDateTime)
                    {
                        var isAvailable = true;
                        foreach (var appointmentInfo in staffAppointmentInfo)
                        {
                            if (openDateTime >= appointmentInfo.AppointmentDateStart && openDateTime < appointmentInfo.AppointmentDateEnd)
                            {
                                isAvailable = false;
                                break;
                            }
                        }

                        if (isAvailable)
                        {
                            appointmentTimes.Add(new
                            {
                                time = openDateTime.ToString("HH:mm"),
                                date = openDateTime,
                                dateString = openDateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                status = "available"
                            });
                        }
                        else
                        {
                            appointmentTimes.Add(new
                            {
                                time = openDateTime.ToString("HH:mm"),
                                date = openDateTime,
                                dateString = openDateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                status = "decline"
                            });
                        }

                        openDateTime = openDateTime.AddMinutes(totalIntervalMinutes);
                    }
                    appointmentAvailableArray.Add(new Dictionary<string, object>
                        {
                            { "staffId", staffIds },
                            { "serviceId", serviceIds },
                            { "appointmentTimes", appointmentTimes }
                     });
                }
                else if(staffIds.Count() > 1)
                {
                    foreach (var appointmentItem in appointmentArray)
                    {
                        var staffAppointmentInfo = _appointmentInfoService.TGetAppointmentInfoByCompanyAndStaffIdAndDate(appointmentDto.companyId, Convert.ToInt32(appointmentItem["staffId"]), appointmentDto.AppointmentDate); // İŞLETMEDEKİ ÇALIŞANIN O GÜNKÜ RANDEVULARI
                        var openTime = workingHours.OpenTime.ToTimeSpan(); // TimeOnly değerini TimeSpan'e dönüştür
                        var closeTime = workingHours.CloseTime.ToTimeSpan(); // TimeOnly değerini TimeSpan'e dönüştür
                        var appointmentTimes = new List<object>();
                        var openDateTime = new DateTime(appointmentDto.AppointmentDate.Year, appointmentDto.AppointmentDate.Month, appointmentDto.AppointmentDate.Day, openTime.Hours, openTime.Minutes, openTime.Seconds); // Sadece saat, dakika ve saniye bilgilerini kullanarak DateTime oluştur // workingHours.OpenTime'i bugünkü tarihin saat bilgisiyle birleştir
                        var closeDateTime = new DateTime(appointmentDto.AppointmentDate.Year, appointmentDto.AppointmentDate.Month, appointmentDto.AppointmentDate.Day, closeTime.Hours, closeTime.Minutes, closeTime.Seconds); // Sadece saat, dakika ve saniye bilgilerini kullanarak DateTime oluştur
                        if (openDateTime < DateTime.Now)
                        {
                            openDateTime = DateTime.Now.AddHours(2); // Şu anki zamanı 2 saat ileri al
                            openDateTime = new DateTime(openDateTime.Year, openDateTime.Month, openDateTime.Day, openDateTime.Hour, 0, 0); // Yeni saat bilgisini al, dakika ve saniyeyi 0 olarak ayarla
                            closeDateTime = new DateTime(openDateTime.Year, openDateTime.Month, openDateTime.Day, closeTime.Hours, 0, 0); // Yeni saat bilgisini al, dakika ve saniyeyi 0 olarak ayarla
                        }
                        while (openDateTime < closeDateTime)
                        {
                            var isAvailable = true;
                            foreach (var appointmentInfo in staffAppointmentInfo)
                            {
                                if (openDateTime >= appointmentInfo.AppointmentDateStart && openDateTime < appointmentInfo.AppointmentDateEnd)
                                {
                                    isAvailable = false;
                                    break;
                                }
                            }

                            if (isAvailable)
                            {
                                appointmentTimes.Add(new
                                {
                                    time = openDateTime.ToString("HH:mm"),
                                    date = openDateTime,
                                    dateString = openDateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                    status = "available"
                                });
                            }
                            else
                            {
                                appointmentTimes.Add(new
                                {
                                    time = openDateTime.ToString("HH:mm"),
                                    date = openDateTime,
                                    dateString = openDateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                    status = "decline"
                                });
                            }
                            var doubleIntervalMinutes = Convert.ToDouble(appointmentItem["intervalHours"]);
                            openDateTime = openDateTime.AddMinutes(doubleIntervalMinutes);
                        }
                        appointmentAvailableArray.Add(new Dictionary<string, object>
                        {
                            { "staffId", appointmentItem["staffId"] },
                            { "serviceId", appointmentItem["serviceId"] },
                            { "appointmentTimes", appointmentTimes }
                        });
                    }
                }
                else
                {
                    var returnNullCompanyData = new
                    {
                        status = "error",
                        message = "Personel bulunamadı!"
                    };
                }
                var returnData = new
                {
                    status = "success",
                    companyId = appointmentDto.companyId,
                    appointmentDate = appointmentDto.AppointmentDate,
                    counter = appointmentAvailableArray.Count(),
                    single = appointmentAvailableArray.Count() > 1 ? false : true,
                    data = appointmentAvailableArray
                };
              return Ok(returnData);
               
            }
            catch (Exception ex)
            {

                var returnException = new
                {
                    status = "error",
                    message = ex.Message
                };
                
                return StatusCode(StatusCodes.Status500InternalServerError, returnException);
            }
        }

        [Route("customer/appointment/create")]
        [HttpPost]
        public IActionResult CreateAppointmentForCustomer(CreateAppointmentDtoAdd createAppointment)
        {
            try
            {
                var company = _companyService.TGetByID(createAppointment.CompanyId);
                if (company == null)
                {
                    var returnNullCompanyData = new
                    {
                        status = "error",
                        message = "İşletme Bulunamadı!"
                    };
                    return NotFound(returnNullCompanyData);
                }
                var customerAppointmentInfo = _customerAppointmentInfoService.TGetByID(createAppointment.CustomerAppointmentInfoId);
                if (customerAppointmentInfo == null)
                {
                    var returnNullCompanyData = new
                    {
                        status = "error",
                        message = "Müşteri bilgisi bulunamadı!"
                    };
                    return NotFound(returnNullCompanyData);
                }

                var customerBillingInfo = _customerBillingInfoService.TGetByID(createAppointment.CustomerBillingInfoId);
                if (customerBillingInfo == null)
                {
                    var returnNullCompanyData = new
                    {
                        status = "error",
                        message = "Müşteri fatura bilgisi bulunamadı!"
                    };
                    return NotFound(returnNullCompanyData);
                }
                var startDateGeneralAppoinment = createAppointment.Appointments[0].AppointmentDate;
                var endDateGeneralAppointment = createAppointment.Appointments[0].AppointmentDate;
                var totalAppoinmentTime = new DateTime(1, 1, 1, 1, 1, 1);
                float totalAppoinmentPrice = 0;

                var appointmentInfoArray = new List<Dictionary<string, object>>();
                foreach (var appointment in createAppointment.Appointments)
                {
                    var service = _companyServiceService.TGetByCompanyIdAndServiceId(createAppointment.CompanyId, appointment.ServiceId);
                    if (service == null)
                    {
                        var returnNullCompanyData = new
                        {
                            status = "error",
                            message = "Hizmet bulunamadı!",
                            companyId = createAppointment.CompanyId,
                            serviceId = appointment.ServiceId,
                        };
                        return NotFound(returnNullCompanyData);
                    }
                    if (!service.CompanyStaffIds.Contains(appointment.StaffId))
                    {
                        var returnNullCompanyData = new
                        {
                            status = "error",
                            message = "Personel bulunamadı!",
                            companyId = createAppointment.CompanyId,
                            serviceId = appointment.ServiceId,
                            staffId = appointment.StaffId,
                        };
                        return NotFound(returnNullCompanyData);
                    }
                    var intervalHoursService = _serviceIntervalHoursService.TGetByID(service.ServiceIntervalHoursId).intervalTime;
                    var appointmentInfos = _appointmentInfoService.TGetAppointmentInfoByCompanyAndStaffIdAndDate(createAppointment.CompanyId, appointment.StaffId, appointment.AppointmentDate);

                    var AppointmentStartDate = appointment.AppointmentDate;
                    if (appointment.AppointmentDate < startDateGeneralAppoinment)
                    {
                        startDateGeneralAppoinment = AppointmentStartDate;
                    }
                    
                    var AppointmentEndDate = AppointmentStartDate.AddHours(intervalHoursService.Hour).AddMinutes(intervalHoursService.Minute);
                    if (AppointmentEndDate > endDateGeneralAppointment)
                    {
                        endDateGeneralAppointment = AppointmentEndDate;
                    }

                    foreach (var appointmentInfo in appointmentInfos)
                    {
                        Console.WriteLine(appointmentInfo.AppointmentDateStart);
                        Console.WriteLine(appointmentInfo.AppointmentDateEnd);

                        if (AppointmentStartDate < appointmentInfo.AppointmentDateEnd && AppointmentEndDate > appointmentInfo.AppointmentDateStart)
                        {
                            var returnNullCompanyData = new
                            {
                                status = "error",
                                message = "Randevu tarihi dolu!",
                                companyId = createAppointment.CompanyId,
                                serviceId = appointment.ServiceId,
                                staffId = appointment.StaffId,
                                appointmentDate = appointment.AppointmentDate,
                            };
                            return NotFound(returnNullCompanyData);
                        }
                    }
                    totalAppoinmentTime = totalAppoinmentTime.AddHours(intervalHoursService.Hour).AddMinutes(intervalHoursService.Minute); // Hizmet süresini topla
                    totalAppoinmentPrice += service.Price; // Hizmet fiyatını topla
                    
                    var appointmentInfoData = new Dictionary<string, object>
                    {
                        { "CompanyServiceId", appointment.ServiceId },
                        { "Price", service.Price },
                        { "AppointmentDateStart", AppointmentStartDate },
                        { "AppointmentDateEnd", AppointmentEndDate },
                        { "AppointmentTime", new DateTime(1, 1, 1).AddHours(intervalHoursService.Hour).AddMinutes(intervalHoursService.Minute) },
                        { "StaffId", appointment.StaffId }
                    };
                    appointmentInfoArray.Add(appointmentInfoData);
                }
                var paymentControl = false;
                if(createAppointment.PaymentTypeId == 1)
                {
                    paymentControl = true;
                }
                else if(createAppointment.PaymentTypeId == 2)
                {
                    paymentControl = false;
                }
                else
                {
                    var returnNullCompanyData = new
                    {
                        status = "error",
                        message = "Ödeme tipi bulunamadı!",
                        paymentTypeId = createAppointment.PaymentTypeId,
                    };
                    return NotFound(returnNullCompanyData);
                }
                var generalAppointment = new GeneralAppointment
                {
                   CompanyId = createAppointment.CompanyId,
                   CustomerId = createAppointment.CustomerId,
                   CustomerAppointmentInfoId = createAppointment.CustomerAppointmentInfoId,
                   CustomerBillingInfoId = createAppointment.CustomerBillingInfoId,
                   AppointmentStatusId = 1,
                   AppointmentTime = totalAppoinmentTime,
                   AppointmentDateEnd = endDateGeneralAppointment,
                   AppointmentDateStart = startDateGeneralAppoinment,
                   IsCompanyApproved = false,
                   LastCancelDate = startDateGeneralAppoinment.AddHours(-3),
                   IsCancelAppointment = false,
                   totalAmount = totalAppoinmentPrice,
                   IsPaid = paymentControl,
                   PaymentTypeId = createAppointment.PaymentTypeId,
                   Description = "Randevu oluşturuldu.",
                   CreatedAt = DateTime.Now,
                   UpdatedAt = DateTime.Now 
                };
                var insertControl = _generalAppointment.TInsertGeneralAppointment(generalAppointment);
                if(insertControl == -1)
                {
                    var badRequestData = new
                    {
                        status = "error",
                        message = "Randevu oluşturulamadı! {ERROR: General Appointment Insert}"
                    };
                    return BadRequest(badRequestData);
                }
             
                foreach(var appointmentInfoDetail in appointmentInfoArray)
                {
                    var apoinmentInsertData = new AppointmentInfo
                    {
                        AppointmentId = insertControl,
                        CustomerId = createAppointment.CustomerId,
                        CompanyId = createAppointment.CompanyId,
                        CompanyServiceId = Convert.ToInt32(appointmentInfoDetail["CompanyServiceId"]),
                        StaffId = Convert.ToInt32(appointmentInfoDetail["StaffId"]),
                        Price = Convert.ToInt32(appointmentInfoDetail["Price"]),
                        IsComplate = false,
                        IsCancel = false,
                        AppointmentDateStart = Convert.ToDateTime(appointmentInfoDetail["AppointmentDateStart"]),
                        AppointmentDateEnd = Convert.ToDateTime(appointmentInfoDetail["AppointmentDateEnd"]),
                        AppointmentTime = Convert.ToDateTime(appointmentInfoDetail["AppointmentTime"]),
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };
                    _appointmentInfoService.TInsert(apoinmentInsertData);
                }
                var companyAppointmentInfo = new AppointmentCompanyInfo
                { 
                    AppointmentId = insertControl,
                    CompanyId = createAppointment.CompanyId,
                    PaymentTypeId = createAppointment.PaymentTypeId,
                    TotalAmount = totalAppoinmentPrice,
                    TotalDiscount = 0,
                    TaxRate = 0,
                    TaxAmount = 0,
                    TotalPrice = totalAppoinmentPrice,
                    IsComplate = false,
                    IsCancel = false,
                    IsWithdrawalAllowed = false,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                _appointmentCompanyInfoService.TInsert(companyAppointmentInfo);
                var customer = _customerService.TGetByID(createAppointment.CustomerId);
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
                        <p>Randevunuz başarıyla oluşturulmuştur.</p>
                        <p>İşletme onay verdikten sonra bir bilgi mesajı daha alacaksınız:</p>
                        <p>Randevu Tarihi : ""{generalAppointment.AppointmentDateStart}"" </p>
                    </div>
                    </body>
                    </html>
                    ";
                mailDto.SendMail(mailBody, "Randevuburada - Randevu Oluşturuldu!");
                var returnData = new
                {
                    status = "success",
                    message = "Randevu oluşturuldu!",
                    appointmentId = insertControl
                };
              
                return Ok(returnData);
            }
            catch (Exception ex)
            {
                var returnException = new
                {
                    status = "error",
                    message = ex.Message
                };
                return StatusCode(StatusCodes.Status500InternalServerError, returnException);
            }

        }

        [HttpGet]
        [Route("customer/appointment/list")]
        public IActionResult GetAppointmentForCustomer(int customerId)
        {
            try
            {
                var customer = _customerService.TGetByID(customerId);
                if (customer == null)
                {
                    var returnNullCompanyData = new
                    {
                        status = "error",
                        message = "Müşteri Bulunamadı!"
                    };
                    return NotFound(returnNullCompanyData);
                }


                var customerAppointments = _generalAppointment.TGetByCustomerId(customerId);
                foreach (var customerAppointment in customerAppointments)
                {
                    var company = _companyService.TGetByID(customerAppointment.CompanyId);
                    company.CompanyBankingDetails = null;
                    company.CompanyBankingDetailsId = 0;
                    company.User = null;
                    company.UserId = 0;
                    customerAppointment.Company = _companyService.TGetByID(customerAppointment.CompanyId);
                    customerAppointment.AppointmentStatus = _appointmentStatusService.TGetByID(customerAppointment.AppointmentStatusId);
                }

                var returnData = new
                {
                    status = "success",
                    data = customerAppointments
                };

                return Ok(returnData);
            }
            catch(Exception ex)
            {
                var returnException = new
                {
                    status = "error",
                    message = ex.Message
                };
                return StatusCode(StatusCodes.Status500InternalServerError, returnException);
            }
        }

        [HttpGet]
        [Route("customer/appointment/detail")]
        public IActionResult GetAppointmentDetailForCustomer(int appointmentId)
        {
            try
            {
                var appointment = _generalAppointment.TGetByID(appointmentId); 
                if (appointment == null)
                {
                    var returnNullCompanyData = new
                    {
                        status = "error",
                        message = "Randevu Bulunamadı!"
                    };
                    return NotFound(returnNullCompanyData);
                }
                var appointmentInfos = _appointmentInfoService.TGetAppointmentsByAppointmentId(appointmentId);
                foreach (var appointmentInfo in appointmentInfos)
                {
                    var staff =  _companyStaffService.TGetByID(appointmentInfo.StaffId);
                    staff.Tc = null;
                    staff.BirthDate = DateTime.Now;
                    appointmentInfo.Staff = staff;
                    appointmentInfo.CompanyService = _companyServiceService.TGetByID(appointmentInfo.CompanyServiceId);
                }

                var returnData = new
                {
                    status = "success",
                    data = appointmentInfos
                };
                return Ok(returnData);
            }catch(Exception ex)
            {
                var returnException = new
                {
                    status = "error",
                    message = ex.Message
                };
                return StatusCode(StatusCodes.Status500InternalServerError, returnException);
            }
        }

        [HttpGet]
        [Route("customer/appointment/cancel")]
        public IActionResult CancelAppointmentForCustomer(int appointmentId)
        {
            try
            {
                var appointment = _generalAppointment.TGetByID(appointmentId);
                if (appointment == null)
                {
                    var returnNullCompanyData = new
                    {
                        status = "error",
                        message = "Randevu Bulunamadı!"
                    };
                    return NotFound(returnNullCompanyData);
                }
                if(appointment.LastCancelDate < DateTime.Now)
                {
                    var returnNullCompanyData = new
                    {
                        status = "error",
                        message = "Randevu iptal süresi geçti!"
                    };
                    return NotFound(returnNullCompanyData);
                }
                appointment.IsCancelAppointment = true;
                appointment.AppointmentStatusId = 4;
                appointment.Description = "Randevu iptal edildi.";
                appointment.UpdatedAt = DateTime.Now;
                _generalAppointment.TUpdate(appointment);

                var appointmentInfos = _appointmentInfoService.TGetAppointmentsByAppointmentId(appointmentId);
                var successReturnData = new
                {
                    status = "success",
                    message = "Randevu iptal edildi!",
                    appointment = appointment,
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
                        <p>Randevunuz başarıyla iptal edilmiştir.</p>
                        <p>Randevu Tarihi : ""{appointment.AppointmentDateStart}"" </p>
                    </div>
                    </body>
                    </html>
                    ";
                mailDto.SendMail(mailBody, "Randevuburada - Randevunuz İptal Edildi!");

                return Ok(successReturnData);
                
            }catch(Exception ex)
            {
                var returnException = new
                {
                    status = "error",
                    message = ex.Message
                };
                return StatusCode(StatusCodes.Status500InternalServerError, returnException);
            }
        }

  

    }
}
