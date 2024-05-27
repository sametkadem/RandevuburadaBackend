using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using randevuburada.BusinessLayer.Abstract;
using randevuburada.DtoLayer.Dtos.CompanyDto.CompanyCommentDto;
using randevuburada.EntityLayer.Concrete.Identity;

namespace Randevuburada.WebApi.Controller.CompanyController
{
    [Route("api/v1")]
    [ApiController]
    public class CompanyCommentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICustomerService _customerService;
        private readonly ICompanyService _companyService;
        private readonly ICustomerCommentService _customerCommentService;

        public CompanyCommentController(ICustomerCommentService customerCommentService, IMapper mapper, UserManager<AppUser> userManager, ICustomerService customerService, ICompanyService companyService)
        {
            _customerCommentService = customerCommentService;
            _mapper = mapper;
            _userManager = userManager;
            _customerService = customerService;
            _companyService = companyService;
        }


        [HttpPost]
        [Route("company/comment/answer")]
        public IActionResult AddCompanyCommentAnswer(CompanyCommentAnswerDto companyCommentAnswerDto)
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

            var comment = _customerCommentService.TGetByID(companyCommentAnswerDto.Id);
            if (comment == null)
            {
                var returnIsValid = new
                {
                    status = "error",
                    message = "Yorum bulunamadı!",
                    data = ModelState
                };
                return BadRequest(returnIsValid);
            }

            comment.Answer = companyCommentAnswerDto.Answer;
            comment.UpdatedAt = DateTime.UtcNow;

            _customerCommentService.TUpdate(comment);

            var returnValid = new
            {
                status = "success",
                message = "Yorum başarıyla güncellendi!"
            };
            return Ok(returnValid);
        }

        [HttpGet]
        [Route("company/comment/list")]
        public IActionResult GetCompanyComment(int companyId)
        {
            var company = _companyService.TGetByID(companyId);
            if (company == null)
            {
                var returnIsValid1 = new
                {
                    status = "error",
                    message = "Firma bulunamadı!",
                    data = ModelState
                };
                return NotFound(returnIsValid1);
            }

            var comments = _customerCommentService.TGetCustomerCommentByCompanyId(companyId);
            if (comments == null)
            {
                var returnIsValid2 = new
                {
                    status = "error",
                    message = "Yorum bulunamadı!",
                    data = ModelState
                };
                return NotFound(returnIsValid2);
            }
            var returnIsValid3 = new
            {
                status = "success",
                message = "Yorumlar başarıyla getirildi!",
                data = comments
            };

            return Ok(returnIsValid3);

           
        }

    }
}
