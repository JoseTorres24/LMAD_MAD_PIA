using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClasesData.BD
{
    public class BD_Check
    {

        public static int ObtenerSiguienteID_CheckIn()
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                string query = "SELECT ISNULL(MAX(ID_CheckIn), 0) + 1 FROM CheckIn";
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    return (int)cmd.ExecuteScalar();
                }
            }
        }



        public static bool InsertarCheckIn(CheckIn checkIn)
        {
            try
            {
                using (SqlConnection conexion = ConexionBD.ObtenerConexion())
                {
                    conexion.Open();

                    int nuevoID = ObtenerSiguienteID_CheckIn();

                    string query = @"INSERT INTO CheckIn (ID_CheckIn, ID_Reservacion, UsuarioRegistro, FechaCheckIn, EstadoEntrada, Clave)
                 VALUES (@ID_CheckIn, @ID_Reservacion, @UsuarioRegistro, @FechaCheckIn, @EstadoEntrada, @Clave)";

                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@ID_CheckIn", nuevoID);
                        cmd.Parameters.AddWithValue("@ID_Reservacion", checkIn.ID_Reservacion);
                        cmd.Parameters.AddWithValue("@UsuarioRegistro", checkIn.UsuarioRegistro);
                        cmd.Parameters.AddWithValue("@FechaCheckIn", checkIn.FechaCheckIn);
                        cmd.Parameters.AddWithValue("@EstadoEntrada", checkIn.EstadoEntrada);
                        cmd.Parameters.AddWithValue("@Clave", checkIn.Clave);

                        bool exito = cmd.ExecuteNonQuery() > 0;

                        if (exito)
                        {
                            BD_Reservacion.ActualizarEstatusReservacion(checkIn.ID_Reservacion, "Aceptada");
                            MessageBox.Show("Check-In registrado correctamente");
                            // ...
                        }

                        return exito;
                    }

                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("SQL Error: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error general: " + ex.Message);
                return false;
            }
        }


        public static bool InsertarFactura(Factura factura)
        {
            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                string query = @"
            INSERT INTO Factura (ID_CheckOut, Descuento, ServiciosUtilizados, TotalPagar)
            VALUES (@ID_CheckOut, @Descuento, @ServiciosUtilizados, @TotalPagar)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID_CheckOut", factura.ID_CheckOut);
                cmd.Parameters.AddWithValue("@Descuento", factura.Descuento);
                cmd.Parameters.AddWithValue("@ServiciosUtilizados", factura.ServiciosUtilizados ?? "");
                cmd.Parameters.AddWithValue("@TotalPagar", factura.TotalPagar);

                return cmd.ExecuteNonQuery() > 0;
            }
        }


        public static List<ServiciosReservacion> ObtenerServiciosPorReservacion(string codigoReservacion)
        {
            List<ServiciosReservacion> servicios = new List<ServiciosReservacion>();

            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                string query = @"
            SELECT ID_ServicioReservado, ID_Reservacion, ID_Servicio
            FROM Servicios_Reservacion
            WHERE ID_Reservacion = @CodigoReservacion";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CodigoReservacion", codigoReservacion);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        servicios.Add(new ServiciosReservacion
                        {
                            ID_ServicioReservado = Convert.ToInt32(reader["ID_ServicioReservado"]),
                            ID_Reservacion = reader["ID_Reservacion"].ToString(),
                            ID_Servicio = Convert.ToInt32(reader["ID_Servicio"])
                        });
                    }
                }
            }

            return servicios;
        }




        public static List<Reservacion> ObtenerReservacionesMarcadas(int idHotel)
        {
            List<Reservacion> reservaciones = new List<Reservacion>();

            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();

                string query = @"
        SELECT R.CodigoReservacion, R.Total, R.Anticipo
        FROM Reservacion R
        INNER JOIN CheckIn C ON R.CodigoReservacion = C.ID_Reservacion
        WHERE C.EstadoEntrada = 'Marcado' AND R.ID_Hotel = @ID_Hotel";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID_Hotel", idHotel);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            Console.WriteLine("No se encontraron reservaciones marcadas para el hotel con ID: " + idHotel);
                        }

                        while (reader.Read())
                        {
                            var codigo = reader["CodigoReservacion"].ToString();

                            var total = reader.IsDBNull(reader.GetOrdinal("Total"))
                                ? 0f
                                : Convert.ToSingle(reader["Total"]);

                            var anticipo = reader.IsDBNull(reader.GetOrdinal("Anticipo"))
                                ? 0f
                                : Convert.ToSingle(reader["Anticipo"]);

                            var reservacion = new Reservacion
                            {
                                CodigoReservacion = codigo,
                                Total = total,
                                Anticipo = anticipo,
                                Servicios = ObtenerServiciosPorReservacion(codigo)
                            };

                            reservaciones.Add(reservacion);
                            Console.WriteLine("Reservación encontrada: " + codigo);
                        }
                    }
                }
            }

            return reservaciones;
        }




    }
}
