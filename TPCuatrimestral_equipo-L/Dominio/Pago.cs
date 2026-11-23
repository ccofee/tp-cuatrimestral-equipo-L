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

        public string AlumnoDNI
        {
            get { return Alumno != null ? Alumno.DNI : ""; }
        }

        public string AlumnoNombre
        {
            get { return Alumno != null ? Alumno.Apellido + ", " + Alumno.Nombre : ""; }
        }

        public string PlanDescripcion
        {
            get { return Alumno != null && Alumno.Plan != null ? Alumno.Plan.Descripcion : ""; }
        }

        public decimal Monto
        {
            get { return Alumno != null && Alumno.Plan != null ? Alumno.Plan.Precio : 0; }
        }
    }
}
