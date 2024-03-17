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
    public class EfCompanyServiceDal : GenericRepository<CompanyService>, ICompanyServiceDal
    {
        public EfCompanyServiceDal(Context context) : base(context)
        {
        }

        public List<CompanyService> GetByCompanyId(int companyId)
        {
            var context = new Context();
            return context.CompanyServices.Where(x => x.CompanyId == companyId).ToList();
        }
    }
}
