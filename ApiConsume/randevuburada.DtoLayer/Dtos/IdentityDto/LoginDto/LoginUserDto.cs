using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DtoLayer.Dtos.IdentityDto.LoginDto
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = "E-Posta/Telefon numarası boş geçilemez")]
        public string Identifier { get; set; }
        [Required(ErrorMessage = "Şifre alanı boş geçilemez")]
        public string Password { get; set; }
    }
}
