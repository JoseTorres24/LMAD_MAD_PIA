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
            comboCiudadesCheck.DataSource = BD_Reservacion.ObtenerCiudades();
            CargarCiudades();
            


        }
        private void CHEKIN_FormClosed(object sender, EventArgs e)
        {
            homeOperativo.Show();
        }

        private async void comboCiudadesCheck_SelectedIndexChanged(object sender, EventArgs e)
        {
 //NO JALO DEJALO ASI
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
                     // Se marca el check-in
                                             // Clave se genera automáticamente, p.ej. Guid.NewGuid().ToString()
                    Clave = reservacionSeleccionada.CodigoReservacion
                };

                bool exito = BD_Check.InsertarCheckIn(checkIn);
                if (exito)
                {
                    MessageBox.Show("Check-In registrado correctamente");
                    // Actualizar la lista: se recargan sólo las reservaciones que aún no tienen checkin marcado.
                    
                    listReservaciones.DisplayMember = "CodigoReservacion";
                }
                else
                {
                    MessageBox.Show("Error al registrar Check-In");
                }
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
        private async void iconButton1_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que haya una ciudad seleccionada
                if (comboCiudadesCheck.SelectedIndex < 0)
                {
                    MessageBox.Show("Selecciona una ciudad primero.", "Atención",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string ciudadSeleccionada = comboCiudadesCheck.SelectedItem.ToString();

                // Limpiar y deshabilitar el ListBox mientras carga
                listHotelesCheck.DataSource = null;
                listHotelesCheck.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;

                // Mostrar carga
                listHotelesCheck.Items.Clear();
                listHotelesCheck.Items.Add("Cargando hoteles...");

                // Obtener hoteles en segundo plano
                List<Hoteles> hoteles = await Task.Run(() =>
                    BD_Reservacion.ObtenerHotelesPorCiudad(ciudadSeleccionada));

                // Actualizar interfaz en el hilo principal
                listHotelesCheck.Items.Clear();
                if (hoteles == null || hoteles.Count == 0)
                {
                    listHotelesCheck.Items.Add("No se encontraron hoteles en " + ciudadSeleccionada);
                }
                else
                {
                    listHotelesCheck.DataSource = hoteles;
                    listHotelesCheck.DisplayMember = "NombreHotel";
                    listHotelesCheck.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar hoteles: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                listHotelesCheck.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
        }
        private async void iconButton2_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar selección de hotel
                if (!(listHotelesCheck.SelectedItem is Hoteles hotel))
                {
                    MessageBox.Show("Selecciona un hotel primero.", "Atención",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Configurar estado de carga
                listReservaciones.DataSource = null;
                listReservaciones.Items.Clear();
                listReservaciones.Items.Add("Cargando reservaciones...");
                listReservaciones.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;

                // Obtener reservaciones de forma asíncrona
                List<Reservacion> reservaciones = await Task.Run(() =>
                    BD_Reservacion.ObtenerReservacionesPorHotel(hotel.ID_Hotel));

                // Actualizar UI en el hilo principal
                this.Invoke((MethodInvoker)delegate
                {
                    listReservaciones.Items.Clear();

                    if (reservaciones == null || reservaciones.Count == 0)
                    {
                        listReservaciones.Items.Add("No se encontraron reservaciones para este hotel.");
                    }
                    else
                    {
                        // Configurar correctamente el DataSource
                        listReservaciones.DisplayMember = "DisplayInfo"; // Propiedad que quieres mostrar
                        listReservaciones.ValueMember = "CodigoReservacion"; // Valor asociado
                        listReservaciones.DataSource = reservaciones;
                    }
                });
            }
            catch (InvalidCastException icex)
            {
                MessageBox.Show($"Error de tipo al mostrar reservaciones: {icex.Message}\n\n" +
                              $"Asegúrate que la propiedad 'DisplayInfo' existe en la clase Reservacion",
                              "Error de Configuración",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar reservaciones: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                listReservaciones.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
        }

    }
}
