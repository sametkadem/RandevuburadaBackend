using randevuburada.EntityLayer.Concrete.CustomerConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.BusinessLayer.Abstract
{
    public interface ICustomerAppointmentInfoService : IGenericService<CustomerAppointmentInfo>
    {
        public List<CustomerAppointmentInfo> TGetByCustomerID(int customerId);
    }
}
