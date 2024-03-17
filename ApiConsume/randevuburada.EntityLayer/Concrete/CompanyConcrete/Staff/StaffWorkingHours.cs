using randevuburada.EntityLayer.Concrete.CompanyConcrete.Service;
using randevuburada.EntityLayer.Concrete.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.EntityLayer.Concrete.CompanyConcrete.Staff
{
    public class StaffWorkingHours
    {
        public int Id { get; set; }
        public int StaffId { get; set; }
        public CompanyStaff CompanyStaff { get; set; }
        public int DayId { get; set; }
        public Day Day { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDateBreak { get; set; }
        public DateTime EndDateBreak { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
    }
}
