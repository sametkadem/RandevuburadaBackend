using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DataAccessLayer.Abstract
{
    public interface ICompanyBankingDetailsDal : IGenericDal<CompanyBankingDetails>
    {
        public int GetCountCompanyByUserId(int userId);
        public IEnumerable<CompanyBankingDetails> GetByUserID(int userId);
    }
}
