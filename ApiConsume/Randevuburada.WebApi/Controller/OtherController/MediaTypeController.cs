using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using randevuburada.BusinessLayer.Abstract;
using randevuburada.EntityLayer.Concrete.CompanyConcrete.Service;
using randevuburada.EntityLayer.Concrete.Other;
using System.Net.Mime;

namespace Randevuburada.WebApi.Controller.OtherController
{
    [Route("api/v1")]
    [ApiController]
    public class MediaTypeController : ControllerBase
    {
        public readonly IMediaTypeService _mediaTypeService;

        public MediaTypeController(IMediaTypeService mediaTypeService)
        {
            _mediaTypeService = mediaTypeService;
        }

        [HttpGet]
        [Route("admin/add/collective/mediaType")]
        public IActionResult AddMediaType()
        {
            var mediaTypes = new List<string> { "Profil Fotoğrafı", "Öne Çıkan Görsel", "1.Görsel", "2.Görsel", "3.Görsel" };

            var control = _mediaTypeService.TGetList();
            if(control.Count > 0)
            {
                var errorResponse = new
                {
                    code = 400,
                    status = "error",
                    message = "Medya tipleri zaten ekli!"
                };
                return BadRequest(errorResponse);
            }
            foreach (var mediaType in mediaTypes)
            {
                var mediaTypeModel = new MediaType
                {
                    MediaTypeName = mediaType
                };
                _mediaTypeService.TInsert(mediaTypeModel);
            }

            var successResponse = new
            {
                code = 200,
                status = "success",
                message = "Medya tipleri başarıyla eklendi!"
            };
            return Ok(successResponse);
        }

        [HttpPost]
        [Route("admin/add/singular/mediaType")]
        public IActionResult AddMediaTypeSingular(string mediaTypeName)
        {

            var existingMediaType = _mediaTypeService.TGetList().FirstOrDefault(mt => mt.MediaTypeName == mediaTypeName);
            if (existingMediaType != null)
            {
                var errorResponse = new
                {
                    code = 400,
                    status = "error",
                    message = "Bu isimde bir medya tipi zaten mevcut!"
                };
                return BadRequest(errorResponse);
            }

            var mainServiceModel = new MediaType
            {
                MediaTypeName = mediaTypeName
            };
            

            _mediaTypeService.TInsert(mainServiceModel);

            var successResponse = new
            {
                code = 200,
                status = "success",
                message = "Ana hizmet başarıyla eklendi!"
            };
            return Ok(successResponse);
        }

        [HttpPost]
        [Route("admin/update/singular/mediaType/")]
        public IActionResult UpdateMediaTypeBySingular(int id, string mediaTypeName)
        {
            var existingMediaType = _mediaTypeService.TGetList().FirstOrDefault(mt => mt.Id == id);
            if (existingMediaType == null)
            {
                var errorResponse = new
                {
                    code = 404,
                    status = "error",
                    message = "Belirtilen id ile medya tipi bulunamadı!"
                };
                return BadRequest(errorResponse);
            }

            existingMediaType.MediaTypeName = mediaTypeName;
            _mediaTypeService.TUpdate(existingMediaType);

            var successResponse = new
            {
                code = 200,
                status = "success",
                message = "Medya tipi başarıyla güncellendi!"
            };
            return Ok(successResponse);
        }

        [HttpPost]
        [Route("admin/delete/byId/mediaType")]
        public IActionResult DeleteMediaTypeById(int id)
        {
            var mediaType = _mediaTypeService.TGetByID(id);
            if (mediaType == null)
            {
                var errorResponse = new
                {
                    code = 404,
                    status = "error",
                    message = "Veritabanında id ile ilgili kayıtlı medya tipi bulunamadı!"
                };
                return BadRequest(errorResponse);
            }

            _mediaTypeService.TDelete(mediaType); // Medya türünü sil

            var successResponse = new
            {
                code = 200,
                status = "success",
                message = "Medya tipi başarıyla silindi!"
            };

            return Ok(successResponse);
        }


        [HttpGet]
        [Route("get/list/mediaType")]
        public IActionResult GetMediaType()
        {
            var mediaTypes = _mediaTypeService.TGetList();
            if (mediaTypes == null || !mediaTypes.Any())
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
                data = mediaTypes
            };

            return Ok(successResponse);
        }

        [HttpGet]
        [Route("get/byId/mediaType")]
        public IActionResult GetMediaTypeById(int id)
        {
            var mediaTypes = _mediaTypeService.TGetByID(id);
            if (mediaTypes == null)
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
                data = mediaTypes
            };

            return Ok(successResponse);
        }
    }
}
