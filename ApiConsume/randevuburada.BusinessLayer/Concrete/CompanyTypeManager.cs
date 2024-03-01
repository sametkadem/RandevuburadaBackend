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
    public class CompanyTypeManager : ICompanyTypeService
    {
        public readonly ICompanyTypeDal _companyTypeDal;

        public CompanyTypeManager(ICompanyTypeDal companyTypeDal)
        {
            _companyTypeDal = companyTypeDal;
        }

        public void TDelete(CompanyType t)
        {
            _companyTypeDal.Delete(t);
        }

        public CompanyType TGetByID(int id)
        {
            return _companyTypeDal.GetByID(id);
        }

        public List<CompanyType> TGetList()
        {
            return _companyTypeDal.GetList();
        }

        public void TInsert(CompanyType t)
        {
            _companyTypeDal.Insert(t);
        }

        public void TUpdate(CompanyType t)
        {
            _companyTypeDal.Update(t);
        }
    }
}
