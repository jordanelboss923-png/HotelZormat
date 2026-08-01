using HotelZormat.Datod;
using HotelZormat.Datos.Repositorios;
using HotelZormat.Modelo;
using HotelZormat.Negocio.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelZormat.Negociod.Servicios
{
    public class EstadiaService
    {
        private EstadiaRepository _estadiaRepo = new EstadiaRepository();
        private FacturaRepository _facturaRepo = new FacturaRepository();
        private HabitacionRepository _habitacionRepo = new HabitacionRepository();

        // ---- CHECK IN ----
        public void RealizarCheckIn(int idHabitacion, int idHuesped, string temporada)
        {
            Habitacion h = _habitacionRepo.ObtenerTodas().Find(x => x.Id == idHabitacion);
            if (h == null) throw new Exception("Habitación no encontrada.");
            if (h.Estado != "Disponible") throw new HabitacionOcupadaException(h.Numero);

            Estadia e = new Estadia
            {
                IdHabitacion = idHabitacion,
                IdHuesped = idHuesped,
                FechaEntrada = DateTime.Now,
                Temporada = temporada
            };
            _estadiaRepo.Insertar(e);

            h.Estado = "Ocupada";
            _habitacionRepo.Actualizar(h);
        }

        // ---- CHECK OUT: cierra la estadía y genera factura ----
        public Factura RealizarCheckOut(int idHabitacion, int nochesHospedado)
        {
            Estadia estadia = _estadiaRepo.ObtenerActivaPorHabitacion(idHabitacion);
            if (estadia == null) throw new Exception("No hay una estadía activa para esta habitación.");

            Habitacion h = _habitacionRepo.ObtenerTodas().Find(x => x.Id == idHabitacion);

            decimal factorTemporada;
            switch (estadia.Temporada)
            {
                case "Alta":
                    factorTemporada = 1.0m;      // sin descuento
                    break;
                case "Media":
                    factorTemporada = 0.90m;     // 10% descuento
                    break;
                case "Baja":
                    factorTemporada = 0.80m;     // 20% descuento
                    break;
                default:
                    factorTemporada = 1.0m;
                    break;
            }

            decimal subtotal = h.TarifaBase * nochesHospedado * factorTemporada;
            decimal itbis = subtotal * 0.18m;
            decimal propina = subtotal * 0.10m;
            decimal total = subtotal + itbis + propina;

            int numeroFactura = _facturaRepo.ContarFacturas() + 1;
            string ncf = "B02" + numeroFactura.ToString("D8"); // formato tipo Consumo Final

            Factura factura = new Factura
            {
                IdEstadia = estadia.Id,
                NCF = ncf,
                Subtotal = subtotal,
                ITBIS = itbis,
                Propina = propina,
                Total = total
            };

            _facturaRepo.Insertar(factura);
            _estadiaRepo.Cerrar(estadia.Id);

            h.Estado = "Limpieza";
            _habitacionRepo.Actualizar(h);

            return factura;
        }
    }
}
