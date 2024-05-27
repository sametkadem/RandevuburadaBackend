using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DtoLayer.Dtos.AppointmentDto.CustomerAppointmentAvailableDto
{
    public class CustomerAppointmentAvailableGetDto
    {
        [Required(ErrorMessage = "İşletme Id gereklidir.")]
        public int companyId { get; set; }
        [Required(ErrorMessage = "En az bir Servis ve Personel Id gereklidir.")]
        public required List<AppointmentAvailableDto> AppointmentAvailables { get; set; }
        public DateTime AppointmentDate { get; set; }
    }

    public class AppointmentAvailableDto
    {
        [Required(ErrorMessage = "Servis Id gereklidir.")]
        public int ServiceId { get; set; }

        [Required(ErrorMessage = "Personel Id gereklidir.")]
        public int StaffId { get; set; }
    }
}
