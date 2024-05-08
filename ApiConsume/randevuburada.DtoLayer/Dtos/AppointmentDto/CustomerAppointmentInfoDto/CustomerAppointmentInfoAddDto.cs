using randevuburada.EntityLayer.Concrete.CustomerConcrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DtoLayer.Dtos.AppointmentDto.CustomerAppointmentInfoDto
{
    public class CustomerAppointmentInfoAddDto
    {
        [Required(ErrorMessage = "Müşteri ID gereklidir.")]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Müşteri adı gereklidir.")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Müşteri soyadı gereklidir.")]
        public string CustomerSurname { get; set; }
        [Required(ErrorMessage = "Müşteri TC Kimlik numarası gereklidir.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Türkiye Cumhuriyeti Kimlik Bilgisi 11 haneli olmalıdır.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Türkiye Cumhuriyeti Kimlik Bilgisi yalnızca rakamlardan oluşmalıdır.")]
        public string CustomerTcNo { get; set; }
        [Required(ErrorMessage = "Müşteri telefon numarası gereklidir.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Telefon numarası 11 haneli olmalıdır.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Telefon numarası yalnızca rakamlardan oluşmalıdır.")]
        public string CustomerPhone { get; set; }
    }
}
