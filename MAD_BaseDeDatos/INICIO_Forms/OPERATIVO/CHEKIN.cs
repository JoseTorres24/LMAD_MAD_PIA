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
    public partial class CHEKIN : Form
    {
        private HomeOperativo homeOperativo;
        public CHEKIN(HomeOperativo homeOperativo)
        {
            InitializeComponent();
            this.homeOperativo = homeOperativo;
            this.FormClosed += new FormClosedEventHandler(CHEKIN_FormClosed);
        }

        private void CHEKIN_Load(object sender, EventArgs e)
        {

        }
        private void CHEKIN_FormClosed(object sender, EventArgs e)
        {
            homeOperativo.Show();
        }
    }
}
