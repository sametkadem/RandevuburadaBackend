using randevuburada.DataAccessLayer.Abstract;
using randevuburada.DataAccessLayer.Concrete;
using randevuburada.DataAccessLayer.Repositories;
using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using randevuburada.EntityLayer.Concrete.CustomerConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DataAccessLayer.EntityFramework
{
    public class EfCustomerAppointmentInfoDal : GenericRepository<CustomerAppointmentInfo>, ICustomerAppointmentInfoDal
    {
        public EfCustomerAppointmentInfoDal(Context context) : base(context)
        {

        }

        public List<CustomerAppointmentInfo> GetByCustomerID(int customerId)
        {
            var context = new Context();
            return context.CustomerAppointmentInfo.Where(x => x.CustomerId == customerId).ToList();
        }
    }
}
