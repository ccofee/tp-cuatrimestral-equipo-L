using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Plan
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int ClasesSemanales { get; set; }
        public bool Activo { get; set; }

        public override string ToString()
        {
            return Descripcion;
        }

    }
}
