using System.ComponentModel.DataAnnotations;

namespace Stock.Models
{
    public class JornadaLaboral
    {
        [Key]
        public int JornadaLaboralId { get; set; }
        
        public DateTime FechaYHora { get; set; }
        
        public int TrabajadorId { get; set; }
        public Trabajador? Trabajador { get; set; }

    }
}
