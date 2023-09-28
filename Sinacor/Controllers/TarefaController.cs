using Microsoft.AspNetCore.Mvc;
using Sinacor.Domain;
using Sinacor.Interfaces;

namespace Sinacor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarefaController : ControllerBase
    {
        private ILogger<TarefaController> _logger;
        private ITarefaService _tarefaService;

        public TarefaController(ILogger<TarefaController> logger, ITarefaService tarefaService)
        {
            _logger = logger;
            _tarefaService = tarefaService;
        }
        
        [HttpPost]        
        public ActionResult Post(Tarefa tarefa)
        {
            try
            {
                _tarefaService.EnviarTarefa(tarefa);              

                return Ok(tarefa);
            }
            catch(Exception ex)
            {
                _logger.LogError("Erro ao criar tarefa",ex);
                return new StatusCodeResult(500);
            }            
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                var tarefa = _tarefaService.GetById(id);
                if (tarefa == null)
                    return NotFound();
                return Ok(tarefa);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao buscar tarefa", ex);
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var tarefas = _tarefaService.GetAll();
                if (tarefas == null)
                    return NotFound();
                return Ok(tarefas);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao buscar tarefas", ex);
                return new StatusCodeResult(500);
            }
        }
        
    }
}
