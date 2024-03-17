using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using randevuburada.BusinessLayer.Abstract;
using randevuburada.EntityLayer.Concrete.CompanyConcrete.Staff;

namespace Randevuburada.WebApi.Controller.CompanyController.Staff
{
    [Route("api/v1")]
    [ApiController]
    public class StaffWorkingStatusController : ControllerBase
    {

        public readonly IStaffWorkingStatusService _staffWorkingStatusService;

        public StaffWorkingStatusController(IStaffWorkingStatusService staffWorkingStatusService)
        {
            _staffWorkingStatusService = staffWorkingStatusService;
        }

        [HttpGet]
        [Route("admin/add/collective/staff/workingStatus")]
        public IActionResult AddWorkingStatusCollective()
        {
            var workingStatus = new List<string> { "İzinde", "Çalışıyor", "İşten Ayrıldı", "Doğum İzninde", "Raporlu", "Diğer" };

            var control = _staffWorkingStatusService.TGetList();
            if (control.Count > 0)
            {
                var errorResponse = new
                {
                    code = 400,
                    status = "error",
                    message = "Çalışma durumları zaten ekli!"
                };
                return BadRequest(errorResponse);
            }
            foreach (var workingStat in workingStatus)
            {
                var workingStautsModel = new StaffWorkingStatus
                {
                    StatusName = workingStat
                };
                _staffWorkingStatusService.TInsert(workingStautsModel);
            }

            var successResponse = new
            {
                code = 200,
                status = "success",
                message = "Çalışma durumları başarıyla eklendi!"
            };
            return Ok(successResponse);
        }

        [HttpPost]
        [Route("admin/add/singular/staff/workingStatus")]
        public IActionResult AddWorkingPositionSingular(string statusName)
        {

            var existing = _staffWorkingStatusService.TGetList().FirstOrDefault(mt => mt.StatusName == statusName);
            if (existing != null)
            {
                var errorResponse = new
                {
                    code = 400,
                    status = "error",
                    message = "Bu isimde bir çalışma durumu zaten mevcut!"
                };
                return BadRequest(errorResponse);
            }

            var workingStatusModel = new StaffWorkingStatus
            {
                StatusName = statusName
            };
        
            _staffWorkingStatusService.TInsert(workingStatusModel);

            var successResponse = new
            {
                code = 200,
                status = "success",
                message = "Çalışma pozisyonu başarıyla eklendi!"
            };
            return Ok(successResponse);
        }

        [HttpPost]
        [Route("admin/update/singular/staff/workingStatus")]
        public IActionResult UpdateWorkingStatusBySingular(int id, string statusName)
        {
            var existing = _staffWorkingStatusService.TGetList().FirstOrDefault(mt => mt.Id == id);
            if (existing == null)
            {
                var errorResponse = new
                {
                    code = 404,
                    status = "error",
                    message = "Belirtilen id ile çalışma durumu bulunamadı!"
                };
                return BadRequest(errorResponse);
            }

            existing.StatusName = statusName;
            _staffWorkingStatusService.TUpdate(existing);

            var successResponse = new
            {
                code = 200,
                status = "success",
                message = "Çalışma durumu başarıyla güncellendi!"
            };
            return Ok(successResponse);
        }

        [HttpPost]
        [Route("admin/delete/byId/staff/workingStatus")]
        public IActionResult DeleteWorkingStatusById(int id)
        {
            var existing = _staffWorkingStatusService.TGetByID(id);
            if (existing == null)
            {
                var errorResponse = new
                {
                    code = 404,
                    status = "error",
                    message = "Veritabanında id ile ilgili kayıtlı çalışma durumu bulunamadı!"
                };
                return BadRequest(errorResponse);
            }

            _staffWorkingStatusService.TDelete(existing); // Medya türünü sil

            var successResponse = new
            {
                code = 200,
                status = "success",
                message = "Çalışma durumu başarıyla silindi!"
            };

            return Ok(successResponse);
        }


        [HttpGet]
        [Route("get/list/staff/workingStatus")]
        public IActionResult GetWorkingPosition()
        {
            var result = _staffWorkingStatusService.TGetList();
            if (result == null || !result.Any())
            {
                var errorResponse = new
                {
                    code = 404,
                    status = "error",
                    message = "Veritabanında kayıtlı çalışma durumu bulanamadı!"
                };
                return BadRequest(errorResponse);
            }

            var successResponse = new
            {
                code = 200,
                status = "success",
                data = result
            };

            return Ok(successResponse);
        }

        [HttpGet]
        [Route("get/byId/staff/workingStatus")]
        public IActionResult GetWorkingPositionById(int id)
        {
            var result = _staffWorkingStatusService.TGetByID(id);
            if (result == null)
            {
                var errorResponse = new
                {
                    code = 404,
                    status = "error",
                    message = "Veritabanında id ile ilgili kayıtlı çalışma durumu bulanamadı!"
                };
                return BadRequest(errorResponse);
            }

            var successResponse = new
            {
                code = 200,
                status = "success",
                data = result
            };

            return Ok(successResponse);
        }
    }
}
