using System;
using System.Collections.Generic;
using System.Data;
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

        public static List<string> ObtenerPaises()
        {
            List<string> paises = new List<string>();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                string consulta = "SELECT DISTINCT Pais FROM Hoteles";

                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        paises.Add(reader.GetString(0));
                    }
                }
            }

            return paises;
        }

        // Método para cargar hoteles por ciudad
        public static List<Hoteles> ObtenerHotelesPorCiudad(string ciudad)
        {
            List<Hoteles> hoteles = new List<Hoteles>();

            try
            {
                using (SqlConnection conexion = ConexionBD.ObtenerConexion())
                {
                    conexion.Open();

                    string consulta = @"
            SELECT ID_Hotel, NombreHotel, Ciudad, Domicilio
            FROM Hoteles
            WHERE Ciudad = @Ciudad
            AND CONVERT(DATE, FechaInicioOperaciones) <= CONVERT(DATE, GETDATE())
            ORDER BY NombreHotel;";

                    using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                    {
                        cmd.CommandTimeout = 30;
                        cmd.Parameters.AddWithValue("@Ciudad", ciudad ?? "");

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                hoteles.Add(new Hoteles
                                {
                                    ID_Hotel = reader.GetInt32(0),
                                    NombreHotel = reader.GetString(1),
                                    Ciudad = reader.GetString(2),
                                    Domicilio = reader.GetString(3)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener hoteles por ciudad: " + ex.Message);
            }

            return hoteles;
        }


        public static List<Habitaciones> ObtenerHabitacionesDisponibles(
           int idHotel,
           DateTime fechaInicio,
           DateTime fechaFin,
           string tipoHabitacion,
           string vista,
           string tipoCama,
           int numeroCamas,
           int capacidadMinima)
        {
            List<Habitaciones> habitaciones = new List<Habitaciones>();

            try
            {
                using (SqlConnection conexion = ConexionBD.ObtenerConexion())
                {
                    conexion.Open();

                    string consulta = @"
            SELECT 
                h.ID_Habitacion, 
                h.NumeroHabitacion, 
                h.NivelPiso, 
                h.TipoHabitacion, 
                h.Capacidad, 
                h.NumeroCamas, 
                h.VistaHabitacion,
                h.Estado
            FROM Habitaciones h
            WHERE 
                h.ID_Hotel = @ID_Hotel
                AND h.TipoHabitacion = @TipoHabitacion
                AND h.VistaHabitacion = @Vista
                AND h.NumeroCamas = @NumeroCamas
                AND h.Capacidad = @CapacidadMinima
                AND h.Estado = 'Disponible'
                AND NOT EXISTS (
                    SELECT 1 
                    FROM Reservacion r
                    WHERE 
                        r.ID_Hotel = h.ID_Hotel
                        AND r.ID_Habitacion = h.ID_Habitacion
                        AND (
                            (@FechaInicio BETWEEN r.FechaInicio AND r.FechaFin)
                            OR (@FechaFin BETWEEN r.FechaInicio AND r.FechaFin)
                            OR (r.FechaInicio BETWEEN @FechaInicio AND @FechaFin)
                        )
                )";

                    using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                    {
                        cmd.Parameters.AddWithValue("@ID_Hotel", idHotel);
                        cmd.Parameters.AddWithValue("@TipoHabitacion", tipoHabitacion ?? "");
                        cmd.Parameters.AddWithValue("@Vista", vista ?? "");
                        cmd.Parameters.AddWithValue("@NumeroCamas", numeroCamas);
                        cmd.Parameters.AddWithValue("@CapacidadMinima", capacidadMinima);
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
                                    TipoHabitacion = reader.GetString(3),
                                    Capacidad = reader.GetInt32(4),
                                    NumeroCamas = reader.GetInt32(5),
                                    VistaHabitacion = reader.GetString(6),
                                    Estado = reader.IsDBNull(7) ? "Desconocido" : reader.GetString(7)
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error SQL al buscar habitaciones: {sqlEx.Number} - {sqlEx.Message}");
                throw new Exception("Error de base de datos al buscar habitaciones disponibles");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al buscar habitaciones: {ex.Message}");
                throw new Exception("Error al obtener habitaciones disponibles");
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
        INNER JOIN Hotel_Servicios hs ON s.ID_Servicio = hs.ID_Servicio
        WHERE hs.ID_Hotel = @ID_Hotel
        ORDER BY s.NombreServicio";
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
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                using (SqlTransaction transaction = conexion.BeginTransaction())
                {
                    try
                    {
                        // Consulta modificada para incluir ID_Habitacion y Total
                        string consulta = @"
                INSERT INTO Reservacion (
                    CodigoReservacion, RFC_Cliente, ID_Hotel, ID_Habitacion,
                    FechaInicio, FechaFin, Anticipo, Total, UsuarioRegistro
                ) VALUES (
                    @Codigo, @RFC, @IDHotel, @IDHabitacion,
                    @FechaInicio, @FechaFin, @Anticipo, @Total, @Usuario
                )";

                        using (SqlCommand cmd = new SqlCommand(consulta, conexion, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Codigo", reservacion.CodigoReservacion);
                            cmd.Parameters.AddWithValue("@RFC", reservacion.RFC_Cliente);
                            cmd.Parameters.AddWithValue("@IDHotel", reservacion.ID_Hotel);
                            cmd.Parameters.AddWithValue("@IDHabitacion", reservacion.ID_Habitacion);
                            cmd.Parameters.AddWithValue("@FechaInicio", reservacion.FechaInicio);
                            cmd.Parameters.AddWithValue("@FechaFin", reservacion.FechaFin);
                            cmd.Parameters.AddWithValue("@Anticipo", reservacion.Anticipo);
                            cmd.Parameters.AddWithValue("@Total", reservacion.Total);  // ✅ Nuevo campo
                            cmd.Parameters.AddWithValue("@Usuario", reservacion.UsuarioRegistro);

                            int filasAfectadas = cmd.ExecuteNonQuery();
                            if (filasAfectadas == 0)
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }

                        // Actualizar estado de la habitación reservada
                        string updateHabitacion = @"
                UPDATE Habitaciones 
                SET Estado = 'Ocupada' 
                WHERE ID_Habitacion = @IDHabitacion";

                        using (SqlCommand cmd = new SqlCommand(updateHabitacion, conexion, transaction))
                        {
                            cmd.Parameters.AddWithValue("@IDHabitacion", reservacion.ID_Habitacion);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        throw new Exception($"Error SQL {ex.Number}: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception($"Error al crear reservación: {ex.Message}");
                    }
                }
            }
        }

        // Versión mejorada que usa el ID_Reservacion de cada objeto ServiciosReservacion
        public static bool AgregarServiciosReservacion(List<ServiciosReservacion> servicios)
        {
            if (servicios == null || servicios.Count == 0)
                return false;

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
                            cmd.Parameters.AddWithValue("@IDReservacion", servicio.ID_Reservacion);
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
        public static List<Reservacion> ObtenerReservacionesPorHotel(int idHotel)
        {
            List<Reservacion> reservaciones = new List<Reservacion>();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                string consulta = @"SELECT  
                R.CodigoReservacion,  
                R.RFC_Cliente,  
                R.FechaInicio,  
                R.FechaFin,  
                R.Estatus  
                FROM Reservacion R
                INNER JOIN Hoteles H ON R.ID_Hotel = H.ID_Hotel
                WHERE R.ID_Hotel = @IDHotel
                AND (R.Estatus NOT IN ('Eliminada', 'Aceptada') OR R.Estatus IS NULL)
                AND CAST(R.FechaInicio AS DATE) <= CAST(GETDATE() AS DATE)
                ORDER BY R.FechaInicio DESC;";

                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@IDHotel", idHotel);
                    conexion.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reservaciones.Add(new Reservacion
                            {
                                CodigoReservacion = reader.GetString(0),
                                RFC_Cliente = reader.GetString(1),
                                FechaInicio = reader.GetDateTime(2),
                                FechaFin = reader.GetDateTime(3)
                            });
                        }
                    }
                }
            }

            return reservaciones;
        }



        public static List<Reservacion> ObtenerReservacionesPendientes()
        {
            List<Reservacion> reservaciones = new List<Reservacion>();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                // Consulta: Reservaciones que no aparecen en CheckIn con EstadoEntrada = 'Marcado'
                string query = @"
            SELECT r.CodigoReservacion, r.RFC_Cliente, r.ID_Hotel, r.ID_Habitacion, 
                   r.FechaInicio, r.FechaFin, r.Anticipo, r.Total, r.UsuarioRegistro, r.FechaRegistro
            FROM Reservacion r
            WHERE NOT EXISTS (
                SELECT 1 
                FROM CheckIn c 
                WHERE c.ID_Reservacion = r.CodigoReservacion  
                  AND c.EstadoEntrada = 'Marcado'
            )";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reservaciones.Add(new Reservacion
                        {
                            CodigoReservacion = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                            RFC_Cliente = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                            ID_Hotel = reader.IsDBNull(2) ? 0 : reader.GetInt32(2),
                            // ID_Habitacion puede ser NULL; en ese caso asigna 0 (o el valor que convenga)
                            ID_Habitacion = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                            FechaInicio = reader.IsDBNull(4) ? DateTime.MinValue : reader.GetDateTime(4),
                            FechaFin = reader.IsDBNull(5) ? DateTime.MinValue : reader.GetDateTime(5),
                            // Para FLOAT usamos GetDouble y luego convertimos a float
                            Anticipo = reader.IsDBNull(6) ? 0 : (float)reader.GetDouble(6),
                            Total = reader.IsDBNull(7) ? 0 : (float)reader.GetDouble(7),
                            UsuarioRegistro = reader.IsDBNull(8) ? 0 : reader.GetInt32(8),
                            FechaRegistro = reader.IsDBNull(9) ? DateTime.MinValue : reader.GetDateTime(9)
                        });
                    }
                }
            }

            return reservaciones;
        }


        public static List<Reservacion> ObtenerReservacionesActivas()
        {

            string query = "SELECT * FROM Reservacion WHERE Estatus != 'Eliminada'";

            List<Reservacion> reservaciones = new List<Reservacion>();

            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reservaciones.Add(new Reservacion
                        {
                            CodigoReservacion = reader["CodigoReservacion"].ToString(),
                            RFC_Cliente = reader["RFC_Cliente"].ToString(),
                            ID_Hotel = Convert.ToInt32(reader["ID_Hotel"]),
                            ID_Habitacion = Convert.ToInt32(reader["ID_Habitacion"]),
                            FechaInicio = Convert.ToDateTime(reader["FechaInicio"]),
                            FechaFin = Convert.ToDateTime(reader["FechaFin"]),
                            Anticipo = Convert.ToSingle(reader["Anticipo"]),
                            Total = Convert.ToSingle(reader["Total"]),
                            Estatus = reader["Estatus"].ToString()
                        });
                    }
                }
            }
            return reservaciones;
        }



        public static List<Cliente> ObtenerClientesPorHotel(int idHotel)
        {
            string query = @"
    SELECT  
        r.CodigoReservacion,  
        c.RFC,  
        c.NombreCompleto,  
        c.CorreoElectronico,  
        c.Ciudad,  
        c.Estado,  
        c.Pais  
    FROM Reservacion r
    INNER JOIN Clientes c ON r.RFC_Cliente = c.RFC
    WHERE r.ID_Hotel = @IDHotel
    AND (r.Estatus IN ('Aceptada', 'Eliminada') OR r.Estatus IS NULL)
    ORDER BY r.CodigoReservacion ASC;";

            List<Cliente> clientes = new List<Cliente>();

            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IDHotel", idHotel);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            clientes.Add(new Cliente
                            {
                                RFC = reader["RFC"].ToString(),
                                NombreCompleto = reader["NombreCompleto"].ToString(),
                                CorreoElectronico = reader["CorreoElectronico"].ToString(),
                                Ciudad = reader["Ciudad"].ToString(),
                                Estado = reader["Estado"].ToString(),
                                Pais = reader["Pais"].ToString()
                            });
                        }
                    }
                }
            }
            return clientes;
        }



        public static bool ActualizarEstatusReservacion(string codigoReservacion, string nuevoEstatus)
        {
            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                string query = "UPDATE Reservacion SET Estatus = @Estatus WHERE CodigoReservacion = @CodigoReservacion";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Estatus", nuevoEstatus);
                    cmd.Parameters.AddWithValue("@CodigoReservacion", codigoReservacion);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // Ejemplo para filtrar en CancelacionReservacion.cs
        public static List<Reservacion> ObtenerReservacionesSinEstatus(int idHotel)
        {
            string query = @"
        SELECT 
            CodigoReservacion, 
            RFC_Cliente, 
            FechaInicio, 
            FechaFin 
        FROM Reservacion 
        WHERE Estatus IS NULL
            AND ID_Hotel = @IDHotel
            AND DATEDIFF(DAY, GETDATE(), FechaInicio) >= 3;";

            List<Reservacion> reservaciones = new List<Reservacion>();

            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IDHotel", idHotel);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reservaciones.Add(new Reservacion
                            {
                                CodigoReservacion = reader["CodigoReservacion"].ToString(),
                                RFC_Cliente = reader["RFC_Cliente"].ToString(),
                                FechaInicio = reader.GetDateTime(reader.GetOrdinal("FechaInicio")),
                                FechaFin = reader.GetDateTime(reader.GetOrdinal("FechaFin"))
                            });
                        }
                    }
                }
            }

            return reservaciones;
        }

        public static bool PuedeCancelarReservacion(string codigoReservacion)
        {
            string query = @"
        SELECT 
            DATEDIFF(DAY, GETDATE(), FechaInicio) AS DiasRestantes
        FROM Reservacion
        WHERE CodigoReservacion = @CodigoReservacion;";

            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CodigoReservacion", codigoReservacion);
                    object resultado = cmd.ExecuteScalar();

                    if (resultado != null && int.TryParse(resultado.ToString(), out int diasRestantes))
                    {
                        return diasRestantes >= 3;
                    }
                }
            }

            return false; // No se encontró la reservación o no cumple la condición
        }



    }
}

