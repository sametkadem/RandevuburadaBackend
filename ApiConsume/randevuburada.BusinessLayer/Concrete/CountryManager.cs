using randevuburada.BusinessLayer.Abstract;
using randevuburada.DataAccessLayer.Abstract;
using randevuburada.EntityLayer.Concrete.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.BusinessLayer.Concrete
{
    public class CountryManager : ICountryService
    {
        private readonly ICountryDal _countryDal;

        public CountryManager(ICountryDal countryDal)
        {
            _countryDal = countryDal;
        }

        public void TDelete(Country t)
        {
            _countryDal.Delete(t);
        }

        public Country TGetByID(int id)
        {
            return _countryDal.GetByID(id);
        }

        public List<Country> TGetList()
        {
            return _countryDal.GetList();
        }

        public void TInsert(Country t)
        {
            _countryDal.Insert(t);
        }

        public void TUpdate(Country t)
        {
            _countryDal.Update(t);
        }
    }
}
