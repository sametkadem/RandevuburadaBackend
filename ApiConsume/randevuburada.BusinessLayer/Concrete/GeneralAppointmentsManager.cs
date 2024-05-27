using randevuburada.BusinessLayer.Abstract;
using randevuburada.DataAccessLayer.Abstract;
using randevuburada.EntityLayer.Concrete.AppointmentConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.BusinessLayer.Concrete
{
    public class GeneralAppointmentsManager : IGeneralAppointmentService
    {
        private readonly IGeneralAppointmentDal _generalAppointmentDal;

        public GeneralAppointmentsManager(IGeneralAppointmentDal generalAppointmentDal)
        {
            _generalAppointmentDal = generalAppointmentDal;
        }

        public void TDelete(GeneralAppointment t)
        {
            _generalAppointmentDal.Delete(t);
        }

        public List<GeneralAppointment> TGetByCompanyId(int companyId)
        {
            return _generalAppointmentDal.GetByCompanyId(companyId);
        }

        public List<GeneralAppointment> TGetByCustomerId(int customerId)
        {
            return _generalAppointmentDal.GetByCustomerId(customerId);
        }

        public GeneralAppointment TGetByID(int id)
        {
            return _generalAppointmentDal.GetByID(id);
        }

        public List<GeneralAppointment> TGetList()
        {
            return _generalAppointmentDal.GetList();
        }

        public void TInsert(GeneralAppointment t)
        {
            _generalAppointmentDal.Insert(t);
        }

        public int TInsertGeneralAppointment(GeneralAppointment generalAppointment)
        {
            return _generalAppointmentDal.InsertGeneralAppointment(generalAppointment);
        }

        public void TUpdate(GeneralAppointment t)
        {
            _generalAppointmentDal.Update(t);
        }
    }
}
