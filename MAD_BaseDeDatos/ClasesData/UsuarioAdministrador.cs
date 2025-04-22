using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesData
{
    public class UsuarioAdministrador
    {
        public string CorreoElectronico { get; set; }
        public string NombreCompleto { get; set; }
        public long NumeroNomina { get; set; } // Cambiar a string si es necesario.
        public string FechaNacimiento { get; set; }
        public long TelefonoCasa { get; set; } // Cambiar a string.
        public long TelefonoCelular { get; set; } // Cambiar a string.
        public string FechaRegistro { get; set; }
    }

}
