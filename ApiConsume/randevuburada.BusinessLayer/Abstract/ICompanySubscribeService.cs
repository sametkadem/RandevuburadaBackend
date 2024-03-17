using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using randevuburada.EntityLayer.Concrete.CustomerConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.BusinessLayer.Abstract
{
    public interface ICompanySubscribeService : IGenericService<CompanySubscribe>
    {
        public bool TCheckCompany(int userId);
        public CompanySubscribe TGetByUserID(int userId);
    }
}
