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
    public class EfCompanyStaffDal : GenericRepository<CompanyStaff>, ICompanyStaffDal
    {
        public EfCompanyStaffDal(Context context) : base(context)
        {
            
        }

        public List<CompanyStaff> GetByCompanyId(int companyId)
        {
            var context = new Context();
            return context.CompanyStaffs.Where(x => x.CompanyId == companyId).ToList();
        }

        public CompanyStaff updateCompanyStaff(CompanyStaff companyStaff)
        {
            var context = new Context();
            var findCompanyStaff = context.CompanyStaffs.Find(companyStaff.Id);
            findCompanyStaff.CompanyId = companyStaff.CompanyId;
            findCompanyStaff.StaffWorkingPositionId = companyStaff.StaffWorkingPositionId;
            findCompanyStaff.StaffWorkingStatusId = companyStaff.StaffWorkingStatusId;
            findCompanyStaff.ProfilPicture = companyStaff.ProfilPicture;
            findCompanyStaff.FirstName = companyStaff.FirstName;
            findCompanyStaff.LastName = companyStaff.LastName;
            findCompanyStaff.PhoneNumber = companyStaff.PhoneNumber;
            findCompanyStaff.Email = companyStaff.Email;
            findCompanyStaff.Tc = companyStaff.Tc;
            findCompanyStaff.BirthDate = companyStaff.BirthDate;

            var updatedEntity = context.Entry(findCompanyStaff);
            updatedEntity.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return findCompanyStaff;
        }



        public List<CompanyStaff> getStaffsByArrayInts(List<int> staffIds)
        {
            var context = new Context();
            var staffs = context.CompanyStaffs.Where(x => staffIds.Contains(x.Id)).ToList();
            return staffs;
        }





    }
}
