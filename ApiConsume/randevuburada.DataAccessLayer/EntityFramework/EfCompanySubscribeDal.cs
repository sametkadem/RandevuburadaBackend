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
    public class EfCompanySubscribeDal : GenericRepository<CompanySubscribe>, ICompanySubscribeDal
    {
        public EfCompanySubscribeDal(Context context) : base(context)
        {
        }

        public bool CheckCompany(int userId)
        {
            var context = new Context();
            return context.CompanySubscribes.Any(x => x.UserId == userId);
        }

        public CompanySubscribe GetByUserID(int userId)
        {
            var context = new Context();
            return context.CompanySubscribes.FirstOrDefault(x => x.UserId == userId);
        }
    }
}
