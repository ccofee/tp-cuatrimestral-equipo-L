using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ReservaNegocio
    {
        public List<Reserva> Listar()
        {
            List<Reserva> lista = new List<Reserva>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta(@"SELECT R.Id, R.FechaClase, R.Estado, R.FechaAlta,
                                             A.Id AS IdAlumno, A.Nombre, A.Apellido,
                                             H.Id AS IdHorario, H.DiaSemana, H.HoraInicio, H.HoraFin,
                                             H.CupoMaximo, AC.Id AS IdActividad, AC.Nombre AS ActividadNombre
                                      FROM RESERVAS R
                                      INNER JOIN ALUMNOS A ON R.IdAlumno = A.Id
                                      INNER JOIN HORARIOS H ON R.IdHorario = H.Id
                                      INNER JOIN ACTIVIDADES AC ON H.IdActividad = AC.Id
                                      ORDER BY R.FechaClase, H.HoraInicio");

                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Reserva reserva = new Reserva();

                    reserva.Id = (int)datos.Lector["Id"];
                    reserva.FechaClase = (DateTime)datos.Lector["FechaClase"];
                    reserva.Estado = (string)datos.Lector["Estado"];
                    reserva.FechaAlta = (DateTime)datos.Lector["FechaAlta"];

                    reserva.Alumno = new Alumno();
                    reserva.Alumno.Id = (int)datos.Lector["IdAlumno"];
                    reserva.Alumno.Nombre = (string)datos.Lector["Nombre"];
                    reserva.Alumno.Apellido = (string)datos.Lector["Apellido"];

                    reserva.Horario = new Horario();
                    reserva.Horario.Id = (int)datos.Lector["IdHorario"];
                    reserva.Horario.DiaSemana = (int)datos.Lector["DiaSemana"];
                    reserva.Horario.HoraInicio = (TimeSpan)datos.Lector["HoraInicio"];
                    reserva.Horario.HoraFin = (TimeSpan)datos.Lector["HoraFin"];
                    reserva.Horario.CupoMaximo = (int)datos.Lector["CupoMaximo"];

                    reserva.Horario.Actividad = new Actividad();
                    reserva.Horario.Actividad.Id = (int)datos.Lector["IdActividad"];
                    reserva.Horario.Actividad.Nombre = (string)datos.Lector["ActividadNombre"];

                    lista.Add(reserva);
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