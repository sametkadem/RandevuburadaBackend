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

        public List<CompanyWorkingHoursDayList> CompanyWorkingHours { get; set; }
    }

    public class CompanyWorkingHoursDayList
    {
        public int DayId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }

}
