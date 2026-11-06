using System;
using System.Globalization;
using System.Web.UI;
using Dominio;
using Negocio;

namespace Presentacion
{
    public partial class Planes : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarGrilla();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Plan nuevo = new Plan();
            nuevo.Descripcion = txtDescripcion.Text.Trim();
            nuevo.Precio = decimal.Parse(txtPrecio.Text, CultureInfo.InvariantCulture);
            nuevo.ClasesSemanales = int.Parse(txtClasesSemanales.Text, CultureInfo.InvariantCulture);

            PlanNegocio negocio = new PlanNegocio();
            negocio.Agregar(nuevo);

            LimpiarFormulario();
            CargarGrilla();
        }

        private void CargarGrilla()
        {
            PlanNegocio negocio = new PlanNegocio();
            dgvPlanes.DataSource = negocio.Listar();
            dgvPlanes.DataBind();
        }

        private void LimpiarFormulario()
        {
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
            txtClasesSemanales.Text = "";
        }
    }
}
