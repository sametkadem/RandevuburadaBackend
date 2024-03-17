using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using randevuburada.BusinessLayer.Abstract;
using randevuburada.EntityLayer.Concrete.Other;

namespace Randevuburada.WebApi.Controller.OtherController
{
    [Route("api/v1/")]
    [ApiController]
    public class DayController : ControllerBase
    {
        private readonly IDayService _dayService;

        public DayController(IDayService dayService)
        {
            _dayService = dayService;
        }

        [HttpGet]
        [Route("admin/add/day")]
        public IActionResult AddDay()
        {
            var days = new List<string> { "Pazartesi", "Salı", "Çarşamba", "Perşembe", "Cuma", "Cumartesi", "Pazar" };

            var control = _dayService.TGetList();
            if (control.Count > 0)
            {
                var errorResponse = new
                {
                    code = 400,
                    status = "error",
                    message = "Günler zaten ekli!"
                };
                return BadRequest(errorResponse);
            }

            foreach (var day in days)
            {
                var dayModel = new Day
                {
                    DayName = day
                };
                _dayService.TInsert(dayModel);
            }
            return Ok();
        }

        [HttpGet]
        [Route("get/list/day")]
        public IActionResult GetDay()
        {
            var days = _dayService.TGetList();
            if(days == null)
            {
                var errorResponse = new
                {
                    code = 404,
                    status = "error",
                    message = "Veritabanında kayıtlı gün bulanamadı!"
                };
                return BadRequest(errorResponse);
            }

            var successResponse = new
            {
                code = 200,
                status = "success",
                data = days
            };

            return Ok(successResponse);
        }

        [HttpGet]
        [Route("get/byId/day")]
        public IActionResult GetByIdDay(int id)
        {
            var days = _dayService.TGetByID(id);
            if (days == null)
            {
                var errorResponse = new
                {
                    code = 404,
                    status = "error",
                    message = "Veritabanında kayıtlı id ile ilgili gün bulanamadı!"
                };
                return BadRequest(errorResponse);
            }

            var successResponse = new
            {
                code = 200,
                status = "success",
                data = days
            };

            return Ok(successResponse);
        }
    }
}
