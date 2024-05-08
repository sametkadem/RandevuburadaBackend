using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using randevuburada.EntityLayer.Concrete.CustomerConcrete;
using randevuburada.EntityLayer.Concrete.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.EntityLayer.Concrete.AppointmentConcrete
{
    public class GeneralAppointment
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int AppointmentStatusId { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; }
        public DateTime AppointmentDate { get; set; }
        public bool IsCompanyApproved { get; set; }
        public DateTime LastCancelDate { get; set; }
        public bool IsCancelAppointment { get; set; }
        public float totalAmount { get; set; }
        public bool IsPaid { get; set; }
        public int PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
    }
}
