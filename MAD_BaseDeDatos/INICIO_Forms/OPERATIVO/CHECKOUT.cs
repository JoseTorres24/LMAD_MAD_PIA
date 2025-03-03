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
    public partial class CHECKOUT : Form
    {
        private HomeOperativo home;
        public CHECKOUT(HomeOperativo home)
        {
            InitializeComponent();
            this.home = home;
            this.FormClosed += new FormClosedEventHandler(CHECKOUT_FormClosed);
        }
        private void CHECKOUT_FormClosed(object sender, EventArgs e)
        {
            home.Show();
        }

        private void CHECKOUT_Load(object sender, EventArgs e)
        {

        }
    }
}
