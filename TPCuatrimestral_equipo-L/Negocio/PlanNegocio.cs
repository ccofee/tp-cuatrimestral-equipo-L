using System;
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
                datos.SetearConsulta("SELECT Id, Descripcion, Precio, ClasesSemanales, IncluyeMusculacion, IncluyeClases, Activo FROM PLANES WHERE Activo = 1");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Plan plan = new Plan();
                    plan.Id = (int)datos.Lector["Id"];
                    plan.Descripcion = (string)datos.Lector["Descripcion"];
                    plan.Precio = (decimal)datos.Lector["Precio"];
                    plan.ClasesSemanales = (int)datos.Lector["ClasesSemanales"];
                    plan.IncluyeMusculacion = (bool)datos.Lector["IncluyeMusculacion"];
                    plan.IncluyeClases = (bool)datos.Lector["IncluyeClases"];
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
            if (!nuevo.IncluyeMusculacion && !nuevo.IncluyeClases)
                throw new Exception("El plan debe incluir al menos musculación o clases grupales.");

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("INSERT INTO PLANES (Descripcion, Precio, ClasesSemanales, IncluyeMusculacion, IncluyeClases) VALUES (@descripcion, @precio, @clasesSemanales, @incluyeMusculacion, @incluyeClases)");
                datos.SetearParametro("@descripcion", nuevo.Descripcion);
                datos.SetearParametro("@precio", nuevo.Precio);
                datos.SetearParametro("@clasesSemanales", nuevo.ClasesSemanales);
                datos.SetearParametro("@incluyeMusculacion", nuevo.IncluyeMusculacion);
                datos.SetearParametro("@incluyeClases", nuevo.IncluyeClases);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Modificar(Plan plan)
        {
            if (!plan.IncluyeMusculacion && !plan.IncluyeClases)
                throw new Exception("El plan debe incluir al menos musculación o clases grupales.");

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE PLANES SET Descripcion = @descripcion, Precio = @precio, ClasesSemanales = @clasesSemanales, IncluyeMusculacion = @incluyeMusculacion, IncluyeClases = @incluyeClases WHERE Id = @id");
                datos.SetearParametro("@descripcion", plan.Descripcion);
                datos.SetearParametro("@precio", plan.Precio);
                datos.SetearParametro("@clasesSemanales", plan.ClasesSemanales);
                datos.SetearParametro("@incluyeMusculacion", plan.IncluyeMusculacion);
                datos.SetearParametro("@incluyeClases", plan.IncluyeClases);
                datos.SetearParametro("@id", plan.Id);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public bool TieneAlumnosActivos(int idPlan)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT Id FROM ALUMNOS WHERE Activo = 1 AND IdPlan = @id");
                datos.SetearParametro("@id", idPlan);
                datos.EjecutarLectura();

                return datos.Lector.Read();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void BajaLogica(int id)
        {
            if (TieneAlumnosActivos(id))
                throw new Exception("No se puede dar de baja el plan: tiene alumnos activos asociados.");

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE PLANES SET Activo = 0 WHERE Id = @id");
                datos.SetearParametro("@id", id);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public Plan BuscarPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT Id, Descripcion, Precio, ClasesSemanales, IncluyeMusculacion, IncluyeClases, Activo FROM PLANES WHERE Id = @id");
                datos.SetearParametro("@id", id);
                datos.EjecutarLectura();

                if (!datos.Lector.Read())
                    return null;

                Plan plan = new Plan();
                plan.Id = (int)datos.Lector["Id"];
                plan.Descripcion = (string)datos.Lector["Descripcion"];
                plan.Precio = (decimal)datos.Lector["Precio"];
                plan.ClasesSemanales = (int)datos.Lector["ClasesSemanales"];
                plan.IncluyeMusculacion = (bool)datos.Lector["IncluyeMusculacion"];
                plan.IncluyeClases = (bool)datos.Lector["IncluyeClases"];
                plan.Activo = (bool)datos.Lector["Activo"];

                return plan;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}
