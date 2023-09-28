using Sinacor.Domain;

namespace Sinacor.Interfaces
{
    public interface ITarefaRepository
    {        
        Tarefa GetById(int id);
        List<Tarefa> GetAll();
    }
}
