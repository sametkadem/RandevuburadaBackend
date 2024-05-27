using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using randevuburada.BusinessLayer.Abstract;
using randevuburada.DtoLayer.Dtos.ChatDto;
using randevuburada.EntityLayer.Concrete.ChatConcrete;
using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using randevuburada.EntityLayer.Concrete.Identity;
using System;

namespace Randevuburada.WebApi.Controller.ChatController
{
    [Route("api/v1")]
    [ApiController]
    public class CompanyChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICustomerService _customerService;
        private readonly ICompanyService _companyService;
        private readonly IChatStatusService _chatStatusService;
        public CompanyChatController(IChatService chatService, IMapper mapper, UserManager<AppUser> userManager, ICustomerService customerService, ICompanyService companyService, IMessageService messageService, IChatStatusService chatStatusService)
        {
            _chatService = chatService;
            _mapper = mapper;
            _userManager = userManager;
            _customerService = customerService;
            _companyService = companyService;
            _messageService = messageService;
            _chatStatusService = chatStatusService;
        }

        [HttpPost]
        [Route("company/chat/message/send")]
        public IActionResult SendMessageByCompany(CustomerChatDto customerChatDto)
        {
            if (!ModelState.IsValid)
            {
                var returnIsValid = new
                {
                    code = 400,
                    status = "error",
                    message = "İşlem başarısız!",
                    data = ModelState
                };
                return BadRequest(returnIsValid);
            }

            var customer = _customerService.TGetByID(customerChatDto.CustomerId);
            if (customer == null)
            {
                var returnIsValid = new
                {
                    code = 404,
                    status = "error",
                    message = "Müşteri bulunamadı!",
                    data = ModelState
                };

                return NotFound(returnIsValid);
            }

            var company = _companyService.TGetByID(customerChatDto.CompanyId);
            if (company == null)
            {
                var returnIsValid = new
                {
                    code = 404,
                    status = "error",
                    message = "İşletme bulunamadı!",
                    data = ModelState
                };

                return NotFound(returnIsValid);
            }

            var chat = _chatService.TGetChatByCustomerIdAndCompanyId(customerChatDto.CustomerId, customerChatDto.CompanyId);
            if (chat == null)
            {
                var chatData = new Chat
                {
                    CustomerId = customerChatDto.CustomerId,
                    CompanyId = customerChatDto.CompanyId,
                    ChatStatusId = 1,
                    ChatStartDate = DateTime.UtcNow,
                    LastMessageDate = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                _chatService.TInsert(chatData);
            }

            var chatId = _chatService.TGetChatByCustomerIdAndCompanyId(customerChatDto.CustomerId, customerChatDto.CompanyId).Id;

            var message = new Message
            {
                ChatId = chatId,
                MessageDate = DateTime.UtcNow,
                MessageText = customerChatDto.MessageText,
                IsRead = false,
                IsSystemMessage = false,
                IsCustomerMessage = false,
                IsCompanyMessage = true,
                IsCustomerRead = false,
                IsCompanyRead = true,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _messageService.TInsert(message);
            _chatService.TupdateChatLastMessageDate(chatId);
            _chatService.TupdateChatStatus(chatId, 5);
            var successReturn = new
            {
                code = 200,
                status = "success",
                message = "Mesaj başarıyla gönderildi!"
            };

            return Ok(successReturn);
        }

        [HttpGet]
        [Route("company/chat/list")]
        [Authorize(Roles = "Company")]
        public IActionResult GetChatCompany(int companyId)
        {
            var company = _companyService.TGetByID(companyId);
            if (company == null)
            {
                var returnIsValid = new
                {
                    code = 404,
                    status = "error",
                    message = "İşletme bulunamadı!"
                };

                return NotFound(returnIsValid);
            }

            var chats = _chatService.TGetChatListByCompanyId(companyId);
            if (chats == null)
            {
                var returnIsValid = new
                {
                    code = 404,
                    status = "error",
                    message = "Sohbet bulunamadı!"
                };

                return NotFound(returnIsValid);
            }

            if(chats.Count == 0)
            {
                var returnIsValid = new
                {
                    code = 404,
                    status = "error",
                    message = "Sohbet bulunamadı!"
                };

                return NotFound(returnIsValid);
            }

            var objectList = chats.Select(item =>
            {
                var chatStatusName = _chatStatusService.TGetChatStatusName(item.ChatStatusId);
                var customer = _customerService.TGetCustomerFirstNameLastNameAndPhoneNumbers(item.CustomerId);
                return new
                {
                    item.Id,
                    item.CompanyId,
                    item.CustomerId,
                    item.ChatStatusId,
                    chatStatusName,
                    customer,
                    item.ChatStartDate,
                    item.LastMessageDate,
                };
            }).ToList();

            var returnSuccess = new
            {
                code = 200,
                status = "success",
                message = "Sohbetler başarıyla getirildi!",
                data = objectList
            };

            return Ok(returnSuccess);
        }

        [HttpGet]
        [Route("company/chat/message/list")]
        public IActionResult GetMessagesCompany(int companyId, int chatId)
        {
            var company = _companyService.TGetByID(companyId);
            if (company == null)
            {
                var returnIsValid = new
                {
                    code = 404,
                    status = "error",
                    message = "İşletme bulunamadı!"
                };

                return NotFound(returnIsValid);
            }

            var chat = _chatService.TGetByID(chatId);
            if (chat == null)
            {
                var returnIsValid = new
                {
                    code = 404,
                    status = "error",
                    message = "Sohbet bulunamadı!"
                };

                return NotFound(returnIsValid);
            }

            var messages = _messageService.TGetMessagesByChatId(chatId);
            foreach (var message in messages)
            {
                message.IsCustomerRead = true;
                if (message.IsCustomerRead == true && message.IsCompanyRead == true)
                {
                    message.IsRead = true;
                }
                else
                {
                    message.IsRead = false;
                }
                message.MessageReadDate = DateTime.UtcNow;
                _messageService.TUpdate(message);
            }

            var chatStatusControl = _messageService.TControlAllMessageStatusByChatIdIsRead(chatId);
            if (chatStatusControl == true)
            {
                _chatService.TupdateChatStatus(chatId, 1);
            }
            else if (chatStatusControl == false)
            {
                _chatService.TupdateChatStatus(chatId, 4);
            }
             
            var objectList = messages.Select(item =>
            {
                var userName = "Sistem";
                if(item.IsCompanyMessage == true)
                {
                    userName = company.CompanyName;
                }
                else if(item.IsCustomerMessage == true)
                {
                    userName = _customerService.TGetCustomerName(chat.CustomerId);
                }
                var messageDateText = item.CreatedAt.ToString("dd.MM.yyyy HH:mm:ss");
                return new
                {
                    chat.CustomerId,
                    item.Id,
                    item.ChatId,
                    item.MessageText,
                    item.IsRead,
                    item.IsSystemMessage,
                    item.IsCustomerMessage,
                    item.IsCompanyMessage,
                    item.IsCustomerRead,
                    item.IsCompanyRead,
                    item.IsDeleted,
                    item.MessageDate,
                    messageDateText,
                    userName
                };
            }).ToList();

            var returnSuccess = new
            {
                code = 200,
                status = "success",
                message = "Mesajlar başarıyla getirildi!",
                data = objectList,
        
            };
            return Ok(returnSuccess);
        }

    }
}
