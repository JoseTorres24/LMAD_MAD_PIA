using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesData
{
    public class ListItem
    {
        public string Nombre { get; set; }
        public int ID { get; set; }

        public ListItem(string nombre, int id)
        {
            Nombre = nombre;
            ID = id;
        }

        public override string ToString() => Nombre;
    }
}
