using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using randevuburada.BusinessLayer.Abstract;
using randevuburada.EntityLayer.Concrete.CompanyConcrete.Service;

namespace Randevuburada.WebApi.Controller.CompanyController.Service
{
    [Route("api/v1")]
    [ApiController]
    public class ServiceIntervalHoursController : ControllerBase
    {
        private readonly IServiceIntervalHoursService _serviceIntervalHoursService;

        public ServiceIntervalHoursController(IServiceIntervalHoursService serviceIntervalHoursService)
        {
            _serviceIntervalHoursService = serviceIntervalHoursService;
        }


        [HttpGet]
        [Route("admin/service/interval-hours/collective/set")]
        public IActionResult AddCollectiveInterval()
        {
            var intervals = new List<int> { 15, 30, 45, 60 };

            var control = _serviceIntervalHoursService.TGetList();
            if (control.Count > 0)
            {
                var errorResponse = new
                {
                    code = 400,
                    status = "error",
                    message = "Saat aralıkları zaten ekli!"
                };
                return BadRequest(errorResponse);
            }

            foreach (var interval in intervals)
            {
                var mainServiceModel = new ServiceIntervalHours
                {
                    intervalTime = new TimeOnly(0, interval)
                };
                if (interval == 60)
                {
                    mainServiceModel = new ServiceIntervalHours
                    {
                        intervalTime = new TimeOnly(1, 0)
                    };
                }
                _serviceIntervalHoursService.TInsert(mainServiceModel);
            }

            var successResponse = new
            {
                code = 200,
                status = "success",
                message = "Saat aralıkları başarıyla eklendi!",
            };
            return Ok(successResponse);
        }

        [HttpPost]
        [Route("admin/service/interval-hours/set")]
        public IActionResult AddInterval(int time)
        {
            var existingInterval = _serviceIntervalHoursService.TGetList().Any(s => s.intervalTime.Minute == time);
            if (existingInterval)
            {
                var errorResponse = new
                {
                    code = 400,
                    status = "error",
                    message = "Belirtilen aralık zaten mevcut!"
                };
                return BadRequest(errorResponse);
            }

            var mainServiceModel = new ServiceIntervalHours();

            if (time < 60)
            {
                mainServiceModel.intervalTime = new TimeOnly(0, time);
            }
            else
            {
                int hours = time / 60;
                int minutes = time % 60;

                mainServiceModel.intervalTime = new TimeOnly(hours, minutes);
            }
            _serviceIntervalHoursService.TInsert(mainServiceModel);

            var successResponse = new
            {
                code = 200,
                status = "success",
                message = "Saat aralığı başarıyla eklendi!",
            };
            return Ok(successResponse);
        }

        [HttpPost]
        [Route("admin/service/interval-hours/update")]
        public IActionResult UpdateInterval(int id, int time)
        {
            var existingInterval = _serviceIntervalHoursService.TGetByID(id);
            if (existingInterval == null)
            {
                var errorResponse = new
                {
                    code = 404,
                    status = "error",
                    message = "Belirtilen aralık bulunamadı!"
                };
                return NotFound(errorResponse);
            }

            existingInterval.intervalTime = new TimeOnly(0, time, 0);
            _serviceIntervalHoursService.TUpdate(existingInterval);

            var successResponse = new
            {
                code = 200,
                status = "success",
                message = "Saat aralığı başarıyla güncellendi!",
            };
            return Ok(successResponse);
        }

        [HttpPost]
        [Route("admin/service/interval-hours/delete")]
        public IActionResult DeleteInterval(int id)
        {
            var existingInterval = _serviceIntervalHoursService.TGetByID(id);
            if (existingInterval == null)
            {
                var errorResponse = new
                {
                    code = 404,
                    status = "error",
                    message = "Belirtilen aralık bulunamadı!"
                };
                return NotFound(errorResponse);
            }

            _serviceIntervalHoursService.TDelete(existingInterval);

            var successResponse = new
            {
                code = 200,
                status = "success",
                message = "Saat aralığı başarıyla silindi!",
            };
            return Ok(successResponse);
        }

        [HttpGet]
        [Route("company/service/interval-hours/list")]
        public IActionResult GetMediaType()
        {
            var mediaTypes = _serviceIntervalHoursService.TGetList();
            if (mediaTypes == null || !mediaTypes.Any())
            {
                var errorResponse = new
                {
                    code = 404,
                    status = "error",
                    message = "Veritabanında kayıtlı saat aralığı bulanamadı!"
                };
                return BadRequest(errorResponse);
            }

            var successResponse = new
            {
                code = 200,
                status = "success",
                data = mediaTypes
            };

            return Ok(successResponse);
        }


        [HttpGet]
        [Route("company/service/interval-hours/get/byId")]
        public IActionResult GetMediaTypeById(int id)
        {
            var mediaTypes = _serviceIntervalHoursService.TGetByID(id);
            if (mediaTypes == null)
            {
                var errorResponse = new
                {
                    code = 404,
                    status = "error",
                    message = "Veritabanında ilgili idye kayıtlı saat aralığı bulanamadı!"
                };
                return BadRequest(errorResponse);
            }

            var successResponse = new
            {
                code = 200,
                status = "success",
                data = mediaTypes
            };

            return Ok(successResponse);
        }


    }
}
