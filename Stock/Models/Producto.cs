using System.ComponentModel.DataAnnotations;

namespace Stock.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; } = "";

        public int? CategoriaId { get; set; }

        public Categoria? Categoria { get; set; } 

    }
}
