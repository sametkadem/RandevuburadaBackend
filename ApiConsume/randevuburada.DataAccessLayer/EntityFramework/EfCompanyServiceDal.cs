using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using randevuburada.DataAccessLayer.Abstract;
using randevuburada.DataAccessLayer.Concrete;
using randevuburada.DataAccessLayer.Repositories;
using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DataAccessLayer.EntityFramework
{
    public class EfCompanyServiceDal : GenericRepository<CompanyService>, ICompanyServiceDal
    {
        public EfCompanyServiceDal(Context context) : base(context)
        {
        }

        public List<CompanyService> GetByCompanyId(int companyId)
        {
            using (var context = new Context())
            {
                var companyIdParam = new SqlParameter("@CompanyId", companyId);
                var query = "SELECT * FROM CompanyServices WHERE CompanyId = @CompanyId";
                return context.CompanyServices.FromSqlRaw(query, companyIdParam).ToList();
            }
        }

        public async Task<List<CompanyService>> GetByCompanyIdAsync(int companyId)
        {
            using (var context = new Context())
            {
                return await context.CompanyServices.Where(x => x.CompanyId == companyId).ToListAsync();
            }
        }
        public CompanyService GetByCompanyIdAndServiceId(int companyId, int serviceId)
        {
            using (var context = new Context())
            {
                return context.CompanyServices.FirstOrDefault(x => x.CompanyId == companyId && x.Id == serviceId);
            }
        }

        public CompanyService updateCompanyService(CompanyService companyService)
        {
            using (var context = new Context())
            {
                var findCompanyService = context.CompanyServices.Find(companyService.Id);
                findCompanyService.CompanyId = companyService.CompanyId;
                findCompanyService.MainServiceId = companyService.MainServiceId;
                findCompanyService.ServiceName = companyService.ServiceName;
                findCompanyService.ServiceDescription = companyService.ServiceDescription;
                findCompanyService.GenderId = companyService.GenderId;
                findCompanyService.Price = companyService.Price;
                findCompanyService.ServiceIntervalHoursId = companyService.ServiceIntervalHoursId;
                findCompanyService.CompanyStaffIds = companyService.CompanyStaffIds;

                var updatedEntity = context.Entry(findCompanyService);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
                return findCompanyService;
            }
        }



    }
}
