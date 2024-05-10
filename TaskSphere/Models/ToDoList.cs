using System.ComponentModel.DataAnnotations;

namespace TaskSphere.Models
{
    public class ToDoList
    {
        [Key]
        public int ToDoListId { get; set; }

        [Display(Name = "Tarefa")]
        [Required]
        public string? TaskName { get; set; }

        [Display(Name = "Descrição")]
        public string? TaskDescription { get; set; }

        [Display(Name = "Status")]
        [Required]
        public string? TaskStatus { get; set; }

        [Display(Name = "Categoria")]
        public string? TaskType { get; set; }

        [Display(Name = "Data de Início")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateOnly? StartDate { get; set; }

        [Display(Name = "Data de Conclusão")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateOnly? EndDate { get; set; }
    }
}
