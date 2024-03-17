using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DtoLayer.Dtos.IdentityDto.RegisterDto
{
    public class CreateNewUserDto
    {
     
        [Required(ErrorMessage = "Kullanıcı adı alanı boş geçilemez")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email alanı boş geçilemez")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre alanı boş geçilemez")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifre tekrarı alanı boş geçilemez")]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Şifre en az bir küçük harf, bir büyük harf, bir rakam ve bir özel karakter içermelidir ve 8-15 karakter uzunluğunda olmalıdır.")]
        public string ConfirmPassword { get; set; }
    }
}
