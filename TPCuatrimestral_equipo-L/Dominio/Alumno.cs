using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dominio
{
    public class Alumno
    {
        public int Id { get; set; }
        public Usuario Usuario { get; set; }
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public Plan Plan { get; set; }
        public DateTime FechaVencimientoPlan { get; set; }
        public DateTime FechaAlta { get; set; }
        public bool Activo { get; set; }

        //calcula si el alumno esta atrasado en el pago del plan
        public bool Atrasado
        {
            get { return FechaVencimientoPlan.Date < DateTime.Today; }
        }

        //calcula la antiguedad del alumno en meses desde FechaAlta
        public int AntiguedadMeses
        {
            get
            {
                int meses = ((DateTime.Today.Year - FechaAlta.Year) * 12) + DateTime.Today.Month - FechaAlta.Month;

                if (DateTime.Today.Day < FechaAlta.Day)
                    meses--;

                return meses < 0 ? 0 : meses;
            }
        }

        public string Antiguedad
        {
            get
            {
                int meses = AntiguedadMeses;

                if (meses <= 0)
                    return "< de 1 mes";
                if (meses == 1)
                    return "1 mes";

                return meses + " meses";
            }
        }


        public string NombreCompleto
        {
            get { return Apellido + ", " + Nombre; }
        }


        public override string ToString()
        {
            return Apellido + ", " + Nombre;
        }
    }
}
