using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesData.BD
{
    public class BD_Check
    {
        public static bool InsertarCheckIn(CheckIn check)
        {
            try
            {
                using (SqlConnection conn = ConexionBD.ObtenerConexion())
                {
                    conn.Open();
                    string query = @"INSERT INTO CheckIn (ID_Reservacion, UsuarioRegistro, FechaCheckIn, EstadoEntrada)
                             VALUES (@ID_Reservacion, @UsuarioRegistro, @FechaCheckIn, @EstadoEntrada)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID_Reservacion", check.ID_Reservacion);
                        cmd.Parameters.AddWithValue("@UsuarioRegistro", check.UsuarioRegistro);
                        cmd.Parameters.AddWithValue("@FechaCheckIn", check.FechaCheckIn);
                        cmd.Parameters.AddWithValue("@EstadoEntrada", check.EstadoEntrada);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

    }
}
