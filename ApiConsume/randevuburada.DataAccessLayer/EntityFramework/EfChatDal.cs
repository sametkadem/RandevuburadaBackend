using randevuburada.DataAccessLayer.Abstract;
using randevuburada.DataAccessLayer.Concrete;
using randevuburada.DataAccessLayer.Repositories;
using randevuburada.EntityLayer.Concrete.ChatConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DataAccessLayer.EntityFramework
{
    public class EfChatDal : GenericRepository<Chat>, IChatDal
    {
        public EfChatDal(Context context) : base(context)
        {

        }

        public Chat GetChatByCustomerIdAndCompanyId(int customerId, int companyId)
        {
            var context = new Context();
            return context.Chats.FirstOrDefault(x => x.CustomerId == customerId && x.CompanyId == companyId);
        }

        public int GetChatId(int customerId, int companyId)
        {
            var context = new Context();
            return context.Chats.FirstOrDefault(x => x.CustomerId == customerId && x.CompanyId == companyId).Id;
        }

        public List<Chat> GetChatListByCustomerId(int customerId)
        {
            var context = new Context();
            return context.Chats.Where(x => x.CustomerId == customerId).ToList();
        }

        public List<Chat> GetChatListByCompanyId(int companyId)
        {
            var context = new Context();
            return context.Chats.Where(x => x.CompanyId == companyId).ToList();
        }

        public List<Chat> GetChatListByCustomerIdAndStatusId(int customerId, int statusId)
        {
            var context = new Context();
            return context.Chats.Where(x => x.CustomerId == customerId && x.ChatStatusId == statusId).ToList();
        }

        public List<Chat> GetChatListByCompanyIdAndStatusId(int companyId, int statusId)
        {
            var context = new Context();
            return context.Chats.Where(x => x.CompanyId == companyId && x.ChatStatusId == statusId).ToList();
        }

        public void updateChatLastMessageDate(int chatId)
        {
            var context = new Context();
            var chat = context.Chats.FirstOrDefault(x => x.Id == chatId);
            chat.LastMessageDate = DateTime.UtcNow;
            context.SaveChanges();
        }

        public void updateChatStatus(int chatId, int statusId)
        {
            var context = new Context();
            var chat = context.Chats.FirstOrDefault(x => x.Id == chatId);
            chat.ChatStatusId = statusId;
            context.SaveChanges();
        }


    }
}
