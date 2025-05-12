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
            string mensajeError;

            if (!ValidarUsuarioOperativo(nuevo, contrasena, out mensajeError))
            {
                MessageBox.Show(mensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return nuevoID;
            }

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();

                // Insertar el usuario en la tabla Usuario
                string insertUser = @"
                INSERT INTO Usuario (CorreoElectronico, NombreCompleto, NumeroNomina, FechaNacimiento, TelefonoCasa, TelefonoCelular, FechaRegistro,Tipo,ID_UsuarioRegistro)
                OUTPUT INSERTED.ID_Usuario
                VALUES (@Correo, @Nombre, @Nomina, @FechaNac, @TelCasa, @TelCel, @FechaReg,@Tipo,@IDRegistro)";

                using (SqlCommand cmd = new SqlCommand(insertUser, conexion))
                {
                    cmd.Parameters.AddWithValue("@Correo", nuevo.CorreoElectronico);
                    cmd.Parameters.AddWithValue("@Nombre", nuevo.NombreCompleto);
                    cmd.Parameters.AddWithValue("@Nomina", nuevo.NumeroNomina);
                    cmd.Parameters.AddWithValue("@FechaNac", nuevo.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@TelCasa", nuevo.TelefonoCasa);
                    cmd.Parameters.AddWithValue("@TelCel", nuevo.TelefonoCelular);
                    cmd.Parameters.AddWithValue("@FechaReg", nuevo.FechaRegistro);
                    cmd.Parameters.AddWithValue("@Tipo", "Operativo");
                    cmd.Parameters.AddWithValue("@IDRegistro", nuevo.ID_UsuarioRegistro);

                    nuevoID = (int)cmd.ExecuteScalar();
                }

                // Insertar la contraseña solo si el usuario fue creado correctamente
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
                string mensajeError = string.Empty;
                conexion.Open();

                if (!ValidarModificacionUsuarioOperativo(actualizado, nuevaContrasena,out mensajeError))
                {
                    MessageBox.Show(mensajeError);
                    return;
                }
                // Si el correo es válido, proceder con la actualización
                // Obtener el valor actual de "Tipo" para asegurarnos de que no quede NULL
                string obtenerTipo = "SELECT ISNULL(Tipo, 'Operativo') FROM Usuario WHERE ID_Usuario = @ID";
                string tipoUsuario;
                using (SqlCommand cmdTipo = new SqlCommand(obtenerTipo, conexion))
                {
                    cmdTipo.Parameters.AddWithValue("@ID", actualizado.ID_Usuario);
                    tipoUsuario = cmdTipo.ExecuteScalar()?.ToString() ?? "Operativo"; // Asignar 'Operativo' si está NULL
                }
                string updateUser = @"
                UPDATE Usuario
                SET CorreoElectronico = @Correo, NombreCompleto = @Nombre, NumeroNomina = @Nomina,
                FechaNacimiento = @FechaNac, TelefonoCasa = @TelCasa, TelefonoCelular = @TelCel, Tipo = @Tipo
                WHERE ID_Usuario = @ID";



                using (SqlCommand cmd = new SqlCommand(updateUser, conexion))
                {
                    cmd.Parameters.AddWithValue("@Correo", actualizado.CorreoElectronico);
                    cmd.Parameters.AddWithValue("@Nombre", actualizado.NombreCompleto);
                    cmd.Parameters.AddWithValue("@Nomina", actualizado.NumeroNomina);
                    cmd.Parameters.AddWithValue("@FechaNac", actualizado.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@TelCasa", actualizado.TelefonoCasa);
                    cmd.Parameters.AddWithValue("@TelCel", actualizado.TelefonoCelular);
                    cmd.Parameters.AddWithValue("@Tipo", tipoUsuario);
                    cmd.Parameters.AddWithValue("@ID", actualizado.ID_Usuario);
                    cmd.ExecuteNonQuery();
                }

                // Insertar nueva contraseña si se ha cambiado
                string insertPass = "INSERT INTO Contraseñas (Contraseña, ID_Usuario) VALUES (@Contrasena, @ID)";
                using (SqlCommand cmd = new SqlCommand(insertPass, conexion))
                {
                    cmd.Parameters.AddWithValue("@Contrasena", nuevaContrasena);
                    cmd.Parameters.AddWithValue("@ID", actualizado.ID_Usuario);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Usuario modificado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);


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
                string query = @"
            SELECT TOP 1 Contraseña 
            FROM Contraseñas 
            WHERE ID_Usuario = @ID 
            ORDER BY ID_Contraseña DESC";
        
        using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@ID", idUsuario);
                    contraseña = cmd.ExecuteScalar()?.ToString();
                }
            }

            return contraseña;
        }

        public bool IniciarSesion(string correo, string contrasena)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();

                string query = @"
            SELECT u.ID_Usuario 
            FROM Usuario u
            JOIN (
                SELECT TOP 1 ID_Usuario, Contraseña 
                FROM Contraseñas 
                WHERE ID_Usuario IN (SELECT ID_Usuario FROM Usuario WHERE CorreoElectronico = @Correo) 
                ORDER BY ID_Contraseña DESC
            ) AS c ON u.ID_Usuario = c.ID_Usuario
            WHERE u.CorreoElectronico = @Correo AND c.Contraseña = @Contrasena";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@Correo", correo);
                    cmd.Parameters.AddWithValue("@Contrasena", contrasena);

                    object resultado = cmd.ExecuteScalar();
                    if (resultado != null)
                    {
                        Sesion.ID_Usuario = (int)resultado;
                        return true;
                    }
                    return false;
                }
            }
        }
        // Este sirve para el insert
        public static bool ValidarUsuarioOperativo(Usuario nuevo, string contrasena, out string mensajeError)
        {
            mensajeError = string.Empty;

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();

                // Verificar si el número de nómina ya existe en Usuario o UsuarioHotelAdministrador
                string checkNomina = @"
            SELECT
            (SELECT COUNT(*) FROM Usuario WHERE NumeroNomina = @Nomina)
            +
            (SELECT COUNT(*) FROM UsuarioHotelAdministrador WHERE NumeroNomina = @Nomina)";
                using (SqlCommand cmdCheckNomina = new SqlCommand(checkNomina, conexion))
                {
                    cmdCheckNomina.Parameters.AddWithValue("@Nomina", nuevo.NumeroNomina);
                    int countNomina = Convert.ToInt32(cmdCheckNomina.ExecuteScalar());

                    if (countNomina > 0)
                    {
                        mensajeError = "El número de nómina ya está registrado.";
                        return false;
                    }
                }

                // Verificar si el correo ya existe en Usuario o UsuarioHotelAdministrador
                string checkCorreo = @"SELECT
            (SELECT COUNT(*) FROM Usuario WHERE CorreoElectronico = @Correo)
            +
            (SELECT COUNT(*) FROM UsuarioHotelAdministrador WHERE CorreoElectronico = @Correo)";
                using (SqlCommand cmdCheckCorreo = new SqlCommand(checkCorreo, conexion))
                {
                    cmdCheckCorreo.Parameters.AddWithValue("@Correo", nuevo.CorreoElectronico);
                    int countCorreo = Convert.ToInt32(cmdCheckCorreo.ExecuteScalar());

                    if (countCorreo > 0)
                    {
                        mensajeError = "El correo ya está registrado. Debe usar uno diferente.";
                        return false;
                    }
                }

            }

            // Si todas las verificaciones pasan, el usuario es válido para ser creado
            return true;
        }
        //Esto es para modificar chingao
    public static bool ValidarModificacionUsuarioOperativo(Usuario actualizado, string nuevaContrasena, out string mensajeError)
    {
        mensajeError = string.Empty;

        using (SqlConnection conexion = ConexionBD.ObtenerConexion())
        {
            conexion.Open();

            // Verificar si el número de nómina ya existe en otro usuario
            string checkNomina = @"
            SELECT
            (SELECT COUNT(*) FROM Usuario WHERE NumeroNomina = @Nomina AND ID_Usuario != @ID)
            +
            (SELECT COUNT(*) FROM UsuarioHotelAdministrador WHERE NumeroNomina = @Nomina)";
            using (SqlCommand cmdCheckNomina = new SqlCommand(checkNomina, conexion))
            {
                cmdCheckNomina.Parameters.AddWithValue("@Nomina", actualizado.NumeroNomina);
                cmdCheckNomina.Parameters.AddWithValue("@ID", actualizado.ID_Usuario);

                int countNomina = Convert.ToInt32(cmdCheckNomina.ExecuteScalar());
                if (countNomina > 0)
                {
                    mensajeError = "El número de nómina ya está en uso por otro usuario o administrador.";
                    return false;
                }
            }

            // Verificar si el correo ya existe en otro usuario
            string checkCorreo = @"
            SELECT
            (SELECT COUNT(*) FROM Usuario WHERE CorreoElectronico = @Correo AND ID_Usuario != @ID)
            +
            (SELECT COUNT(*) FROM UsuarioHotelAdministrador WHERE CorreoElectronico = @Correo AND ID_Usuario != @ID)";
            using (SqlCommand cmdCheckCorreo = new SqlCommand(checkCorreo, conexion))
            {
                cmdCheckCorreo.Parameters.AddWithValue("@Correo", actualizado.CorreoElectronico);
                cmdCheckCorreo.Parameters.AddWithValue("@ID", actualizado.ID_Usuario);

                int countCorreo = Convert.ToInt32(cmdCheckCorreo.ExecuteScalar());
                if (countCorreo > 0)
                {
                    mensajeError = "El correo ya está en uso por otro usuario o administrador.";
                    return false;
                }
            }

            // Verificar si la nueva contraseña ya fue usada
            string checkPass = @"
                SELECT
                (SELECT COUNT(*) FROM Contraseñas WHERE Contraseña = @Contrasena)
                +
                (SELECT COUNT(*) FROM ContraseñasHotelAdministrador WHERE Contraseña = @Contrasena)";
            using (SqlCommand cmdCheckPass = new SqlCommand(checkPass, conexion))
            {
                    cmdCheckPass.Parameters.AddWithValue("@Contrasena", nuevaContrasena);
                    int countPass = Convert.ToInt32(cmdCheckPass.ExecuteScalar());

                    if (countPass > 0)
                    {
                        mensajeError = "La contraseña ya ha sido utilizada. Por favor, elige una nueva.";
                        return false;
                    }
                }
            }

            return true;
        }



    }
}
