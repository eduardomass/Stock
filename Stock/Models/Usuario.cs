using System.ComponentModel.DataAnnotations;

namespace Stock.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Password { get; set; } = "";

        public IList<ProductoPorUsuario>? ProductosPorUsuario { get; set; }
    }
}
