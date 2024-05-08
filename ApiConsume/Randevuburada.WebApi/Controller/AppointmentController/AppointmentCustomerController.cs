using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using randevuburada.BusinessLayer.Abstract;

namespace Randevuburada.WebApi.Controller.AppointmentController
{
    [Route("api/v1")]
    [ApiController]
    public class AppointmentCustomerController : ControllerBase
    {
        public readonly IGeneralAppointmentService _generalAppointment;
        public readonly IAppointmentCompanyInfoService _appointmentCompanyInfoService;
        public readonly IAppointmentStatusService _appointmentStatusService;
        public readonly ICompanyService _companyService;
        public readonly IPaymentTypeService _paymentTypeService;
        public readonly IAppointmentInfoService _appointmentInfoService;

        public AppointmentCustomerController(IGeneralAppointmentService generalAppointment, IAppointmentCompanyInfoService appointmentCompanyInfoService, IAppointmentStatusService appointmentStatusService, ICompanyService companyService, IPaymentTypeService paymentTypeService, IAppointmentInfoService appointmentInfoService)
        {
            _generalAppointment = generalAppointment;
            _appointmentCompanyInfoService = appointmentCompanyInfoService;
            _appointmentStatusService = appointmentStatusService;
            _companyService = companyService;
            _paymentTypeService = paymentTypeService;
            _appointmentInfoService = appointmentInfoService;
        }

        [HttpGet]
        [Route("customer/appointment/set")]
        public IActionResult CreateAppointmentForCustomer()
        {
            return Ok();
        }

        [HttpGet]
        [Route("customer/appointment/list")]
        public IActionResult GetAppointmentForCustomer()
        {
            return Ok();
        }

        [HttpGet]
        [Route("customer/appointment/detail")]
        public IActionResult GetAppointmentDetailForCustomer()
        {
            return Ok();
        }

        [HttpGet]
        [Route("customer/appointment/cancel")]
        public IActionResult CancelAppointmentForCustomer()
        {
            return Ok();
        }     

    }
}
