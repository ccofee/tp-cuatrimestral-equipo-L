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

        protected void dgvPlanes_SelectedIndexChanged(object sender, EventArgs e)
        {
            //precarga del formulario para modificar
            int idSeleccionado = int.Parse(dgvPlanes.SelectedDataKey.Value.ToString());

            PlanNegocio negocio = new PlanNegocio();
            Plan seleccionado = negocio.BuscarPorId(idSeleccionado);

            if (seleccionado != null)
            {
                txtDescripcion.Text = seleccionado.Descripcion;
                txtPrecio.Text = seleccionado.Precio.ToString(CultureInfo.InvariantCulture);
                txtClasesSemanales.Text = seleccionado.ClasesSemanales.ToString(CultureInfo.InvariantCulture);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            lblErrorPlan.Visible = false;

            Page.Validate();
            if (!Page.IsValid)
                return;

            Plan plan = new Plan();
            plan.Descripcion = txtDescripcion.Text.Trim();
            plan.Precio = decimal.Parse(txtPrecio.Text, CultureInfo.InvariantCulture);
            plan.ClasesSemanales = int.Parse(txtClasesSemanales.Text, CultureInfo.InvariantCulture);

            if (plan.Precio <= 0)
            {
                lblErrorPlan.Text = "El precio debe ser mayor a 0.";
                lblErrorPlan.Visible = true;
                return;
            }

            if (plan.ClasesSemanales < 1 || plan.ClasesSemanales > 14)
            {
                lblErrorPlan.Text = "Las clases por semana deben estar entre 1 y 14.";
                lblErrorPlan.Visible = true;
                return;
            }

            PlanNegocio negocio = new PlanNegocio();

            //alta o modificacion de un plan
            try
            {
                if (dgvPlanes.SelectedIndex == -1)
                {
                    negocio.Agregar(plan);
                }
                else
                {
                    plan.Id = int.Parse(dgvPlanes.SelectedDataKey.Value.ToString());
                    negocio.Modificar(plan);
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                throw;
            }

            LimpiarFormulario();
            CargarGrilla();
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            //baja logica de un plan
            if (dgvPlanes.SelectedIndex == -1)
                return;

            int id = int.Parse(dgvPlanes.SelectedDataKey.Value.ToString());
            PlanNegocio negocio = new PlanNegocio();

            try
            {
                negocio.BajaLogica(id);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                throw;
            }

            LimpiarFormulario();
            CargarGrilla();
        }

        private void CargarGrilla()
        {
            //listado de planes
            PlanNegocio negocio = new PlanNegocio();
            dgvPlanes.DataSource = negocio.Listar();
            dgvPlanes.DataBind();
        }

        private void LimpiarFormulario()
        {
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
            txtClasesSemanales.Text = "";

            lblErrorPlan.Text = "";
            lblErrorPlan.Visible = false;

            dgvPlanes.SelectedIndex = -1;
        }
    }
}
