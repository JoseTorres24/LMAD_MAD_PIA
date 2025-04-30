using ClasesData.BD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace INICIO_Forms.ADMINISTRATIVO
{
    public partial class HomeAdministrador : Form
    {
        public HomeAdministrador()
        {
            InitializeComponent();
        }

        private void HomeAdministrador_Load(object sender, EventArgs e)
        {

        }

        private void RegistroUsuario_Click(object sender, EventArgs e)
        {

        }

        private void ReporteHotel_Click(object sender, EventArgs e)
        {

        }

        private void ReporteClientes_Click(object sender, EventArgs e)
        {

        }
        // Y cuando salgamos tenemos que cerrar esa instancia
        private void Salir_Click(object sender, EventArgs e)
        {
            Sesion.CerrarSesion();
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void SalirMenu_Click(object sender, EventArgs e)
        {

        }

        private void Hoteles_Click(object sender, EventArgs e)
        {

        }

        private void ProcesoHotel_Click(object sender, EventArgs e)
        {
            Hoteles hoteles = new Hoteles();
            hoteles.Show();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Registro usuario = new Registro();
            usuario.Show();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            ReporteHotel reporte = new ReporteHotel();
            reporte.Show();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            ReporteCliente reporte = new ReporteCliente();
            reporte.Show();
        }

        private void VentasReporte_Click(object sender, EventArgs e)
        {
            ReporteVentas ventas = new ReporteVentas();
            ventas.Show();
        }
    }
}
