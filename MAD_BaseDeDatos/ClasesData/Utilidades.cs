using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClasesData
{
    public static class Utilidades
    {
        public static bool ValidarContraseña(string contraseña)
        {
            string patron = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9]).{8,}$";
            return Regex.IsMatch(contraseña, patron);
        }


        public static bool ValidarCorreo(string correo)
        {
            string patron = @"^[a-zA-Z0-9._%+-]+@(outlook\.com|gmail\.com|hotmail\.com)$";
            return Regex.IsMatch(correo, patron);
        }
        public static bool ValidarNombre(string nombre)
        {
            return Regex.IsMatch(nombre, @"^[a-zA-Z\s]+$");
        }

        public static bool ValidarNumeroNomina(string numeroNomina)
        {
            return Regex.IsMatch(numeroNomina, @"^\d{11}$"); // 11 dígitos numéricos
        }

        public static bool ValidarTelefono(string telefono)
        {
            return Regex.IsMatch(telefono, @"^\d{10}$"); // Mínimo 10 dígitos
        }
    }
}
