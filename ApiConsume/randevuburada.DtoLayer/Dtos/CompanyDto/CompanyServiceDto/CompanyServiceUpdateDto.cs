using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DtoLayer.Dtos.CompanyDto.CompanyServiceDto
{
    public class CompanyServiceUpdateDto
    {
        [Required(ErrorMessage = "ID gereklidir.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Şirket Kimliği alanı gereklidir.")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "Ana Hizmet Kimliği alanı gereklidir.")]
        public int MainServiceId { get; set; }

        [Required(ErrorMessage = "Hizmet Adı alanı gereklidir.")]
        public string ServiceName { get; set; }

        [Required(ErrorMessage = "Hizmet Açıklaması alanı gereklidir.")]
        public string ServiceDescription { get; set; }

        [Required(ErrorMessage = "Cinsiyet Kimliği alanı gereklidir.")]
        public int GenderId { get; set; }

        [Required(ErrorMessage = "Fiyat alanı gereklidir.")]
        [Range(0, float.MaxValue, ErrorMessage = "Fiyat sıfırdan büyük olmalıdır.")]
        public float Price { get; set; }
    }
}
