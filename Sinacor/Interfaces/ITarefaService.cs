using Sinacor.Domain;

namespace Sinacor.Interfaces
{
    public interface ITarefaService
    {
        void EnviarTarefa(Tarefa tarefa);
        Tarefa GetById(int id);
        List<Tarefa> GetAll();
    }
}
