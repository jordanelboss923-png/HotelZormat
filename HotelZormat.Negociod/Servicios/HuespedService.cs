using HotelZormat.Datos.Repositorios;
using HotelZormat.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelZormat.Negocio.Servicios
{
    public class HuespedService
    {
        private HuespedRepository _repositorio = new HuespedRepository();

        public List<Huesped> ObtenerTodos()
        {
            return _repositorio.ObtenerTodos();
        }

        public Huesped Buscar(string numeroDocumento)
        {
            return _repositorio.BuscarPorDocumento(numeroDocumento);
        }

        public void Guardar(Huesped huesped)
        {
            // Validar que el huésped no sea nulo
            if (huesped == null)
                throw new ArgumentNullException(nameof(huesped));

            // Validar número de documento
            if (string.IsNullOrWhiteSpace(huesped.NumeroDocumento))
                throw new Exception("El número de documento es obligatorio.");

            // Si es cédula, debe tener 11 dígitos
            if (huesped.TipoDocumento == "Cedula")
            {
                if (huesped.NumeroDocumento.Length != 11 ||
                    !huesped.NumeroDocumento.All(char.IsDigit))
                {
                    throw new Exception("La cédula debe contener exactamente 11 dígitos.");
                }
            }

            // Validar email
            if (!string.IsNullOrWhiteSpace(huesped.Email))
            {
                if (!huesped.Email.Contains("@"))
                    throw new Exception("El correo electrónico no es válido.");
            }

            var existente = Buscar(huesped.NumeroDocumento);
            if (existente == null)
                _repositorio.Insertar(huesped);
            else
                _repositorio.Actualizar(huesped);
        }

        public void Eliminar(string numeroDocumento)
        {
            _repositorio.Eliminar(numeroDocumento);
        }
    }
}