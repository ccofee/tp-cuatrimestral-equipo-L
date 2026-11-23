using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class PagoNegocio
    {

        public List<Pago> listar()
        {
            List<Pago> lista = new List<Pago>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"SELECT P.Id, P.FechaPago, A.Id AS IdAlumno, A.DNI, A.Nombre, A.Apellido, 
                                           PL.Id AS IdPlan, PL.Descripcion AS NombrePlan, PL.Precio 
                                    FROM PAGOS P 
                                    INNER JOIN ALUMNOS A ON P.IdAlumno = A.Id 
                                    INNER JOIN PLANES PL ON A.IdPlan = PL.Id 
                                    ORDER BY P.FechaPago DESC";

                datos.SetearConsulta(consulta);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    //pago
                    Pago aux = new Pago();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.FechaPago = (DateTime)datos.Lector["FechaPago"];

                    //alumno
                    aux.Alumno = new Alumno();
                    aux.Alumno.Id = (int)datos.Lector["IdAlumno"];
                    aux.Alumno.DNI = (string)datos.Lector["DNI"];
                    aux.Alumno.Nombre = (string)datos.Lector["Nombre"];
                    aux.Alumno.Apellido = (string)datos.Lector["Apellido"];

                    //plan
                    aux.Alumno.Plan = new Plan();
                    aux.Alumno.Plan.Id = (int)datos.Lector["IdPlan"];
                    aux.Alumno.Plan.Descripcion = (string)datos.Lector["NombrePlan"];
                    aux.Alumno.Plan.Precio = (decimal)datos.Lector["Precio"];

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


        public void registrar(Pago nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {

                // si tenia dias vigentes se suma 30 dias a la fecha de vencimiento, sino se suma 30 dias a la fecha actual
                DateTime nuevaFecha;

                if (nuevo.Alumno.FechaVencimientoPlan > DateTime.Today)
                    nuevaFecha = nuevo.Alumno.FechaVencimientoPlan.AddDays(30);
                else
                    nuevaFecha = DateTime.Today.AddDays(30);

                // alta de pago y actualizacion de vencimiento
                string consulta = "INSERT INTO PAGOS (IdAlumno, FechaPago) VALUES (@idAlumno, GETDATE()); " +
                                  "UPDATE ALUMNOS SET FechaVencimientoPlan = @nuevaFecha WHERE Id = @idAlumno;";

                datos.SetearConsulta(consulta);
                datos.SetearParametro("@idAlumno", nuevo.Alumno.Id);
                datos.SetearParametro("@nuevaFecha", nuevaFecha);
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
