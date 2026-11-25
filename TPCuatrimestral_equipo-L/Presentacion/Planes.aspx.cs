using System;
using System.Collections.Generic;
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
            Usuario usuarioLogueado = Session["UsuarioLogueado"] as Usuario;

            if (usuarioLogueado == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (usuarioLogueado.Rol != 1)
            {
                Response.Redirect("Default.aspx");
                return;
            }

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
                chkIncluyeMusculacion.Checked = seleccionado.IncluyeMusculacion;
                chkIncluyeClases.Checked = seleccionado.IncluyeClases;
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
            plan.IncluyeMusculacion = chkIncluyeMusculacion.Checked;
            plan.IncluyeClases = chkIncluyeClases.Checked;

            if (plan.Precio <= 0)
            {
                lblErrorPlan.Text = "El precio debe ser mayor a 0.";
                lblErrorPlan.Visible = true;
                return;
            }

            if (plan.ClasesSemanales < 0 || plan.ClasesSemanales > 14)
            {
                lblErrorPlan.Text = "Las clases por semana deben estar entre 0 y 14.";
                lblErrorPlan.Visible = true;
                return;
            }

            if (!plan.IncluyeMusculacion && !plan.IncluyeClases)
            {
                lblErrorPlan.Text = "El plan debe incluir al menos musculación o clases grupales.";
                lblErrorPlan.Visible = true;
                return;
            }

            PlanNegocio negocio = new PlanNegocio();
            bool esAlta = dgvPlanes.SelectedIndex == -1;

            //alta o modificacion de un plan
            try
            {
                if (esAlta)
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

            LimpiarCampos();
            CargarGrilla();

            lblMensaje.Text = esAlta ? "Plan agregado correctamente." : "Plan modificado correctamente.";
            lblMensaje.Visible = true;
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Plan> lista = (List<Plan>)Session["listaPlanes"];
            List<Plan> listaFiltrada = lista.FindAll(x =>
                x.Descripcion.ToUpper().Contains(txtFiltro.Text.ToUpper())
            );

            LimpiarFormulario();
            dgvPlanes.DataSource = listaFiltrada;
            dgvPlanes.DataBind();
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            //baja logica de un plan
            lblErrorPlan.Visible = false;

            if (dgvPlanes.SelectedIndex == -1)
                return;

            int id = int.Parse(dgvPlanes.SelectedDataKey.Value.ToString());
            PlanNegocio negocio = new PlanNegocio();

            if (negocio.TieneAlumnosActivos(id))
            {
                lblErrorPlan.Text = "No se puede dar de baja el plan: tiene alumnos activos asociados.";
                lblErrorPlan.Visible = true;
                return;
            }

            try
            {
                negocio.BajaLogica(id);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                throw;
            }

            LimpiarCampos();
            CargarGrilla();

            lblMensaje.Text = "Plan dado de baja correctamente.";
            lblMensaje.Visible = true;
        }

        private void CargarGrilla()
        {
            //listado de planes
            PlanNegocio negocio = new PlanNegocio();
            List<Plan> lista = negocio.Listar();
            Session["listaPlanes"] = lista;
            dgvPlanes.DataSource = lista;
            dgvPlanes.DataBind();
        }

        private void LimpiarFormulario()
        {
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
            txtClasesSemanales.Text = "";
            chkIncluyeMusculacion.Checked = false;
            chkIncluyeClases.Checked = false;

            lblErrorPlan.Text = "";
            lblErrorPlan.Visible = false;

            lblMensaje.Text = "";
            lblMensaje.Visible = false;

            dgvPlanes.SelectedIndex = -1;
        }

        private void LimpiarCampos()
        {
            LimpiarFormulario();
            txtFiltro.Text = "";
        }
    }
}
