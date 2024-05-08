using randevuburada.EntityLayer.Concrete.ChatConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DataAccessLayer.Abstract
{
    public interface IMessageDal : IGenericDal<Message>
    {
        public List<Message> GetMessagesByChatId(int chatId);
        public List<Message> GetMessagesByChatIdAndDate(int chatId, DateTime date);
        public List<Message> GetMessagesByChatIdAndDateRange(int chatId, DateTime startDate, DateTime endDate);
        public void SetMessageStatusIsReadAndCustomerRead(int chatId);
        public void SetMessageStatusIsReadAndCompanyRead(int chatId);
        public bool ControlAllMessageStatusByChatIdIsRead(int chatId);
    }
}
