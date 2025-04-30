using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ClasesData.BD
{
    public class BD_Administrador
    {
        public bool RegistrarAdministrador(UsuarioAdministrador admin, string contrasena)
        {
            try
            {
                using (SqlConnection conexion = ConexionBD.ObtenerConexion())
                {
                    conexion.Open();

                    string insertUsuario = @"INSERT INTO UsuarioHotelAdministrador 
                    (CorreoElectronico, NombreCompleto, NumeroNomina, FechaNacimiento, TelefonoCasa, TelefonoCelular, FechaRegistro) 
                    OUTPUT INSERTED.ID_Usuario
                    VALUES (@Correo, @Nombre, @Nomina, @FechaNac, @TelCasa, @TelCel, @FechaRegistro)";

                    using (SqlCommand cmdUsuario = new SqlCommand(insertUsuario, conexion))
                    {
                        cmdUsuario.Parameters.AddWithValue("@Correo", admin.CorreoElectronico);
                        cmdUsuario.Parameters.AddWithValue("@Nombre", admin.NombreCompleto);
                        cmdUsuario.Parameters.AddWithValue("@Nomina", admin.NumeroNomina);
                        cmdUsuario.Parameters.AddWithValue("@FechaNac", admin.FechaNacimiento);
                        cmdUsuario.Parameters.AddWithValue("@TelCasa", admin.TelefonoCasa);
                        cmdUsuario.Parameters.AddWithValue("@TelCel", admin.TelefonoCelular);
                        cmdUsuario.Parameters.AddWithValue("@FechaRegistro", admin.FechaRegistro);

                        int idUsuario = (int)cmdUsuario.ExecuteScalar();

                        string insertPass = "INSERT INTO ContraseñasHotelAdministrador (Contraseña, ID_Usuario) VALUES (@Contrasena, @ID)";
                        using (SqlCommand cmdPass = new SqlCommand(insertPass, conexion))
                        {
                            cmdPass.Parameters.AddWithValue("@Contrasena", contrasena);
                            cmdPass.Parameters.AddWithValue("@ID", idUsuario);
                            cmdPass.ExecuteNonQuery();
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar administrador: " + ex.Message, "Error BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool ExisteAdministrador()
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                string query = "SELECT COUNT(*) FROM UsuarioHotelAdministrador";
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    int cantidad = (int)cmd.ExecuteScalar();
                    return cantidad > 0;
                }
            }
        }


        public bool IniciarSesionAdministrador(string correo, string contrasena)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();

                string query = @"
            SELECT u.ID_Usuario, u.CorreoElectronico, c.Contraseña
            FROM UsuarioHotelAdministrador u
            JOIN ContraseñasHotelAdministrador c ON u.ID_Usuario = c.ID_Usuario
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





    }
}
