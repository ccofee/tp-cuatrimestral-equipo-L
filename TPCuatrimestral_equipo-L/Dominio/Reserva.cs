using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Reserva
    {
        public int Id { get; set; }
        public Alumno Alumno { get; set; }
        public Horario Horario { get; set; }
        public DateTime FechaClase { get; set; }
        public string Estado { get; set; }
        public DateTime FechaAlta { get; set; }
    }
}
