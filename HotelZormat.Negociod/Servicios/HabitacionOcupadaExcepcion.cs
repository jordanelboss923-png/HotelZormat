using System;

namespace HotelZormat.Negocio.Servicios
{
    public class HabitacionOcupadaException : Exception
    {
        public int NumeroHabitacion { get; }

        public HabitacionOcupadaException(int numero)
            : base("La habitación ya está ocupada.")
        {
            NumeroHabitacion = numero;
        }
    }
}