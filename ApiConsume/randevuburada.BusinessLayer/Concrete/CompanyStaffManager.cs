using randevuburada.BusinessLayer.Abstract;
using randevuburada.DataAccessLayer.Abstract;
using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.BusinessLayer.Concrete
{
    public class CompanyStaffManager : ICompanyStaffService
    {
        private readonly ICompanyStaffDal _companyStaffDal;

        public CompanyStaffManager(ICompanyStaffDal companyStaffDal)
        {
            _companyStaffDal = companyStaffDal;
        }

        public void TDelete(CompanyStaff t)
        {
            _companyStaffDal.Delete(t);
        }

        public List<CompanyStaff> TGetByCompanyId(int companyId)
        {
            return _companyStaffDal.GetByCompanyId(companyId);
        }

        public CompanyStaff TGetByID(int id)
        {
            return _companyStaffDal.GetByID(id);
        }

        public List<CompanyStaff> TGetList()
        {
            return _companyStaffDal.GetList();
        }

        public List<CompanyStaff> TgetStaffsByArrayInts(List<int> staffIds)
        {
            return _companyStaffDal.getStaffsByArrayInts(staffIds);
        }

        public void TInsert(CompanyStaff t)
        {
            _companyStaffDal.Insert(t);
        }

        public void TUpdate(CompanyStaff t)
        {
            _companyStaffDal.Update(t);
        }

        public CompanyStaff TupdateCompanyStaff(CompanyStaff companyStaff)
        {
            return _companyStaffDal.updateCompanyStaff(companyStaff);
        }
    }
}
