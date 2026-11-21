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

                string consulta = @"SELECT A.Id, A.IdUsuario, A.DNI, A.Nombre, A.Apellido, A.Email, A.Telefono, 
                                           A.FechaNacimiento, A.IdPlan, P.Descripcion AS NombrePlan, P.Precio, 
                                           P.ClasesSemanales, P.IncluyeMusculacion, P.IncluyeClases, 
                                           A.FechaVencimientoPlan, A.FechaAlta, A.Activo 
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
                    aux.FechaAlta = (DateTime)datos.Lector["FechaAlta"];
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
                    aux.Plan.IncluyeMusculacion = (bool)datos.Lector["IncluyeMusculacion"];
                    aux.Plan.IncluyeClases = (bool)datos.Lector["IncluyeClases"];

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


        public void eliminarLogico(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE ALUMNOS SET Activo = 0 WHERE Id = @id");
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


        public bool existeDni(string dni)
        {

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT Id FROM ALUMNOS WHERE DNI = @dni");
                datos.SetearParametro("@dni", dni);
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
        public bool existeDni(string dni, int id)
        {

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT Id FROM ALUMNOS WHERE DNI = @dni AND Id != @id");
                datos.SetearParametro("@dni", dni);
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


        public Alumno buscarPorIdUsuario(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"SELECT A.Id, A.IdUsuario, A.DNI, A.Nombre, A.Apellido, A.Email, A.Telefono, 
                                            A.FechaNacimiento, A.IdPlan, P.Descripcion AS NombrePlan, P.Precio, 
                                            P.ClasesSemanales, P.IncluyeMusculacion, P.IncluyeClases, 
                                            A.FechaVencimientoPlan, A.FechaAlta, A.Activo 
                                    FROM ALUMNOS A 
                                    INNER JOIN PLANES P ON A.IdPlan = P.Id 
                                    WHERE A.IdUsuario = @idUsuario AND A.Activo = 1";

                datos.SetearConsulta(consulta);
                datos.SetearParametro("@idUsuario", idUsuario);
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                {
                    Alumno aux = new Alumno();

                    //obligatorias 
                    aux.Id = (int)datos.Lector["Id"];
                    aux.DNI = (string)datos.Lector["DNI"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Apellido = (string)datos.Lector["Apellido"];
                    aux.FechaVencimientoPlan = (DateTime)datos.Lector["FechaVencimientoPlan"];
                    aux.FechaAlta = (DateTime)datos.Lector["FechaAlta"];
                    aux.Activo = (bool)datos.Lector["Activo"];

                    //nulleables
                    if (!(datos.Lector["Email"] is DBNull))
                        aux.Email = (string)datos.Lector["Email"];

                    if (!(datos.Lector["Telefono"] is DBNull))
                        aux.Telefono = (string)datos.Lector["Telefono"];

                    if (!(datos.Lector["FechaNacimiento"] is DBNull))
                        aux.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];

                    //usuario
                    aux.Usuario = new Usuario();
                    aux.Usuario.Id = (int)datos.Lector["IdUsuario"];

                    //plan
                    aux.Plan = new Plan();
                    aux.Plan.Id = (int)datos.Lector["IdPlan"];
                    aux.Plan.Descripcion = (string)datos.Lector["NombrePlan"];
                    aux.Plan.Precio = (decimal)datos.Lector["Precio"];
                    aux.Plan.ClasesSemanales = (int)datos.Lector["ClasesSemanales"];
                    aux.Plan.IncluyeMusculacion = (bool)datos.Lector["IncluyeMusculacion"];
                    aux.Plan.IncluyeClases = (bool)datos.Lector["IncluyeClases"];

                    return aux;
                }

                return null;
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
