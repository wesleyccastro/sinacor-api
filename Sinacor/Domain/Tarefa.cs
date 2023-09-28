using Sinacor.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sinacor.Domain
{
    [Table("Tarefas")]
    public class Tarefa
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public EnumStatusTarefa Status { get; set; }
       
    }
}
