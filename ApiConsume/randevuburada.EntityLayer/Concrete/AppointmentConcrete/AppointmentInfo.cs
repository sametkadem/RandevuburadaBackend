using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using randevuburada.EntityLayer.Concrete.CustomerConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.EntityLayer.Concrete.AppointmentConcrete
{
    public class AppointmentInfo
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public GeneralAppointment Appointment { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public int CompanyServiceId { get; set; }
        public CompanyService CompanyService { get; set; }
        public float Price { get; set; }
        public bool IsComplate { get; set; }
        public bool IsCancel { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
    }
}
