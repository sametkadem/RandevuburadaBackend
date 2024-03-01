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
    public class CompanyBankingDetailsManager : ICompanyBankingDetailsService
    {
        public readonly ICompanyBankingDetailsDal _companyBankingDetailsDal;

        public CompanyBankingDetailsManager(ICompanyBankingDetailsDal companyBankingDetailsDal)
        {
            _companyBankingDetailsDal = companyBankingDetailsDal;
        }

        public void TDelete(CompanyBankingDetails t)
        {
            _companyBankingDetailsDal.Delete(t);
        }

        public CompanyBankingDetails TGetByID(int id)
        {
            return _companyBankingDetailsDal.GetByID(id);
        }

        public List<CompanyBankingDetails> TGetList()
        {
            return _companyBankingDetailsDal.GetList();
        }

        public void TInsert(CompanyBankingDetails t)
        {
            _companyBankingDetailsDal.Insert(t);
        }

        public void TUpdate(CompanyBankingDetails t)
        {
            _companyBankingDetailsDal.Update(t);
        }
    }
}
