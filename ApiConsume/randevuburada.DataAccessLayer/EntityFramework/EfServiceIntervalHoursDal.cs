using randevuburada.DataAccessLayer.Abstract;
using randevuburada.DataAccessLayer.Concrete;
using randevuburada.DataAccessLayer.Repositories;
using randevuburada.EntityLayer.Concrete.CompanyConcrete.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DataAccessLayer.EntityFramework
{
    public class EfServiceIntervalHoursDal : GenericRepository<ServiceIntervalHours>, IServiceIntervalHoursDal
    {
        public EfServiceIntervalHoursDal(Context context) : base(context)
        {

        }

        public TimeOnly GetServiceIntervalHour(int id)
        {
            using (var context = new Context())
            {
                return context.ServiceIntervalHours.Where(x => x.Id == id).Select(x => x.intervalTime).FirstOrDefault();
            }
        }
    }
}
