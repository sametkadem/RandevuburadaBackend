using randevuburada.EntityLayer.Concrete.AppointmentConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DataAccessLayer.Abstract
{
    public interface IAppointmentInfoDal : IGenericDal<AppointmentInfo>
    {
        public List<AppointmentInfo> GetAppointmentInfoByCompanyAndServiceIdAndDate(int companyId, int serviceId, DateTime date);
        public List<AppointmentInfo> GetAppointmentInfoByCompanyAndStaffIdAndDate(int companyId, int staffId, DateTime date);

        public List<AppointmentInfo> GetAppointmentsByAppointmentId(int appointmentId);


    }
}
