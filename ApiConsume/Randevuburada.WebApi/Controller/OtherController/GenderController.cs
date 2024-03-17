using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using randevuburada.BusinessLayer.Abstract;
using randevuburada.EntityLayer.Concrete.Other;

namespace Randevuburada.WebApi.Controller.OtherController
{
    [Route("api/v1")]
    [ApiController]


    public class GenderController : ControllerBase
    {
       public readonly IGenderService _genderService;
       
       public GenderController(IGenderService genderService)
       {
           _genderService = genderService;
       }

        [HttpGet]
        [Route("admin/add/gender")]
        public IActionResult AddGender()
        {
            var control = _genderService.TGetList();
            if (control.Count > 0)
            {
                var errorResponse = new
                {
                    code = 400,
                    status = "error",
                    message = "Cinsiyetler zaten ekli!"
                };
                return BadRequest(errorResponse);
            }

            var genders = new List<string> { "Erkek", "Kadın", "Unisex"};
            foreach (var gender in genders)
            {
                var genderModel = new Gender
                {
                    GenderName = gender
                };
                _genderService.TInsert(genderModel);
            }

            var successResponse = new
            {
                code = 200,
                status = "success",
                message = "Cinsiyetler başarıyla eklendi!"
            };
            return Ok(successResponse);
        }

        [HttpGet]
        [Route("get/list/gender")]
        public IActionResult GetGender()
        {
            var genders = _genderService.TGetList();
            if (genders == null)
            {
                var errorResponse = new
                {
                    code = 404,
                    status = "error",
                    message = "Veritabanında kayıtlı cinsiyet bulanamadı!"
                };
                return NotFound(errorResponse);
            }

            var successResponse = new
            {
                code = 200,
                status = "success",
                data = genders
            };

            return Ok(successResponse);
        }

        [HttpGet]
        [Route("get/byId/gender/")]
        public IActionResult GetGenderById(int id)
        {
            var gender = _genderService.TGetByID(id);
            if (gender == null)
            {
                var errorResponse = new
                {
                    code = 404,
                    status = "error",
                    message = "Belirtilen id ile cinsiyet bulunamadı!"
                };
                return NotFound(errorResponse);
            }

            var successResponse = new
            {
                code = 200,
                status = "success",
                data = gender
            };

            return Ok(successResponse);
        }

    }
}
