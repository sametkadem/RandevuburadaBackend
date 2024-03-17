using randevuburada.BusinessLayer.Abstract;
using randevuburada.DataAccessLayer.Abstract;
using randevuburada.EntityLayer.Concrete.CompanyConcrete.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.BusinessLayer.Concrete
{
    public class ServiceStaffManager : IServiceStaffService
    {
        private readonly IServiceStaffDal _serviceStaffDal;

        public ServiceStaffManager(IServiceStaffDal serviceStaffDal)
        {
            _serviceStaffDal = serviceStaffDal;
        }

        public void TDelete(ServiceStaff t)
        {
            _serviceStaffDal.Delete(t);
        }

        public ServiceStaff TGetByID(int id)
        {
            return _serviceStaffDal.GetByID(id);
        }

        public List<ServiceStaff> TGetList()
        {
            return _serviceStaffDal.GetList();
        }

        public void TInsert(ServiceStaff t)
        {
            _serviceStaffDal.Insert(t);
        }

        public void TUpdate(ServiceStaff t)
        {
            _serviceStaffDal.Update(t);
        }
    }
}
