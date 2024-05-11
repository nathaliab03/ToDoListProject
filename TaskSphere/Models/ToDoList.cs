using System.ComponentModel.DataAnnotations;

namespace TaskSphere.Models
{
    public class ToDoList
    {
        [Key]
        [Display(Name  = "Id")]
        public int ToDoListId { get; set; }

        [Display(Name = "Tarefa")]
        [Required]
        public string? TaskName { get; set; }

        [Display(Name = "Descrição")]
        public string? TaskDescription { get; set; }

        [Display(Name = "Status")]
        [Required]
        public string? TaskStatus { get; set; } = "Em Andamento";

        [Display(Name = "Categoria")]
        public string? TaskType { get; set; }

        [Display(Name = "Data de Início")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        [Display(Name = "Data de Conclusão")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateOnly? EndDate { get; set; }

        public bool IsCompleted { get; set; }

        public void SetEndDate()
        {
            if (IsCompleted == true)
            {
                EndDate = DateOnly.FromDateTime(DateTime.Now);
            } else
            {

             EndDate = null;
            }

        }

        public string SetTaskStatus()
        {
            if (IsCompleted != true)
                return TaskStatus = "Em Andamento";

            return TaskStatus = "Concluída";
        }

        public void ToggleTaskStatus()
        {
            IsCompleted = !IsCompleted; // Alterna o estado de concluída para em andamento e vice-versa
            SetTaskStatus(); // Chama o método para definir o status com base no IsCompleted atualizado
        }

    }
}
