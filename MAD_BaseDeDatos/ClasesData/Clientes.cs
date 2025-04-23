using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesData
{
    public class Cliente
    {
        public long RFC { get; set; }
        public string NombreCompleto { get; set; }
        public string CorreoElectronico { get; set; }
        public int TelefonoCasa { get; set; }
        public int TelefonoCelular { get; set; }
        public string EstadoCivil { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }

    }

}
