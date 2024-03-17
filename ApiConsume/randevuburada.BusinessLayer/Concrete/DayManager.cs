using randevuburada.BusinessLayer.Abstract;
using randevuburada.DataAccessLayer.Abstract;
using randevuburada.DataAccessLayer.EntityFramework;
using randevuburada.EntityLayer.Concrete.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.BusinessLayer.Concrete
{
    public class DayManager : IDayService
    {
        public readonly IDayDal _dayDal;

        public DayManager(IDayDal dayDal)
        {
            _dayDal = dayDal;
        }
        public void TDelete(Day t)
        {
            _dayDal.Delete(t);
        }

        public Day TGetByID(int id)
        {
            return _dayDal.GetByID(id);
        }

        public List<Day> TGetList()
        {
            return _dayDal.GetList();
        }

        public void TInsert(Day t)
        {
            _dayDal.Insert(t);
        }

        public void TUpdate(Day t)
        {
            _dayDal.Update(t);
        }
    }
}
