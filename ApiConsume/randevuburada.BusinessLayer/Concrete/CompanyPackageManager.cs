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
    public class CompanyPackageManager : ICompanyPackageService
    {
        public readonly ICompanyPackageDal _companyPackageDal;

        public CompanyPackageManager(ICompanyPackageDal companyPackageDal)
        {
            _companyPackageDal = companyPackageDal;
        }

        public void TDelete(CompanyPackage t)
        {
            _companyPackageDal.Delete(t);
        }

        public CompanyPackage TGetByID(int id)
        {
            return _companyPackageDal.GetByID(id);
        }

        public List<CompanyPackage> TGetList()
        {
            return _companyPackageDal.GetList();
        }

        public void TInsert(CompanyPackage t)
        {
            _companyPackageDal.Insert(t);
        }

        public void TUpdate(CompanyPackage t)
        {
            _companyPackageDal.Update(t);
        }
    }
}
