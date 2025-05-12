using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesData.BD
{
    public class ServicioItem
    {
        public int ID_Servicio { get; set; }
        public string NombreServicio { get; set; }
        public double Costo { get; set; }

        public override string ToString()
        {
            return $"{ID_Servicio} - {NombreServicio} ($ {Costo})";
        }


    }
}
