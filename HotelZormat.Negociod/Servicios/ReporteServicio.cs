using HotelZormat.Datod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelZormat.Negociod.Servicios
{
    public class ReporteServicio
    {
        private ReporteRepository _repositorio = new ReporteRepository();

        public Dictionary<string, int> ObtenerOcupacion()
        {
            return _repositorio.ObtenerOcupacionPorEstado();
        }

        public List<object[]> ObtenerIngresos(DateTime desde, DateTime hasta)
        {
            if (hasta < desde)
                throw new Exception("La fecha final no puede ser anterior a la fecha inicial.");

            return _repositorio.ObtenerIngresosPorRango(desde, hasta);
        }

        public decimal CalcularTotalIngresos(List<object[]> facturas)
        {
            decimal total = 0;
            foreach (object[] f in facturas)
            {
                total += (decimal)f[4]; // columna Total
            }
            return total;
        }
    }
}
