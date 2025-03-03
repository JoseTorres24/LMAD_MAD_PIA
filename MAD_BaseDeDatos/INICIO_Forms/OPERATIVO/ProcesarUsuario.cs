using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace INICIO_Forms.OPERATIVO
{
    public partial class ProcesarUsuario: Form
    {
        private HomeOperativo home;
        public ProcesarUsuario(HomeOperativo home)
        {
            InitializeComponent();
            this.home = home;
            this.FormClosed += new FormClosedEventHandler(ProcesarUsuario_FormClosed);
        }
        private void ProcesarUsuario_FormClosed(object sender, EventArgs e)
        {
            home.Show();
        }

        private void ProcesarUsuario_Load(object sender, EventArgs e)
        {

        }
    }
}
