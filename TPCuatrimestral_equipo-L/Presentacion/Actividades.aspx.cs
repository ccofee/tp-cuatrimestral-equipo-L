using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class Actividades : System.Web.UI.Page
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
            {
                cargarGrilla();
            }

        }


        private void cargarGrilla()
        {

            ActividadNegocio negocio = new ActividadNegocio();

            try
            {

                List<Actividad> lista = negocio.listar();
                Session.Add("listaActividades", lista);
                dgvActividades.DataSource = lista;
                dgvActividades.DataBind();
            }

            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                throw ex;
            }
        }



        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                lblErrorNombre.Visible = false;
                lblErrorActividad.Visible = false;

                Page.Validate();
                if (!Page.IsValid)
                    return;

                Actividad actividad = new Actividad();
                ActividadNegocio negocio = new ActividadNegocio();

                actividad.Nombre = txtNombre.Text;
                actividad.Descripcion = txtDescripcion.Text;

                if (dgvActividades.SelectedIndex == -1)
                {
                    if (negocio.existeNombre(actividad.Nombre))
                    {
                        lblErrorNombre.Text = "Ya existe una actividad activa con ese nombre.";
                        lblErrorNombre.Visible = true;
                        return;
                    }

                    negocio.agregar(actividad);
                }
                else
                {

                    actividad.Id = int.Parse(dgvActividades.SelectedDataKey.Value.ToString());

                    if (negocio.existeNombre(actividad.Nombre, actividad.Id))
                    {
                        lblErrorNombre.Text = "Ya existe otra actividad activa con ese nombre.";
                        lblErrorNombre.Visible = true;
                        return;
                    }

                    negocio.modificar(actividad);
                }

                cargarGrilla();
                limpiarCampos();

            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                throw ex;
            }
        }


        protected void dgvActividades_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblErrorNombre.Visible = false;
                lblErrorActividad.Visible = false;

                int idSeleccionado = int.Parse(dgvActividades.SelectedDataKey.Value.ToString());

                ActividadNegocio negocio = new ActividadNegocio();
                Actividad seleccionada = negocio.listar().Find(x => x.Id == idSeleccionado);

                if (seleccionada != null)
                {
                    txtNombre.Text = seleccionada.Nombre;
                    txtDescripcion.Text = seleccionada.Descripcion;
                }
            }

            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                throw ex;
            }

        }


        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ActividadNegocio negocio = new ActividadNegocio();

            try
            {
                lblErrorActividad.Visible = false;

                if (dgvActividades.SelectedIndex != -1)
                {
                    int id = int.Parse(dgvActividades.SelectedDataKey.Value.ToString());

                    if (negocio.tieneHorariosActivos(id))
                    {
                        lblErrorActividad.Text = "No se puede dar de baja la actividad porque tiene horarios activos asociados.";
                        lblErrorActividad.Visible = true;
                        return;
                    }

                    negocio.eliminarLogico(id);

                    cargarGrilla();
                    limpiarCampos();
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                throw ex;
            }
        }


        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiarCampos();
            dgvActividades.DataSource = (List<Actividad>)Session["listaActividades"];
            dgvActividades.DataBind();
        }


        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            try
            {
                List<Actividad> lista = (List<Actividad>)Session["listaActividades"];
                List<Actividad> listaFiltrada = lista.FindAll(x =>
                    x.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper())
                );


                dgvActividades.SelectedIndex = -1;
                dgvActividades.DataSource = listaFiltrada;
                dgvActividades.DataBind();

            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                throw ex;
            }
        }

        private void limpiarCampos()
        {

            txtNombre.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtFiltro.Text = string.Empty;

            lblErrorNombre.Text = string.Empty;
            lblErrorNombre.Visible = false;

            lblErrorActividad.Text = string.Empty;
            lblErrorActividad.Visible = false;

            dgvActividades.SelectedIndex = -1;
        }

    }
}