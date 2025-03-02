using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace INICIO_Forms
{
    public partial class Create : Form
    {
        public Create()
        {
            InitializeComponent();
        }

        private void Home_Load(object sender, EventArgs e)
        {

        }

        private void iconButton1_Click(object sender, EventArgs e)

        {
            string correo = textCorreoAdmin.Text;
            string contraseña = textContraAdmin.Text;
            string nombre = textNombreAdmin.Text;
            string numeroNomina = textNomina.Text;
            DateTime fechaNacimiento = FechaNacimiento.Value;
            string telefonoCasa = textPhoneCasaAdmin.Text;
            string telefonoCelular = textPhoneAdmin.Text;
            

            if (!ValidarCorreo(correo))
            {
                MessageBox.Show("El correo debe ser @outlook.com, @gmail.com o @hotmail.com", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidarContraseña(contraseña))
            {
                MessageBox.Show("La contraseña debe tener al menos 8 caracteres, una mayúscula, una minúscula y un carácter especial.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidarNombre(nombre))
            {
                MessageBox.Show("El nombre solo debe contener letras y espacios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidarNumeroNomina(numeroNomina))
            {
                MessageBox.Show("El número de nómina debe tener exactamente 11 dígitos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar que el usuario tenga al menos 21 años
            DateTime fecha = FechaNacimiento.Value;
            int edad = DateTime.Now.Year - fechaNacimiento.Year;

            // Ajustar si aún no ha cumplido años este año
            if (fecha > DateTime.Now.AddYears(-edad))
            {
                edad--;
            }

            if (edad < 21)
            {
                MessageBox.Show("Debes tener al menos 21 años.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (!ValidarTelefono(telefonoCasa) || !ValidarTelefono(telefonoCelular))
            {
                MessageBox.Show("Los teléfonos deben contener solo números y tener al menos 10 dígitos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

        }


        private bool ValidarContraseña(string contraseña)
        {
            string patron = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9]).{8,}$";
            return Regex.IsMatch(contraseña, patron);
        }


        private bool ValidarCorreo(string correo)
        {
            string patron = @"^[a-zA-Z0-9._%+-]+@(outlook\.com|gmail\.com|hotmail\.com)$";
            return Regex.IsMatch(correo, patron);
        }
        private bool ValidarNombre(string nombre)
        {
            return Regex.IsMatch(nombre, @"^[a-zA-Z\s]+$");
        }

        private bool ValidarNumeroNomina(string numeroNomina)
        {
            return Regex.IsMatch(numeroNomina, @"^\d{11}$"); // 11 dígitos numéricos
        }

        private bool ValidarTelefono(string telefono)
        {
            return Regex.IsMatch(telefono, @"^\d{10,}$"); // Mínimo 10 dígitos
        }
    }
}
