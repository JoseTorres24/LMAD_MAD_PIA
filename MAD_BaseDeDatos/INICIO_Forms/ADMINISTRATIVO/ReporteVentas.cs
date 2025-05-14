using ClasesData.BD;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Windows.Controls;
using System.Windows.Forms;

namespace INICIO_Forms.ADMINISTRATIVO
{
    public partial class ReporteVentas : Form
    {
        public ReporteVentas()
        {
            InitializeComponent();
        }

        private void ReporteVentas_Load(object sender, EventArgs e)
        {
            CargarCiudades();
            CargarPaises();
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
                MessageBox.Show("Error al cargar países: " + ex.Message);
            }
        }

        // Botón "Agregar al reporte"
        // Este botón ejecuta la consulta usando los filtros y muestra el resultado en listBox1.
        private void button2_Click(object sender, EventArgs e)
        {
            // Validar que se introduzca un año válido en textBox1.
            if (!int.TryParse(textBox1.Text, out int anio))
            {
                MessageBox.Show("Por favor ingrese un año válido.");
                return;
            }
            // Obtenemos los filtros: país y ciudad.
            string pais = comboBox1.SelectedItem?.ToString() ?? "";
            string ciudad = comboBox2.SelectedItem?.ToString() ?? "";
            // Para este ejemplo, no usamos un filtro de hotel en particular (podrías agregar un comboBox para ello).
            int? idHotel = null;

            List<string> reporte = ObtenerReporteVentas(pais, ciudad, idHotel, anio);
            listHoteles.Items.Clear();
            foreach (var linea in reporte)
            {
                listHoteles.Items.Add(linea);
            }
            if (reporte.Count == 0)
            {
                MessageBox.Show("No se encontraron datos para los filtros seleccionados.");
            }
        }

        // Botón "Exportar"
        // Este botón exporta el contenido de listBox1 a un archivo de texto y lo abre con el Bloc de notas.
        private void button1_Click(object sender, EventArgs e)
        {
            if (listHoteles.Items.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.");
                return;
            }

            string filePath = Path.Combine(Application.StartupPath, "ReporteVentas.txt");
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    foreach (object item in listHoteles.Items)
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

        /// <summary>
        /// Obtiene el reporte de ventas para la ocupación y servicios
        /// de un hotel (o de todos los hoteles de una ciudad) para un año determinado.
        /// Los filtros son País, Ciudad, (opcionalmente ID_Hotel) y Año.
        /// Se muestran todos los meses del año, incluso si no hay datos.
        /// 
        /// Columnas: 
        ///   - Ciudad
        ///   - Nombre del hotel
        ///   - Año
        ///   - Mes (en letra)
        ///   - Ingresos por hospedaje (TotalPagar menos ingresos por servicios adicionales)
        ///   - Ingresos por servicios adicionales (suma de costos de servicios reservados)
        ///   - Ingresos totales (TotalPagar)
        /// </summary>
        public List<string> ObtenerReporteVentas(string pais, string ciudad, int? idHotel, int anio)
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
HotelesBase AS (
    SELECT H.Ciudad, H.NombreHotel, H.ID_Hotel
    FROM Hoteles H
    WHERE H.Pais = @Pais 
      AND H.Ciudad = @Ciudad
      AND (@ID_Hotel IS NULL OR H.ID_Hotel = @ID_Hotel)
),
ServiciosPorReservacion AS (
    SELECT SR.ID_Reservacion, SUM(S.Costo) AS CostoServicios
    FROM Servicios_Reservacion SR
    JOIN Servicios S ON SR.ID_Servicio = S.ID_Servicio
    GROUP BY SR.ID_Reservacion
),
Ventas AS (
    SELECT 
        H.ID_Hotel,
        MONTH(CO.FechaCheckOut) AS Mes,
        SUM(F.TotalPagar) AS TotalVentas,
        SUM(ISNULL(SPR.CostoServicios, 0)) AS ServiciosVentas
    FROM Factura F
    JOIN CheckOut CO ON F.ID_CheckOut = CO.ID_CheckOut
    JOIN Reservacion R ON CO.ID_Reservacion = R.CodigoReservacion
    JOIN Hoteles H ON R.ID_Hotel = H.ID_Hotel
    LEFT JOIN ServiciosPorReservacion SPR ON R.CodigoReservacion = SPR.ID_Reservacion
    WHERE YEAR(CO.FechaCheckOut) = @Anio
    GROUP BY H.ID_Hotel, MONTH(CO.FechaCheckOut)
)
SELECT 
    hb.Ciudad,
    hb.NombreHotel,
    @Anio AS Año,
    m.NombreMes AS Mes,
    ISNULL(v.TotalVentas - v.ServiciosVentas, 0) AS IngresosHospedaje,
    ISNULL(v.ServiciosVentas, 0) AS IngresosServicios,
    ISNULL(v.TotalVentas, 0) AS IngresosTotales
FROM HotelesBase hb
CROSS JOIN Meses m
LEFT JOIN Ventas v 
    ON hb.ID_Hotel = v.ID_Hotel 
   AND m.Mes = v.Mes
ORDER BY hb.Ciudad, hb.NombreHotel, m.Mes;
";

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
                                           $"Ingresos Hospedaje: {reader["IngresosHospedaje"]} | Ingresos Servicios: {reader["IngresosServicios"]} | " +
                                           $"Ingresos Totales: {reader["IngresosTotales"]}";
                            reporte.Add(linea);
                        }
                    }
                }
            }
            return reporte;
        }
    }
}
