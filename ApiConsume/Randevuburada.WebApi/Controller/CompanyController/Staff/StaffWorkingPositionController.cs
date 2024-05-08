using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using randevuburada.BusinessLayer.Abstract;
using randevuburada.EntityLayer.Concrete.CompanyConcrete.Staff;

namespace Randevuburada.WebApi.Controller.CompanyController.Staff
{
    [Route("api/v1")]
    [ApiController]
    public class StaffWorkingPositionController : ControllerBase
    {

        private readonly IStaffWorkingPositionService _staffWorkingPositionService;

        public StaffWorkingPositionController(IStaffWorkingPositionService staffWorkingPositionService)
        {
            _staffWorkingPositionService = staffWorkingPositionService;
        }


        [HttpGet]
        [Route("admin/staff/workingPosition/collective/set")]
        public IActionResult AddWorkingPositionCollective()
        {
            var workingPositions = new List<string> { "Berber", "Kuaför", "Müdür", "Yönetici", "Asistan" };

            var control = _staffWorkingPositionService.TGetList();
            if (control.Count > 0)
            {
                var errorResponse = new
                {
                    code = 400,
                    status = "error",
                    message = "Çalışma tipleri zaten ekli!"
                };
                return BadRequest(errorResponse);
            }
            foreach (var workingPosition in workingPositions)
            {
                var workingPositionModel = new StaffWorkingPosition
                {
                    PositionName = workingPosition
                };
                _staffWorkingPositionService.TInsert(workingPositionModel);
            }

            var successResponse = new
            {
                code = 200,
                status = "success",
                message = "Çalışma pozisyonları başarıyla eklendi!"
            };
            return Ok(successResponse);
        }

        [HttpPost]
        [Route("admin/staff/workingPosition/set")]
        public IActionResult AddWorkingPositionSingular(string positionName)
        {

            var existing = _staffWorkingPositionService.TGetList().FirstOrDefault(mt => mt.PositionName == positionName);
            if (existing != null)
            {
                var errorResponse = new
                {
                    code = 400,
                    status = "error",
                    message = "Bu isimde bir çalışma pozisyonu zaten mevcut!"
                };
                return BadRequest(errorResponse);
            }

            var workingPositionModel = new StaffWorkingPosition
            {
                PositionName = positionName
            };
           
            _staffWorkingPositionService.TInsert(workingPositionModel);

            var successResponse = new
            {
                code = 200,
                status = "success",
                message = "Çalışma pozisyonu başarıyla eklendi!"
            };
            return Ok(successResponse);
        }

        [HttpPost]
        [Route("admin/staff/workingPosition/update")]
        public IActionResult UpdateMediaTypeBySingular(int id, string positionName)
        {
            var existing = _staffWorkingPositionService.TGetList().FirstOrDefault(mt => mt.Id == id);
            if (existing == null)
            {
                var errorResponse = new
                {
                    code = 404,
                    status = "error",
                    message = "Belirtilen id ile çalışan pozisyon bulunamadı!"
                };
                return BadRequest(errorResponse);
            }

            existing.PositionName = positionName;
            _staffWorkingPositionService.TUpdate(existing);

            var successResponse = new
            {
                code = 200,
                status = "success",
                message = "Çalışan pozisyon başarıyla güncellendi!"
            };
            return Ok(successResponse);
        }

        [HttpPost]
        [Route("admin/staff/workingPosition/delete")]
        public IActionResult DeleteMediaTypeById(int id)
        {
            var existing = _staffWorkingPositionService.TGetByID(id);
            if (existing == null)
            {
                var errorResponse = new
                {
                    code = 404,
                    status = "error",
                    message = "Veritabanında id ile ilgili kayıtlı medya tipi bulunamadı!"
                };
                return BadRequest(errorResponse);
            }

            _staffWorkingPositionService.TDelete(existing); // Medya türünü sil

            var successResponse = new
            {
                code = 200,
                status = "success",
                message = "Medya tipi başarıyla silindi!"
            };

            return Ok(successResponse);
        }


        [HttpGet]
        [Route("company/staff/workingPosition/list")]
        public IActionResult GetWorkingPosition()
        {
            var result = _staffWorkingPositionService.TGetList();
            if (result == null || !result.Any())
            {
                var errorResponse = new
                {
                    code = 404,
                    status = "error",
                    message = "Veritabanında kayıtlı medya tipi bulanamadı!"
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
        [Route("company/staff/workingPosition/get/byId")]
        public IActionResult GetWorkingPositionById(int id)
        {
            var result = _staffWorkingPositionService.TGetByID(id);
            if (result == null)
            {
                var errorResponse = new
                {
                    code = 404,
                    status = "error",
                    message = "Veritabanında id ile ilgili kayıtlı medya tipi bulanamadı!"
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
