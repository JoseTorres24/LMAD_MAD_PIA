using System;
using System.Data.SqlClient;
using System.Collections.Generic;

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

                    // Insertar en tabla Usuario
                    string insertUsuario = @"INSERT INTO UsuarioHotelAdministrador 
                        (CorreoElectronico, NombreCompleto, NumeroNomina, FechaNacimiento, TelefonoCasa, TelefonoCelular, FechaHoraRegistro, Tipo) 
                        OUTPUT INSERTED.ID_Usuario
                        VALUES (@Correo, @Nombre, @Nomina, @FechaNac, @TelCasa, @TelCel, @FechaRegistro, 'Administrador')";

                    SqlCommand cmdUsuario = new SqlCommand(insertUsuario, conexion);
                    cmdUsuario.Parameters.AddWithValue("@Correo", admin.CorreoElectronico);
                    cmdUsuario.Parameters.AddWithValue("@Nombre", admin.NombreCompleto);
                    cmdUsuario.Parameters.AddWithValue("@Nomina", admin.NumeroNomina);
                    cmdUsuario.Parameters.AddWithValue("@FechaNac", admin.FechaNacimiento);
                    cmdUsuario.Parameters.AddWithValue("@TelCasa", admin.TelefonoCasa);
                    cmdUsuario.Parameters.AddWithValue("@TelCel", admin.TelefonoCelular);
                    cmdUsuario.Parameters.AddWithValue("@FechaRegistro", admin.FechaRegistro);

                    int idUsuario = (int)cmdUsuario.ExecuteScalar();

                    // Insertar contraseña
                    string insertPass = "INSERT INTO Contraseñas (Contraseña, ID_Usuario) VALUES (@Contrasena, @ID)";
                    SqlCommand cmdPass = new SqlCommand(insertPass, conexion);
                    cmdPass.Parameters.AddWithValue("@Contrasena", contrasena);
                    cmdPass.Parameters.AddWithValue("@ID", idUsuario);
                    cmdPass.ExecuteNonQuery();
                }

                return true;
            }
            catch
            {
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
