using randevuburada.BusinessLayer.Abstract;
using randevuburada.DataAccessLayer.Abstract;
using randevuburada.EntityLayer.Concrete.CompanyConcrete.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.BusinessLayer.Concrete
{
    public class StaffWorkingHoursManager : IStaffWorkingHoursService
    {
        private readonly IStaffWorkingHoursDal _staffWorkingHoursDal;

        public StaffWorkingHoursManager(IStaffWorkingHoursDal staffWorkingHoursDal)
        {
            _staffWorkingHoursDal = staffWorkingHoursDal;
        }

        public void TDelete(StaffWorkingHours t)
        {
            _staffWorkingHoursDal.Delete(t);
        }

        public StaffWorkingHours TGetByID(int id)
        {
            return _staffWorkingHoursDal.GetByID(id);
        }

        public List<StaffWorkingHours> TGetList()
        {
            return _staffWorkingHoursDal.GetList();
        }

        public void TInsert(StaffWorkingHours t)
        {
            _staffWorkingHoursDal.Insert(t);
        }

        public void TUpdate(StaffWorkingHours t)
        {
            _staffWorkingHoursDal.Update(t);
        }
    }
}
