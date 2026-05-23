using System;
using System.Collections.Generic;
using HotelZormat.Negocio.Modelo;

namespace HotelZormat.Negocio.Servicios
{
    /// <summary>
    /// Servicio de validaciones y cálculos del flujo de reservas.
    /// Lab día 05 · ISW-123 · semana 02
    /// /// </summary>
    public class ReservaService
    {
        public bool ValidarTipoHabitacion(string tipo)
        {
            if (string.IsNullOrWhiteSpace(tipo))
            {
                return false;
            }

            if (tipo == "Sencilla" || tipo == "Doble" || tipo == "Suite")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}