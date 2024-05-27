using randevuburada.EntityLayer.Concrete.CompanyConcrete.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DataAccessLayer.Abstract
{
    public interface IServiceIntervalHoursDal : IGenericDal<ServiceIntervalHours>
    {
        public TimeOnly GetServiceIntervalHour(int id);
    }
}
