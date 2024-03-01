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
    [Route("api/[controller]")]
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
        [Route("addCustomerInfo")]
        [Authorize]
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
        [Route("updateCustomerInfo")]
        [Authorize]
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
                return BadRequest("Kullanıcı bulunamadı");
            }
            var control = _customerService.TCheckCustomer(userId);
            if (!control)
            {
                return BadRequest("Bu kullanıcının kaydı bulunmamaktadır.");
            }
            customer.UpdatedAt = DateTime.Now;

            _customerService.TUpdate(customer);
            return Ok();
        }

        [HttpGet]
        [Route("getCustomerInfo")]
        [Authorize]
        public IActionResult GetCustomer(int userId)
        {
            var customer = _customerService.TGetByUserID(userId);
            if (customer == null)
            {
                return BadRequest("Kullanıcı bulunamadı");
            }
            var customerDto = _mapper.Map<CustomerUpdateDto>(customer);
            return Ok(customerDto);
        }

    }
}
