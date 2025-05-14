using ClasesData;
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
            CargarCiudades();
        }
        private void CHEKIN_FormClosed(object sender, EventArgs e)
        {
            homeOperativo.Show();
        }

        private void comboCiudadesCheck_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Limpiar controles dependientes
                listHotelesCheck.DataSource = null;
                listReservaciones.DataSource = null;

                // Validar selección
                if (comboCiudadesCheck.SelectedIndex < 0) return;

                string ciudadSeleccionada = comboCiudadesCheck.SelectedItem.ToString();

                // Mostrar carga mientras se consulta
                Cursor.Current = Cursors.WaitCursor;
                listHotelesCheck.DataSource = null;
                listHotelesCheck.Items.Clear();
                listHotelesCheck.Items.Add("Cargando hoteles...");

                Task.Run(() =>
                {
                    List<Hoteles> hoteles = BD_Reservacion.ObtenerHotelesPorCiudad(ciudadSeleccionada);

                    this.Invoke((MethodInvoker)delegate
                    {
                        Cursor.Current = Cursors.Default;

                        if (hoteles == null || hoteles.Count == 0)
                        {
                            listHotelesCheck.DataSource = null;
                            listHotelesCheck.Items.Clear();
                            listHotelesCheck.Items.Add("No se encontraron hoteles en " + ciudadSeleccionada);
                            return;
                        }

                        listHotelesCheck.DataSource = hoteles;
                        listHotelesCheck.DisplayMember = "NombreHotel";
                        listHotelesCheck.ValueMember = "ID_Hotel";

                        // Seleccionar el primer hotel por defecto
                        if (hoteles.Count > 0)
                        {
                            listHotelesCheck.SelectedIndex = 0;
                        }
                    });
                });
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show($"Error al cargar hoteles: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listHotelesCheck_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listHotelesCheck.SelectedItem is Hoteles hotel)
            {
                listReservaciones.DataSource = BD_Reservacion.ObtenerReservacionesPorHotel(hotel.ID_Hotel);
                listReservaciones.DisplayMember = "CodigoReservacion"; // O usa otro campo para mostrar
            }
        }


        private void MostrarCodigo_Click(object sender, EventArgs e)
        {
            if (listReservaciones.SelectedItem is Reservacion reservacionSeleccionada)
            {
                MessageBox.Show("Código de reservación: " + reservacionSeleccionada.CodigoReservacion);

                // Insertar en tabla CheckIn
                var checkIn = new CheckIn
                {
                    ID_Reservacion = reservacionSeleccionada.CodigoReservacion,
                    UsuarioRegistro = Sesion.ID_Usuario,
                    FechaCheckIn = DateTime.Now,
                    EstadoEntrada = "Marcado", // O cualquier otra lógica
                                               // Clave puede ser un código generado, opcionalmente
                                               // Clave = GenerarClaveAleatoria()
                };

                bool exito = BD_Check.InsertarCheckIn(checkIn);
                if (exito)
                    MessageBox.Show("Check-In registrado correctamente");
                else
                    MessageBox.Show("Error al registrar Check-In");
            }
            else
            {
                MessageBox.Show("Selecciona una reservación primero.");
            }
        }

        private void CargarCiudades()
        {
            try
            {
                comboCiudadesCheck.DataSource = BD_Reservacion.ObtenerCiudades();
                comboCiudadesCheck.SelectedIndex = 0; // Sin selección inicial
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar ciudades: " + ex.Message);
            }
        }


    }
}
