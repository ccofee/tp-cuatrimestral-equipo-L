using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace Presentacion
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HorarioNegocio negocio = new HorarioNegocio();

                dgvHorarios.DataSource = negocio.Listar();
                dgvHorarios.DataBind();
            }
        }
    }
}
    