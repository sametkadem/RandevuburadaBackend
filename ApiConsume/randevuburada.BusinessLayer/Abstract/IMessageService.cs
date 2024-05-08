using randevuburada.EntityLayer.Concrete.ChatConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.BusinessLayer.Abstract
{
    public interface IMessageService : IGenericService<Message>
    {
        public List<Message> TGetMessagesByChatId(int chatId);
        public List<Message> TGetMessagesByChatIdAndDate(int chatId, DateTime date);
        public List<Message> TGetMessagesByChatIdAndDateRange(int chatId, DateTime startDate, DateTime endDate);
        public void TSetMessageStatusIsReadAndCustomerRead(int chatId);
        public void TSetMessageStatusIsReadAndCompanyRead(int chatId);
        public bool TControlAllMessageStatusByChatIdIsRead(int chatId);
    }
}
