using GestionTareas.DAL;
using GestionTareas.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestionTareas.Services;

public class TareaService
{
    private readonly Contexto _contexto;

    public TareaService(Contexto contexto)
    {
        _contexto = contexto;
    }

    private async Task<bool> Insertar(Tareas tareas)
    {
        _contexto.Tareas.Add(tareas);
        return await _contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Existe(int tareaId)
    {
        return await _contexto.Tareas.AnyAsync(e => e.TareaId == tareaId);
    }

    public async Task<bool> ExisteTarea(int tareaId, string title, string description)
    {
        return await _contexto.Tareas.AnyAsync(e => e.TareaId != tareaId
        && e.Title.ToLower().Equals(title.ToLower()) || e.Description.ToLower().Equals(description.ToLower()));
    }

    public async Task<Tareas> Buscar(int id)
    {
        return await _contexto.Tareas.AsNoTracking().FirstOrDefaultAsync(e => e.TareaId == id);
    }

    public async Task<bool> Eliminar(Tareas tarea)
    {
        return await _contexto.Tareas.AsNoTracking().Where(e => e.TareaId == tarea.TareaId).ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Tareas>> Listar(Expression<Func<Tareas, bool>> criterio)
    {
        return await _contexto.Tareas.AsNoTracking().Where(criterio).ToListAsync();
    }

    private async Task<bool> Modificar(Tareas tarea)
    {
        _contexto.Update(tarea);
        var modificado = await _contexto.SaveChangesAsync() > 0;
        _contexto.Entry(tarea).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        return modificado;
    }

    public async Task<bool> Guardar(Tareas tarea)
    {
        if (!await Existe(tarea.TareaId))
            return await Insertar(tarea);
        else
            return await Modificar(tarea);
    }
}
