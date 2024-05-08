using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DtoLayer.Dtos.CompanyDto.CompanyWorkingHoursDto
{
    public class CompanyWorkingHoursUpdateDto
    {
        [Required(ErrorMessage = "ID alanı gereklidir.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Şirket Kimliği alanı gereklidir.")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "Gün Kimliği alanı gereklidir.")]
        public int[] DayIds { get; set; }

        [Required(ErrorMessage = "Açılış Saati alanı gereklidir.")]
        public TimeDto OpenTime { get; set; }

        [Required(ErrorMessage = "Kapanış Saati alanı gereklidir.")]
        public TimeDto CloseTime { get; set; }
    }
    
}
