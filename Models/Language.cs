using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Work.Models
{
    [Table("Languages")]
    public class Language
    {
        //ID Языка программирования
        [Key]
        public int IdLanguage { get; set; }
        //Название языка программирования
        [Display(Name = "Название языка")]
        [Required(ErrorMessage = "Введите название языка")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Текст должен содержать от 1 - 20 символов")]
        public string NameLanguage { get; set; }

    }
}