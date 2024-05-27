using randevuburada.DataAccessLayer.Abstract;
using randevuburada.DataAccessLayer.Concrete;
using randevuburada.DataAccessLayer.Repositories;
using randevuburada.EntityLayer.Concrete.AppointmentConcrete;
using randevuburada.EntityLayer.Concrete.CustomerConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DataAccessLayer.EntityFramework
{
    public class EfGeneralAppointmentDal : GenericRepository<GeneralAppointment>, IGeneralAppointmentDal
    {
        public EfGeneralAppointmentDal(Context context) : base(context)
        {

        }

        public List<GeneralAppointment> GetByCompanyId(int companyId)
        {
            var context = new Context();
            return context.GeneralAppointment.Where(x => x.CompanyId == companyId).ToList();
        }

        public int InsertGeneralAppointment(GeneralAppointment generalAppointment)
        {
            try
            {
                using (var context = new Context())
                {
                    context.GeneralAppointment.Add(generalAppointment);
                    context.SaveChanges();
                    return generalAppointment.Id;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Yükleme başarısız oldu. Hata: " + ex.Message);
                return -1; // Hata durumunda -1 döndür
            }
        }

        public List<GeneralAppointment> GetByCustomerId(int customerId)
        {
            var context = new Context();
            return context.GeneralAppointment.Where(x => x.CustomerId == customerId).ToList();
        }

    }
}
