using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Pago
    {
        public int Id { get; set; }
        public Alumno Alumno { get; set; }
        public DateTime FechaPago { get; set; }
    }
}
