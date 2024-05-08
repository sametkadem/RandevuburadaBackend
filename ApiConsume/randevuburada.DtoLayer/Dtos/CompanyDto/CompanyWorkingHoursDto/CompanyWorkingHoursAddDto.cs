using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DtoLayer.Dtos.CompanyDto.CompanyWorkingHoursDto
{
    public class CompanyWorkingHoursAddDto
    {
        [Required(ErrorMessage = "Şirket Kimliği alanı gereklidir.")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "Gün Kimliği alanı gereklidir.")]
        public int[] DayIds { get; set; }

        [Required(ErrorMessage = "Açılış Saati alanı gereklidir.")]
        public TimeDto OpenTime { get; set; }

        [Required(ErrorMessage = "Kapanış Saati alanı gereklidir.")]
        public TimeDto CloseTime { get; set; }
    }
    public class TimeDto
    {
        [Required(ErrorMessage = "Saat alanı gereklidir.")]
        [Range(0, 23, ErrorMessage = "Saat 0-23 arasında olmalıdır.")]
        public int Hour { get; set; }

        [Required(ErrorMessage = "Dakika alanı gereklidir.")]
        [Range(0, 59, ErrorMessage = "Dakika 0-59 arasında olmalıdır.")]
        public int Minute { get; set; }
    }
}
