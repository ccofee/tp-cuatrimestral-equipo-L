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
        public int CantidadReservasSemana(int idAlumno, DateTime lunes, DateTime domingo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta(@"SELECT COUNT(*) 
                              FROM RESERVAS 
                              WHERE IdAlumno = @idAlumno 
                              AND FechaClase BETWEEN @lunes AND @domingo
                              AND Estado = 'Reservada'");

                datos.SetearParametro("@idAlumno", idAlumno);
                datos.SetearParametro("@lunes", lunes);
                datos.SetearParametro("@domingo", domingo);

                datos.EjecutarLectura();

                if (datos.Lector.Read())
                    return (int)datos.Lector[0];

                return 0;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public int CantidadReservasHorario(int idHorario, DateTime fechaClase)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta(@"SELECT COUNT(*) 
                              FROM RESERVAS 
                              WHERE IdHorario = @idHorario 
                              AND FechaClase = @fechaClase
                              AND Estado = 'Reservada'");

                datos.SetearParametro("@idHorario", idHorario);
                datos.SetearParametro("@fechaClase", fechaClase);

                datos.EjecutarLectura();

                if (datos.Lector.Read())
                    return (int)datos.Lector[0];

                return 0;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public bool ExisteReserva(int idAlumno, int idHorario, DateTime fechaClase)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta(@"SELECT Id 
                              FROM RESERVAS 
                              WHERE IdAlumno = @idAlumno
                              AND IdHorario = @idHorario
                              AND FechaClase = @fechaClase
                              AND Estado != 'Cancelada'");

                datos.SetearParametro("@idAlumno", idAlumno);
                datos.SetearParametro("@idHorario", idHorario);
                datos.SetearParametro("@fechaClase", fechaClase);

                datos.EjecutarLectura();

                return datos.Lector.Read();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public int Agregar(Reserva nueva)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta(@"INSERT INTO RESERVAS
                              (IdAlumno, IdHorario, FechaClase, Estado)
                              OUTPUT INSERTED.Id
                              VALUES
                              (@idAlumno, @idHorario, @fechaClase, @estado)");

                datos.SetearParametro("@idAlumno", nueva.Alumno.Id);
                datos.SetearParametro("@idHorario", nueva.Horario.Id);
                datos.SetearParametro("@fechaClase", nueva.FechaClase);
                datos.SetearParametro("@estado", nueva.Estado);

                return datos.EjecutarAccionScalar();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public void Cancelar(int idReserva)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta(@"UPDATE RESERVAS
                              SET Estado = 'Cancelada'
                              WHERE Id = @idReserva");

                datos.SetearParametro("@idReserva", idReserva);

                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public Reserva BuscarPorId(int idReserva)
        {
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
                              WHERE R.Id = @idReserva");

                datos.SetearParametro("@idReserva", idReserva);

                datos.EjecutarLectura();

                if (datos.Lector.Read())
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

                    return reserva;
                }

                return null;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public void PromoverPrimeroEnEspera(int idHorario, DateTime fechaClase)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta(@"UPDATE RESERVAS
                              SET Estado = 'Reservada'
                              WHERE Id = (
                                  SELECT TOP 1 Id
                                  FROM RESERVAS
                                  WHERE IdHorario = @idHorario
                                  AND FechaClase = @fechaClase
                                  AND Estado = 'EnEspera'
                                  ORDER BY FechaAlta
                              )");

                datos.SetearParametro("@idHorario", idHorario);
                datos.SetearParametro("@fechaClase", fechaClase);

                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}