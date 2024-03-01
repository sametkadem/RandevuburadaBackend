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
    public class CityManager : ICityService
    {

        private readonly ICityDal _cityDal;

        public CityManager(ICityDal cityDal)
        {
            _cityDal = cityDal;
        }
        public void TDelete(City t)
        {
            _cityDal.Delete(t);
        }

        public City TGetByID(int id)
        {
            return _cityDal.GetByID(id);
        }

        public List<City> TGetList()
        {
            return _cityDal.GetList();
        }

        public void TInsert(City t)
        {
            _cityDal.Insert(t);
        }

        public void TUpdate(City t)
        {
            _cityDal.Update(t);
        }
    }
}
