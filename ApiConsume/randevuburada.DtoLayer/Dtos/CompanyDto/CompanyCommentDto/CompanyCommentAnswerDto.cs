using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DtoLayer.Dtos.CompanyDto.CompanyCommentDto
{
    public class CompanyCommentAnswerDto
    {
        [Required(ErrorMessage = "Id gereklidir.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "İşletme cevabı gereklidir.")]
        public string Answer { get; set; }
    }
}
