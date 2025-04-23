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
        public int NivelPiso { get; set; }
        public string TipoHabitacion { get; set; }
        public int Capacidad { get; set; }
        public int NumeroCamas { get; set; }
        public string VistaHabitacion { get; set; }
        public string Estado { get; set; }

    }

}
