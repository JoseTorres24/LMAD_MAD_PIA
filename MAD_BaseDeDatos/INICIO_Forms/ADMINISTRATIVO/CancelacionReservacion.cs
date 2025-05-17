using ClasesData.BD;
using ClasesData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace INICIO_Forms.ADMINISTRATIVO
{
    public partial class CancelacionReservacion : Form
    {
        public CancelacionReservacion()
        {
            InitializeComponent();
        }

        private void CancelacionReservacion_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = BD_Reservacion.ObtenerCiudades();
            CargarCiudades();
            ActualizarListaReservaciones();
        }

        private void CargarCiudades()
        {
            try
            {
                comboBox1.DataSource = BD_Reservacion.ObtenerCiudades();
                comboBox1.SelectedIndex = 0; // Sin selección inicial
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar ciudades: " + ex.Message);
            }
        }

        private async void iconButton3_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que haya una ciudad seleccionada
                if (comboBox1.SelectedIndex < 0)
                {
                    MessageBox.Show("Selecciona una ciudad primero.", "Atención",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string ciudadSeleccionada = comboBox1.SelectedItem?.ToString();

                // Limpiar y deshabilitar el ListBox mientras carga
                listHotelesCheck.DataSource = null;
                listHotelesCheck.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;

                // Mostrar carga
                listHotelesCheck.Items.Clear();
                listHotelesCheck.Items.Add("Cargando hoteles...");

                // Obtener hoteles en segundo plano
                List<ClasesData.Hoteles> hoteles = await Task.Run(() => BD_Reservacion.ObtenerHotelesPorCiudad(ciudadSeleccionada));

                // Actualizar interfaz en el hilo principal
                listHotelesCheck.Items.Clear();
                if (hoteles == null || hoteles.Count == 0)
                {
                    listHotelesCheck.Items.Add($"No se encontraron hoteles en {ciudadSeleccionada}");
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
                MessageBox.Show($"Error al cargar hoteles: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                listHotelesCheck.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
        }
        //Generacion de reservaciones 

        private async void iconButton2_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar selección de hotel
                if (!(listHotelesCheck.SelectedItem is ClasesData.Hoteles hotel))
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
                List<Reservacion> reservaciones = await Task.Run(() => BD_Reservacion.ObtenerReservacionesParaCheckIn(hotel.ID_Hotel));

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
                        reservaciones = BD_Reservacion.ObtenerReservacionesActivas();
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
        //Eliminar Reservacion 
        private void MostrarCodigo_Click(object sender, EventArgs e)
        {
            if (!(listReservaciones.SelectedItem is Reservacion reservacionSeleccionada))
            {
                MessageBox.Show("Selecciona una reservación primero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirmación antes de eliminar
            DialogResult confirmacion = MessageBox.Show($"¿Seguro que deseas eliminar la reservación {reservacionSeleccionada.CodigoReservacion}?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmacion == DialogResult.Yes)
            {
                bool eliminado = EliminarReservacion(reservacionSeleccionada.CodigoReservacion);

                if (eliminado)
                {
                    MessageBox.Show("Reservación eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ActualizarListaReservaciones();  // Actualiza la lista después de eliminar
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar la reservación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }

        private void ActualizarListaReservaciones()
        {
            if (!(listHotelesCheck.SelectedItem is ClasesData.Hoteles hotel))
            {
                MessageBox.Show("Selecciona un hotel primero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<Reservacion> reservaciones = BD_Reservacion.ObtenerReservacionesPorHotel(hotel.ID_Hotel)
                .Where(r => r.Estatus != "Eliminada")  // Filtra solo las "No Eliminadas"
                .ToList();

            listReservaciones.DataSource = reservaciones;
            listReservaciones.DisplayMember = "DisplayInfo";
            listReservaciones.ValueMember = "CodigoReservacion";
        }

        public bool EliminarReservacion(string codigoReservacion)
        {
          
            string query = "UPDATE Reservacion SET Estatus = 'Eliminada' WHERE CodigoReservacion = @CodigoReservacion";

            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CodigoReservacion", codigoReservacion);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }



    }
}

