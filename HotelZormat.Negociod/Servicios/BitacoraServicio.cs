using HotelZormat.Datod;
using HotelZormat.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelZormat.Negociod.Servicios
{
    public class BitacoraServicio
    {
        private BitacoraRepository _repositorio = new BitacoraRepository();

        public void Registrar(int idUsuario, string accion)
        {
            _repositorio.Registrar(idUsuario, accion);
        }

        public List<RegistroBitacora> ObtenerTodos()
        {
            return _repositorio.ObtenerTodos();
        }
    }
}
