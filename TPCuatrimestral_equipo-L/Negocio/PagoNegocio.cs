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
