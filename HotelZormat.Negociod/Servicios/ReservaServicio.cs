using HotelZormat.Datod;
using HotelZormat.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelZormat.Negociod.Servicios
{
    public class ReservaServicio
    {
        private ReservaRepository _repositorio = new ReservaRepository();

        public void CrearReserva(int idHabitacion, int idHuesped, DateTime fechaEntrada)
        {
            if (fechaEntrada.Date < DateTime.Now.Date)
                throw new Exception("La fecha de entrada no puede ser anterior a hoy.");

            Reserva r = new Reserva
            {
                IdHabitacion = idHabitacion,
                IdHuesped = idHuesped,
                FechaEntradaEstimada = fechaEntrada
            };
            _repositorio.Insertar(r);
        }

        public List<Reserva> ObtenerTodas()
        {
            return _repositorio.ObtenerTodas();
        }

        public void Confirmar(int idReserva)
        {
            _repositorio.CambiarEstado(idReserva, "Confirmada");
        }

        public void Cancelar(int idReserva)
        {
            _repositorio.CambiarEstado(idReserva, "Cancelada");
        }
    }
}
