using HotelZormat.Datod;
using HotelZormat.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelZormat.Negociod.Servicios
{
    public class UsuarioService
    {
        private UsuarioRepository _repositorio = new UsuarioRepository();

        public Usuario IniciarSesion(string usuario, string clave)
        {
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(clave))
            {
                throw new System.Exception("Debe escribir usuario y contraseña.");
            }
            return _repositorio.ValidarCredenciales(usuario, clave);
        }
    }
}
