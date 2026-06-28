using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace HotelZormat.Datod
{
    public static class ConfiguracionDBS
    {
        public static string ObtenerConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["HotelZormaDB"].ConnectionString;
        }
    }
}

