using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using randevuburada.BusinessLayer.Abstract;
using randevuburada.DtoLayer.Dtos.CustomerDto;
using randevuburada.EntityLayer.Concrete;
using randevuburada.EntityLayer.Concrete.CustomerConcrete;
using Microsoft.AspNetCore.Identity;
using randevuburada.EntityLayer.Concrete.Identity;
using Microsoft.AspNetCore.Authorization;


namespace Randevuburada.WebApi.Controller.Customer
{
    [Route("api/v1/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public CustomerController(ICustomerService customerService, IMapper mapper, UserManager<AppUser> userManager)
        {
            _customerService = customerService;
            _mapper = mapper;
            _userManager = userManager;
        }


        [HttpPost]
        [Route("info/set")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AddCustomer(CustomerAddDto customerAddDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = _mapper.Map<randevuburada.EntityLayer.Concrete.CustomerConcrete.Customer>(customerAddDto);
            int userId = customerAddDto.UserId;
            customer.User = await _userManager.FindByIdAsync(userId.ToString());

            if (customer.User == null)
            {
                return BadRequest("Kullanıcı bulunamadı");
            }
            var control = _customerService.TCheckCustomer(userId);
            if (control)
            {
                return BadRequest("Bu kullanıcının mevcutta kaydı vardır.");
            }

            customer.CreatedAt = DateTime.Now;
            customer.UpdatedAt = DateTime.Now;

            _customerService.TInsert(customer);
            return Ok();
        }

        [HttpPost]
        [Route("info/update")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UpdateCustomer(CustomerUpdateDto customerUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = _mapper.Map<randevuburada.EntityLayer.Concrete.CustomerConcrete.Customer>(customerUpdateDto);
            int userId = customerUpdateDto.UserId;
         
            customer.User = await _userManager.FindByIdAsync(userId.ToString());
            if (customer.User == null)
            {
                var returnData = new
                {
                    status = "error",
                    message = "Kullanıcı Bulunamadı!"
                };
                return BadRequest(returnData);
            }
            var control = _customerService.TCheckCustomer(userId);
            if (!control)
            {
                var returnData = new
                {
                    status = "error",
                    message = "Kullanıcının mevcutta kaydı vardır!"
                };
                return BadRequest(returnData);
            }
            customer.UpdatedAt = DateTime.Now;

            _customerService.TUpdate(customer);

            var AppUserData = new AppUser
            {
                Id = userId,
                Email = customer.User.Email,
                PhoneNumber = customer.User.PhoneNumber,
                FirstName = customer.User.FirstName,
                LastName = customer.User.LastName
            };

            // KULLANICI BİLGİLERİ GÜNCELLENECEK DATABASE DE APPUSER TABLOSUNDA
            var successData = new
            {
                status = "success",
                message = "Kullanıcı bilgileri başarıyla güncellendi!"
            };
            return Ok(successData);
        }

        [HttpGet]
        [Route("info/get/byUserId")]
        [Authorize(Roles = "Customer")]
        public IActionResult GetCustomer(int userId)
        {
            var customer = _customerService.TGetByUserID(userId);
            if (customer == null)
            {
                var returnData = new
                {
                    status = "error",
                    message = "Kullanıcı bulunamadı!"
                };
                return BadRequest(returnData);
            }
            var customerDto = _mapper.Map<CustomerUpdateDto>(customer);

            var successData = new
            {
                status = "success",
                message = "Kullanıcı bilgileri başarıyla bulundu!",
                data = customerDto
            };
            return Ok(customerDto);
        }

    }
}
