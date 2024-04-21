using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace endsem_project.Models
{
    public class Todo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("Task")]
        [Required(ErrorMessage ="Task cannot be empty")]
        public string? TaskName { get; set; }
        [Column("Tags")]
        public string? TaskTag  { get; set; }
        [Column("Priority")]
        public string? Priority { get; set; }
    }
}