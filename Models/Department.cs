using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Work.Models
{
    [Table("Deportaments")]
    public class Department
    {
        [Key]
        //ID Отдела сотрудников
        public int IdDepartment { get; set; }
        //Название отдела сотрудника
        [Display(Name = "Имя отдела")]
        [Required(ErrorMessage = "Введите название отдела")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Текст должен содержать от 1 - 20 символов")]
        public string NameDepart { get; set; }
        //Этаж отдела
        [Display(Name = "Этаж")]
        [Required(ErrorMessage = "Введите этаж")]
        [Range(1, 9, ErrorMessage = "Этаж должен быть от 1 до 9")]
        public int Floor { get; set; }
    }
}