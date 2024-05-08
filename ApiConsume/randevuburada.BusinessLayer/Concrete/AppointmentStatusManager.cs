using randevuburada.BusinessLayer.Abstract;
using randevuburada.DataAccessLayer.Abstract;
using randevuburada.EntityLayer.Concrete.AppointmentConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.BusinessLayer.Concrete
{
    public class AppointmentStatusManager : IAppointmentStatusService
    {

        private readonly IAppointmentStatusDal _appointmentStatusDal;

        public AppointmentStatusManager(IAppointmentStatusDal appointmentStatusDal)
        {
            _appointmentStatusDal = appointmentStatusDal;
        }

        public void TDelete(AppointmentStatus t)
        {
            _appointmentStatusDal.Delete(t);
        }

        public AppointmentStatus TGetByID(int id)
        {
            return _appointmentStatusDal.GetByID(id);
        }

        public List<AppointmentStatus> TGetList()
        {
            return _appointmentStatusDal.GetList();
        }

        public void TInsert(AppointmentStatus t)
        {
            _appointmentStatusDal.Insert(t);
        }

        public void TUpdate(AppointmentStatus t)
        {
            _appointmentStatusDal.Update(t);
        }
    }
}
