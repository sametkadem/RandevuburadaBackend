using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.BusinessLayer.Abstract
{
    public interface ICompanyBankingDetailsService : IGenericService<CompanyBankingDetails>
    {
        public int TGetCountCompanyByUserId(int userId);
        public IEnumerable<CompanyBankingDetails> TGetByUserID(int userId);
    }
}
