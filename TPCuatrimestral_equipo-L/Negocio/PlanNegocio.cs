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

        public void Agregar(Plan nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("INSERT INTO PLANES (Descripcion, Precio, ClasesSemanales) VALUES (@descripcion, @precio, @clasesSemanales)");
                datos.SetearParametro("@descripcion", nuevo.Descripcion);
                datos.SetearParametro("@precio", nuevo.Precio);
                datos.SetearParametro("@clasesSemanales", nuevo.ClasesSemanales);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Modificar(Plan plan)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE PLANES SET Descripcion = @descripcion, Precio = @precio, ClasesSemanales = @clasesSemanales WHERE Id = @id");
                datos.SetearParametro("@descripcion", plan.Descripcion);
                datos.SetearParametro("@precio", plan.Precio);
                datos.SetearParametro("@clasesSemanales", plan.ClasesSemanales);
                datos.SetearParametro("@id", plan.Id);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}
