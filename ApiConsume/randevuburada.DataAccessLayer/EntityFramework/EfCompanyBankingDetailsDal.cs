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
    public class EfCompanyBankingDetailsDal : GenericRepository<CompanyBankingDetails>, ICompanyBankingDetailsDal
    {
        public EfCompanyBankingDetailsDal(Context context) : base(context)
        {
        }

        public IEnumerable<CompanyBankingDetails> GetByUserID(int userId)
        {
            var context = new Context();
            return context.CompanyBankingDetails.Where(x => x.UserId == userId).ToList();
        }

        public int GetCountCompanyByUserId(int userId)
        {
            var context = new Context();
            return context.CompanyBankingDetails.Count(x => x.UserId == userId);
        }
    }
}
