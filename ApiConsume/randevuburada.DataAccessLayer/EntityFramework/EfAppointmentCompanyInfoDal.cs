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
    public class EfAppointmentCompanyInfoDal : GenericRepository<AppointmentCompanyInfo>, IAppointmentCompanyInfoDal
    {
        public EfAppointmentCompanyInfoDal(Context context) : base(context)
        {

        }
    }
}
