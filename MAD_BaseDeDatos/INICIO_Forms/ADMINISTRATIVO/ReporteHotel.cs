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
using System.Windows.Controls;
using System.Windows.Forms;

namespace INICIO_Forms.ADMINISTRATIVO
{
    public partial class ReporteHotel: Form
    {
        public ReporteHotel()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void CargarCiudades()
        {
            try
            {
                comboBox2.DataSource = BD_Reservacion.ObtenerCiudades();
                comboBox2.SelectedIndex = 0; // Sin selección inicial
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar ciudades: " + ex.Message);
            }
        }
        private void CargarPaises()
        {
            try
            {
                comboBox1.DataSource = BD_Reservacion.ObtenerPaises();
                comboBox1.SelectedIndex = 0; // Sin selección inicial
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar ciudades: " + ex.Message);
            }
        }

        // Botón para generar el reporte (cargar la información en el ListBox)
        private void button2_Click(object sender, EventArgs e)
        {
            // Validar Año (textBox1)
            if (!int.TryParse(textBox1.Text, out int anio))
            {
                MessageBox.Show("Ingrese un año válido.");
                return;
            }
            // Obtener parámetros de filtros
            string pais = comboBox1.SelectedItem?.ToString() ?? "";
            string ciudad = comboBox2.SelectedItem?.ToString() ?? "";

            // Si tuvieras un filtro de hotel específico (por ejemplo, comboBoxHotel) podrías obtener su ID; 
            // En este ejemplo se usa null para TODOS los hoteles.
            int? idHotel = null;

            // Llamamos a la función para obtener el reporte. Esta función se conecta a la BD y ejecuta la query.
            List<string> reporte = ObtenerReporteHotel(pais, ciudad, idHotel, anio);

            listHoteles.Items.Clear();
            foreach (var linea in reporte)
            {
                listHoteles.Items.Add(linea);
            }
            if (reporte.Count == 0)
            {
                MessageBox.Show("No hay datos para los filtros seleccionados.");
            }
        }

        // Botón para exportar el reporte a un archivo de texto y abrir Notepad
        private void button1_Click(object sender, EventArgs e)
        {
            if (listHoteles.Items.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.");
                return;
            }

            string filePath = Path.Combine(Application.StartupPath, "ReporteHotel.txt");
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    foreach (object item in listHoteles.Items)
                        sw.WriteLine(item.ToString());
                }
                Process.Start("notepad.exe", filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar el reporte: " + ex.Message);
            }
        }

        /// <summary>
        /// Esta función ejecuta una consulta que devuelve el reporte de ocupación
        /// por hotel, agrupado por mes y por tipo de habitación.
        /// Parámetros: 
        ///    @Pais, @Ciudad, @ID_Hotel (opcional) y @Anio.
        /// Retorna una lista de cadenas formateadas, cada una representando una línea del reporte.
        /// </summary>
        public List<string> ObtenerReporteHotel(string pais, string ciudad, int? idHotel, int anio)
        {
            List<string> reporte = new List<string>();

            string query = @"
WITH Meses AS (
    SELECT 1 AS Mes, 'Enero' AS NombreMes UNION ALL
    SELECT 2, 'Febrero' UNION ALL
    SELECT 3, 'Marzo' UNION ALL
    SELECT 4, 'Abril' UNION ALL
    SELECT 5, 'Mayo' UNION ALL
    SELECT 6, 'Junio' UNION ALL
    SELECT 7, 'Julio' UNION ALL
    SELECT 8, 'Agosto' UNION ALL
    SELECT 9, 'Septiembre' UNION ALL
    SELECT 10, 'Octubre' UNION ALL
    SELECT 11, 'Noviembre' UNION ALL
    SELECT 12, 'Diciembre'
),
BaseRooms AS (
   SELECT H.Ciudad, H.NombreHotel, H.ID_Hotel, Ha.TipoHabitacion, COUNT(*) AS TotalHabitaciones
   FROM Habitaciones Ha
   JOIN Hoteles H ON Ha.ID_Hotel = H.ID_Hotel
   WHERE H.Pais = @Pais 
     AND H.Ciudad = @Ciudad
     AND (@ID_Hotel IS NULL OR H.ID_Hotel = @ID_Hotel)
   GROUP BY H.Ciudad, H.NombreHotel, H.ID_Hotel, Ha.TipoHabitacion
),
ResOccupancy AS (
   SELECT H.ID_Hotel, Ha.TipoHabitacion, MONTH(CH.FechaCheckIn) AS Mes, 
          COUNT(*) AS Ocupadas,
          SUM(Ha.Capacidad) AS PersonasHospedadas
   FROM Reservacion R
   JOIN CheckIn CH ON R.CodigoReservacion = CH.ID_Reservacion
   JOIN Habitaciones Ha ON R.ID_Habitacion = Ha.ID_Habitacion
   JOIN Hoteles H ON Ha.ID_Hotel = H.ID_Hotel
   WHERE YEAR(CH.FechaCheckIn) = @Anio
   GROUP BY H.ID_Hotel, Ha.TipoHabitacion, MONTH(CH.FechaCheckIn)
)
SELECT 
   b.Ciudad,
   b.NombreHotel,
   @Anio AS Año,
   m.NombreMes AS Mes,
   b.TipoHabitacion,
   b.TotalHabitaciones,
   ISNULL(o.Ocupadas, 0) AS HabitacionesOcupadas,
   CASE WHEN b.TotalHabitaciones > 0 THEN (ISNULL(o.Ocupadas,0) * 100.0 / b.TotalHabitaciones) ELSE 0 END AS PorcentajeOcupacion,
   ISNULL(o.PersonasHospedadas, 0) AS PersonasHospedadas
FROM BaseRooms b
CROSS JOIN Meses m
LEFT JOIN ResOccupancy o 
    ON b.ID_Hotel = o.ID_Hotel 
   AND b.TipoHabitacion = o.TipoHabitacion 
   AND m.Mes = o.Mes
ORDER BY b.Ciudad, b.NombreHotel, m.Mes, b.TipoHabitacion;";

            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Pais", pais);
                    cmd.Parameters.AddWithValue("@Ciudad", ciudad);
                    if (idHotel.HasValue)
                        cmd.Parameters.AddWithValue("@ID_Hotel", idHotel.Value);
                    else
                        cmd.Parameters.AddWithValue("@ID_Hotel", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Anio", anio);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string linea = $"Ciudad: {reader["Ciudad"]} | Hotel: {reader["NombreHotel"]} | Año: {reader["Año"]} | Mes: {reader["Mes"]} | " +
                                           $"Tipo: {reader["TipoHabitacion"]} | Total Habitaciones: {reader["TotalHabitaciones"]} | " +
                                           $"Ocupadas: {reader["HabitacionesOcupadas"]} | % Ocupación: {reader["PorcentajeOcupacion"]} | " +
                                           $"Personas Hospedadas: {reader["PersonasHospedadas"]}";
                            reporte.Add(linea);
                        }
                    }
                }
            }
            return reporte;
        }

        private void ReporteHotel_Load(object sender, EventArgs e)
        {
            CargarCiudades();
            CargarPaises();
        }
    }
}
