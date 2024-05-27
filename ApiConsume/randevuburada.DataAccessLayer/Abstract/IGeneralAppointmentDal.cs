using randevuburada.EntityLayer.Concrete.AppointmentConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DataAccessLayer.Abstract
{
    public interface IGeneralAppointmentDal : IGenericDal<GeneralAppointment>
    {
        public int InsertGeneralAppointment(GeneralAppointment generalAppointment);

        public List<GeneralAppointment> GetByCustomerId(int customerId);

        public List<GeneralAppointment> GetByCompanyId(int companyId);


    }
}
