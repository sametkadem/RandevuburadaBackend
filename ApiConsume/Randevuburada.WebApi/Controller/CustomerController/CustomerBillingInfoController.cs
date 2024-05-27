using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using randevuburada.BusinessLayer.Abstract;
using randevuburada.DtoLayer.Dtos.AppointmentDto.CustomerAppointmentInfoDto;
using randevuburada.DtoLayer.Dtos.CustomerDto.CustomerBillingInfoDto;
using randevuburada.EntityLayer.Concrete.CustomerConcrete;
using randevuburada.EntityLayer.Concrete.Identity;

namespace Randevuburada.WebApi.Controller.CustomerController
{
    [Route("api/v1")]
    [ApiController]
    public class CustomerBillingInfoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;
        private readonly ICustomerBillingInfoService _customerBillingInfoService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;
        private readonly IDistrictService _districtService;

        public CustomerBillingInfoController(IMapper mapper, ICustomerService customerService, ICustomerBillingInfoService customerBillingInfoService, UserManager<AppUser> userManager, ICountryService countryService, ICityService cityService, IDistrictService districtService)
        {
            _mapper = mapper;
            _customerService = customerService;
            _customerBillingInfoService = customerBillingInfoService;
            _userManager = userManager;
            _countryService = countryService;
            _cityService = cityService;
            _districtService = districtService;
        }

        [HttpPost]
        [Route("customer/billing-info/set")]
        public async Task<IActionResult> AddCustomerBillingInfo(CustomerBillingInfoAddDto customerBillingInfoAddDto)
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

            var customerBillingInfo = _mapper.Map<CustomerBillingInfo>(customerBillingInfoAddDto);
            var customer = _customerService.TGetByID(customerBillingInfoAddDto.CustomerId);
            if(customer == null)
            {
                var returnCustomerNotFound = new
                {
                    status = "error",
                    message = "Müşteri bulunamadı!"
                };
                return NotFound(returnCustomerNotFound);
            }
            var userId = customer.UserId;
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                var returnUserNotFound = new
                {
                    status = "error",
                    message = "Kullanıcı bulunamadı!"
                };
                return NotFound(returnUserNotFound);
            }

            var count = _customerBillingInfoService.TGetRecordCountByCustomerID(customer.Id);
            if (count >= 3)
            {
                var returnCount = new
                {
                    status = "error",
                    message = "Maksimum sayıda fatura bilgisi mevcuttur!"
                };
                return BadRequest(returnCount);
            }

            var countryControl = _countryService.TGetByID(customerBillingInfo.CountryId);
            if(countryControl == null)
            {
                var returnCountryNotFound = new
                {
                    status = "error",
                    message = "Ülke bulunamadı!"
                };
                return NotFound(returnCountryNotFound);
            }
            customerBillingInfo.Country = countryControl;

            var cityControl = _cityService.TGetByID(customerBillingInfo.CityId);
            if(cityControl == null)
            {
                var returnCityNotFound = new
                {
                    status = "error",
                    message = "Şehir bulunamadı!"
                };
                return NotFound(returnCityNotFound);
            }
            customerBillingInfo.City = cityControl;

            var districtControl = _districtService.TGetByID(customerBillingInfo.DistrictId);
            if(districtControl == null)
            {
                var returnDistrictNotFound = new
                {
                    status = "error",
                    message = "İlçe bulunamadı!"
                };
                return NotFound(returnDistrictNotFound);
            }

            customerBillingInfo.District = districtControl;
            customerBillingInfo.Customer = customer;
            customerBillingInfo.CreatedAt = DateTime.Now;
            customerBillingInfo.UpdatedAt = DateTime.Now;

            _customerBillingInfoService.TInsert(customerBillingInfo);

            var returnSuccess = new
            {
                status = "success",
                message = "Fatura bilgisi başarıyla eklendi!"
            };
            return Ok(returnSuccess);
        }

        [HttpPost]
        [Route("customer/billing-info/update")]
        public async Task<IActionResult> UpdateCustomerBillingInfo(CustomerBillingInfoUpdateDto customerBillingInfoUpdateDto)
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

            var customerBillingInfo = _customerBillingInfoService.TGetByID(customerBillingInfoUpdateDto.Id);
            if(customerBillingInfo == null)
            {
                var returnNotFound = new
                {
                    status = "error",
                    message = "Fatura bilgisi bulunamadı!"
                };
                return NotFound(returnNotFound);
            }

            var customer = _customerService.TGetByID(customerBillingInfoUpdateDto.CustomerId);
            if(customer == null)
            {
                var returnCustomerNotFound = new
                {
                    status = "error",
                    message = "Müşteri bulunamadı!"
                };
                return NotFound(returnCustomerNotFound);
            }

            var userId = customer.UserId;
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                var returnUserNotFound = new
                {
                    status = "error",
                    message = "Kullanıcı bulunamadı!"
                };
                return NotFound(returnUserNotFound);
            }

            var countryControl = _countryService.TGetByID(customerBillingInfoUpdateDto.CountryId);
            if(countryControl == null)
            {
                var returnCountryNotFound = new
                {
                    status = "error",
                    message = "Ülke bulunamadı!"
                };
                return NotFound(returnCountryNotFound);
            }
            customerBillingInfo.Country = countryControl;

            var cityControl = _cityService.TGetByID(customerBillingInfoUpdateDto.CityId);
            if(cityControl == null)
            {
                var returnCityNotFound = new
                {
                    status = "error",
                    message = "Şehir bulunamadı!"
                };
                return NotFound(returnCityNotFound);
            }
            customerBillingInfo.City = cityControl;

            var districtControl = _districtService.TGetByID(customerBillingInfoUpdateDto.DistrictId);
            if(districtControl == null)
            {
                var returnDistrictNotFound = new
                {
                    status = "error",
                    message = "İlçe bulunamadı!"
                };

                return NotFound(returnDistrictNotFound);
            }

            customerBillingInfo.District = districtControl;
            customerBillingInfo.Customer = customer;
            customerBillingInfo.UpdatedAt = DateTime.Now;

            _customerBillingInfoService.TUpdate(customerBillingInfo);

            var returnSuccess = new
            {
                status = "success",
                message = "Fatura bilgisi başarıyla güncellendi!"
            };
            return Ok(returnSuccess);
        }


        [HttpGet]
        [Route("customer/billing-info/get/byId")]
        public IActionResult GetCustomerBillingInfoById(int id)
        {
            var customerBillingInfo = _customerBillingInfoService.TGetByID(id);
            if(customerBillingInfo == null)
            {
                var returnNotFound = new
                {
                    status = "error",
                    message = "Fatura bilgisi bulunamadı!"
                };
                return NotFound(returnNotFound);
            }

            var customerBillingInfoDto = _mapper.Map<CustomerBillingInfoUpdateDto>(customerBillingInfo);

            var returnSuccess = new
            {
                status = "success",
                message = "Fatura bilgisi başarıyla getirildi!",
                data = customerBillingInfoDto
            };
            return Ok(returnSuccess);
        }

        [HttpGet]
        [Route("customer/billing-info/get/byCustomerId")]
        public IActionResult GetCustomerBillingInfoByCustomerId(int customerId)
        {
            var customerBillingInfo = _customerBillingInfoService.TGetByCustomerID(customerId);
            if(customerBillingInfo == null)
            {
                var returnNotFound = new
                {
                    status = "error",
                    message = "Fatura bilgisi bulunamadı!"
                };
                return NotFound(returnNotFound);
            }

            var mappedCustomerAppointmentInfo = new List<CustomerBillingInfoUpdateDto>();

            for (int i = 0; i < customerBillingInfo.Count; i++)
            {
                mappedCustomerAppointmentInfo.Add(_mapper.Map<CustomerBillingInfoUpdateDto>(customerBillingInfo[i]));
            }

            var returnSuccess = new
            {
                status = "success",
                message = "Fatura bilgisi başarıyla getirildi!",
                data = mappedCustomerAppointmentInfo
            };
            return Ok(returnSuccess);
        }
    }
}
