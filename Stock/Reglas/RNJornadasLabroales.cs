using Stock.Models;
using Stock.ModelsView;

namespace Stock.Reglas
{
    public class RNJornadasLabroales
    {
        private readonly StockContext _context;

        public RNJornadasLabroales(StockContext context)
        {
            _context = context;
        }

        public async Task<string> GenerarJornadasLaborales(GeneradorJornada generador)
        {
            DateTime fechaInicio = generador.FechaDesde;
            fechaInicio = fechaInicio.AddMinutes(-fechaInicio.Minute);
            if (generador.FechaDesde.Minute >= 30)
                fechaInicio = fechaInicio.AddMinutes(30);
            var fechaInicioInicial = fechaInicio;
            bool estoyDentroDeLaFecha = fechaInicio <= generador.FechaHasta;
            while (estoyDentroDeLaFecha)
            {
                #region Generacion de Bloques dentro de un Dia
                for (int i = 0; i < generador.CantidadBloques; i++)
                {
                    //Crear jornada de la fecha, al trabajor en cuestion
                    bool noExiste = _context.JornadasLaborales
                        .Where(o => o.TrabajadorId == generador.IdTrabajador &&
                                o.FechaYHora == fechaInicio).Count() == 0;
                    noExiste = !_context.JornadasLaborales
                        .Any(o => o.TrabajadorId == generador.IdTrabajador &&
                                o.FechaYHora == fechaInicio);

                    if (noExiste)
                    {
                        JornadaLaboral jornada = new JornadaLaboral();
                        jornada.FechaYHora = fechaInicio;
                        jornada.TrabajadorId = generador.IdTrabajador;
                        //grabar en algun lado esas jornada
                        _context.JornadasLaborales.Add(jornada);
                        await _context.SaveChangesAsync();
                    }
                    if (fechaInicio.Hour == 23 && fechaInicio.Minute == 30)
                        break;
                    //sumar 30 minutos a la fecha
                    fechaInicio = fechaInicio.AddMinutes(30);
                }
                #endregion
                //01/11/2022 23:30
                fechaInicioInicial = fechaInicioInicial.AddDays(1); //01/11/2022 09:00
                //02/11/2022 09:00
                fechaInicio = fechaInicioInicial;

                estoyDentroDeLaFecha = fechaInicio <= generador.FechaHasta;
            }

            return "";
        }
    }
}
