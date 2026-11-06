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
                //cambie a inner, habia quedado mal de una prueba anterior
                string consulta = @"SELECT A.Id, A.IdUsuario, A.DNI, A.Nombre, A.Apellido, A.Email, A.Telefono, 
                                           A.FechaNacimiento, A.IdPlan, P.Descripcion AS NombrePlan, P.Precio, 
                                           P.ClasesSemanales, A.FechaVencimientoPlan, A.Activo 
                                    FROM ALUMNOS A 
                                    INNER JOIN PLANES P ON A.IdPlan = P.Id 
                                    WHERE A.Activo = 1";

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
                    aux.FechaVencimientoPlan = (DateTime)datos.Lector["FechaVencimientoPlan"];
                    aux.Activo = (bool)datos.Lector["Activo"];

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
                    aux.Plan.Descripcion = (string)datos.Lector["NombrePlan"];
                    aux.Plan.Precio = (decimal)datos.Lector["Precio"];
                    aux.Plan.ClasesSemanales = (int)datos.Lector["ClasesSemanales"];

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


        public void agregar(Alumno nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"INSERT INTO ALUMNOS (IdUsuario, DNI, Nombre, Apellido, Email, Telefono, 
                                                        FechaNacimiento, IdPlan, FechaVencimientoPlan, Activo) 
                                    VALUES (@idUsuario, @dni, @nombre, @apellido, @email, @telefono, 
                                            @fechaNacimiento, @idPlan, @fechaVencimientoPlan, 1)";

                datos.SetearConsulta(consulta);

                //FK usuario puede ser NULL
                if (nuevo.Usuario != null && nuevo.Usuario.Id > 0)
                    datos.SetearParametro("@idUsuario", nuevo.Usuario.Id);
                else
                    datos.SetearParametro("@idUsuario", DBNull.Value);

                //campos obligatorios   
                datos.SetearParametro("@dni", nuevo.DNI);
                datos.SetearParametro("@nombre", nuevo.Nombre);
                datos.SetearParametro("@apellido", nuevo.Apellido);
                datos.SetearParametro("@idPlan", nuevo.Plan.Id);
                datos.SetearParametro("@fechaVencimientoPlan", nuevo.FechaVencimientoPlan);

                //campos opcionales
                if (!string.IsNullOrEmpty(nuevo.Email))
                    datos.SetearParametro("@email", nuevo.Email);
                else
                    datos.SetearParametro("@email", DBNull.Value);

                if (!string.IsNullOrEmpty(nuevo.Telefono))
                    datos.SetearParametro("@telefono", nuevo.Telefono);
                else
                    datos.SetearParametro("@telefono", DBNull.Value);

                if (nuevo.FechaNacimiento > DateTime.MinValue)
                    datos.SetearParametro("@fechaNacimiento", nuevo.FechaNacimiento);
                else
                    datos.SetearParametro("@fechaNacimiento", DBNull.Value);

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

        public void modificar(Alumno alumno)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"UPDATE ALUMNOS SET 
                                    IdUsuario = @idUsuario, 
                                    DNI = @dni, 
                                    Nombre = @nombre, 
                                    Apellido = @apellido, 
                                    Email = @email, 
                                    Telefono = @telefono, 
                                    FechaNacimiento = @fechaNacimiento, 
                                    IdPlan = @idPlan, 
                                    FechaVencimientoPlan = @fechaVencimientoPlan 
                                   WHERE Id = @id";

                datos.SetearConsulta(consulta);

                datos.SetearParametro("@id", alumno.Id);

                //FK usuario puede ser NULL
                if (alumno.Usuario != null && alumno.Usuario.Id > 0)
                    datos.SetearParametro("@idUsuario", alumno.Usuario.Id);
                else
                    datos.SetearParametro("@idUsuario", DBNull.Value);

                //obligatorios
                datos.SetearParametro("@dni", alumno.DNI);
                datos.SetearParametro("@nombre", alumno.Nombre);
                datos.SetearParametro("@apellido", alumno.Apellido);
                datos.SetearParametro("@idPlan", alumno.Plan.Id);
                datos.SetearParametro("@fechaVencimientoPlan", alumno.FechaVencimientoPlan);

                //opcionales
                if (!string.IsNullOrEmpty(alumno.Email))
                    datos.SetearParametro("@email", alumno.Email);
                else
                    datos.SetearParametro("@email", DBNull.Value);

                if (!string.IsNullOrEmpty(alumno.Telefono))
                    datos.SetearParametro("@telefono", alumno.Telefono);
                else
                    datos.SetearParametro("@telefono", DBNull.Value);

                if (alumno.FechaNacimiento > DateTime.MinValue)
                    datos.SetearParametro("@fechaNacimiento", alumno.FechaNacimiento);
                else
                    datos.SetearParametro("@fechaNacimiento", DBNull.Value);

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


    }


}
