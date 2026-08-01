using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelZormat.Modelo
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
        public string Rol { get; set; }          // "Administrador" o "Recepcionista"
        public string NombreCompleto { get; set; }

        public bool EsAdministrador()
        {
            return Rol == "Administrador";
        }
    }
}
