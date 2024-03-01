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
    public class CompanyManager : ICompanyService
    {
        public readonly ICompanyDal _companyDal;

        public CompanyManager(ICompanyDal companyDal)
        {
            _companyDal = companyDal;
        }

        public void TDelete(Company t)
        {
            _companyDal.Delete(t);
        }

        public Company TGetByID(int id)
        {
            return _companyDal.GetByID(id);
        }

        public List<Company> TGetList()
        {
            return _companyDal.GetList();
        }

        public void TInsert(Company t)
        {
            _companyDal.Insert(t);
        }

        public void TUpdate(Company t)
        {
            _companyDal.Update(t);
        }
    }
}
