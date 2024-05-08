using randevuburada.DataAccessLayer.Abstract;
using randevuburada.DataAccessLayer.Concrete;
using randevuburada.DataAccessLayer.Repositories;
using randevuburada.DtoLayer.Dtos.CompanyDto.CompanyWorkingHoursDto;
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

        public List<CompanyWorkingHours> GetByCompanyId(int companyId)
        {
            var context = new Context();
            return context.CompanyWorkingHours.Where(x => x.CompanyId == companyId).ToList();
        }


        public void DeleteByCompanyId(int companyId)
        {
            var context = new Context();
            var workingHours = context.CompanyWorkingHours.Where(x => x.CompanyId == companyId).ToList();
            foreach (var item in workingHours)
            {
                context.CompanyWorkingHours.Remove(item);
            }
            context.SaveChanges();
        }

        public void UpdateByCompanyId(int companyId, CompanyWorkingHoursUpdateDto companyWorkingHours)
        {
            var context = new Context();

            
                foreach (var item in companyWorkingHours.DayIds)
                {
                    var existingRecord = context.CompanyWorkingHours.FirstOrDefault(x => x.CompanyId == companyId && x.DayId == item);

                    if (existingRecord != null)
                    {
                        existingRecord.OpenTime = new TimeOnly(companyWorkingHours.OpenTime.Hour, companyWorkingHours.OpenTime.Minute);
                        existingRecord.CloseTime = new TimeOnly(companyWorkingHours.CloseTime.Hour, companyWorkingHours.CloseTime.Minute);
                        existingRecord.UpdatedAt = DateTime.Now;
                        context.CompanyWorkingHours.Update(existingRecord);
                    }
                    else
                    {
                        // Eğer kayıt yoksa, yeni bir kayıt oluştur
                        context.CompanyWorkingHours.Add(new CompanyWorkingHours
                        {
                            CompanyId = companyId,
                            Company = context.Companies.FirstOrDefault(x => x.Id == companyId),
                            DayId = item,
                            Day = context.Days.FirstOrDefault(x => x.Id == item),
                            OpenTime = new TimeOnly(companyWorkingHours.OpenTime.Hour, companyWorkingHours.OpenTime.Minute),
                            CloseTime = new TimeOnly(companyWorkingHours.CloseTime.Hour, companyWorkingHours.CloseTime.Minute),
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now
                        });
                    }
                }
                context.SaveChanges();
        }

    }
}
