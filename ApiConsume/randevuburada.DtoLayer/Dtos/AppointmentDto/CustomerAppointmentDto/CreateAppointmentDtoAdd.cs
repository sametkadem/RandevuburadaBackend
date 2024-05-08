using randevuburada.EntityLayer.Concrete.Other;
using System;
using System.ComponentModel.DataAnnotations;

namespace randevuburada.DtoLayer.Dtos.AppointmentDto.CustomerAppointmentDto
{
    public class CreateAppointmentDtoAdd
    {
        [Required(ErrorMessage = "Şirket ID gereklidir.")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "Müşteri ID gereklidir.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Randevu bilgileri gereklidir.")]
        public AppointmentDto[] Appointments { get; set; }

        public int PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }

        public class AppointmentDto
        {
            [Required(ErrorMessage = "Şirket Servis ID gereklidir.")]
            public int CompanyServiceId { get; set; }

            [Required(ErrorMessage = "Şirket Personel ID gereklidir.")]
            public int CompanyStaffId { get; set; }

            [Required(ErrorMessage = "Randevu Tarihi Gereklidir.")]
            public DateTime AppointmentDate { get; set; }
        }
    }
}
