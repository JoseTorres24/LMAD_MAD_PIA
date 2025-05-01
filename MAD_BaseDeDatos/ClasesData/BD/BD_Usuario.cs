using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ClasesData.BD
{
    public class BD_Usuario
    {
        public static int CrearUsuarioOperativo(Usuario nuevo, string contrasena)
        {
            int nuevoID;

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();

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

                string insertPass = "INSERT INTO Contraseñas (Contraseña, ID_Usuario) VALUES (@Contrasena, @ID)";
                using (SqlCommand cmd = new SqlCommand(insertPass, conexion))
                {
                    cmd.Parameters.AddWithValue("@Contrasena", contrasena);
                    cmd.Parameters.AddWithValue("@ID", nuevoID);
                    cmd.ExecuteNonQuery();
                }
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

        public static List<(int ID, string Nombre)> ObtenerUsuariosOperativos()
        {
            List<(int, string)> lista = new List<(int, string)>();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                string query = "SELECT ID_Usuario, NombreCompleto FROM Usuario WHERE Tipo = 'Operativo'";
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
    }
}
