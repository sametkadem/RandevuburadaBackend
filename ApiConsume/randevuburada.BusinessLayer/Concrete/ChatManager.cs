using randevuburada.BusinessLayer.Abstract;
using randevuburada.DataAccessLayer.Abstract;
using randevuburada.EntityLayer.Concrete.ChatConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.BusinessLayer.Concrete
{
    public class ChatManager : IChatService
    {
        private readonly IChatDal _chatDal;

        public ChatManager(IChatDal chatDal)
        {
            _chatDal = chatDal;
        }

        public void TDelete(Chat t)
        {
            _chatDal.Delete(t);
        }

        public Chat TGetByID(int id)
        {
            return _chatDal.GetByID(id);
        }

        public Chat TGetChatByCustomerIdAndCompanyId(int customerId, int companyId)
        {
            return _chatDal.GetChatByCustomerIdAndCompanyId(customerId, companyId);
        }

        public List<Chat> TGetList()
        {
            return _chatDal.GetList();
        }

        public void TInsert(Chat t)
        {
            _chatDal.Insert(t);
        }

        public void TUpdate(Chat t)
        {
            _chatDal.Update(t);
        }

        int IChatService.TGetChatId(int customerId, int companyId)
        {
            return _chatDal.GetChatId(customerId, companyId);
        }

        List<Chat> IChatService.TGetChatListByCompanyId(int companyId)
        {
            return _chatDal.GetChatListByCompanyId(companyId);
        }

        List<Chat> IChatService.TGetChatListByCompanyIdAndStatusId(int companyId, int statusId)
        {
            return _chatDal.GetChatListByCompanyIdAndStatusId(companyId, statusId);
        }

        List<Chat> IChatService.TGetChatListByCustomerId(int customerId)
        {
            return _chatDal.GetChatListByCustomerId(customerId);
        }

        List<Chat> IChatService.TGetChatListByCustomerIdAndStatusId(int customerId, int statusId)
        {
            return _chatDal.GetChatListByCustomerIdAndStatusId(customerId, statusId);
        }

        void IChatService.TupdateChatLastMessageDate(int chatId)
        {
            _chatDal.updateChatLastMessageDate(chatId);
        }

        void IChatService.TupdateChatStatus(int chatId, int statusId)
        {
            _chatDal.updateChatStatus(chatId, statusId);
        }
    }
}
