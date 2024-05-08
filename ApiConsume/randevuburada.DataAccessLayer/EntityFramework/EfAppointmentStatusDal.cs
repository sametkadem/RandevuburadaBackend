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
    public class EfAppointmentStatusDal : GenericRepository<AppointmentStatus>, IAppointmentStatusDal
    {
        public EfAppointmentStatusDal(Context context) : base(context)
        {
        }
    }
}
