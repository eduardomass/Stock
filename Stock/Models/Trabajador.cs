using System.ComponentModel.DataAnnotations;

namespace Stock.Models
{
    public class Trabajador
    {
        [Key]
        public int TrabajadorId { get; set; }
        public string NombreYApllido { get; set; } = "";
        public ICollection<JornadaLaboral>? JornadasLaborales { get; set; }
    }
}
