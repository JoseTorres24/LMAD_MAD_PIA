using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ClasesData.BD
{
    public class BD_Usuario
    {
        public static int CrearUsuarioOperativo(Usuario nuevo, string contrasena)
        {
            int nuevoID = -1; // Valor por defecto si no se inserta

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();

                // Verificar si el número de nómina ya existe
                string checkNomina = "SELECT COUNT(*) FROM Usuario WHERE NumeroNomina = @Nomina";
                using (SqlCommand cmdCheck = new SqlCommand(checkNomina, conexion))
                {
                    cmdCheck.Parameters.AddWithValue("@Nomina", nuevo.NumeroNomina);
                    int count = (int)cmdCheck.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("El número de nómina ya está registrado. No se puede crear el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return nuevoID; // Retorna -1 indicando que no se insertó
                    }
                }

                // Si la nómina es única, proceder con el INSERT
                string insertUser = @"
            INSERT INTO Usuario (CorreoElectronico, NombreCompleto, NumeroNomina, FechaNacimiento, TelefonoCasa, TelefonoCelular, FechaRegistro, Tipo, ID_UsuarioRegistro)
            OUTPUT INSERTED.ID_Usuario
            VALUES (@Correo, @Nombre, @Nomina, @FechaNac, @TelCasa, @TelCel, @FechaReg, 'Operativo', @IDRegistro)";

                using (SqlCommand cmd = new SqlCommand(insertUser, conexion))
                {
                    cmd.Parameters.AddWithValue("@Correo", nuevo.CorreoElectronico);
                    cmd.Parameters.AddWithValue("@Nombre", nuevo.NombreCompleto);
                    cmd.Parameters.AddWithValue("@Nomina", nuevo.NumeroNomina);
                    cmd.Parameters.AddWithValue("@FechaNac", nuevo.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@TelCasa", nuevo.TelefonoCasa);
                    cmd.Parameters.AddWithValue("@TelCel", nuevo.TelefonoCelular);
                    cmd.Parameters.AddWithValue("@FechaReg", nuevo.FechaRegistro);
                    cmd.Parameters.AddWithValue("@IDRegistro", nuevo.ID_UsuarioRegistro);

                    nuevoID = (int)cmd.ExecuteScalar();
                }

                // Insertar la contraseña solo si el usuario fue creado
                if (nuevoID > 0)
                {
                    string insertPass = "INSERT INTO Contraseñas (Contraseña, ID_Usuario) VALUES (@Contrasena, @ID)";
                    using (SqlCommand cmd = new SqlCommand(insertPass, conexion))
                    {
                        cmd.Parameters.AddWithValue("@Contrasena", contrasena);
                        cmd.Parameters.AddWithValue("@ID", nuevoID);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Usuario creado exitosamente.");
                
            }

            return nuevoID;
        }

        public static void EliminarUsuarioOperativo(int idUsuario)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();

                string deletePass = "DELETE FROM Contraseñas WHERE ID_Usuario = @ID";
                using (SqlCommand cmd = new SqlCommand(deletePass, conexion))
                {
                    cmd.Parameters.AddWithValue("@ID", idUsuario);
                    cmd.ExecuteNonQuery();
                }

                string deleteUser = "DELETE FROM Usuario WHERE ID_Usuario = @ID";
                using (SqlCommand cmd = new SqlCommand(deleteUser, conexion))
                {
                    cmd.Parameters.AddWithValue("@ID", idUsuario);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void ModificarUsuarioOperativo(Usuario actualizado, string nuevaContrasena)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();

                string updateUser = @"
                    UPDATE Usuario
                    SET CorreoElectronico = @Correo, NombreCompleto = @Nombre, NumeroNomina = @Nomina,
                        FechaNacimiento = @FechaNac, TelefonoCasa = @TelCasa, TelefonoCelular = @TelCel
                    WHERE ID_Usuario = @ID";

                using (SqlCommand cmd = new SqlCommand(updateUser, conexion))
                {
                    cmd.Parameters.AddWithValue("@Correo", actualizado.CorreoElectronico);
                    cmd.Parameters.AddWithValue("@Nombre", actualizado.NombreCompleto);
                    cmd.Parameters.AddWithValue("@Nomina", actualizado.NumeroNomina);
                    cmd.Parameters.AddWithValue("@FechaNac", actualizado.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@TelCasa", actualizado.TelefonoCasa);
                    cmd.Parameters.AddWithValue("@TelCel", actualizado.TelefonoCelular);
                    cmd.Parameters.AddWithValue("@ID", actualizado.ID_Usuario);
                    cmd.ExecuteNonQuery();
                }

                // Insertar nueva contraseña
                string insertPass = "INSERT INTO Contraseñas (Contraseña, ID_Usuario) VALUES (@Contrasena, @ID)";
                using (SqlCommand cmd = new SqlCommand(insertPass, conexion))
                {
                    cmd.Parameters.AddWithValue("@Contrasena", nuevaContrasena);
                    cmd.Parameters.AddWithValue("@ID", actualizado.ID_Usuario);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        //Falta corregir
        public static List<(int ID, string Nombre)> ObtenerUsuariosOperativos()
        {
            List<(int, string)> lista = new List<(int, string)>();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                string query = "SELECT ID_Usuario, NombreCompleto FROM Usuario";
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string nombre = reader.GetString(1);
                        lista.Add((id, nombre));
                    }
                }
            }

            return lista;
        }
        public static Usuario ObtenerUsuarioPorID(int idUsuario)
        {
            Usuario usuario = null;

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                string query = @"SELECT CorreoElectronico, NombreCompleto, NumeroNomina, FechaNacimiento, TelefonoCasa, TelefonoCelular 
                         FROM Usuario WHERE ID_Usuario = @ID";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@ID", idUsuario);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuario = new Usuario
                            {
                                ID_Usuario = idUsuario,
                                CorreoElectronico = reader.GetString(0),
                                NombreCompleto = reader.GetString(1),
                                NumeroNomina = reader.GetInt64(2),
                                FechaNacimiento = reader.GetDateTime(3),
                                TelefonoCasa = reader.GetInt64(4),
                                TelefonoCelular = reader.GetInt64(5)
                            };
                        }
                    }
                }
            }

            return usuario;
        }

        public static string ObtenerContraseñaPorID(int idUsuario)
        {
            string contraseña = "";

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                string query = "SELECT Contraseña FROM Contraseñas WHERE ID_Usuario = @ID";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@ID", idUsuario);
                    contraseña = cmd.ExecuteScalar()?.ToString();
                }
            }

            return contraseña;
        }


    }
}
