using System.Data.SqlClient;

namespace ClasesData.BD
{
    public static class ConexionBD
    {
        private static string cadenaConexion = "Server=localhost\\SQLEXPRESS;Database=ManejoHoteles;Trusted_Connection=True;";

        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadenaConexion);
        }
    }
}
