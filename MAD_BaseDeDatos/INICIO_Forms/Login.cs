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
using ClasesData.BD;

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
            //Ya cargamos a ver como funciona con la base de datos
            BD_Administrador bd = new BD_Administrador();
            if (bd.ExisteAdministrador())
            {
                CrearCuentaAdmin_btn.Visible = false;
            }

        }

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {

        }
        // Boton Incio Session
        private void iconButton1_Click(object sender, EventArgs e)
        {


        }

        private void CrearCuentaAdmin_btn_Click(object sender, EventArgs e)
        {
            Create CrearAdmin = new Create();
            CrearAdmin.Show();
            this.Hide();
            

        }

        private void Salir_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
