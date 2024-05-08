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
    public class AppointmentCompanyInfoManager : IAppointmentCompanyInfoService
    {
        private readonly IAppointmentCompanyInfoDal _appointmentCompanyInfoDal;

        public AppointmentCompanyInfoManager(IAppointmentCompanyInfoDal appointmentCompanyInfoDal)
        {
            _appointmentCompanyInfoDal = appointmentCompanyInfoDal;
        }

        public void TDelete(AppointmentCompanyInfo t)
        {
            _appointmentCompanyInfoDal.Delete(t);
        }

        public AppointmentCompanyInfo TGetByID(int id)
        {
            return _appointmentCompanyInfoDal.GetByID(id);
        }

        public List<AppointmentCompanyInfo> TGetList()
        {
            return _appointmentCompanyInfoDal.GetList();
        }

        public void TInsert(AppointmentCompanyInfo t)
        {
            _appointmentCompanyInfoDal.Insert(t);
        }

        public void TUpdate(AppointmentCompanyInfo t)
        {
            _appointmentCompanyInfoDal.Update(t);
        }
    }
}
