using GestionTareas.Models;
using Microsoft.EntityFrameworkCore;


namespace GestionTareas.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> optinos) : base(optinos) { }
    public DbSet<Tareas> Tareas { get; set; }
}
