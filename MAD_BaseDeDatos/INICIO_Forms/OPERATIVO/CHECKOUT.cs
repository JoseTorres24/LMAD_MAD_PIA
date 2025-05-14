using ClasesData;
using ClasesData.BD;
using ClosedXML.Excel;
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
            CargarCiudades();
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
        //Boton de factura 
        private void iconButton1_Click(object sender, EventArgs e)
        {
            if (listClientes.SelectedItem is Reservacion reservacion)
            {
                var workbook = new XLWorkbook();
                var ws = workbook.Worksheets.Add("Factura");

                // ENCABEZADO
                ws.Cell("A1").Value = "Hotel TuOtaku, S.A. de C.V.";
                ws.Cell("A2").Value = "RFC: TUO123456789";
                ws.Cell("A3").Value = "Factura de Check-Out";
                ws.Range("A1:D1").Merge().Style
                    .Font.SetBold().Font.FontSize = 16;
                ws.Range("A1:D3").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                int row = 5;

                // DATOS DE FACTURA
                ws.Cell(row, 1).Value = "Folio:";
                ws.Cell(row++, 2).Value = $"FACT-{reservacion.CodigoReservacion}";

                ws.Cell(row, 1).Value = "Fecha:";
                ws.Cell(row++, 2).Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                ws.Cell(row, 1).Value = "RFC Cliente:";
                ws.Cell(row++, 2).Value = string.IsNullOrEmpty(reservacion.RFC_Cliente) ? "No registrado" : reservacion.RFC_Cliente;

                ws.Cell(row, 1).Value = "Total:";
                ws.Cell(row++, 2).Value = reservacion.Total.ToString("C");

                ws.Cell(row, 1).Value = "Anticipo (Descuento):";
                ws.Cell(row++, 2).Value = reservacion.Anticipo.ToString("C");

                ws.Cell(row, 1).Value = "Total a Pagar:";
                ws.Cell(row++, 2).Value = (reservacion.Total - reservacion.Anticipo).ToString("C");

                row += 2;

                // SERVICIOS
                ws.Cell(row++, 1).Value = "Servicios Utilizados:";
                ws.Cell(row, 1).Value = "ID Servicio";
                ws.Cell(row, 2).Value = "Nombre Servicio";
                ws.Range(row, 1, row, 2).Style.Font.SetBold();
                row++;

                if (reservacion.Servicios != null && reservacion.Servicios.Count > 0)
                {
                    foreach (var servicio in reservacion.Servicios)
                    {
                        ws.Cell(row, 1).Value = servicio.ID_Servicio;
                        ws.Cell(row, 2).Value = BD_Servicios.ObtenerNombreServicio(servicio.ID_Servicio);
                        row++;
                    }
                }
                else
                {
                    ws.Cell(row++, 1).Value = "No se registraron servicios.";
                }

                // DISEÑO Y BORDES
                ws.Range("A5:B" + (row - 1)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ws.Range("A5:B" + (row - 1)).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                ws.Columns().AdjustToContents();

                // GUARDAR
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Archivo Excel (*.xlsx)|*.xlsx";
                saveFileDialog.FileName = $"Factura_{reservacion.CodigoReservacion}.xlsx";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    workbook.SaveAs(saveFileDialog.FileName);
                    MessageBox.Show("Factura generada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Selecciona una reservación para generar la factura.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private async void iconButton2_Click(object sender, EventArgs e)
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
        //Generar clientes que ya hayan sido "Marcado"
        private void iconButton3_Click(object sender, EventArgs e)
        {
            if (listHotelesCheck.SelectedItem is Hoteles hotel)
            {
                List<Reservacion> reservacionesMarcadas = BD_Check.ObtenerReservacionesMarcadas(hotel.ID_Hotel);

                if (reservacionesMarcadas == null || reservacionesMarcadas.Count == 0)
                {
                    MessageBox.Show("No hay reservaciones con Check-In marcado para este hotel.");
                    return;
                }
                //Aqui se 
                listClientes.DataSource = reservacionesMarcadas;
                listClientes.DisplayMember = "CodigoReservacion";
            }
            else
            {
                MessageBox.Show("Selecciona un hotel.");
            }
        } // y Mostrarme los clientes para poder hacer un insert en la tabla checkOut y Factura de acuerdo a los datos
    }
}
