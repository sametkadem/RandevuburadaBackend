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
    public class EfMessageDal : GenericRepository<Message>, IMessageDal
    {
        public EfMessageDal(Context context) : base(context)
        {

        }

        public List<Message> GetMessagesByChatId(int chatId)
        {
            var context = new Context();
            return context.Messages.Where(x => x.ChatId == chatId).ToList();
        }

        public List<Message> GetMessagesByChatIdAndDate(int chatId, DateTime date)
        {
            var context = new Context();
            return context.Messages.Where(x => x.ChatId == chatId && x.MessageDate.Date == date.Date).ToList();
        }

        public List<Message> GetMessagesByChatIdAndDateRange(int chatId, DateTime startDate, DateTime endDate)
        {
            var context = new Context();
            return context.Messages.Where(x => x.ChatId == chatId && x.MessageDate.Date >= startDate.Date && x.MessageDate.Date <= endDate.Date).ToList();
        }

        public void SetMessageStatusIsReadAndCustomerRead(int chatId)
        {
            var context = new Context();
            var messages = context.Messages.Where(x => x.ChatId == chatId && x.IsRead == false && x.IsCustomerMessage == false).ToList();
            foreach (var message in messages)
            {
                message.IsRead = true;
                message.IsCustomerRead = true;
                context.SaveChanges();
            }
        }

        public void SetMessageStatusIsReadAndCompanyRead(int chatId)
        {
            var context = new Context();
            var messages = context.Messages.Where(x => x.ChatId == chatId && x.IsRead == false && x.IsCompanyMessage == false).ToList();
            foreach (var message in messages)
            {
                message.IsRead = true;
                message.IsCompanyRead = true;
                context.SaveChanges();
            }
        }


        public bool ControlAllMessageStatusByChatIdIsRead(int chatId)
        {
            using (var context = new Context())
            {
                var messages = context.Messages.Where(x => x.ChatId == chatId).ToList();
                var isAllRead = messages.All(x => x.IsRead == true);
                if (isAllRead)
                {
                    var chat = context.Chats.FirstOrDefault(x => x.Id == chatId);
                    context.SaveChanges();
                }
                return isAllRead;
            }
        }



    }
}
