using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using randevuburada.BusinessLayer.Abstract;
using randevuburada.EntityLayer.Concrete.AppointmentConcrete;
using randevuburada.EntityLayer.Concrete.Other;

namespace Randevuburada.WebApi.Controller.AppointmentController
{
    [Route("api/v1")]
    [ApiController]
    public class AppointmentStatusController : ControllerBase
    {
        public readonly IAppointmentStatusService _appointmentStatusService;

        public AppointmentStatusController(IAppointmentStatusService appointmentStatusService)
        {
            _appointmentStatusService = appointmentStatusService;
        }

        [HttpGet]
        [Route("admin/appointment/status/set/collective")]
        public IActionResult AddAppointmentStatus()
        {
            var status = new List<string> { "İşleme Alındı", "Onaylandı", "Reddedildi", "İptal Edildi", "Tamamlandı" };

            var control = _appointmentStatusService.TGetList();
            if (control.Count > 0)
            {
                var errorResponse = new
                {
                    code = 400,
                    status = "error",
                    message = "Randevu durum tipleri zaten ekli!"
                };
                return BadRequest(errorResponse);
            }
            foreach (var item in status)
            {
                var statusTypeModel = new AppointmentStatus
                {
                    Status = item
                };
                _appointmentStatusService.TInsert(statusTypeModel);
            }

            var successResponse = new
            {
                code = 200,
                status = "success",
                message = "Randevu durum tipleri başarıyla eklendi!"
            };
            return Ok(successResponse);
        }

        [HttpPost]
        [Route("admin/appointment/status/set")]
        public IActionResult AddAppointmentStatusSingular(string statusName)
        {

            var existing = _appointmentStatusService.TGetList().FirstOrDefault(mt => mt.Status == statusName);
            if (existing != null)
            {
                var errorResponse = new
                {
                    code = 400,
                    status = "error",
                    message = "Bu isimde bir randevu durum tipi zaten mevcut!"
                };
                return BadRequest(errorResponse);
            }

            var statusModel = new AppointmentStatus
            {
                Status = statusName
            };

            _appointmentStatusService.TInsert(statusModel);

            var successResponse = new
            {
                code = 200,
                status = "success",
                message = "Randevu durum tipi başarıyla eklendi!"
            };
            return Ok(successResponse);
        }

        [HttpPost]
        [Route("admin/appointment/status/update")]
        public IActionResult UpdateAppointmentStatusBySingular(int id, string status)
        {
            var existing = _appointmentStatusService.TGetList().FirstOrDefault(mt => mt.Id == id);
            if (existing == null)
            {
                var errorResponse = new
                {
                    code = 404,
                    status = "error",
                    message = "Belirtilen id ile randevu durum tipi bulunamadı!"
                };
                return BadRequest(errorResponse);
            }

            existing.Status = status;
            _appointmentStatusService.TUpdate(existing);

            var successResponse = new
            {
                code = 200,
                status = "success",
                message = "Randevu durum tipi başarıyla güncellendi!"
            };
            return Ok(successResponse);
        }

        [HttpPost]
        [Route("admin/appointment/status/delete/byId")]
        public IActionResult DeleteAppointmentStatusById(int id)
        {
            var status = _appointmentStatusService.TGetByID(id);
            if (status == null)
            {
                var errorResponse = new
                {
                    code = 404,
                    status = "error",
                    message = "Veritabanında id ile ilgili kayıtlı randevu durum tipi bulunamadı!"
                };
                return BadRequest(errorResponse);
            }

            _appointmentStatusService.TDelete(status);

            var successResponse = new
            {
                code = 200,
                status = "success",
                message = "Randevu durum tipi başarıyla silindi!"
            };

            return Ok(successResponse);
        }


        [HttpGet]
        [Route("appointment/status/list")]
        public IActionResult GetAppointmentStatus()
        {
            var types = _appointmentStatusService.TGetList();
            if (types == null || !types.Any())
            {
                var errorResponse = new
                {
                    code = 404,
                    status = "error",
                    message = "Veritabanında kayıtlı randevu durum tipi bulanamadı!"
                };
                return BadRequest(errorResponse);
            }

            var successResponse = new
            {
                code = 200,
                status = "success",
                data = types
            };

            return Ok(successResponse);
        }

        [HttpGet]
        [Route("appointment/status/byId")]
        public IActionResult GetAppointmentStatusById(int id)
        {
            var status = _appointmentStatusService.TGetByID(id);
            if (status == null)
            {
                var errorResponse = new
                {
                    code = 404,
                    status = "error",
                    message = "Veritabanında id ile ilgili kayıtlı randevu durum tipi bulanamadı!"
                };
                return BadRequest(errorResponse);
            }

            var successResponse = new
            {
                code = 200,
                status = "success",
                data = status
            };

            return Ok(successResponse);
        }
    }
}
