using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace Presentacion
{
    public partial class Alumnos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Alumno> lista = new List<Alumno>();

                Plan plan1 = new Plan
                {
                    Id = 1,
                    Descripcion = "Plan 2 dias",
                    Precio = 18000,
                    ClasesSemanales = 2,
                    Activo = true
                };

                Plan plan2 = new Plan
                {
                    Id = 3,
                    Descripcion = "Pase Libre",
                    Precio = 32000,
                    ClasesSemanales = 5,
                    Activo = true
                };

                Alumno alumno1 = new Alumno
                {
                    Id = 1,
                    DNI = "39177873",
                    Nombre = "Enzo",
                    Apellido = "Sander",
                    Email = "enzosanderr@email.com",
                    Telefono = "341-6150987",
                    FechaNacimiento = new DateTime(1995, 4, 11),
                    Plan = plan1,
                    FechaVencimientoPlan = DateTime.Now.AddDays(15),
                    Activo = true
                };

                Alumno alumno2 = new Alumno
                {
                    Id = 2,
                    DNI = "39606975",
                    Nombre = "Ailen",
                    Apellido = "Mircovich",
                    Email = "ailen.mircovich@email.com",
                    Telefono = "341-6150459",
                    FechaNacimiento = new DateTime(1996, 7, 25),
                    Plan = plan2,
                    FechaVencimientoPlan = DateTime.Now.AddDays(25),
                    Activo = true
                };

                lista.Add(alumno1);
                lista.Add(alumno2);

                dgvAlumnos.DataSource = lista;
                dgvAlumnos.DataBind();
            }
        }
    }
}