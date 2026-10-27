using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Negocio
{
    //se encarga de la conexion a la base y de ejecutar las consultas
    public class AccesoDatos
    {
        //objetos core de ADO.NET
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        //la capa de negocio lee los datos desde aca
        public SqlDataReader Lector { get { return lector; } }

        public AccesoDatos()
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["GimnasioDB"].ConnectionString;

            conexion = new SqlConnection(cadenaConexion);
            comando = new SqlCommand();
        }

        public void SetearConsulta(string consulta)
        {
            comando.CommandType = CommandType.Text;
            comando.CommandText = consulta;
        }

        //los valores siempre entran como parametro, nunca concatenados a la consulta
        public void SetearParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }

        //para los SELECT: deja el lector listo para recorrer
        public void EjecutarLectura()
        {
            comando.Connection = conexion;
            conexion.Open();
            lector = comando.ExecuteReader();
        }

        //para los INSERT, UPDATE y DELETE logico
        public void EjecutarAccion()
        {
            comando.Connection = conexion;
            conexion.Open();
            comando.ExecuteNonQuery();
        }

        //se llama siempre desde el finally de quien la usa
        public void CerrarConexion()
        {
            if (lector != null)
                lector.Close();

            conexion.Close();
        }
    }
}
