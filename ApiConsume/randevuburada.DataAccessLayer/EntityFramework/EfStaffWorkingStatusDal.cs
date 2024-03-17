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
    public class EfStaffWorkingStatusDal : GenericRepository<StaffWorkingStatus>, IStaffWorkingStatusDal
    {
        public EfStaffWorkingStatusDal(Context context) : base(context)
        {

        }
    }
}
