using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class HorarioNegocio
    {
        public List<Horario> Listar()
        {
            List<Horario> lista = new List<Horario>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT Id, DiaSemana, HoraInicio, HoraFin, CupoMaximo, Activo FROM HORARIOS WHERE Activo = 1 ORDER BY DiaSemana, HoraInicio");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Horario horario = new Horario();
                    horario.Id = (int)datos.Lector["Id"];
                    horario.DiaSemana = (int)datos.Lector["DiaSemana"];
                    horario.HoraInicio = (System.TimeSpan)datos.Lector["HoraInicio"];
                    horario.HoraFin = (System.TimeSpan)datos.Lector["HoraFin"];
                    horario.CupoMaximo = (int)datos.Lector["CupoMaximo"];
                    horario.Activo = (bool)datos.Lector["Activo"];

                    lista.Add(horario);
                }

                return lista;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Agregar(Horario nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("INSERT INTO HORARIOS (DiaSemana, HoraInicio, HoraFin, CupoMaximo) VALUES (@diaSemana, @horaInicio, @horaFin, @cupoMaximo)");
                datos.SetearParametro("@diaSemana", nuevo.DiaSemana);
                datos.SetearParametro("@horaInicio", nuevo.HoraInicio);
                datos.SetearParametro("@horaFin", nuevo.HoraFin);
                datos.SetearParametro("@cupoMaximo", nuevo.CupoMaximo);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Modificar(Horario horario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE HORARIOS SET DiaSemana = @diaSemana, HoraInicio = @horaInicio, HoraFin = @horaFin, CupoMaximo = @cupoMaximo WHERE Id = @id");
                datos.SetearParametro("@diaSemana", horario.DiaSemana);
                datos.SetearParametro("@horaInicio", horario.HoraInicio);
                datos.SetearParametro("@horaFin", horario.HoraFin);
                datos.SetearParametro("@cupoMaximo", horario.CupoMaximo);
                datos.SetearParametro("@id", horario.Id);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void BajaLogica(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE HORARIOS SET Activo = 0 WHERE Id = @id");
                datos.SetearParametro("@id", id);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public Horario BuscarPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT Id, DiaSemana, HoraInicio, HoraFin, CupoMaximo, Activo FROM HORARIOS WHERE Id = @id");
                datos.SetearParametro("@id", id);
                datos.EjecutarLectura();

                if (!datos.Lector.Read())
                    return null;

                Horario horario = new Horario();
                horario.Id = (int)datos.Lector["Id"];
                horario.DiaSemana = (int)datos.Lector["DiaSemana"];
                horario.HoraInicio = (System.TimeSpan)datos.Lector["HoraInicio"];
                horario.HoraFin = (System.TimeSpan)datos.Lector["HoraFin"];
                horario.CupoMaximo = (int)datos.Lector["CupoMaximo"];
                horario.Activo = (bool)datos.Lector["Activo"];

                return horario;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}
