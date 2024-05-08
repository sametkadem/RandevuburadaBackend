using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DtoLayer.Dtos.CustomerDto.CustomerFavouriteDto
{
    public class CustomerFavouriteAddDto
    {
        [Required(ErrorMessage = "Müşteri Id gereklidir.")]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Favori Şirket Id gereklidir.")]
        public int CompanyId { get; set; }

    }
}
