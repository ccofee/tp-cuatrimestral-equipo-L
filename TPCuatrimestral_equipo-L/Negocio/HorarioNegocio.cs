using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                datos.SetearConsulta("SELECT Id, DiaSemana, HoraInicio, HoraFin, CupoMaximo, Activo FROM HORARIOS WHERE Activo = 1");
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
    }
}
