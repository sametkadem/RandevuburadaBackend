using randevuburada.EntityLayer.Concrete.CustomerConcrete;
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
        [Required(ErrorMessage = "Randevu müşteri bilgisi gereklidir.")]
        public int CustomerAppointmentInfoId { get; set; }
        [Required(ErrorMessage = "Randevu müşteri fatura bilgisi gereklidir.")]
        public int CustomerBillingInfoId { get; set; }

        [Required(ErrorMessage = "Randevu bilgileri gereklidir.")]
        [MinLength(1, ErrorMessage = "En az 1 randevu bilgisi gereklidir.")]
        public AppointmentDto[] Appointments { get; set; }

        [Required(ErrorMessage = "Ödeme tipi gereklidir.")]
        public int PaymentTypeId { get; set; }
        public class AppointmentDto
        {
            [Required(ErrorMessage = "Şirket Servis ID gereklidir.")]
            public int ServiceId { get; set; }

            [Required(ErrorMessage = "Şirket Personel ID gereklidir.")]
            public int StaffId { get; set; }

            [Required(ErrorMessage = "Randevu Tarihi Gereklidir.")]
            public DateTime AppointmentDate { get; set; }
        }

    }
}
