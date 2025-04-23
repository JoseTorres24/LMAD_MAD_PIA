using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesData
{
    public class CheckIn
    {
        public int ID_CheckIn { get; set; }
        public long ID_Reservacion { get; set; }
        public int UsuarioRegistro { get; set; }
        public DateTime FechaCheckIn { get; set; }
        public string Clave { get; set; }

    }

}
