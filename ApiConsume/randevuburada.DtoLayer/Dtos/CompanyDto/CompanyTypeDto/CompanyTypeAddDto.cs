using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DtoLayer.Dtos.CompanyDto.CompanyTypeDto
{
    public class CompanyTypeAddDto
    {
        [Required(ErrorMessage = "İşletme Tür Adı gereklidir.")]
        public string CompanyTypeName { get; set; }
    }
}
