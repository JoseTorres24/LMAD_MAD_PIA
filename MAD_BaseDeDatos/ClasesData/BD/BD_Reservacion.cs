using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesData.BD
{
    public class BD_Reservacion
    {
        // Método para cargar clientes en el combo
        public static List<Cliente> ObtenerClientes()
        {
            List<Cliente> clientes = new List<Cliente>();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                string consulta = "SELECT RFC, NombreCompleto FROM Clientes";

                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clientes.Add(new Cliente
                        {
                            RFC = reader.GetString(0),
                            NombreCompleto = reader.GetString(1)
                        });
                    }
                }
            }

            return clientes;
        }

        // Método para cargar ciudades disponibles
        public static List<string> ObtenerCiudades()
        {
            List<string> ciudades = new List<string>();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                string consulta = "SELECT DISTINCT Ciudad FROM Hoteles";

                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ciudades.Add(reader.GetString(0));
                    }
                }
            }

            return ciudades;
        }

        // Método para cargar hoteles por ciudad
        public static List<Hoteles> ObtenerHotelesPorCiudad(string ciudad)
        {
            List<Hoteles> hoteles = new List<Hoteles>();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                string consulta = "SELECT ID_Hotel, NombreHotel FROM Hoteles WHERE Ciudad = @Ciudad";

                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@Ciudad", ciudad);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            hoteles.Add(new Hoteles
                            {
                                ID_Hotel = reader.GetInt32(0),
                                NombreHotel = reader.GetString(1)
                            });
                        }
                    }
                }
            }

            return hoteles;
        }

        
        public static List<Habitaciones> ObtenerHabitacionesDisponibles(
    int idHotel,
    DateTime fechaInicio,
    DateTime fechaFin,
    string nivel,
    string vista,
    string tipoCama,
    int numeroCamas)
        {
            List<Habitaciones> habitaciones = new List<Habitaciones>();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                string consulta = @"
                SELECT h.ID_Habitacion, h.NumeroHabitacion, h.PisoHabitacion, h.NivelHabitacion, 
                       h.Capacidad, h.NumeroCamas, h.VistaHabitacion
                FROM Habitaciones h
                WHERE h.ID_Hotel = @ID_Hotel
                AND h.NivelHabitacion = @Nivel
                AND h.VistaHabitacion = @Vista
                AND h.NumeroCamas >= @NumeroCamas
                AND h.Estado = 'Disponible'
                AND NOT EXISTS (
                    SELECT 1 FROM Reservacion r
                    WHERE r.ID_Hotel = h.ID_Hotel
                    AND (
                        (@FechaInicio BETWEEN r.FechaInicio AND r.FechaFin)
                        OR (@FechaFin BETWEEN r.FechaInicio AND r.FechaFin)
                        OR (r.FechaInicio BETWEEN @FechaInicio AND @FechaFin)
                    )
                )";

                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@ID_Hotel", idHotel);
                    cmd.Parameters.AddWithValue("@Nivel", nivel);
                    cmd.Parameters.AddWithValue("@Vista", vista);
                    cmd.Parameters.AddWithValue("@NumeroCamas", numeroCamas);
                    cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    cmd.Parameters.AddWithValue("@FechaFin", fechaFin);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            habitaciones.Add(new Habitaciones
                            {
                                ID_Habitacion = reader.GetInt32(0),
                                NumeroHabitacion = reader.GetInt32(1),
                                PisoHabitacion = reader.GetInt32(2),
                                NivelHabitacion = reader.GetString(3),
                                Capacidad = reader.GetInt32(4),
                                NumeroCamas = reader.GetInt32(5),
                                VistaHabitacion = reader.GetString(6),
                                Estado = "Disponible"
                            });
                        }
                    }
                }
            }

            return habitaciones;
        }





        public static List<Servicios> ObtenerServiciosPorHotel(int idHotel)
        {
            List<Servicios> servicios = new List<Servicios>();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                string consulta = @"
                SELECT s.ID_Servicio, s.NombreServicio, s.Costo, s.Descripcion
                FROM Servicios s
                INNER JOIN HotelServicios hs ON s.ID_Servicio = hs.ID_Servicio
                WHERE hs.ID_Hotel = @ID_Hotel";

                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@ID_Hotel", idHotel);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            servicios.Add(new Servicios
                            {
                                ID_Servicio = reader.GetInt32(0),
                                NombreServicio = reader.GetString(1),
                                Costo = reader.GetDouble(2),
                                Descripcion = reader.GetString(3)
                            });
                        }
                    }
                }
            }

            return servicios;
        }


        public static bool CrearReservacion(Reservacion reservacion)
        {
            SqlTransaction transaction = null;

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                try
                {
                    conexion.Open();

                    // Iniciar transacción
                    transaction = conexion.BeginTransaction();

                    // 1. Insertar la reservación
                    string consultaReservacion = @"
                INSERT INTO Reservacion (CodigoReservacion, RFC_Cliente, ID_Hotel, ID_Habitacion,
                                       FechaInicio, FechaFin, Anticipo, UsuarioRegistro)
                VALUES (@Codigo, @RFC, @IDHotel, @IDHabitacion, 
                        @FechaInicio, @FechaFin, @Anticipo, @Usuario)";

                    using (SqlCommand cmdReservacion = new SqlCommand(consultaReservacion, conexion, transaction))
                    {
                        cmdReservacion.Parameters.AddWithValue("@Codigo", reservacion.CodigoReservacion);
                        cmdReservacion.Parameters.AddWithValue("@RFC", reservacion.RFC_Cliente);
                        cmdReservacion.Parameters.AddWithValue("@IDHotel", reservacion.ID_Hotel);
                        cmdReservacion.Parameters.AddWithValue("@IDHabitacion", reservacion.ID_Habitacion);
                        cmdReservacion.Parameters.AddWithValue("@FechaInicio", reservacion.FechaInicio);
                        cmdReservacion.Parameters.AddWithValue("@FechaFin", reservacion.FechaFin);
                        cmdReservacion.Parameters.AddWithValue("@Anticipo", reservacion.Anticipo);
                        cmdReservacion.Parameters.AddWithValue("@Usuario", reservacion.UsuarioRegistro);

                        int filasAfectadas = cmdReservacion.ExecuteNonQuery();

                        if (filasAfectadas == 0)
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }

                    // 2. Actualizar estado de la habitación a "Ocupada"
                    string consultaHabitacion = @"
                UPDATE Habitaciones 
                SET Estado = 'Ocupada' 
                WHERE ID_Habitacion = @IDHabitacion";

                    using (SqlCommand cmdHabitacion = new SqlCommand(consultaHabitacion, conexion, transaction))
                    {
                        cmdHabitacion.Parameters.AddWithValue("@IDHabitacion", reservacion.ID_Habitacion);

                        int filasActualizadas = cmdHabitacion.ExecuteNonQuery();

                        if (filasActualizadas == 0)
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }

                    // Si todo salió bien, confirmar la transacción
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    // Si hay error, revertir la transacción
                    transaction?.Rollback();
                    // Considera registrar el error (log)
                    return false;
                }
            }
        }

        public static bool AgregarServiciosReservacion(List<ServiciosReservacion> servicios, string codigoReservacion)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                SqlTransaction transaction = conexion.BeginTransaction();

                try
                {
                    string consulta = @"INSERT INTO ServiciosReservacion (ID_Reservacion, ID_Servicio)
                               VALUES (@IDReservacion, @IDServicio)";

                    foreach (var servicio in servicios)
                    {
                        using (SqlCommand cmd = new SqlCommand(consulta, conexion, transaction))
                        {
                            cmd.Parameters.AddWithValue("@IDReservacion", codigoReservacion);
                            cmd.Parameters.AddWithValue("@IDServicio", servicio.ID_Servicio);

                            if (cmd.ExecuteNonQuery() <= 0)
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }
                    }

                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error al agregar servicios a reservación: {ex.Message}");
                    return false;
                }
            }
        }









    }



}
