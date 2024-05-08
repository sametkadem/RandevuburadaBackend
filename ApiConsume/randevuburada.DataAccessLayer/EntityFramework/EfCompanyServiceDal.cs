using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
    public class EfCompanyServiceDal : GenericRepository<CompanyService>, ICompanyServiceDal
    {
        public EfCompanyServiceDal(Context context) : base(context)
        {
        }

        public List<CompanyService> GetByCompanyId(int companyId)
        {
            Console.WriteLine("GetByCompanyId fonksiyonu içine girildi.");

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



    }
}
