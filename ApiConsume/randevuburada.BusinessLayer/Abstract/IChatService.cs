using randevuburada.EntityLayer.Concrete.ChatConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.BusinessLayer.Abstract
{
    public interface IChatService : IGenericService<Chat>
    {
        public Chat TGetChatByCustomerIdAndCompanyId(int customerId, int companyId);
        public int TGetChatId(int customerId, int companyId);
        public List<Chat> TGetChatListByCustomerId(int customerId);
        public List<Chat> TGetChatListByCompanyId(int companyId);
        public List<Chat> TGetChatListByCustomerIdAndStatusId(int customerId, int statusId);
        public List<Chat> TGetChatListByCompanyIdAndStatusId(int companyId, int statusId);
        public void TupdateChatLastMessageDate(int chatId);
        public void TupdateChatStatus(int chatId, int statusId);
    }
}
