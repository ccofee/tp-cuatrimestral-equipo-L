using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class AlumnoNegocio
    {
        public List<Alumno> listar()
        {
            List<Alumno> lista = new List<Alumno>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                //consulta con left en vez de inner para alumnos sin plan asociadpo
                string consulta = "SELECT A.Id, A.IdUsuario, A.DNI, A.Nombre, A.Apellido, A.Email, A.Telefono, A.FechaNacimiento, A.IdPlan, P.Descripcion AS NombrePlan, P.Precio, P.ClasesSemanales, A.FechaVencimientoPlan, A.Activo FROM ALUMNOS A LEFT JOIN PLANES P ON A.IdPlan = P.Id";

                datos.SetearConsulta(consulta);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    //alumno
                    Alumno aux = new Alumno();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.DNI = (string)datos.Lector["DNI"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Apellido = (string)datos.Lector["Apellido"];


                    if (!(datos.Lector["Email"] is DBNull))
                        aux.Email = (string)datos.Lector["Email"];

                    if (!(datos.Lector["Telefono"] is DBNull))
                        aux.Telefono = (string)datos.Lector["Telefono"];

                    if (!(datos.Lector["FechaNacimiento"] is DBNull))
                        aux.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];

                    if (!(datos.Lector["IdUsuario"] is DBNull))
                    {
                        aux.Usuario = new Usuario();
                        aux.Usuario.Id = (int)datos.Lector["IdUsuario"];
                    }

                    //plan
                    aux.Plan = new Plan();
                    aux.Plan.Id = (int)datos.Lector["IdPlan"];
                    if (!(datos.Lector["NombrePlan"] is DBNull))
                        aux.Plan.Descripcion = (string)datos.Lector["NombrePlan"];
                    if (!(datos.Lector["Precio"] is DBNull))
                        aux.Plan.Precio = (decimal)datos.Lector["Precio"];
                    if (!(datos.Lector["ClasesSemanales"] is DBNull))
                        aux.Plan.ClasesSemanales = (int)datos.Lector["ClasesSemanales"];

                    aux.FechaVencimientoPlan = (DateTime)datos.Lector["FechaVencimientoPlan"];
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
    }
}
