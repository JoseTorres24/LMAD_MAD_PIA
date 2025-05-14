using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesData
{
    public class Cliente
    {
        public string RFC { get; set; }
        public string NombreCompleto { get; set; }
        public string CorreoElectronico { get; set; }
        public long TelefonoCasa { get; set; }  //  Cambiado a long
        public long TelefonoCelular { get; set; }  //  Cambiado a long
        public string EstadoCivil { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        



        // Esta propiedad es la que se mostrará en el ListBox.
        public string DisplayInfo
        {
            get { return $"{RFC} - {NombreCompleto}"; }
        }


    }

}
