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
    public class EfCompanyDal : GenericRepository<Company>, ICompanyDal
    {
        public EfCompanyDal(Context context) : base(context)
        {
        }
    }
}
