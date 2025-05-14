using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesData
{
    public class Habitaciones
    {
        public int ID_Habitacion { get; set; }
        public int ID_Hotel { get; set; }
        public int NumeroHabitacion { get; set; }
        public int PisoHabitacion { get; set; } // NivelPiso en la BD
        public string TipoHabitacion { get; set; } // Sencilla/Lujo/Suite
        public int Capacidad { get; set; }
        public int NumeroCamas { get; set; }
        public string VistaHabitacion { get; set; } // Frente al mar/No frente al mar
        public string Estado { get; set; }
    }

}
