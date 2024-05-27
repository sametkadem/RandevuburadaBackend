using randevuburada.EntityLayer.Concrete.AppointmentConcrete;
using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using randevuburada.EntityLayer.Concrete.CustomerConcrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DtoLayer.Dtos.CustomerDto.CustomerCommentDto
{
    public class CustomerCommentAddDto
    {
        [Required(ErrorMessage = "Id gereklidir.")]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "İşletme Id gereklidir.")]
        public int CompanyId { get; set; }
        [Required(ErrorMessage = "Randevu Id gereklidir.")]
        public int AppoinmentId { get; set; }
        [Required(ErrorMessage = "Kullanıcı adı gizlilik kontrol gereklidir.")]
        public bool HideUserName { get; set; }
        [Required(ErrorMessage = "Yorum gereklidir.")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "Puan gereklidir.")]
        public float Rating { get; set; }
    }
}
