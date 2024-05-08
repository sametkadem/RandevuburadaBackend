using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using randevuburada.BusinessLayer.Abstract;
using randevuburada.EntityLayer.Concrete.ChatConcrete;

namespace Randevuburada.WebApi.Controller.AdminController
{
    [Route("api/v1")]
    [ApiController]
    public class ChatStatusController : ControllerBase
    {
        public readonly IChatStatusService _chatStatusService;

        public ChatStatusController(IChatStatusService chatStatusService)
        {
            _chatStatusService = chatStatusService;
        }

        [HttpGet]
        [Route("admin/chat/status/set/collective")]
        public IActionResult AddChatStatus()
        {
            var chatStatuses = new List<string> { "Tamamlandı", "Müşteri Mesajları Okudu", "Müşteri Mesaj Gönderdi", "İşletme Mesajları Okudu", "İşletme Mesaj Gönderdi", "Sohbet Silindi", "Sohbet Engellendi"};

            var control = _chatStatusService.TGetList();
            if (control.Count > 0)
            {
                var errorResponse = new
                {
                    code = 400,
                    status = "error",
                    message = "Chat durumları zaten ekli!"
                };
                return BadRequest(errorResponse);
            }
            foreach (var chatStatus in chatStatuses)
            {
                var chatStatusModel = new ChatStatus
                {
                    Status = chatStatus
                };
                _chatStatusService.TInsert(chatStatusModel);
            }

            var successResponse = new
            {
                code = 200,
                status = "success",
                message = "Chat durumları başarıyla eklendi!"
            };
            return Ok(successResponse);
        }

        [HttpPost]
        [Route("admin/chat/status/set")]
        public IActionResult AddChatStatusSingular(string chatStatusName)
        {

            var existingChatStatus = _chatStatusService.TGetList().FirstOrDefault(mt => mt.Status == chatStatusName);
            if (existingChatStatus != null)
            {
                var errorResponse = new
                {
                    code = 400,
                    status = "error",
                    message = "Chat durumu zaten ekli!"
                };
                return BadRequest(errorResponse);
            }

            var chatStatusModel = new ChatStatus
            {
                Status = chatStatusName
            };
            _chatStatusService.TInsert(chatStatusModel);

            var successResponse = new
            {
                code = 200,
                status = "success",
                message = "Chat durumu başarıyla eklendi!"
            };
            return Ok(successResponse);
        }

        [HttpPost]
        [Route("admin/chat/status/update")]
        public IActionResult UpdateChatStatus(int id, string chatStatusName)
        {
            var existingChatStatus = _chatStatusService.TGetByID(id);
            if (existingChatStatus == null)
            {
                var errorResponse = new
                {
                    code = 400,
                    status = "error",
                    message = "Chat durumu bulunamadı!"
                };
                return BadRequest(errorResponse);
            }

            existingChatStatus.Status = chatStatusName;
            _chatStatusService.TUpdate(existingChatStatus);

            var successResponse = new
            {
                code = 200,
                status = "success",
                message = "Chat durumu başarıyla güncellendi!"
            };
            return Ok(successResponse);
        }

        [HttpPost]
        [Route("admin/chat/status/delete")]
        public IActionResult DeleteChatStatus(int id)
        {
            var chatStatus = _chatStatusService.TGetByID(id);
            if (chatStatus == null)
            {
                var errorResponse = new
                {
                    code = 400,
                    status = "error",
                    message = "Chat durumu bulunamadı!"
                };
                return BadRequest(errorResponse);
            }

            _chatStatusService.TDelete(chatStatus);

            var successResponse = new
            {
                code = 200,
                status = "success",
                message = "Chat durumu başarıyla silindi!"
            };
            return Ok(successResponse);
        }

        [HttpGet]
        [Route("chat/status/list")]
        public IActionResult GetChatStatusList()
        {
            var chatStatusList = _chatStatusService.TGetList();
            if (chatStatusList.Count == 0)
            {
                var errorResponse = new
                {
                    code = 400,
                    status = "error",
                    message = "Chat durumu bulunamadı!"
                };
                return BadRequest(errorResponse);
            }

            return Ok(chatStatusList);
        }

        [HttpGet]
        [Route("chat/status/get/byId")]
        public IActionResult GetChatStatusById(int id)
        {
            var chatStatus = _chatStatusService.TGetByID(id);
            if (chatStatus == null)
            {
                var errorResponse = new
                {
                    code = 400,
                    status = "error",
                    message = "Chat durumu bulunamadı!"
                };
                return BadRequest(errorResponse);
            }

            return Ok(chatStatus);
        }
    }
}
