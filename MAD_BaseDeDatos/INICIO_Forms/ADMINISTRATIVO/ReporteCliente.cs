using ClasesData;
using ClasesData.BD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace INICIO_Forms.ADMINISTRATIVO
    //Componentes
    //comboReservacion, busca el cliente y los datos del cliente 






{
    public partial class ReporteCliente: Form
    {
        public ReporteCliente()
        {
            InitializeComponent();
        }

        private void ReporteCliente_Load(object sender, EventArgs e)
        {
            CargarClientesEnListBox();
        }

        private void CargarClientesEnListBox()
        {
            comboReservacion.DataSource = ObtenerClientes();
            comboReservacion.DisplayMember = "RFC";
            comboReservacion.ValueMember = "RFC";
        }

        private void comboReservacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboReservacion.SelectedItem is Cliente clienteSeleccionado)
            {
                string año = Convert.ToString(clienteSeleccionado.FechaNacimiento);
                textAño.Text =año;
            }
            else
            {
                textAño.Text = string.Empty;
            }
        }

        private void textAño_TextChanged(object sender, EventArgs e)
        {

        }




        public static List<Cliente> ObtenerClientes()
        {
            List<Cliente> clientes = new List<Cliente>();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                string consulta = "SELECT RFC, NombreCompleto, FechaNacimiento FROM Clientes";

                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clientes.Add(new Cliente
                        {
                            RFC = reader.GetString(0),
                            NombreCompleto = reader.GetString(1),
                            FechaNacimiento = reader.GetDateTime(2)
                        });
                    }
                }
            }

            return clientes;
        }
        // Esto de acuerdo a esto me tiene que salir informacion para mandar el textBox2



        // Cuando se cambia la selección en el combo de clientes,
        // mostramos en el textAño el año del cliente (utilizando FechaNacimiento.Year)
        private void comboReservacion_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboReservacion.SelectedItem is Cliente clienteSeleccionado)
            {
                textAño.Text = clienteSeleccionado.FechaNacimiento.Year.ToString();
            }
            else
            {
                textAño.Text = string.Empty;
            }
        }

        // Este método ejecuta la consulta y carga el ListBox con el reporte
        // Utiliza el RFC del cliente seleccionado y el año obtenido (del TextBox)
        private void button2_Click(object sender, EventArgs e)
        {
            if (comboReservacion.SelectedItem is Cliente cliente)
            {
                string rfc = cliente.RFC;
                int? anio = null;
                if (!string.IsNullOrEmpty(textAño.Text))
                {
                    if (int.TryParse(textAño.Text, out int valor))
                    {
                        anio = valor;
                    }
                }
                // Obtiene la lista de líneas del reporte
                List<string> reporte = ObtenerReporteReservacion(rfc, anio);
                listBox1.Items.Clear();
                foreach (var linea in reporte)
                {
                    listBox1.Items.Add(linea);
                }
                if (reporte.Count == 0)
                {
                    MessageBox.Show("No se encontraron registros para los parámetros seleccionados.");
                }
            }
            else
            {
                MessageBox.Show("Seleccione un cliente en el combo.");
            }
        }

        // Este método exporta el contenido de listBox1 a un archivo de texto y lo abre con Notepad.
        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.");
                return;
            }

            string filePath = Path.Combine(Application.StartupPath, "Reporte.txt");
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    foreach (object item in listBox1.Items)
                    {
                        sw.WriteLine(item.ToString());
                    }
                }
                Process.Start("notepad.exe", filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar el reporte: " + ex.Message);
            }
        }

        // Este método ejecuta la consulta de reporte y retorna las líneas resultantes como lista de strings.
        public static List<string> ObtenerReporteReservacion(string rfc, int? anio)
        {
            List<string> reporte = new List<string>();

            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                string query = @"
SELECT 
    C.NombreCompleto, 
    H.Ciudad AS Ciudad, 
    H.NombreHotel AS Hotel, 
    Ha.TipoHabitacion AS TipoHabitacion,
    Ha.NumeroHabitacion, 
    R.CodigoReservacion, 
    R.FechaRegistro AS FechaReservacion,
    CONVERT(varchar, CH.FechaCheckIn, 120) AS FechaCheckIn, 
    CONVERT(varchar, CO.FechaCheckOut, 120) AS FechaCheckOut, 
    CH.EstadoEntrada AS Estatus,
    R.Anticipo, 
    R.Total AS MontoHospedaje, 
    ISNULL(SUM(S.Costo), 0) AS MontoServicios
FROM Reservacion R
JOIN Clientes C ON R.RFC_Cliente = C.RFC
JOIN Hoteles H ON R.ID_Hotel = H.ID_Hotel
JOIN Habitaciones Ha ON R.ID_Habitacion = Ha.ID_Habitacion
LEFT JOIN CheckIn CH ON R.CodigoReservacion = CH.ID_Reservacion
LEFT JOIN CheckOut CO ON R.CodigoReservacion = CO.ID_Reservacion
LEFT JOIN Servicios_Reservacion SR ON R.CodigoReservacion = SR.ID_Reservacion
LEFT JOIN Servicios S ON SR.ID_Servicio = S.ID_Servicio
WHERE C.RFC = @RFC 
  AND (YEAR(R.FechaRegistro) = @Anio OR @Anio IS NULL)
GROUP BY 
    C.NombreCompleto, 
    H.Ciudad, 
    H.NombreHotel, 
    Ha.TipoHabitacion, 
    Ha.NumeroHabitacion, 
    R.CodigoReservacion, 
    R.FechaRegistro,
    CH.FechaCheckIn, 
    CO.FechaCheckOut, 
    CH.EstadoEntrada,
    R.Anticipo, 
    R.Total
ORDER BY R.FechaRegistro DESC;
";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RFC", rfc);
                    if (anio.HasValue)
                        cmd.Parameters.AddWithValue("@Anio", anio.Value);
                    else
                        cmd.Parameters.AddWithValue("@Anio", DBNull.Value);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Formatea la línea que deseas que se muestre.
                            string linea = $"Cliente: {reader["NombreCompleto"]} | Ciudad: {reader["Ciudad"]} | Hotel: {reader["Hotel"]}" +
                                            $" | Tipo: {reader["TipoHabitacion"]} | Hab: {reader["NumeroHabitacion"]} | " +
                                            $"CodRes: {reader["CodigoReservacion"]} | FechaRes: {reader["FechaReservacion"]} | " +
                                            $"CheckIn: {reader["FechaCheckIn"]} | CheckOut: {reader["FechaCheckOut"]} | " +
                                            $"Estatus: {reader["Estatus"]} | Anticipo: {reader["Anticipo"]} | " +
                                            $"Monto Hospedaje: {reader["MontoHospedaje"]} | Servicios: {reader["MontoServicios"]}";
                            reporte.Add(linea);
                        }
                    }
                }
            }
            return reporte;
        }









    }
}
