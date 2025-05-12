using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesData.BD
{
    public class HabitacionItem
    {
        public int ID_Habitacion { get; set; }
        public int ID_Hotel { get; set; }
        public int NumeroHabitacion { get; set; }
        public string NivelHabitacion { get; set; }
        public string Estado { get; set; }

        public override string ToString()
        {
            return $"Hab. {NumeroHabitacion} - {NivelHabitacion} ({Estado})";
        }

    }
}
