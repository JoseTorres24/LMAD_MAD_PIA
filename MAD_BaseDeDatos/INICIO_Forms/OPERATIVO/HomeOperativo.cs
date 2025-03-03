using INICIO_Forms.OPERATIVO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace INICIO_Forms
{
    public partial class HomeOperativo : Form
    {
        public HomeOperativo()
        {
            InitializeComponent();
        }

        private void HomeOperativo_Load(object sender, EventArgs e)
        {

        }


        private void iconMenuItem1_Click(object sender, EventArgs e)
        {
            Reservaciones reservacion = new Reservaciones(this);
            reservacion.Show();
            this.Hide();
        }

        private void iconMenuItem2_Click(object sender, EventArgs e)
        {
            CHEKIN entrada = new CHEKIN(this);
            entrada.Show();
            this.Hide();

        }

        private void iconMenuItem3_Click(object sender, EventArgs e)
        {
            CHECKOUT salida = new CHECKOUT(this);
            salida.Show();
            this.Hide();
        }

        private void iconMenuItem4_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
    }
}
