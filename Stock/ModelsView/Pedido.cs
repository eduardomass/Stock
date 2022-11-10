using Microsoft.AspNetCore.Mvc.Rendering;
using Stock.Models;

namespace Stock.ModelsView
{
    public class Pedido
    {
        public DateOnly Fecha { get; set; }
        public SelectList Productos { get; set; }
        public List<Int32> ProductosSeleccionados { get; set; }
    }
}
