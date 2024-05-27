using randevuburada.EntityLayer.Concrete.AppointmentConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.BusinessLayer.Abstract
{
    public interface IGeneralAppointmentService : IGenericService<GeneralAppointment>
    {
        public int TInsertGeneralAppointment(GeneralAppointment generalAppointment);
        public List<GeneralAppointment> TGetByCustomerId(int customerId);

        public List<GeneralAppointment> TGetByCompanyId(int companyId);

    }
}
