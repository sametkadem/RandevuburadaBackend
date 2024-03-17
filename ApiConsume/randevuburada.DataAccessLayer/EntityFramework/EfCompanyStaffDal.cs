using randevuburada.DataAccessLayer.Abstract;
using randevuburada.DataAccessLayer.Concrete;
using randevuburada.DataAccessLayer.Repositories;
using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DataAccessLayer.EntityFramework
{
    public class EfCompanyStaffDal : GenericRepository<CompanyStaff>, ICompanyStaffDal
    {
        public EfCompanyStaffDal(Context context) : base(context)
        {
            
        }

        public List<CompanyStaff> GetByCompanyId(int companyId)
        {
            var context = new Context();
            return context.CompanyStaffs.Where(x => x.CompanyId == companyId).ToList();
        }
    }
}
