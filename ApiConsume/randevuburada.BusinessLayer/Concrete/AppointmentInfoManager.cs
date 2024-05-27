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
    public class AppointmentInfoManager : IAppointmentInfoService
    {
        private readonly IAppointmentInfoDal _appointmentInfoDal;

        public AppointmentInfoManager(IAppointmentInfoDal appointmentInfoDal)
        {
            _appointmentInfoDal = appointmentInfoDal;
        }

        public List<AppointmentInfo> TGetAppointmentInfoByCompanyAndServiceIdAndDate(int companyId, int serviceId, DateTime date)
        {
            return _appointmentInfoDal.GetAppointmentInfoByCompanyAndServiceIdAndDate(companyId, serviceId, date);
        }

        public void TDelete(AppointmentInfo t)
        {
            _appointmentInfoDal.Delete(t);
        }

        public AppointmentInfo TGetByID(int id)
        {
            return _appointmentInfoDal.GetByID(id);
        }

        public List<AppointmentInfo> TGetList()
        {
            return _appointmentInfoDal.GetList();
        }

        public void TInsert(AppointmentInfo t)
        {
            _appointmentInfoDal.Insert(t);
        }

        public void TUpdate(AppointmentInfo t)
        {
            _appointmentInfoDal.Update(t);
        }

        public List<AppointmentInfo> TGetAppointmentInfoByCompanyAndStaffIdAndDate(int companyId, int staffId, DateTime date)
        {
            return _appointmentInfoDal.GetAppointmentInfoByCompanyAndStaffIdAndDate(companyId, staffId, date);
        }

        public List<AppointmentInfo> TGetAppointmentsByAppointmentId(int appointmentId)
        {
            return _appointmentInfoDal.GetAppointmentsByAppointmentId(appointmentId);
        }
    }
}
