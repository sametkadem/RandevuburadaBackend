using randevuburada.DataAccessLayer.Abstract;
using randevuburada.DataAccessLayer.Concrete;
using randevuburada.DataAccessLayer.Repositories;
using randevuburada.EntityLayer.Concrete.CompanyConcrete.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DataAccessLayer.EntityFramework
{
    public class EfStaffWorkingHoursDal : GenericRepository<StaffWorkingHours>, IStaffWorkingHoursDal
    {
        public EfStaffWorkingHoursDal(Context context) : base(context)
        {

        }
    }
}
