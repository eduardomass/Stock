using Stock.Models;

namespace Stock.ModelsView
{
    public class ProductoCarrito
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = "";
        public string DescripcionCategoria { get; set; } = "";
        public int Cantidad { get; set; } = 0;

        public void SetProducto(Producto p)
        {
            this.Id = p.Id;
            this.Descripcion = p.Nombre;
            this.DescripcionCategoria = p.Categoria.Descripcion;
        }
    }
}
