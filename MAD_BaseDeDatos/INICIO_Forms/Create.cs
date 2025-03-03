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
using static INICIO_Forms.Utilidades;


namespace INICIO_Forms
{
    public partial class Create : Form
    {   // Representacion necesaria solo para evitar que se encargue para representar
        // El hecho de que hay un usuario administrador
        //Todo esto se tendra que implementar con Base de datos?
        private Login login;

        public Create(Login login)
        {
            InitializeComponent();
            this.login = login;
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
            // Ocultar el botón en el formulario Login
            if (login != null)
            {
                login.OcultarBotonCrearCuenta();
            }

            // Volver a mostrar el formulario Login
            login.Show();
            this.Close(); // Cierra el formulario de crear cuenta

        }




    }
}
