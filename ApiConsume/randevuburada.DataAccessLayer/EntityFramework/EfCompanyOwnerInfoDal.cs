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
    public class EfCompanyOwnerInfoDal : GenericRepository<CompanyOwnerInfo>, ICompanyOwnerInfoDal
    {
        public EfCompanyOwnerInfoDal(Context context) : base(context)
        {
        }

        public bool CheckCompany(int userId)
        {
            var context = new Context();
            return context.CompanyOwnerInfos.Any(x => x.UserId == userId);
        }

        public CompanyOwnerInfo GetByUserID(int userId)
        {
            var context = new Context();
            return context.CompanyOwnerInfos.FirstOrDefault(x => x.UserId == userId);
        }
    }
}
