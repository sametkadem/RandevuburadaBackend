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
    public class ServiceIntervalHoursManager : IServiceIntervalHoursService
    {
        private readonly IServiceIntervalHoursDal _serviceIntervalHoursDal;

        public ServiceIntervalHoursManager(IServiceIntervalHoursDal serviceIntervalHoursDal)
        {
            _serviceIntervalHoursDal = serviceIntervalHoursDal;
        }

        public void TDelete(ServiceIntervalHours t)
        {
            _serviceIntervalHoursDal.Delete(t);
        }

        public ServiceIntervalHours TGetByID(int id)
        {
            return _serviceIntervalHoursDal.GetByID(id);
        }

        public List<ServiceIntervalHours> TGetList()
        {
            return _serviceIntervalHoursDal.GetList();
        }

        public TimeOnly TGetServiceIntervalHour(int id)
        {
            return _serviceIntervalHoursDal.GetServiceIntervalHour(id);
        }

        public void TInsert(ServiceIntervalHours t)
        {
            _serviceIntervalHoursDal.Insert(t);
        }

        public void TUpdate(ServiceIntervalHours t)
        {
            _serviceIntervalHoursDal.Update(t);
        }
    }
}
