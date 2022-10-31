using System.ComponentModel.DataAnnotations;

namespace Stock.ModelsView
{
    public class GeneradorJornada
    {
        public int IdTrabajador { get; set; }
        public DateTime FechaDesde { get; set; }
        public int CantidadBloques { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaHasta { get; set; }

    }
}
