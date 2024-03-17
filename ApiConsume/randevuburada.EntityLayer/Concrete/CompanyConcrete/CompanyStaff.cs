using randevuburada.EntityLayer.Concrete.CompanyConcrete.Staff;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.EntityLayer.Concrete.CompanyConcrete
{
    public class CompanyStaff
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public int StaffWorkingPositionId { get; set; }
        public StaffWorkingPosition StaffWorkingPosition { get; set; }
        public int StaffWorkingStatusId { get; set; }
        public StaffWorkingStatus StaffWorkingStatus { get; set; }
        public string ProfilPicture { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Tc { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Gender { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }




    }
}
