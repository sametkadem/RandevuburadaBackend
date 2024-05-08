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
    public class MessageManager : IMessageService
    {
        private readonly IMessageDal _messageDal;

        public MessageManager(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }
        public void TDelete(Message t)
        {
            _messageDal.Delete(t);
        }

        public Message TGetByID(int id)
        {
            return _messageDal.GetByID(id);
        }

        public List<Message> TGetList()
        {
            return _messageDal.GetList();
        }

        public void TInsert(Message t)
        {
            _messageDal.Insert(t);
        }

        public void TUpdate(Message t)
        {
            _messageDal.Update(t);
        }

        bool IMessageService.TControlAllMessageStatusByChatIdIsRead(int chatId)
        {
            return _messageDal.ControlAllMessageStatusByChatIdIsRead(chatId);
        }

        List<Message> IMessageService.TGetMessagesByChatId(int chatId)
        {
            return _messageDal.GetMessagesByChatId(chatId);
        }

        List<Message> IMessageService.TGetMessagesByChatIdAndDate(int chatId, DateTime date)
        {
            return _messageDal.GetMessagesByChatIdAndDate(chatId, date);
        }

        List<Message> IMessageService.TGetMessagesByChatIdAndDateRange(int chatId, DateTime startDate, DateTime endDate)
        {
            return _messageDal.GetMessagesByChatIdAndDateRange(chatId, startDate, endDate);
        }

        void IMessageService.TSetMessageStatusIsReadAndCompanyRead(int chatId)
        {
            _messageDal.SetMessageStatusIsReadAndCompanyRead(chatId);
        }

        void IMessageService.TSetMessageStatusIsReadAndCustomerRead(int chatId)
        {
            _messageDal.SetMessageStatusIsReadAndCustomerRead(chatId);
        }
    }
}
