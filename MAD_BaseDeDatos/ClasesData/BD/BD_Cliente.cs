using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ClasesData.BD
{
    public class BD_Cliente
    {
        // Método para obtener todos los clientes
        public static List<Cliente> ObtenerClientes()
        {
            List<Cliente> lista = new List<Cliente>();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                string consulta = @"SELECT RFC, NombreCompleto, CorreoElectronico, TelefonoCasa, TelefonoCelular, 
                                           EstadoCivil, FechaNacimiento, Ciudad, Estado, Pais 
                                    FROM Clientes";
                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Cliente cliente = new Cliente
                        {
                            RFC = reader.GetString(0),
                            NombreCompleto = reader.GetString(1),
                            CorreoElectronico = reader.GetString(2),
                            TelefonoCasa = reader.GetInt64(3),
                            TelefonoCelular = reader.GetInt64(4),
                            EstadoCivil = reader.GetString(5),
                            FechaNacimiento = reader.GetDateTime(6),
                            Ciudad = reader.GetString(7),
                            Estado = reader.GetString(8),
                            Pais = reader.GetString(9)
                        };
                        lista.Add(cliente);
                    }
                }
            }
            return lista;
        }

        // Método para insertar un nuevo cliente
        // Ahora se inserta el RFC (tipo string) y se retorna el mismo RFC insertado.
        public static string CrearCliente(Cliente nuevoCliente)
        {
            string nuevoRFC = null;

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                string insertCliente = @"INSERT INTO Clientes (RFC, NombreCompleto, CorreoElectronico, TelefonoCasa, TelefonoCelular, 
                                                                EstadoCivil, FechaNacimiento, Ciudad, Estado, Pais)
                                         OUTPUT INSERTED.RFC
                                         VALUES (@RFC, @Nombre, @Correo, @TelefonoCasa, @TelefonoCelular, @EstadoCivil, 
                                                 @FechaNacimiento, @Ciudad, @Estado, @Pais)";

                using (SqlCommand cmd = new SqlCommand(insertCliente, conexion))
                {
                    cmd.Parameters.AddWithValue("@RFC", nuevoCliente.RFC);
                    cmd.Parameters.AddWithValue("@Nombre", nuevoCliente.NombreCompleto);
                    cmd.Parameters.AddWithValue("@Correo", nuevoCliente.CorreoElectronico);
                    cmd.Parameters.AddWithValue("@TelefonoCasa", nuevoCliente.TelefonoCasa);
                    cmd.Parameters.AddWithValue("@TelefonoCelular", nuevoCliente.TelefonoCelular);
                    cmd.Parameters.AddWithValue("@EstadoCivil", nuevoCliente.EstadoCivil);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", nuevoCliente.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@Ciudad", nuevoCliente.Ciudad);
                    cmd.Parameters.AddWithValue("@Estado", nuevoCliente.Estado);
                    cmd.Parameters.AddWithValue("@Pais", nuevoCliente.Pais);

                    nuevoRFC = Convert.ToString(cmd.ExecuteScalar());
                }
            }
            return nuevoRFC;
        }

        // Método para modificar un cliente
        public static void ModificarCliente(Cliente clienteModificado)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                string updateCliente = @"UPDATE Clientes
                                         SET NombreCompleto = @Nombre, 
                                             CorreoElectronico = @Correo, 
                                             TelefonoCasa = @TelefonoCasa, 
                                             TelefonoCelular = @TelefonoCelular,
                                             EstadoCivil = @EstadoCivil, 
                                             FechaNacimiento = @FechaNacimiento,
                                             Ciudad = @Ciudad, 
                                             Estado = @Estado, 
                                             Pais = @Pais
                                         WHERE RFC = @RFC";

                using (SqlCommand cmd = new SqlCommand(updateCliente, conexion))
                {
                    cmd.Parameters.AddWithValue("@RFC", clienteModificado.RFC);
                    cmd.Parameters.AddWithValue("@Nombre", clienteModificado.NombreCompleto);
                    cmd.Parameters.AddWithValue("@Correo", clienteModificado.CorreoElectronico);
                    cmd.Parameters.AddWithValue("@TelefonoCasa", clienteModificado.TelefonoCasa);
                    cmd.Parameters.AddWithValue("@TelefonoCelular", clienteModificado.TelefonoCelular);
                    cmd.Parameters.AddWithValue("@EstadoCivil", clienteModificado.EstadoCivil);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", clienteModificado.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@Ciudad", clienteModificado.Ciudad);
                    cmd.Parameters.AddWithValue("@Estado", clienteModificado.Estado);
                    cmd.Parameters.AddWithValue("@Pais", clienteModificado.Pais);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Método para eliminar un cliente
        public static void EliminarCliente(string RFC)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                string deleteCliente = "DELETE FROM Clientes WHERE RFC = @RFC";
                using (SqlCommand cmd = new SqlCommand(deleteCliente, conexion))
                {
                    cmd.Parameters.AddWithValue("@RFC", RFC);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Método para verificar si existe un cliente dado un RFC
        public static bool ClienteExiste(string RFC)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                string consulta = "SELECT COUNT(*) FROM Clientes WHERE RFC = @RFC";
                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@RFC", RFC);
                    int cantidad = Convert.ToInt32(cmd.ExecuteScalar());
                    return cantidad > 0;
                }
            }
        }
    }
}
    
