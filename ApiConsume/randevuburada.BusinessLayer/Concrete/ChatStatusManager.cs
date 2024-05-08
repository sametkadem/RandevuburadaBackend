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
    public class ChatStatusManager : IChatStatusService
    {
        private readonly IChatStatusDal _chatStatusDal;

        public ChatStatusManager(IChatStatusDal chatStatusDal)
        {
            _chatStatusDal = chatStatusDal;
        }

        public void TDelete(ChatStatus t)
        {
            _chatStatusDal.Delete(t);
        }

        public ChatStatus TGetByID(int id)
        {
            return _chatStatusDal.GetByID(id);
        }

        public List<ChatStatus> TGetList()
        {
            return _chatStatusDal.GetList();
        }

        public void TInsert(ChatStatus t)
        {
            _chatStatusDal.Insert(t);
        }

        public void TUpdate(ChatStatus t)
        {
            _chatStatusDal.Update(t);
        }
    }
}
