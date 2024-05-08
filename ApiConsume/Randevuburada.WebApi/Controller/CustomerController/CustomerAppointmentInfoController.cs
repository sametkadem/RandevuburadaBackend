using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using randevuburada.BusinessLayer.Abstract;
using randevuburada.DtoLayer.Dtos.AppointmentDto.CustomerAppointmentInfoDto;
using randevuburada.DtoLayer.Dtos.CompanyDto.CompanyOwnerInfoDto;
using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using randevuburada.EntityLayer.Concrete.CustomerConcrete;
using randevuburada.EntityLayer.Concrete.Identity;

namespace Randevuburada.WebApi.Controller.CustomerController
{
    [Route("api/v1")]
    [ApiController]
    public class CustomerAppointmentInfoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICustomerAppointmentInfoService _customerAppointmentInfoService;
        private readonly ICustomerService _customerService;
        private readonly UserManager<AppUser> _userManager;

        public CustomerAppointmentInfoController(IMapper mapper, ICustomerAppointmentInfoService customerAppointmentInfoService, ICustomerService customerService, UserManager<AppUser> userManager)
        {
            _customerAppointmentInfoService = customerAppointmentInfoService;
            _mapper = mapper;
            _customerService = customerService;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("customer/appointment/info/set")]
        public async Task<IActionResult> CreateAppointmentInfoForCustomerAsync(CustomerAppointmentInfoAddDto customerAppointmentInfoAddDto)
        {
            if (!ModelState.IsValid)
            {
                var returnIsValid = new
                {
                    status = "error",
                    message = "İşlem başarısız!",
                    data = ModelState
                };
                return BadRequest(returnIsValid);
            }

            var customerAppointmentInfo = _mapper.Map<CustomerAppointmentInfoAddDto, CustomerAppointmentInfo>(customerAppointmentInfoAddDto);
            var customer = _customerService.TGetByID(customerAppointmentInfo.CustomerId);
            if (customer == null)
            {
                var returnCustomerNotFound = new
                {
                    status = "error",
                    message = "Müşteri bulunamadı!"
                };
                return NotFound(returnCustomerNotFound);
            }

            var user = await _userManager.FindByIdAsync(customer.Id.ToString());
            if (user == null)
            {
                var returnUserNotFound = new
                {
                    status = "error",
                    message = "Kullanıcı bulunamadı!"
                };

                return NotFound(returnUserNotFound);
            }

            customerAppointmentInfo.Customer = customer;
            customerAppointmentInfo.CreatedAt = DateTime.UtcNow;
            customerAppointmentInfo.UpdatedAt = DateTime.UtcNow;

            _customerAppointmentInfoService.TInsert(customerAppointmentInfo);

            var returnSuccess = new
            {
                status = "success",
                message = "İşlem başarılı!"
            };

            return Ok(returnSuccess);
        }

        [HttpPost]
        [Route("customer/appointment/info/update")]
        public async Task<IActionResult> UpdateAppointmentInfoForCustomerAsync(CustomerAppointmentInfoUpdateDto customerAppointmentInfoUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                var returnIsValid = new
                {
                    status = "error",
                    message = "İşlem başarısız!",
                    data = ModelState
                };
                return BadRequest(returnIsValid);
            }

            var customerAppointmentInfo = _mapper.Map<CustomerAppointmentInfoUpdateDto, CustomerAppointmentInfo>(customerAppointmentInfoUpdateDto);
            var customer = _customerService.TGetByID(customerAppointmentInfo.CustomerId);

            if (customer == null)
            {
                var returnCustomerNotFound = new
                {
                    status = "error",
                    message = "Müşteri bulunamadı!"
                };
                return NotFound(returnCustomerNotFound);
            }
            var user = await _userManager.FindByIdAsync(customer.Id.ToString());
            if (user == null)
            {
                var returnUserNotFound = new
                {
                    status = "error",
                    message = "Kullanıcı bulunamadı!"
                };

                return NotFound(returnUserNotFound);
            }

            var appointmentInfoControl = _customerAppointmentInfoService.TGetByID(customerAppointmentInfo.Id);
            if (appointmentInfoControl == null)
            {
                var returnAppointmentInfoNotFound = new
                {
                    status = "error",
                    message = "Müşteri Randevu Kayıt bilgisi bulunamadı!"
                };
                return NotFound(returnAppointmentInfoNotFound);
            }

            appointmentInfoControl.CustomerSurname = customerAppointmentInfo.CustomerSurname;
            appointmentInfoControl.CustomerName = customerAppointmentInfo.CustomerName;
            appointmentInfoControl.CustomerPhone = customerAppointmentInfo.CustomerPhone;
            appointmentInfoControl.CustomerTcNo = customerAppointmentInfo.CustomerTcNo;
            appointmentInfoControl.Customer = customer;
            appointmentInfoControl.UpdatedAt = DateTime.UtcNow;
            _customerAppointmentInfoService.TUpdate(appointmentInfoControl);

            var returnSuccess = new
            {
                status = "success",
                message = "İşlem başarılı!"
            };


            return Ok(returnSuccess);
        }


        [HttpGet]
        [Route("customer/appointment/info/get/byId")]
        public IActionResult GetAppointmentInfoForById(int id)
        {
            var customerAppointmentInfo = _customerAppointmentInfoService.TGetByID(id);
            if (customerAppointmentInfo == null)
            {
                var returnNotFound = new
                {
                    status = "error",
                    message = "Müşteri Randevu Kayıt bilgisi bulunamadı!"
                };
                return NotFound(returnNotFound);
            }

            var customerAppointmentInfoResult = _mapper.Map<CustomerAppointmentInfoUpdateDto>(customerAppointmentInfo);

            var returnSuccess = new
            {
                status = "success",
                message = "İşlem başarılı!",
                data = customerAppointmentInfoResult
            };

            return Ok(returnSuccess);
        }

        [HttpGet]
        [Route("customer/appointment/info/list/byCustomerId")]
        public IActionResult GetAppointmentInfoForByCustomerId(int customerId)
        {
            var customer = _customerService.TGetByID(customerId);
            if (customer == null)
            {
                var returnCustomerNotFound = new
                {
                    status = "error",
                    message = "Müşteri bulunamadı!"
                };
                return NotFound(returnCustomerNotFound);
            }

            var user = _userManager.FindByIdAsync(customer.Id.ToString());
            if (user == null)
            {
                var returnUserNotFound = new
                {
                    status = "error",
                    message = "Kullanıcı bulunamadı!"
                };

                return NotFound(returnUserNotFound);
            }
            var customerAppointmentInfo = _customerAppointmentInfoService.TGetByCustomerID(customerId);
            if (customerAppointmentInfo == null)
            {
                var returnNotFound = new
                {
                    status = "error",
                    message = "Müşteri Randevu Kayıt bilgisi bulunamadı!"
                };
                return NotFound(returnNotFound);
            }

            var mappedCustomerAppointmentInfo = new List<CustomerAppointmentInfoUpdateDto>();

            for (int i = 0; i < customerAppointmentInfo.Count; i++)
            {
                mappedCustomerAppointmentInfo.Add(_mapper.Map<CustomerAppointmentInfoUpdateDto>(customerAppointmentInfo[i]));
            }


            var returnSuccess = new
            {
                status = "success",
                message = "İşlem başarılı!",
                data = mappedCustomerAppointmentInfo
            };

            return Ok(returnSuccess);
        }


        [HttpGet]
        [Route("customer/appointment/info/list/byUserId")]
        public IActionResult GetAppointmentInfoForByUserId(int userId)
        {
            var user = _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                var returnUserNotFound = new
                {
                    status = "error",
                    message = "Kullanıcı bulunamadı!"
                };

                return NotFound(returnUserNotFound);
            }

            var customer = _customerService.TGetByUserID(userId);
            if (customer == null)
            {
                var returnCustomerNotFound = new
                {
                    status = "error",
                    message = "Müşteri bulunamadı!"
                };
                return NotFound(returnCustomerNotFound);
            }

            var customerAppointmentInfo = _customerAppointmentInfoService.TGetByCustomerID(customer.Id);
            if (customerAppointmentInfo == null)
            {
                var returnNotFound = new
                {
                    status = "error",
                    message = "Müşteri Randevu Kayıt bilgisi bulunamadı!"
                };
                return NotFound(returnNotFound);
            }

            var mappedCustomerAppointmentInfo = new List<CustomerAppointmentInfoUpdateDto>();

            for (int i = 0; i < customerAppointmentInfo.Count; i++)
            {
                mappedCustomerAppointmentInfo.Add(_mapper.Map<CustomerAppointmentInfoUpdateDto>(customerAppointmentInfo[i]));
            }


            var returnSuccess = new
            {
                status = "success",
                message = "İşlem başarılı!",
                data = mappedCustomerAppointmentInfo
            };

            return Ok(returnSuccess);
        }

    }
}
