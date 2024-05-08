using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using randevuburada.BusinessLayer.Abstract;
using randevuburada.EntityLayer.Concrete.Other;

namespace Randevuburada.WebApi.Controller.AdminController
{
    [Route("api/v1")]
    [ApiController]
    public class PaymentTypeController : ControllerBase
    {
        public readonly IPaymentTypeService _paymentTypeService;

        public PaymentTypeController(IPaymentTypeService paymentTypeService)
        {
            _paymentTypeService = paymentTypeService;
        }


        [HttpGet]
        [Route("admin/payment/type/set/collective")]
        public IActionResult AddPaymentType()
        {
            var paymentTypes = new List<string> { "Kredi Kartı", "Nakit"};

            var control = _paymentTypeService.TGetList();
            if (control.Count > 0)
            {
                var errorResponse = new
                {
                    code = 400,
                    status = "error",
                    message = "Ödeme tipleri zaten ekli!"
                };
                return BadRequest(errorResponse);
            }
            foreach (var paymentType in paymentTypes)
            {
                var paymnetTypeModel = new PaymentType
                {
                    TypeName = paymentType
                };
                _paymentTypeService.TInsert(paymnetTypeModel);
            }

            var successResponse = new
            {
                code = 200,
                status = "success",
                message = "Ödeme tipleri başarıyla eklendi!"
            };
            return Ok(successResponse);
        }

        [HttpPost]
        [Route("admin/payment/type/set")]
        public IActionResult AddMediaTypeSingular(string paymentTypeName)
        {

            var existingPaymentType = _paymentTypeService.TGetList().FirstOrDefault(mt => mt.TypeName == paymentTypeName);
            if (existingPaymentType != null)
            {
                var errorResponse = new
                {
                    code = 400,
                    status = "error",
                    message = "Bu isimde bir ödeme tipi zaten mevcut!"
                };
                return BadRequest(errorResponse);
            }

            var mainServiceModel = new PaymentType
            {
                TypeName = paymentTypeName
            };
         


            _paymentTypeService.TInsert(mainServiceModel);

            var successResponse = new
            {
                code = 200,
                status = "success",
                message = "Ödeme tipi başarıyla eklendi!"
            };
            return Ok(successResponse);
        }

        [HttpPost]
        [Route("admin/payment/type/update")]
        public IActionResult UpdatePaymentTypeBySingular(int id, string paymentTypeName)
        {
            var existingPaymentType = _paymentTypeService.TGetList().FirstOrDefault(mt => mt.Id == id);
            if (existingPaymentType == null)
            {
                var errorResponse = new
                {
                    code = 404,
                    status = "error",
                    message = "Belirtilen id ile ödeme tipi bulunamadı!"
                };
                return BadRequest(errorResponse);
            }

            existingPaymentType.TypeName = paymentTypeName;
            _paymentTypeService.TUpdate(existingPaymentType);

            var successResponse = new
            {
                code = 200,
                status = "success",
                message = "Ödeme tipi başarıyla güncellendi!"
            };
            return Ok(successResponse);
        }

        [HttpPost]
        [Route("admin/payment/type/delete")]
        public IActionResult DeletePaymentTypeById(int id)
        {
            var paymentType = _paymentTypeService.TGetByID(id);
            if (paymentType == null)
            {
                var errorResponse = new
                {
                    code = 400,
                    status = "error",
                    message = "Veritabanında id ile ilgili kayıtlı ödeme tipi bulunamadı!"
                };
                return BadRequest(errorResponse);
            }

            _paymentTypeService.TDelete(paymentType);

            var successResponse = new
            {
                code = 200,
                status = "success",
                message = "Ödeme tipi başarıyla silindi!"
            };

            return Ok(successResponse);
        }


        [HttpGet]
        [Route("payment/type/list")]
        public IActionResult GetPaymentType()
        {
            var paymentTypes = _paymentTypeService.TGetList();
            if (paymentTypes == null || !paymentTypes.Any())
            {
                var errorResponse = new
                {
                    code = 400,
                    status = "error",
                    message = "Veritabanında kayıtlı ödeme tipi bulanamadı!"
                };
                return BadRequest(errorResponse);
            }

            var successResponse = new
            {
                code = 200,
                status = "success",
                data = paymentTypes
            };

            return Ok(successResponse);
        }

        [HttpGet]
        [Route("payment/type/get/byId")]
        public IActionResult GetPaymentTypeById(int id)
        {
            var paymentType = _paymentTypeService.TGetByID(id);
            if (paymentType == null)
            {
                var errorResponse = new
                {
                    code = 400,
                    status = "error",
                    message = "Veritabanında id ile ilgili kayıtlı ödeme tipi bulanamadı!"
                };
                return BadRequest(errorResponse);
            }

            var successResponse = new
            {
                code = 200,
                status = "success",
                data = paymentType
            };

            return Ok(successResponse);
        }
    }
}
