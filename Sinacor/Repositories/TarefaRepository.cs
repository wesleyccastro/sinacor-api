using Microsoft.EntityFrameworkCore;
using Sinacor.Context;
using Sinacor.Domain;
using Sinacor.Interfaces;

namespace Sinacor.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {        
        private readonly AppDbContext _context;
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public TarefaRepository(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
            _context = _contextFactory.CreateDbContext();
        }

        public Tarefa GetById(int id)
        {
            return _context.Tarefas.Find(id);
        }
        public List<Tarefa> GetAll()
        {
            return _context.Tarefas.ToList();
        }           
    }
}
