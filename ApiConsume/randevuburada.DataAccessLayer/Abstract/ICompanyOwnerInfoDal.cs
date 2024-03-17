using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DataAccessLayer.Abstract
{
    public interface ICompanyOwnerInfoDal : IGenericDal<CompanyOwnerInfo>
    {
        public bool CheckCompany(int userId);
        public CompanyOwnerInfo GetByUserID(int userId);
    }
}
