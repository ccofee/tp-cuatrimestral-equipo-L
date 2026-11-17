using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ActividadNegocio
    {
        public List<Actividad> listar()
        {

            List<Actividad> lista = new List<Actividad>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT Id, Nombre, Descripcion, Activo FROM ACTIVIDADES WHERE Activo = 1 ORDER BY Nombre");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Actividad aux = new Actividad();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];

                    if (!(datos.Lector["Descripcion"] is DBNull))
                        aux.Descripcion = (string)datos.Lector["Descripcion"];

                    aux.Activo = (bool)datos.Lector["Activo"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }


        public void agregar(Actividad nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.SetearConsulta("INSERT INTO ACTIVIDADES (Nombre, Descripcion, Activo) VALUES (@nombre, @descripcion, 1)");
                datos.SetearParametro("@nombre", nuevo.Nombre);

                if (!string.IsNullOrEmpty(nuevo.Descripcion))
                    datos.SetearParametro("@descripcion", nuevo.Descripcion);
                else
                    datos.SetearParametro("@descripcion", DBNull.Value);

                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }



        public void modificar(Actividad actividad)
        {

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE ACTIVIDADES SET Nombre = @nombre, Descripcion = @descripcion WHERE Id = @id");
                datos.SetearParametro("@nombre", actividad.Nombre);

                if (!string.IsNullOrEmpty(actividad.Descripcion))
                    datos.SetearParametro("@descripcion", actividad.Descripcion);
                else
                    datos.SetearParametro("@descripcion", DBNull.Value);

                datos.SetearParametro("@id", actividad.Id);


                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }


        public void eliminarLogico(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE ACTIVIDADES SET Activo = 0 WHERE Id = @id");
                datos.SetearParametro("@id", id);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        //para el ALTA
        public bool existeNombre(string nombre)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT Id FROM ACTIVIDADES WHERE Nombre = @nombre AND Activo = 1");
                datos.SetearParametro("@nombre", nombre);
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        //para la MODIFICACION
        public bool existeNombre(string nombre, int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT Id FROM ACTIVIDADES WHERE Nombre = @nombre AND Activo = 1 AND Id != @id");
                datos.SetearParametro("@nombre", nombre);
                datos.SetearParametro("@id", id);
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }








    }
}