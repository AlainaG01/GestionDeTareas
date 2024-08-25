using System.ComponentModel.DataAnnotations;

namespace GestionTareas.Models;

public class Tareas
{
    [Key]
    public int TareaId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Priority { get; set; }
    public string? Status { get; set; }

}
