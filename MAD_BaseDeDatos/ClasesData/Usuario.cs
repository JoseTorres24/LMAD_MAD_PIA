using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesData
{
    public class Usuario
    {
        public int ID_Usuario { get; set; }
        public string CorreoElectronico { get; set; }
        public string NombreCompleto { get; set; }
        public long NumeroNomina { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public long TelefonoCasa { get; set; }
        public long TelefonoCelular { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Tipo { get; set; }
        public int? ID_UsuarioRegistro { get; set; } // Nullable por si el usuario fue creado por sistema directamente
    }
    
}
