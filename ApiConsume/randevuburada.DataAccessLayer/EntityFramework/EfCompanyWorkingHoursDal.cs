using randevuburada.DataAccessLayer.Abstract;
using randevuburada.DataAccessLayer.Concrete;
using randevuburada.DataAccessLayer.Repositories;
using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DataAccessLayer.EntityFramework
{
    public class EfCompanyWorkingHoursDal : GenericRepository<CompanyWorkingHours>, ICompanyWorkingHoursDal
    {
        public EfCompanyWorkingHoursDal(Context context) : base(context)
        {

        }

        public List<int> HasCompanyWorkingHours(int companyId, int[] dayIds)
        {
            var context = new Context();
            var find = new List<int>();
            foreach (var dayId in dayIds)
            {
                var control = context.CompanyWorkingHours.Any(x => x.CompanyId == companyId && x.DayId == dayId);
                if (control)
                {
                    find.Add(dayId);
                }
            }
            return find;
        }

    }
}
