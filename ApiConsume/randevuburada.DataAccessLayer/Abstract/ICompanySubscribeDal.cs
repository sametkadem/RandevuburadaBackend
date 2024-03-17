using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DataAccessLayer.Abstract
{
    public interface ICompanySubscribeDal : IGenericDal<CompanySubscribe>
    {
        public bool CheckCompany(int userId);
        public CompanySubscribe GetByUserID(int userId);
    }
}
