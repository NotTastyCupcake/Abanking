using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Work.Models
{
    [Table("Persons")]
    public class MainPersonModel
    {
        //ID сотридника
        [Key]
        public int IdPerson { get; set; }
        //Имя сотрудника
        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Введите имя")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Текст должен содержать от 2 - 20 символов")]
        [RegularExpression(@"^[а-яА-Я]{1,20}$", ErrorMessage = "Здесь должен быть текст на русском")]
        public string FirstName { get; set; }
        //Фамилия сотридника
        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Введите фамилию")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Текст должен содержать от 2 - 20 символов")]
        [RegularExpression(@"^[а-яА-Я]{1,20}$", ErrorMessage = "Здесь должен быть текст на русском")]
        public string LastName { get; set; }
        //Возраст сотрудника
        [Display(Name = "Возраст")]
        [Required(ErrorMessage = "Введите возраст")]
        [Range(18, 99, ErrorMessage = "Возраст должен быть от 18 до 99 лет")]
        public int Age { get; set; }
        //Пол сотрудника
        [Display(Name = "Пол")]
        [Required(ErrorMessage = "Введите пол")]
        [RegularExpression(@"^[М, Ж]{1,1}$", ErrorMessage = "Текст должен содержать букву М или Ж")]
        public string Gender { get; set; }

        //Внешний ключ отдела
        [Display(Name = "Отдел")]
        public int IdDepartment { get; set; }
        [ForeignKey("IdDepartment")]
        [Display(Name = "Отдел")]
        public virtual Department Department { get; set; }
        //Внешний ключ языка программирования
        [Display(Name = "Язык")]
        public int IdLanguage { get; set; }
        [ForeignKey("IdLanguage")]
        [Display(Name = "Язык")]
        public virtual Language Language { get; set; }

    }
}