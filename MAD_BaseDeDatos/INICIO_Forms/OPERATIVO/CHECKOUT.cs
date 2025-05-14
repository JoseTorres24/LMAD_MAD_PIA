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
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.Data.SqlClient;

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

        public void ObtenerDatosFactura(string codigoReservacion)
        {

            string query = @"SELECT 
                        r.CodigoReservacion, r.FechaInicio, r.FechaFin, r.Anticipo, r.Total, r.ID_Habitacion, 
                        c.RFC, c.NombreCompleto, c.Ciudad, c.Estado, c.Pais,
                        s.ID_Servicio, bd.NombreServicio
                    FROM Reservacion r
                    INNER JOIN Cliente c ON r.RFC_Cliente = c.RFC
                    LEFT JOIN ServiciosReservacion s ON s.CodigoReservacion = r.CodigoReservacion
                    LEFT JOIN BD_Servicios bd ON bd.ID_Servicio = s.ID_Servicio
                    WHERE r.CodigoReservacion = @CodigoReservacion";

            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CodigoReservacion", codigoReservacion);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Reservación: {reader["CodigoReservacion"]}");
                            Console.WriteLine($"Cliente: {reader["NombreCompleto"]} ({reader["RFC"]})");
                            Console.WriteLine($"Ubicación: {reader["Ciudad"]}, {reader["Estado"]}, {reader["Pais"]}");
                            Console.WriteLine($"Total: {reader["Total"]}");
                            Console.WriteLine($"Anticipo: {reader["Anticipo"]}");
                            Console.WriteLine($"Habitación: {reader["ID_Habitacion"]}");

                            if (!reader.IsDBNull(reader.GetOrdinal("NombreServicio")))
                                Console.WriteLine($"Servicio utilizado: {reader["NombreServicio"]}");
                        }
                    }
                }
            }
        }



        //Boton de factura 
        private void iconButton1_Click(object sender, EventArgs e)
        {
            if (listClientes.SelectedItem is Reservacion reservacion)
            {

                Cliente referenciado = ObtenerClientePorReservacion(reservacion.RFC_Cliente);



                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Archivo Excel (*.xlsx)|*.xlsx|Archivo PDF (*.pdf)|*.pdf";
                saveFileDialog.FileName = $"Factura_{reservacion.CodigoReservacion}";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedExtension = System.IO.Path.GetExtension(saveFileDialog.FileName).ToLower();

                    if (selectedExtension == ".xlsx")
                    {
                        // 🔹 GENERAR EXCEL
                        var workbook = new XLWorkbook();
                        var ws = workbook.Worksheets.Add("Factura");

                        ws.Cell("A1").Value = "Hotel TuOtaku, S.A. de C.V.";
                        ws.Cell("A2").Value = "RFC: TUO123456789";
                        ws.Cell("A3").Value = "Factura de Check-Out";
                        ws.Range("A1:D1").Merge().Style.Font.SetBold().Font.FontSize = 16;
                        ws.Range("A1:D3").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                        int row = 5;
                        ws.Cell(row, 1).Value = "Folio:";
                        ws.Cell(row++, 2).Value = $"FACT-{reservacion.CodigoReservacion}";
                        ws.Cell(row, 1).Value = "Fecha:";
                        ws.Cell(row++, 2).Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                        // 🔹 DATOS DEL CLIENTE
                        ws.Cell(row++, 1).Value = "Datos del Cliente:";
                        ws.Cell(row, 1).Value = "Nombre Completo:";
                        ws.Cell(row++, 2).Value = referenciado.NombreCompleto ?? "No registrado";
                        ws.Cell(row, 1).Value = "RFC:";
                        ws.Cell(row++, 2).Value = referenciado.RFC ?? "No registrado";
                        ws.Cell(row, 1).Value = "Ubicación:";
                        ws.Cell(row++, 2).Value = $"{referenciado.Ciudad ?? "No registrado"}, {referenciado.Estado ?? "No registrado"}, {referenciado.Pais ?? "No registrado"}";
                        ws.Cell(row, 1).Value = "Estado Civil:";
                        ws.Cell(row++, 2).Value = referenciado.EstadoCivil ?? "No registrado";

                        ws.Columns().AdjustToContents();
                        workbook.SaveAs(saveFileDialog.FileName);
                        MessageBox.Show("Factura en Excel generada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (selectedExtension == ".pdf")
                    {
                        // 🔹 GENERAR PDF
                        using (var writer = new PdfWriter(saveFileDialog.FileName))
                        using (var pdf = new PdfDocument(writer))
                        using (var document = new Document(pdf))
                        {

                            document.Add(new Paragraph("Hotel TuOtaku, S.A. de C.V.")
                                .SetFontSize(16).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                            document.Add(new Paragraph("RFC: TUO123456789"));
                            document.Add(new Paragraph("Factura de Check-Out"));
                            document.Add(new Paragraph($"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm}"));
                            document.Add(new Paragraph($"Folio: FACT-{reservacion.CodigoReservacion}"));

                            document.Add(new Paragraph("\n🔹 **Datos del Cliente**"));
                            document.Add(new Paragraph($"Nombre: {referenciado.NombreCompleto ?? "No registrado"}"));
                            document.Add(new Paragraph($"RFC: {referenciado.RFC ?? "No registrado"}"));
                            document.Add(new Paragraph($"Ubicación: {referenciado.Ciudad ?? "No registrado"}, {referenciado.Estado ?? "No registrado"}, {referenciado.Pais ?? "No registrado"}"));
                            document.Add(new Paragraph($"Estado Civil: {referenciado.EstadoCivil ?? "No registrado"}"));

                            document.Add(new Paragraph("\n🔹 **Servicios Utilizados**"));
                            if (reservacion.Servicios != null && reservacion.Servicios.Count > 0)
                            {
                                foreach (var servicio in reservacion.Servicios)
                                {
                                    document.Add(new Paragraph($"- {BD_Servicios.ObtenerNombreServicio(servicio.ID_Servicio)}"));
                                }
                            }
                            else
                            {
                                document.Add(new Paragraph("No se registraron servicios."));
                            }

                            MessageBox.Show("Factura en PDF generada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecciona una reservación para generar la factura.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        public Cliente ObtenerClientePorReservacion(string codigoReservacion)
        {
            //Aqui parece que hay problemas
            if (string.IsNullOrEmpty(codigoReservacion))
            {
                throw new ArgumentException("El código de reservación no puede estar vacío");
            }

            string query = @"SELECT c.RFC, c.NombreCompleto, c.Ciudad, c.Estado, c.Pais, c.EstadoCivil
            FROM Reservacion r
            INNER JOIN Cliente c ON r.RFC_Cliente = c.RFC
            WHERE r.CodigoReservacion = @CodigoReservacion";

            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CodigoReservacion", codigoReservacion);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Cliente
                            {
                                RFC = reader["RFC"].ToString(),
                                NombreCompleto = reader["NombreCompleto"].ToString(),
                                Ciudad = reader["Ciudad"].ToString(),
                                Estado = reader["Estado"].ToString(),
                                Pais = reader["Pais"].ToString(),
                                EstadoCivil = reader["EstadoCivil"].ToString()
                            };
                        }
                    }
                }
            }
            return null; // Si no se encuentra el cliente
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
            if (!(listHotelesCheck.SelectedItem is Hoteles hotel))
            {
                MessageBox.Show("Selecciona un hotel.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener clientes de reservaciones activas para ese hotel
            List<Cliente> clientes = BD_Reservacion.ObtenerClientesPorHotel(hotel.ID_Hotel);

            if (clientes == null || clientes.Count == 0)
            {
                MessageBox.Show("No hay clientes con reservaciones activas en este hotel.");
                return;
            }

            listClientes.DataSource = clientes;
            listClientes.DisplayMember = "NombreCompleto"; // Se muestra el nombre del cliente
            listClientes.ValueMember = "RFC"; // Valor asociado será el RFC
        }
    }

}

