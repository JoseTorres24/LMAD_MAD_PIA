using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using INICIO_Forms.ADMINISTRATIVO;

namespace INICIO_Forms
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            // Luego se implementara Base de datos para esto <-
            string AdminCorreo = "";
            string AdminContraseña = "";

            //Guardar
            string correo = textCorreo.Text;
            string contraseña = textContraseña.Text;

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

            if (contraseña == AdminContraseña && correo == AdminCorreo)
            {
                HomeAdministrador homeAdmin = new HomeAdministrador();
                homeAdmin.Show();
                this.Hide();
            }
            else
            {
                HomeOperativo home = new HomeOperativo();
                home.Show();
                this.Hide();
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

        private void CrearCuentaAdmin_btn_Click(object sender, EventArgs e)
        {
            Create CrearAdmin = new Create();
            CrearAdmin.Show();
            this.Hide();
            CrearCuentaAdmin_btn.Hide();

        }
    }
}
