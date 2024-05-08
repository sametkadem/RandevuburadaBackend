using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using randevuburada.BusinessLayer.Abstract;
using randevuburada.DtoLayer.Dtos.CustomerDto.CustomerFavouriteDto;
using randevuburada.EntityLayer.Concrete.CustomerConcrete;
using randevuburada.EntityLayer.Concrete.Identity;

namespace Randevuburada.WebApi.Controller.CustomerController
{
    [Route("api/v1")]
    [ApiController]
    public class CustomerFavouriteController : ControllerBase
    {
        private readonly ICustomerFavouriteService _customerFavouriteService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICustomerService _customerService;
        private readonly ICompanyService _companyService;

        public CustomerFavouriteController(ICustomerFavouriteService customerFavouriteService, IMapper mapper, UserManager<AppUser> userManager, ICustomerService customerService, ICompanyService companyService)
        {
            _customerFavouriteService = customerFavouriteService;
            _mapper = mapper;
            _userManager = userManager;
            _customerService = customerService;
            _companyService = companyService;
        }

        [HttpPost]
        [Route("customer/favourite/set")]
        public async Task<IActionResult> AddCustomerFavourite(CustomerFavouriteAddDto customerFavouriteAddDto)
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

            var customerFavourite = _mapper.Map<CustomerFavourite>(customerFavouriteAddDto);
            var customerId = customerFavouriteAddDto.CustomerId;
            var customer = _customerService.TGetByID(customerId);
            if (customer == null)
            {
                var returnIsValid = new
                {
                    status = "error",
                    message = "Müşteri bulunamadı!",
                    data = ModelState
                };

                return NotFound(returnIsValid);
            }

            var user = await _userManager.FindByIdAsync(customer.UserId.ToString());
            if (user == null)
            {
                var returnIsValid = new
                {
                    status = "error",
                    message = "Kullanıcı bulunamadı!",
                    data = ModelState
                };

                return NotFound(returnIsValid);
            }

            var countFavourite = _customerFavouriteService.TGetRecordCountByCustomerID(customerId);
            if (countFavourite >=  10)
            {
                var returnIsValid = new
                {
                    status = "error",
                    message = "Bu müşteriye ait maksimum sayıda favori kaydı bulunmaktadır!",
                    data = ModelState
                };

                return BadRequest(returnIsValid);
            }

            var company = _companyService.TGetByID(customerFavouriteAddDto.CompanyId);
            if (company == null)
            {
                var returnIsValid = new
                {
                    status = "error",
                    message = "İşletme bulunamadı!"
                };

                return NotFound(returnIsValid);
            }

            var isExist = _customerFavouriteService.TIsExist(customerFavouriteAddDto.CustomerId, customerFavouriteAddDto.CompanyId);
            if (isExist)
            {
                var returnIsValid = new
                {
                    status = "error",
                    message = "Bu müşteriye ait bu işletme favori kaydı bulunmaktadır!",
                    data = ModelState
                };

                return BadRequest(returnIsValid);
            }


            customerFavourite.Customer = customer;
            customerFavourite.Company = company;
            customerFavourite.CreatedAt = DateTime.Now;
            customerFavourite.UpdatedAt = DateTime.Now;

            _customerFavouriteService.TInsert(customerFavourite);

            var returnIsValidSuccess = new
            {
                status = "success",
                message = "İşlem başarılı!"
            };
            return Ok(returnIsValidSuccess);
        }


        [HttpGet]
        [Route("customer/favourite/list/byCustomerId")]
        public IActionResult GetCustomerFavouriteList(int customerId)
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

            var customer = _customerService.TGetByID(customerId);
            if (customer == null)
            {
                var returnIsValid = new
                {
                    status = "error",
                    message = "Müşteri bulunamadı!",
                    data = ModelState
                };

                return NotFound(returnIsValid);
            }

            var user = _userManager.FindByIdAsync(customer.UserId.ToString());
            if (user == null)
            {
                var returnIsValid = new
                {
                    status = "error",
                    message = "Kullanıcı bulunamadı!",
                    data = ModelState
                };

                return NotFound(returnIsValid);
            }

            var customerFavouriteList = _customerFavouriteService.TGetByCustomerID(customerId);
            if (customerFavouriteList == null)
            {
                var returnIsValid = new
                {
                    status = "error",
                    message = "Favori kaydı bulunamadı!",
                    data = ModelState
                };

                return NotFound(returnIsValid);
            }

            return Ok(customerFavouriteList);
        }

        [HttpPost]
        [Route("customer/favourite/delete")]

        public IActionResult DeleteCustomerFavourite(int id)
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

            var customerFavourite = _customerFavouriteService.TGetByID(id);
            if (customerFavourite == null)
            {
                var returnIsValid = new
                {
                    status = "error",
                    message = "Favori kaydı bulunamadı!",
                    data = ModelState
                };

                return NotFound(returnIsValid);
            }

            _customerFavouriteService.TDelete(customerFavourite);

            var returnIsValidSuccess = new
            {
                status = "success",
                message = "İşlem başarılı!"
            };
            return Ok(returnIsValidSuccess);
        }


    }
}
