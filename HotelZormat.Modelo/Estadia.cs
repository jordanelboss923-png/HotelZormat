using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelZormat.Modelo
{
    public class Estadia
    {
        public int Id { get; set; }
        public int IdHabitacion { get; set; }
        public int IdHuesped { get; set; }
        public DateTime FechaEntrada { get; set; }
        public DateTime? FechaSalida { get; set; }
        public string Temporada { get; set; }   // "Alta", "Media", "Baja"
        public string Estado { get; set; }       // "Activa", "Cerrada"
    }
}
