using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesData
{
    public class Hoteles
    {
        public int ID_Hotel { get; set; }
        public string NombreHotel { get; set; }
        public string Pais { get; set; }
        public string Ciudad { get; set; }
        public string Domicilio { get; set; }
        public int NumeroPisos { get; set; }
        public int NumeroHabitaciones { get; set; }
        public DateTime FechaInicioOperaciones { get; set; }
        public int UsuarioRegistro { get; set; }

    }

}
