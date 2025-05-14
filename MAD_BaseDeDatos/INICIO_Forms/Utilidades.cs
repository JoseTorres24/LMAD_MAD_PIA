using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// Funciones a utilidad
namespace INICIO_Forms
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


       public static bool ValidarRFC(string rfc)
        {
        // Verifica que el RFC no sea nulo ni vacío.
        if (string.IsNullOrWhiteSpace(rfc))
            return false;
        // Quita espacios y pasa todo a mayúsculas
        rfc = rfc.Trim().ToUpper();
        // El RFC debe tener 12 o 13 caracteres dependiendo del tipo.
        if (!(rfc.Length == 12 || rfc.Length == 13))
            return false;
        // Esta expresión regular valida el siguiente formato:
        // - Primer grupo: 3 o 4 letras (se permiten Ñ y &)
        // - Segundo grupo: 6 dígitos (dos para año, dos para mes y dos para día)
        // - Tercer grupo: 3 caracteres alfanuméricos
        string pattern = @"^([A-ZÑ&]{3,4})(\d{2})(0[1-9]|1[0-2])(0[1-9]|[12]\d|3[0-1])([A-Z\d]{3})$";

        return Regex.IsMatch(rfc, pattern);
       }

        public static bool ValidarEdadMinima(DateTime fechaNacimiento)
        {
            // Calcula la edad aproximada.
            int edad = DateTime.Now.Year - fechaNacimiento.Year;

            // Ajusta la edad si la fecha de nacimiento aún no ha ocurrido en el año actual.
            if (fechaNacimiento > DateTime.Now.AddYears(-edad))
            {
                edad--;
            }

            // Retorna true si la persona tiene al menos 21 años.
            return edad >= 21;
        }
        public static string GenerarCodigoReservacion()
        {
            // Genera un nuevo GUID y lo formatea sin guiones.
            return Guid.NewGuid().ToString("N");
        }
    }
}
