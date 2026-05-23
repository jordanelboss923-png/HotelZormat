using System;

namespace HotelZormat.Negocio.Modelo
{
    /// <summary>
    /// Representa una habitación del hotel.
    /// /// </summary>
    public class Habitacion
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public string Tipo { get; set; }
        public int Piso { get; set; }
        public int Capacidad { get; set; }
        public decimal TarifaBase { get; set; }
        public string Estado { get; set; }

        public bool EstaDisponible()
        {
            return Estado == "Disponible";
        }
    }
}