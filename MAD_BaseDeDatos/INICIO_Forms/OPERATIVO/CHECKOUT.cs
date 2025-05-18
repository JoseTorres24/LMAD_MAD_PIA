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
using System.Data.SqlClient;
using System.IO;
using OfficeOpenXml;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using ControlzEx;
using System.Diagnostics;
using System.Threading;




namespace INICIO_Forms.OPERATIVO
{
    
    public partial class CHECKOUT : Form
    {

        private HomeOperativo home;
        private static double sumaServicios;

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


        private void iconButton1_Click(object sender, EventArgs e)
        {
            if (listClientes.SelectedItem is Cliente clienteSeleccionado)
            {
                Reservacion reservacion = ObtenerReservacionPorCliente(clienteSeleccionado.RFC);

                if (reservacion == null)
                {
                    MessageBox.Show("Este cliente no tiene reservaciones activas.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Archivo Excel (*.xlsx)|*.xlsx|Archivo PDF (*.pdf)|*.pdf",
                    FileName = $"Factura_{reservacion.CodigoReservacion}"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedExtension = Path.GetExtension(saveFileDialog.FileName).ToLower();
                    double gastoTotal = reservacion.Servicios.Sum(s => ObtenerDetalleServicio(s.ID_Servicio)?.Costo ?? 0);

                    List<Servicios> serviciosSeleccionados = reservacion.Servicios
                        .Select(s => ObtenerDetalleServicio(s.ID_Servicio))
                        .Where(servicio => servicio != null)
                        .ToList();

                    // Verificación de los servicios seleccionados
                    if (serviciosSeleccionados.Count == 0)
                    {
                        Console.WriteLine("No hay servicios asociados a esta reservación.");
                    }
                    else
                    {
                        Console.WriteLine("Servicios asociados:");
                        foreach (var servicio in serviciosSeleccionados)
                        {
                            Console.WriteLine($"- {servicio.NombreServicio}: {servicio.Costo:C2}");
                        }
                    }

                    if (selectedExtension == ".xlsx")
                    {
                        GenerarFacturaExcel(clienteSeleccionado, reservacion, serviciosSeleccionados, gastoTotal, saveFileDialog.FileName);
                    }
                    else if (selectedExtension == ".pdf")
                    {
                        GenerarFacturaPDF(clienteSeleccionado, reservacion, serviciosSeleccionados, gastoTotal, saveFileDialog.FileName);
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecciona un cliente para generar la factura.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        public static Servicios ObtenerDetalleServicio(int idServicio)
        {
            string query = "SELECT NombreServicio, Costo FROM Servicios WHERE ID_Servicio = @IDServicio;";
            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IDServicio", idServicio);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Validar que los datos no sean nulos antes de asignarlos
                            return new Servicios
                            {
                                ID_Servicio = idServicio,
                                NombreServicio = reader["NombreServicio"].ToString(),
                                Costo = reader["Costo"] == DBNull.Value ? 0 : Convert.ToDouble(reader["Costo"])
                            };
                        }
                    }
                }
            }
            return null; // Retornar null si no se encuentra
        }

        public static List<Servicios> ObtenerServiciosPorReservacion(string idReservacion)
        {
            string query = @"
        SELECT S.ID_Servicio, S.NombreServicio, S.Costo
        FROM Servicios_Reservacion SR
        JOIN Servicios S ON SR.ID_Servicio = S.ID_Servicio
        WHERE SR.ID_Reservacion = @IDReservacion;";

            List<Servicios> servicios = new List<Servicios>();

            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IDReservacion", idReservacion);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            servicios.Add(new Servicios
                            {
                                ID_Servicio = Convert.ToInt32(reader["ID_Servicio"]),
                                NombreServicio = reader["NombreServicio"].ToString(),
                                Costo = reader["Costo"] == DBNull.Value ? 0 : Convert.ToDouble(reader["Costo"])
                            });
                        }
                    }
                }
            }

            return servicios;
        }



        // The issue is caused by an extra closing brace '}' in the method `GenerarFacturaExcel`.
        // The misplaced brace prematurely ends the method, causing a syntax error.
        // Below is the corrected version of the method.

        public static void GenerarFacturaExcel(Cliente cliente, Reservacion reservacion, List<Servicios> servicios, double gastoTotal, string filePath)
        {
            Console.WriteLine($"Generando factura en Excel para la reservación {reservacion.CodigoReservacion}");

            using (ExcelPackage excel = new ExcelPackage())
            {
                var ws = excel.Workbook.Worksheets.Add("Factura");

                // Encabezado
                ws.Cells["A1"].Value = "Hotel TuOtaku, S.A. de C.V.";
                ws.Cells["A2"].Value = "RFC: TUO123456789";
                ws.Cells["A3"].Value = "Factura de Check-Out";
                ws.Cells["A1:A3"].Style.Font.Bold = true;
                ws.Cells["A1:A3"].Style.Font.Size = 14;

                int row = 5;
                ws.Cells[row, 1].Value = "Folio:";
                ws.Cells[row++, 2].Value = $"FACT-{reservacion.CodigoReservacion}";
                ws.Cells[row, 1].Value = "Fecha:";
                ws.Cells[row++, 2].Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                // Datos del Cliente
                ws.Cells[row++, 1].Value = "Datos del Cliente:";
                ws.Cells[row, 1].Value = "Nombre Completo:";
                ws.Cells[row++, 2].Value = cliente.NombreCompleto ?? "No registrado";
                ws.Cells[row, 1].Value = "RFC:";
                ws.Cells[row++, 2].Value = cliente.RFC ?? "No registrado";
                ws.Cells[row, 1].Value = "Ubicación:";
                ws.Cells[row++, 2].Value = $"{cliente.Ciudad ?? "No registrado"}, {cliente.Estado ?? "No registrado"}, {cliente.Pais ?? "No registrado"}";

                // Servicios Seleccionados
                ws.Cells[row++, 1].Value = "Servicios Seleccionados:";

                if (servicios.Count > 0)
                {
                    foreach (var servicio in servicios)
                    {
                        ws.Cells[row, 1].Value = servicio.NombreServicio;
                        ws.Cells[row++, 2].Value = servicio.Costo.ToString("C2");
                        sumaServicios += servicio.Costo;
                    }
                }
                else
                {
                    ws.Cells[row++, 1].Value = "No se registraron servicios.";
                    sumaServicios = 0;
                }

                gastoTotal = reservacion.Total + sumaServicios - reservacion.Anticipo;
                // Gasto Total
                ws.Cells[row, 1].Value = "Gasto Total:";
                ws.Cells[row++, 2].Value = gastoTotal.ToString("C2");

                File.WriteAllBytes(filePath, excel.GetAsByteArray());
                Console.WriteLine("Factura en Excel generada exitosamente.");
            }
        }
        
        
        public static void GenerarFacturaPDF(Cliente cliente, Reservacion reservacion, List<Servicios> servicios, double gastoTotal, string filePath)
        {
            
            Console.WriteLine($"Generando factura en PDF para la reservación {reservacion.CodigoReservacion}");

            PdfDocument pdf = new PdfDocument();
            pdf.Info.Title = "Factura de Check-Out";
            PdfPage page = pdf.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Arial", 12);
            XFont fontBold = new XFont("Arial", 14);

            int yPos = 50;

            // Encabezado
            gfx.DrawString("Hotel TuOtaku, S.A. de C.V.", fontBold, XBrushes.Black, new XRect(20, yPos, page.Width, page.Height), XStringFormats.TopLeft);
            yPos += 20;
            gfx.DrawString("RFC: TUO123456789", font, XBrushes.Black, 20, yPos);
            yPos += 20;
            gfx.DrawString($"Factura de Check-Out", font, XBrushes.Black, 20, yPos);
            yPos += 30;
            gfx.DrawString($"Folio: FACT-{reservacion.CodigoReservacion}", font, XBrushes.Black, 20, yPos);
            yPos += 20;
            gfx.DrawString($"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm}", font, XBrushes.Black, 20, yPos);
            yPos += 40;

            // Datos del Cliente
            gfx.DrawString("Datos del Cliente:", fontBold, XBrushes.Black, 20, yPos);
            yPos += 20;
            gfx.DrawString($"Nombre: {cliente.NombreCompleto ?? "No registrado"}", font, XBrushes.Black, 20, yPos);
            yPos += 20;
            gfx.DrawString($"RFC: {cliente.RFC ?? "No registrado"}", font, XBrushes.Black, 20, yPos);
            yPos += 20;
            gfx.DrawString($"Ubicación: {cliente.Ciudad ?? "No registrado"}, {cliente.Estado ?? "No registrado"}, {cliente.Pais ?? "No registrado"}", font, XBrushes.Black, 20, yPos);
            yPos += 40;

            // Servicios Seleccionados
            gfx.DrawString("Servicios Seleccionados:", fontBold, XBrushes.Black, 20, yPos);
            yPos += 20;
            if (servicios.Count > 0)
            {
                foreach (var servicio in servicios)
                {
                    gfx.DrawString($"- {servicio.NombreServicio}: {servicio.Costo:C2}", font, XBrushes.Black, 20, yPos);
                    yPos += 20;
                    sumaServicios = +servicio.Costo;
                }
            }
            else
            {
                gfx.DrawString("No se registraron servicios.", font, XBrushes.Black, 20, yPos);
                sumaServicios = 0;
            }
            yPos += 40;

            gastoTotal = reservacion.Total + sumaServicios - reservacion.Anticipo;
            // Gasto Total
            gfx.DrawString($"Gasto Total: {gastoTotal:C2}", fontBold, XBrushes.Black, 20, yPos);

            pdf.Save(filePath);
            Console.WriteLine("Factura en PDF generada exitosamente.");
        }


        public static Reservacion ObtenerReservacionPorCliente(string rfcCliente)
        {
            string query = @"
    SELECT   
        R.CodigoReservacion, 
        R.FechaInicio, 
        R.FechaFin, 
        R.Estatus, 
        R.Total,
        R.Anticipo,
        S.ID_Servicio, 
        S.NombreServicio, 
        S.Costo
    FROM Reservacion R  
    LEFT JOIN Servicios_Reservacion SR 
        ON R.CodigoReservacion = SR.ID_Reservacion  
    LEFT JOIN Servicios S 
        ON SR.ID_Servicio = S.ID_Servicio  
    WHERE R.RFC_Cliente = @RFCCliente    
    ORDER BY R.FechaInicio DESC;";

            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RFCCliente", rfcCliente);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Reservacion reservacion = null;

                        while (reader.Read())
                        {
                            if (reservacion == null)
                            {
                                reservacion = new Reservacion
                                {
                                    CodigoReservacion = reader["CodigoReservacion"].ToString(),
                                    FechaInicio = Convert.ToDateTime(reader["FechaInicio"]),
                                    FechaFin = Convert.ToDateTime(reader["FechaFin"]),
                                    Estatus = reader["Estatus"].ToString(),
                                    Total = Convert.ToSingle(reader["Total"]),
                                    Anticipo = Convert.ToSingle(reader["Anticipo"]),
                                    Servicios = new List<Servicios>()
                                };
                            }

                            // Agregar servicios asociados si existen
                            if (!reader.IsDBNull(reader.GetOrdinal("ID_Servicio")))
                            {
                                reservacion.Servicios.Add(new Servicios
                                {
                                    ID_Servicio = Convert.ToInt32(reader["ID_Servicio"]),
                                    NombreServicio = reader["NombreServicio"].ToString(),
                                    Costo = reader["Costo"] == DBNull.Value ? 0 : Convert.ToDouble(reader["Costo"])
                                });
                            }
                        }

                        return reservacion;
                    }
                }
            }
        }






        public static Cliente ObtenerClientePorReservacion(string codigoReservacion)
        {
            // Validar que el código de reservación no esté vacío o nulo
            if (string.IsNullOrEmpty(codigoReservacion))
            {
                throw new ArgumentException("El código de reservación no puede estar vacío");
            }

            // Consulta para obtener el cliente relacionado con la reservación filtrando por estatus
            string query = @"
            SELECT c.RFC, c.NombreCompleto, c.Ciudad, c.Estado, c.Pais, c.EstadoCivil
            FROM Reservacion r
            INNER JOIN Cliente c ON r.RFC_Cliente = c.RFC
            WHERE r.CodigoReservacion = @CodigoReservacion 
            AND (r.Estatus IN ('Aceptada', 'Eliminada') OR r.Estatus IS NULL);";

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
                            // Crear y devolver el cliente si se encuentra en la base de datos
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

            // Devolver null si no se encuentra ningún cliente
            return null;
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

        // En tu lógica de check-out
     
    }

}

