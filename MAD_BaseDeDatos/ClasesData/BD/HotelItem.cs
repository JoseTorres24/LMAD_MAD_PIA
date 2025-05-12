using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesData.BD
{
    public class HotelItem
    {
        public int ID_Hotel { get; set; }
        public string NombreHotel { get; set; }

        public override string ToString()
        {
            return NombreHotel;
        }
    }
}
