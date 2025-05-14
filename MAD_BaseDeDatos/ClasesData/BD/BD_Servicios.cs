using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesData.BD
{
    public class BD_Servicios
    {
        public static string ObtenerNombreServicio(int idServicio)
        {
            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();

                string query = "SELECT NombreServicio FROM Servicios WHERE ID_Servicio = @ID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", idServicio);

                    object resultado = cmd.ExecuteScalar();

                    if (resultado != null && resultado != DBNull.Value)
                    {
                        return resultado.ToString();
                    }
                    else
                    {
                        return "Servicio no encontrado";
                    }
                }
            }
        }
    }
}
