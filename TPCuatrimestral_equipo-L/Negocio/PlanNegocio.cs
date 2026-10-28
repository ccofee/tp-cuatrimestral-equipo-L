using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class PlanNegocio
    {
        public List<Plan> Listar()
        {
            List<Plan> lista = new List<Plan>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT Id, Descripcion, Precio, ClasesSemanales, Activo FROM PLANES WHERE Activo = 1");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Plan plan = new Plan();
                    plan.Id = (int)datos.Lector["Id"];
                    plan.Descripcion = (string)datos.Lector["Descripcion"];
                    plan.Precio = (decimal)datos.Lector["Precio"];
                    plan.ClasesSemanales = (int)datos.Lector["ClasesSemanales"];
                    plan.Activo = (bool)datos.Lector["Activo"];

                    lista.Add(plan);
                }

                return lista;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}
