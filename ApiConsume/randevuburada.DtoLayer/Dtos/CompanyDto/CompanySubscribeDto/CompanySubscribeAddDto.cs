using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using randevuburada.EntityLayer.Concrete.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DtoLayer.Dtos.CompanyDto.CompanySubscribeDto
{
    public class CompanySubscribeAddDto
    {
        [Required(ErrorMessage = "Kullanıcı Id gereklidir.")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "İşletme Paket Id gereklidir.")]
        public int companyPackageId { get; set; }
    }
}
