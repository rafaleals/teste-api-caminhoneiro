using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_JSL.Models
{
    [Table("Motorista")]
    public class Motorista
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public int IdCaminhao { get; set; }
        public int IdEndereco { get; set; }

        public virtual Caminhao Caminhao { get; set; }
        public virtual Endereco Endereco { get; set; }

    }
}
