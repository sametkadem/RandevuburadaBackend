using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DtoLayer.Dtos.AppointmentDto.AppointmentCompanyDto
{
    public class CompanyDeleteAppointmentDto
    {
        [Required(ErrorMessage = "Randevu ID gereklidir.")]
        public int AppointmentId { get; set; }
        [Required(ErrorMessage = "Açıklama gereklidir.")]
        public string Description { get; set; }
    }
}
