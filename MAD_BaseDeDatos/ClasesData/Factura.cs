using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesData
{
    public class Factura
    {
        public int ID_Factura { get; set; }
        public int ID_CheckOut { get; set; }
        public float Descuento { get; set; }
        public string ServiciosUtilizados { get; set; }
        public float TotalPagar { get; set; }

    }

}
