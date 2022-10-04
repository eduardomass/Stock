using System.ComponentModel.DataAnnotations;

namespace Stock.Models
{
    /// <summary>
    /// Ejemplos de Categorias
    /// Libreria, Computacion, Comedor, Limpieza.. etc
    /// </summary>
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }
        [Required]
        public string Descripcion { get; set; } = "";

        public DateTime? FechaCreacion { get; set; }

        public ICollection<Producto>? Productos { get; set; }
    }
}

