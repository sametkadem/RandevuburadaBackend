using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DataAccessLayer.Abstract
{
    public interface ICompanyStaffDal : IGenericDal<CompanyStaff>
    {
        public List<CompanyStaff> GetByCompanyId(int companyId);

        public CompanyStaff updateCompanyStaff(CompanyStaff companyStaff);
        public List<CompanyStaff> getStaffsByArrayInts(List<int> staffIds);

    }
}
