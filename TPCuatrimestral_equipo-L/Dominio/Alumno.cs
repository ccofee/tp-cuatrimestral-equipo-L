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
        public bool Activo { get; set; }
    }
}
