using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_JSL.Models
{
    [Table("Endereco")]
    public class Endereco
    {
        [Key]
        public int IdEndereco { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string Coordenadas { get; set; }
    }
}

