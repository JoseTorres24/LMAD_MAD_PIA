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
using static INICIO_Forms.Utilidades;

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

        private void CrearCuentaAdmin_btn_Click(object sender, EventArgs e)
        {
            Create CrearAdmin = new Create(this);
            CrearAdmin.Show();
            this.Hide();
            

        }

        public void OcultarBotonCrearCuenta()
        {
            CrearCuentaAdmin_btn.Visible = false;
        }
    }
}
