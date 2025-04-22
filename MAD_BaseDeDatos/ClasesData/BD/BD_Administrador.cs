using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ClasesData.BD
{
    public class BD_Administrador
    {
        private string cadenaConexion = "Server=localhost\\SQLEXPRESS;Database=ManejoHoteles;Trusted_Connection=True;";


        public bool RegistrarAdministrador(UsuarioAdministrador admin, string contrasena)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string insertUsuario = @"INSERT INTO UsuarioHotelAdministrador 
                    (CorreoElectronico, NombreCompleto, NumeroNomina, FechaNacimiento, TelefonoCasa, TelefonoCelular, FechaRegistro) 
                    OUTPUT INSERTED.ID_Usuario
                    VALUES (@Correo, @Nombre, @Nomina, @FechaNac, @TelCasa, @TelCel, @FechaRegistro)";

                    SqlCommand cmdUsuario = new SqlCommand(insertUsuario, conexion);
                    cmdUsuario.Parameters.AddWithValue("@Correo", admin.CorreoElectronico);
                    cmdUsuario.Parameters.AddWithValue("@Nombre", admin.NombreCompleto);
                    cmdUsuario.Parameters.AddWithValue("@Nomina", admin.NumeroNomina);
                    cmdUsuario.Parameters.AddWithValue("@FechaNac", admin.FechaNacimiento);
                    cmdUsuario.Parameters.AddWithValue("@TelCasa", admin.TelefonoCasa);
                    cmdUsuario.Parameters.AddWithValue("@TelCel", admin.TelefonoCelular);
                    cmdUsuario.Parameters.AddWithValue("@FechaRegistro", admin.FechaRegistro);
                    int idUsuario = (int)cmdUsuario.ExecuteScalar();

                    string insertPass = "INSERT INTO Contraseñas (Contraseña, ID_Usuario)  VALUES (@Contrasena, @ID)";
                    SqlCommand cmdPass = new SqlCommand(insertPass, conexion);

                    cmdPass.Parameters.AddWithValue("@Contrasena", contrasena);
                    cmdPass.Parameters.AddWithValue("@ID", idUsuario);


                    cmdPass.ExecuteNonQuery();
                    

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
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM UsuarioHotelAdministrador";
                SqlCommand cmd = new SqlCommand(query, conn);
                int cantidad = (int)cmd.ExecuteScalar();
                return cantidad > 0;
            }
        }


    }
}
