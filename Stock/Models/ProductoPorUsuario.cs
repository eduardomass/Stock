namespace Stock.Models
{
    public class ProductoPorUsuario
    {
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public int ProductoId { get; set; }
        public Producto? Producto { get; set; }
    }
}
