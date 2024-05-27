using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using randevuburada.BusinessLayer.Abstract;
using randevuburada.DtoLayer.Dtos.CustomerDto.CustomerCommentDto;
using randevuburada.EntityLayer.Concrete.CustomerConcrete;
using randevuburada.EntityLayer.Concrete.Identity;

namespace Randevuburada.WebApi.Controller.CustomerController
{
    [Route("api/v1")]
    [ApiController]
    public class CustomerCommentController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICustomerService _customerService;
        private readonly ICompanyService _companyService;
        private readonly ICustomerCommentService _customerCommentService;

        public CustomerCommentController(ICustomerCommentService customerCommentService, IMapper mapper, UserManager<AppUser> userManager, ICustomerService customerService, ICompanyService companyService)
        {
            _customerCommentService = customerCommentService;
            _mapper = mapper;
            _userManager = userManager;
            _customerService = customerService;
            _companyService = companyService;
        }

        [HttpPost]
        [Route("customer/comment/set")]
        public IActionResult AddCustomerComment(CustomerCommentAddDto customerCommentAddDto)
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

            var customerComment = _mapper.Map<CustomerComment>(customerCommentAddDto);
            var customerId = customerCommentAddDto.CustomerId;
            var companyId = customerCommentAddDto.CompanyId;
            var customer = _customerService.TGetByID(customerId);
            var company = _companyService.TGetByID(companyId);
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
            if (company == null)
            {
                var returnIsValid = new
                {
                    status = "error",
                    message = "Firma bulunamadı!",
                    data = ModelState
                };

                return NotFound(returnIsValid);
            }

            customerComment.CommentDate = DateTime.Now;
            customerComment.SystemApproved = true;
            customerComment.CreatedAt = DateTime.Now;
            customerComment.UpdatedAt = DateTime.Now;

            _customerCommentService.TInsert(customerComment);
            return Ok();
        }

        [HttpGet]
        [Route("customer/comment/list")]
        public IActionResult GetCustomerCommentList(int customerId)
        {
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
            var customerCommentList = _customerCommentService.TGetCustomerCommentByCustomerId(customerId);
            if (customerCommentList == null)
            {
                var returnIsValid = new
                {
                    status = "error",
                    message = "Yorum bulunamadı!",
                    data = ModelState
                };

                return NotFound(returnIsValid);
            }
            var returnSuccess = new
            {
                status = "success",
                message = "Yorumlar başarıyla getirildi!",
                data = customerCommentList
            };

            return Ok(returnSuccess);
        }

        [HttpGet]
        [Route("customer/comment/get")]
        public IActionResult GetCustomerComment(int id)
        {
            var customerComment = _customerCommentService.TGetByID(id);
            if (customerComment == null)
            {
                var returnIsValid = new
                {
                    status = "error",
                    message = "Yorum bulunamadı!",
                    data = ModelState
                };

                return NotFound(returnIsValid);
            }
            return Ok(customerComment);
        }

        [HttpGet]
        [Route("customer/comment/delete")]
        public IActionResult DeleteCustomerComment(int id)
        {
            var customerComment = _customerCommentService.TGetByID(id);
            if (customerComment == null)
            {
                var returnIsValid = new
                {
                    status = "error",
                    message = "Yorum bulunamadı!",
                    data = ModelState
                };

                return NotFound(returnIsValid);
            }
            _customerCommentService.TDelete(customerComment);
            return Ok();
        }
    }
}
