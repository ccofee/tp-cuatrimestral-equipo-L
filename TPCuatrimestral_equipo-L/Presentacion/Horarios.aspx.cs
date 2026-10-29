using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace Presentacion
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Horario> horarios = new List<Horario>();

                horarios.Add(new Horario
                {
                    Id = 1,
                    DiaSemana = 1,
                    HoraInicio = new TimeSpan(18, 0, 0),
                    HoraFin = new TimeSpan(19, 0, 0),
                    CupoMaximo = 15,
                    Activo = true
                });

                horarios.Add(new Horario
                {
                    Id = 2,
                    DiaSemana = 3,
                    HoraInicio = new TimeSpan(19, 0, 0),
                    HoraFin = new TimeSpan(20, 0, 0),
                    CupoMaximo = 15,
                    Activo = true
                });

                dgvHorarios.DataSource = horarios;
                dgvHorarios.DataBind();
            }
        }
    }
}
    