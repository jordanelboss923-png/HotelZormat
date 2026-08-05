using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelZormat.Modelo
{
    public class Reserva
    {
        public int Id { get; set; }
        public int IdHabitacion { get; set; }
        public int IdHuesped { get; set; }
        public string NumeroHabitacion { get; set; }   // para mostrar en la lista sin otro JOIN manual
        public string NombreHuesped { get; set; }
        public DateTime FechaReserva { get; set; }
        public DateTime FechaEntradaEstimada { get; set; }
        public string Estado { get; set; }
    }
}
