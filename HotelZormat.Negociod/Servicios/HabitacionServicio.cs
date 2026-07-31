using HotelZormat.Datos.Repositorios;
using HotelZormat.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelZormat.Negocio.Servicios
{
    public class HabitacionService
    {
        private HabitacionRepository _repositorio = new HabitacionRepository();

        public List<Habitacion> ObtenerTodas()
        {
            return _repositorio.ObtenerTodas();
        }

        public Habitacion Buscar(int numero)
        {
            return _repositorio.BuscarPorNumero(numero);
        }

        public void Guardar(Habitacion habitacion)
        {
            var existente = Buscar(habitacion.Numero);
            if (existente != null &&
                existente.Estado == "Ocupada" &&
                habitacion.Estado == "Disponible")
            {
                throw new HabitacionOcupadaException(habitacion.Numero);
            }
            _repositorio.Actualizar(habitacion);
        }
    }
}