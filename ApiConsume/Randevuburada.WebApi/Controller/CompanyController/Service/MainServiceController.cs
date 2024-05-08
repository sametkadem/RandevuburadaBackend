using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using randevuburada.BusinessLayer.Abstract;
using randevuburada.EntityLayer.Concrete.CompanyConcrete.Service;
using randevuburada.EntityLayer.Concrete.Other;
using System.Net.Mime;

namespace Randevuburada.WebApi.Controller.CompanyController.Other
{
    [Route("api/v1")]
    [ApiController]
    public class MainServiceController : ControllerBase
    {

        private readonly IMainServiceService _mainService;

        public MainServiceController(IMainServiceService mainService)
        {
            _mainService = mainService;
        }

        [HttpGet]
        [Route("admin/company/service/main/set/collective")]
        public IActionResult AddCollectiveMainService()
        {
            var mainServices = new List<(string, string)>
            {
                ("Erkek Saç Hizmet", "Erkek saç kesimi ve bakımı hizmeti"),
                ("Kadın Saç Hizmet", "Kadın saç kesimi ve bakımı hizmeti"),
                ("Kadın Makyaj Hizmet", "Kadın makyaj hizmeti")
            }; 
            var control = _mainService.TGetList();
            if (control.Count > 0)
            {
                var errorResponse = new
                {
                    code = 400,
                    status = "error",
                    message = "Ana Hizmet tipleri zaten ekli!"
                };
                return BadRequest(errorResponse);
            }

            foreach (var (serviceName, description) in mainServices)
            {
                var mainServiceModel = new MainService
                {
                    ServiceName = serviceName,
                    ServiceDescription = description
                };
                _mainService.TInsert(mainServiceModel);
            }

            var successResponse = new
            {
                code = 200,
                status = "success",
                message = "Ana hizmetler başarıyla eklendi!",
            };
            return Ok(successResponse);
        }


        [HttpPost]
        [Route("admin/company/service/main/set")]
        public IActionResult AddMainService(string mainServiceName, string mainServiceDescription)
        {

            var existingMediaType = _mainService.TGetList().FirstOrDefault(mt => mt.ServiceName == mainServiceName);
            if (existingMediaType != null)
            {
                var errorResponse = new
                {
                    code = 400,
                    status = "error",
                    message = "Bu isimde bir ana hizmet zaten mevcut!"
                };
                return BadRequest(errorResponse);
            }

            var mainServiceModel = new MainService
            {
                ServiceName = mainServiceName,
                ServiceDescription = mainServiceDescription
            };

            _mainService.TInsert(mainServiceModel);

            var successResponse = new
            {
                code = 200,
                status = "success",
                message = "Ana hizmet başarıyla eklendi!"
            };
            return Ok(successResponse);
        }

        [HttpPost]
        [Route("admin/service/main/update")]
        public IActionResult UpdateMediaTypeBySingular(int id, string serviceName, string serviceDescription)
        {
            var existingMediaType = _mainService.TGetList().FirstOrDefault(mt => mt.Id == id);
            if (existingMediaType == null)
            {
                var errorResponse = new
                {
                    code = 404,
                    status = "error",
                    message = "Belirtilen id ile ana hizmet bulunamadı!"
                };
                return BadRequest(errorResponse);
            }

            existingMediaType.ServiceName = serviceName;
            existingMediaType.ServiceDescription = serviceDescription;

            _mainService.TUpdate(existingMediaType);

            var successResponse = new
            {
                code = 200,
                status = "success",
                message = "Ana hizmet başarıyla güncellendi!"
            };
            return Ok(successResponse);
        }

        [HttpPost]
        [Route("admin/company/service/main/delete")]
        public IActionResult DeleteMainService(int id)
        {
            var mainService = _mainService.TGetByID(id);
            if (mainService == null)
            {
                var errorResponse = new
                {
                    code = 404,
                    status = "error",
                    message = "Veritabanında kayıtlı id ile ilgili hizmet bulunamadı!"
                };
                return BadRequest(errorResponse);
            }

            _mainService.TDelete(mainService); // Ana hizmeti sil

            var successResponse = new
            {
                code = 200,
                status = "success",
                message = "Ana hizmet başarıyla silindi!"
            };

            return Ok(successResponse);
        }

        [HttpGet]
        [Route("company/service/main/list")]
        public IActionResult GetMainService()
        {
            var mainService = _mainService.TGetList();
            if (mainService == null)
            {
                var errorResponse = new
                {
                    code = 404,
                    status = "error",
                    message = "Veritabanında kayıtlı ana hizmet bulanamadı!"
                };
                return BadRequest(errorResponse);
            }

            var successResponse = new
            {
                code = 200,
                status = "success",
                data = mainService
            };

            return Ok(successResponse);
        }


        [HttpGet]
        [Route("company/service/get/byId")]
        public IActionResult GetMainService(int id)
        {
            var mainService = _mainService.TGetByID(id);
            if (mainService == null)
            {
                var errorResponse = new
                {
                    code = 404,
                    status = "error",
                    message = "Veritabanında kayıtlı id ile ilgili hizmet bulanamadı!"
                };
                return BadRequest(errorResponse);
            }

            var successResponse = new
            {
                code = 200,
                status = "success",
                data = mainService
            };

            return Ok(successResponse);
        }
    }
}
