using randevuburada.EntityLayer.Concrete.ChatConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DataAccessLayer.Abstract
{
    public interface IChatDal : IGenericDal<Chat>
    {
        public Chat GetChatByCustomerIdAndCompanyId(int customerId, int companyId);
        public int GetChatId(int customerId, int companyId);
        public List<Chat> GetChatListByCustomerId(int customerId);
        public List<Chat> GetChatListByCompanyId(int companyId);
        public List<Chat> GetChatListByCustomerIdAndStatusId(int customerId, int statusId);
        public List<Chat> GetChatListByCompanyIdAndStatusId(int companyId, int statusId);
        public void updateChatLastMessageDate(int chatId);
        public void updateChatStatus(int chatId, int statusId);
    }
}
