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
    public class EfGeneralAppointmentDal : GenericRepository<GeneralAppointment>, IGeneralAppointmentDal
    {
        public EfGeneralAppointmentDal(Context context) : base(context)
        {

        }
    }
}
