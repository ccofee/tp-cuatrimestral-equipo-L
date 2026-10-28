using System;
using System.Web.UI;
using Negocio;

namespace Presentacion
{
    public partial class Planes : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PlanNegocio negocio = new PlanNegocio();
            dgvPlanes.DataSource = negocio.Listar();
            dgvPlanes.DataBind();
        }
    }
}
