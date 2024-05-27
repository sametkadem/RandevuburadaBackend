using randevuburada.EntityLayer.Concrete.AppointmentConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.BusinessLayer.Abstract
{
    public interface IAppointmentInfoService : IGenericService<AppointmentInfo>
    {
        public List<AppointmentInfo> TGetAppointmentInfoByCompanyAndServiceIdAndDate(int companyId, int serviceId, DateTime date);
        public List<AppointmentInfo> TGetAppointmentInfoByCompanyAndStaffIdAndDate(int companyId, int staffId, DateTime date);

        public List<AppointmentInfo> TGetAppointmentsByAppointmentId(int appointmentId);


    }
}
