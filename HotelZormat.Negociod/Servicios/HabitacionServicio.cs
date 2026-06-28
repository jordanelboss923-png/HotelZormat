using HotelZormat.Modelos;
using HotelZormat.Negocio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelZormat.Negocio.Servicios
{
    public class HabitacionService
    {
        private List<Habitacion> habitaciones;

        public HabitacionService()
        {
            habitaciones = new List<Habitacion>()
            {
                new Habitacion{Numero=301,Piso=3,Tipo="Simple",Estado="Disponible"},
                new Habitacion{Numero=302,Piso=3,Tipo="Doble",Estado="Reservada"},
                new Habitacion{Numero=303,Piso=3,Tipo="Suite",Estado="Ocupada"},
                new Habitacion{Numero=304,Piso=3,Tipo="Simple",Estado="Limpieza"},
                new Habitacion{Numero=401,Piso=4,Tipo="Suite",Estado="Disponible"},
                new Habitacion{Numero=402,Piso=4,Tipo="Doble",Estado="Disponible"}
            };
        }

        public List<Habitacion> ObtenerHabitaciones()
        {
            return habitaciones;
        }

        public Habitacion Buscar(int numero)
        {
            return habitaciones.FirstOrDefault(h => h.Numero == numero);
        }

        public void Guardar(Habitacion habitacion)
        {
            if (habitacion.Estado == "Ocupada")
                throw new HabitacionOcupadaException(habitacion.Numero);

            habitaciones.Add(habitacion);
        }
    }
}