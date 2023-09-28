namespace Sinacor.Services
{
    using Microsoft.Extensions.Logging;
    using Sinacor.Domain;
    using Sinacor.Interfaces;
    using System;

    public class TarefaService : ITarefaService
    {
        private readonly IRabbitMQService _rabbitMQService;
        private readonly ILogger<TarefaService> _logger;
        private readonly ITarefaRepository _tarefaRepository;

        public TarefaService(IRabbitMQService rabbitMQService, ILogger<TarefaService> logger, ITarefaRepository tarefaRepository)
        {
            _rabbitMQService = rabbitMQService;
            _logger = logger;
            _tarefaRepository = tarefaRepository;
        }

        public void EnviarTarefa(Tarefa tarefa)
        {
            try
            {                   
                _rabbitMQService.SendMessageToQueue<Tarefa>(tarefa);
                _logger.LogInformation("Tarefa enviada com sucesso: Descrição = {Descricao}, Data = {Data}", tarefa.Descricao, tarefa.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao enviar tarefa: {ErrorMessage}", ex.Message);
                throw;
            }
        }

        public Tarefa GetById(int id)
        {
            return _tarefaRepository.GetById(id);
        }

        public List<Tarefa> GetAll()
        {
            return _tarefaRepository.GetAll();
        }
    }
}
