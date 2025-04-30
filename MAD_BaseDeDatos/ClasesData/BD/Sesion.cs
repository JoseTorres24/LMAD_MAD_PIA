using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClasesData.BD
{
    public static class Sesion
    {
        public static int ID_Usuario { get; set; }
        public static void CerrarSesion()
        {
            ID_Usuario = 0;
            MessageBox.Show("Cerrando la sesion...");

        }
    }
}
