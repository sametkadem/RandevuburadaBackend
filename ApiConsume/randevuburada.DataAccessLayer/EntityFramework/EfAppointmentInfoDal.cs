using randevuburada.DataAccessLayer.Abstract;
using randevuburada.DataAccessLayer.Concrete;
using randevuburada.DataAccessLayer.Repositories;
using randevuburada.EntityLayer.Concrete.AppointmentConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DataAccessLayer.EntityFramework
{
    public class EfAppointmentInfoDal : GenericRepository<AppointmentInfo>, IAppointmentInfoDal
    {
        public EfAppointmentInfoDal(Context context) : base(context)
        {

        }

        public List<AppointmentInfo> GetAppointmentInfoByCompanyAndServiceIdAndDate(int companyId, int serviceId, DateTime date)
        {
            var context = new Context();
            var startDate = date.Date;
            var endDate = startDate.AddDays(1).AddSeconds(-1);
            return context.AppointmentInfo
                .Where(x => x.CompanyId == companyId && x.CompanyServiceId == serviceId && x.AppointmentDateStart >= startDate && x.AppointmentDateStart <= endDate)
                .ToList();
        }

        public List<AppointmentInfo> GetAppointmentInfoByCompanyAndStaffIdAndDate(int companyId, int staffId, DateTime date)
        {
            var context = new Context();
            var startDate = date.Date;
            var endDate = startDate.AddDays(1).AddSeconds(-1);
            return context.AppointmentInfo
                .Where(x => x.CompanyId == companyId && x.StaffId == staffId && x.AppointmentDateStart >= startDate && x.AppointmentDateStart <= endDate)
                .ToList();
        }

        public List<AppointmentInfo> GetAppointmentsByAppointmentId(int appointmentId)
        {
            var context = new Context();
            return context.AppointmentInfo
                .Where(x => x.AppointmentId == appointmentId)
                .ToList();
        }


    }
}
