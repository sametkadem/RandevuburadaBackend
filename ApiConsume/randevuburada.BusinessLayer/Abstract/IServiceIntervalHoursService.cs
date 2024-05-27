using randevuburada.EntityLayer.Concrete.CompanyConcrete.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.BusinessLayer.Abstract
{
    public interface IServiceIntervalHoursService:IGenericService<ServiceIntervalHours>
    {
        public TimeOnly TGetServiceIntervalHour(int id);
    }
}
