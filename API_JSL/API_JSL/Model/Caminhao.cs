using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_JSL.Models
{
    [Table("Caminhao")]
    public class Caminhao
    {
        [Key]
        public int IdCaminhao { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
        public int Eixo { get; set; }
    }
}
