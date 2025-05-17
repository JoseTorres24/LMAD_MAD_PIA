using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClasesData.BD
{
    public class BD_HotelesHabitacionesServicios
    {
        public static int GuardarHotel(Hoteles nuevoHotel)
        {
            int nuevoID = -1;

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                string insertHotel = @"
            INSERT INTO Hoteles (NombreHotel, Pais, Ciudad, Domicilio, NumeroPisos, NumeroHabitaciones, FechaInicioOperaciones, UsuarioRegistro)
            OUTPUT INSERTED.ID_Hotel
            VALUES (@Nombre, @Pais, @Ciudad, @Domicilio, @Pisos, @Habitaciones, @FechaInicio, @UsuarioRegistro)";

                using (SqlCommand cmd = new SqlCommand(insertHotel, conexion))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nuevoHotel.NombreHotel);
                    cmd.Parameters.AddWithValue("@Pais", nuevoHotel.Pais);
                    cmd.Parameters.AddWithValue("@Ciudad", nuevoHotel.Ciudad);
                    cmd.Parameters.AddWithValue("@Domicilio", nuevoHotel.Domicilio);
                    cmd.Parameters.AddWithValue("@Pisos", nuevoHotel.NumeroPisos);
                    cmd.Parameters.AddWithValue("@Habitaciones", nuevoHotel.NumeroHabitaciones);
                    cmd.Parameters.AddWithValue("@FechaInicio", nuevoHotel.FechaInicioOperaciones);
                    cmd.Parameters.AddWithValue("@UsuarioRegistro", Sesion.ID_Usuario);

                    try
                    {
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                            nuevoID = Convert.ToInt32(result);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al guardar el hotel: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            return nuevoID;
        }


        public static void GuardarHabitacion(Habitaciones nuevaHabitacion)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                string insertHabitacion = @"
            INSERT INTO Habitaciones (ID_Hotel, NumeroHabitacion, NivelPiso, TipoHabitacion, Capacidad, NumeroCamas, VistaHabitacion, Estado)
            VALUES (@ID_Hotel, @NumeroHabitacion, @NivelPiso, @Tipo, @Capacidad, @Camas, @Vista, @Estado)";

                using (SqlCommand cmd = new SqlCommand(insertHabitacion, conexion))
                {
                    cmd.Parameters.AddWithValue("@ID_Hotel", nuevaHabitacion.ID_Hotel);
                    cmd.Parameters.AddWithValue("@NumeroHabitacion", nuevaHabitacion.NumeroHabitacion);
                    cmd.Parameters.AddWithValue("@NivelPiso", nuevaHabitacion.PisoHabitacion);
                    cmd.Parameters.AddWithValue("@Tipo", nuevaHabitacion.TipoHabitacion);
                    cmd.Parameters.AddWithValue("@Capacidad", nuevaHabitacion.Capacidad);
                    cmd.Parameters.AddWithValue("@Camas", nuevaHabitacion.NumeroCamas);
                    cmd.Parameters.AddWithValue("@Vista", nuevaHabitacion.VistaHabitacion);
                    cmd.Parameters.AddWithValue("@Estado", nuevaHabitacion.Estado);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public static int GuardarServicio(Servicios nuevoServicio)
        {
            int nuevoID = -1;

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                string insertServicio = @"INSERT INTO Servicios (NombreServicio, Costo, Descripcion) 
                                  OUTPUT INSERTED.ID_Servicio 
                                  VALUES (@Nombre, @Costo, @Descripcion)";

                using (SqlCommand cmd = new SqlCommand(insertServicio, conexion))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nuevoServicio.NombreServicio ?? string.Empty);
                    cmd.Parameters.AddWithValue("@Costo", nuevoServicio.Costo);
                    cmd.Parameters.AddWithValue("@Descripcion", "Agregado servicio");

                    try
                    {
                        object resultado = cmd.ExecuteScalar();
                        if (resultado != null)
                        {
                            nuevoID = Convert.ToInt32(resultado);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al guardar el servicio: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            return nuevoID;
        }

        public static void AsociarServicioAHotel(HotelServicios Asociados)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                string insertHotelServicio = @"
            INSERT INTO Hotel_Servicios (ID_Hotel, ID_Servicio)
            VALUES (@ID_Hotel, @ID_Servicio)";

                using (SqlCommand cmd = new SqlCommand(insertHotelServicio, conexion))
                {
                    cmd.Parameters.AddWithValue("@ID_Hotel", Asociados.ID_Hotel);
                    cmd.Parameters.AddWithValue("@ID_Servicio", Asociados.ID_Servicio);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public static bool HotelExiste(Hoteles hotel)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                string consulta = @"
                SELECT COUNT(*) FROM Hoteles
                WHERE NombreHotel = @Nombre AND Pais = @Pais AND Ciudad = @Ciudad AND Domicilio = @Domicilio";

                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@Nombre", hotel.NombreHotel);
                    cmd.Parameters.AddWithValue("@Pais", hotel.Pais);
                    cmd.Parameters.AddWithValue("@Ciudad", hotel.Ciudad);
                    cmd.Parameters.AddWithValue("@Domicilio", hotel.Domicilio);

                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public static List<Hoteles> ObtenerHoteles()
        {
            List<Hoteles> lista = new List<Hoteles>();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                string consulta = @"SELECT ID_Hotel, NombreHotel, Pais, Ciudad, Domicilio, NumeroPisos, NumeroHabitaciones, FechaInicioOperaciones, UsuarioRegistro 
                                    FROM Hoteles;";

                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Hoteles hotel = new Hoteles
                        {
                            ID_Hotel = reader.GetInt32(0),
                            NombreHotel = reader.GetString(1),
                            Pais = reader.GetString(2),
                            Ciudad = reader.GetString(3),
                            Domicilio = reader.GetString(4),
                            NumeroPisos = reader.GetInt32(5),
                            NumeroHabitaciones = reader.GetInt32(6),
                            FechaInicioOperaciones = reader.GetDateTime(7),
                            UsuarioRegistro = reader.GetInt32(8)
                        };

                        lista.Add(hotel);
                    }
                }
            }

            return lista;
        }

        public static List<Hoteles> ObtenerHotelesPorFecha()
        {
            List<Hoteles> lista = new List<Hoteles>();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                string consulta = @"SELECT ID_Hotel, NombreHotel, Pais, Ciudad, Domicilio, NumeroPisos, NumeroHabitaciones, FechaInicioOperaciones, UsuarioRegistro 
                                    FROM Hoteles WHERE FechaInicioOperaciones = CAST(GETDATE() AS DATE);";

                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Hoteles hotel = new Hoteles
                        {
                            ID_Hotel = reader.GetInt32(0),
                            NombreHotel = reader.GetString(1),
                            Pais = reader.GetString(2),
                            Ciudad = reader.GetString(3),
                            Domicilio = reader.GetString(4),
                            NumeroPisos = reader.GetInt32(5),
                            NumeroHabitaciones = reader.GetInt32(6),
                            FechaInicioOperaciones = reader.GetDateTime(7),
                            UsuarioRegistro = reader.GetInt32(8)
                        };

                        lista.Add(hotel);
                    }
                }
            }

            return lista;
        }
        public static List<Habitaciones> ObtenerHabitaciones()
        {
            List<Habitaciones> lista = new List<Habitaciones>();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                string consulta = @"SELECT ID_Habitacion, ID_Hotel, NumeroHabitacion, NivelPiso, TipoHabitacion, Capacidad, NumeroCamas, VistaHabitacion, Estado 
                            FROM Habitaciones";

                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Habitaciones habitacion = new Habitaciones
                        {
                            ID_Habitacion = reader.GetInt32(0),
                            ID_Hotel = reader.GetInt32(1),
                            NumeroHabitacion = reader.GetInt32(2),
                            PisoHabitacion = reader.GetInt32(3),
                            TipoHabitacion = reader.GetString(4),
                            Capacidad = reader.GetInt32(5),
                            NumeroCamas = reader.GetInt32(6),
                            VistaHabitacion = reader.GetString(7),
                            Estado = reader.GetString(8)
                        };

                        lista.Add(habitacion);
                    }
                }
            }

            return lista;
        }
        public static List<Servicios> ObtenerServicios()
        {
            List<Servicios> lista = new List<Servicios>();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                string consulta = @"SELECT ID_Servicio, NombreServicio, Costo, Descripcion FROM Servicios";

                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Servicios servicio = new Servicios
                        {
                            ID_Servicio = reader.GetInt32(0),
                            NombreServicio = reader.GetString(1),
                            Costo = reader.GetDouble(2),  // ✅ Usa GetDouble() en lugar de GetFloat()
                            Descripcion = reader.GetString(3)
                        };

                        lista.Add(servicio);
                    }
                }
            }

            return lista;
        }



        public static void CargarHotelesEnListBox(ListBox listBox)
        {
            listBox.Items.Clear();
            List<Hoteles> hoteles = ObtenerHoteles();

            foreach (var hotel in hoteles)
            {
                listBox.Items.Add(new HotelItem
                {
                    ID_Hotel = hotel.ID_Hotel,
                    NombreHotel = hotel.NombreHotel
                });
            }
        }
        public static void CargarServiciosEnListBox(ListBox listBox)
        {
            listBox.Items.Clear();
            List<Servicios> servicios = ObtenerServicios(); // Método que obtiene los servicios de la BD

            foreach (var servicio in servicios)
            {
                listBox.Items.Add(new ServicioItem
                {
                    ID_Servicio = servicio.ID_Servicio,
                    NombreServicio = servicio.NombreServicio,
                    Costo = servicio.Costo
                });
            }
        }

        public static void CargarHabitacionesEnListBox(ListBox listBox)
        {
            listBox.Items.Clear();
            List<Habitaciones> habitaciones = ObtenerHabitaciones(); // Método que obtiene las habitaciones de la BD

            foreach (var habitacion in habitaciones)
            {
                listBox.Items.Add(new HabitacionItem
                {
                    ID_Habitacion = habitacion.ID_Habitacion,
                    ID_Hotel = habitacion.ID_Hotel,
                    NumeroHabitacion = habitacion.NumeroHabitacion,
                    NivelHabitacion = habitacion.TipoHabitacion,
                    Estado = habitacion.Estado
                });
            }
        }

        public static int ObtenerIDHotelPorNombre(string nombreHotel)
            {
                if (string.IsNullOrWhiteSpace(nombreHotel))
                {
                    MessageBox.Show("El nombre del hotel no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                int idHotel = -1;

                using (SqlConnection conexion = ConexionBD.ObtenerConexion())
                {
                    conexion.Open();
                    string consulta = "SELECT ID_Hotel FROM Hoteles WHERE NombreHotel = @NombreHotel";

                    using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                    {
                        cmd.Parameters.AddWithValue("@NombreHotel", nombreHotel);

                        object resultado = cmd.ExecuteScalar();
                        if (resultado != null)
                        {
                            idHotel = Convert.ToInt32(resultado);
                        }
                    }
                }

                return idHotel;
            }

        public static Hoteles ObtenerHotelPorID(int idHotel)
        {
            Hoteles hotel = null;

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                string consulta = @"SELECT ID_Hotel, NombreHotel, Pais, Ciudad, Domicilio, NumeroPisos, NumeroHabitaciones, FechaInicioOperaciones 
                            FROM Hoteles WHERE ID_Hotel = @ID_Hotel";

                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@ID_Hotel", idHotel);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            hotel = new Hoteles
                            {
                                ID_Hotel = reader.GetInt32(0),
                                NombreHotel = reader.GetString(1),
                                Pais = reader.GetString(2),
                                Ciudad = reader.GetString(3),
                                Domicilio = reader.GetString(4),
                                NumeroPisos = reader.GetInt32(5),
                                NumeroHabitaciones = reader.GetInt32(6),
                                FechaInicioOperaciones = reader.GetDateTime(7)
                            };
                        }
                    }
                }
            }

            return hotel;
        }






    }
}
