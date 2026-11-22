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
    public partial class Alumnos : System.Web.UI.Page
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
                cargarPlanes();
                btnEliminar.Enabled = false;
                btnRenovar.Enabled = false;
            }
        }

        private void cargarGrilla()
        {
            AlumnoNegocio negocio = new AlumnoNegocio();
            try
            {
                List<Alumno> lista = negocio.listar();
                Session.Add("listaAlumnos", lista);
                dgvAlumnos.DataSource = lista;
                dgvAlumnos.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                throw ex;
            }
        }

        private void cargarPlanes()
        {
            PlanNegocio negocio = new PlanNegocio();

            try
            {
                ddlPlan.DataSource = negocio.Listar();
                ddlPlan.DataTextField = "Descripcion";
                ddlPlan.DataValueField = "Id";
                ddlPlan.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                throw ex;
            }
        }

        protected void dgvAlumnos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblErrorDni.Visible = false;
                lblMensaje.Visible = false;
                int idSeleccionado = int.Parse(dgvAlumnos.SelectedDataKey.Value.ToString());

                AlumnoNegocio negocio = new AlumnoNegocio();
                Alumno seleccionado = negocio.listar().Find(a => a.Id == idSeleccionado);

                if (seleccionado != null)
                {
                    //precarga de campos textbox
                    txtDni.Text = seleccionado.DNI;
                    txtNombre.Text = seleccionado.Nombre;
                    txtApellido.Text = seleccionado.Apellido;
                    txtEmail.Text = seleccionado.Email;
                    txtTelefono.Text = seleccionado.Telefono;

                    if (seleccionado.FechaNacimiento > DateTime.MinValue)
                        txtFechaNacimiento.Text = seleccionado.FechaNacimiento.ToString("yyyy-MM-dd");
                    else
                        txtFechaNacimiento.Text = string.Empty;

                    ddlPlan.SelectedValue = seleccionado.Plan.Id.ToString();
                    //cambio de formato de la fecha
                    txtFechaVencimiento.Text = seleccionado.FechaVencimientoPlan.ToString("yyyy-MM-dd");

                    //habilito botones sobre el alumno seleccionado
                    btnEliminar.Enabled = true;
                    btnRenovar.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                throw ex;
            }
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            try
            {
                aplicarFiltros();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                throw ex;
            }
        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                aplicarFiltros();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                throw ex;
            }
        }

        private void aplicarFiltros()
        {
            List<Alumno> lista = (List<Alumno>)Session["listaAlumnos"];
            string filtro = txtFiltro.Text.ToUpper();
            string estado = ddlEstado.SelectedValue;

            List<Alumno> listaFiltrada = lista.FindAll(x =>
                (x.DNI.ToUpper().Contains(filtro) ||
                 x.Nombre.ToUpper().Contains(filtro) ||
                 x.Apellido.ToUpper().Contains(filtro)) &&
                (estado == "Todos" ||
                 (estado == "AlDia" && !x.Atrasado) ||
                 (estado == "Atrasado" && x.Atrasado))
            );

            //desmarco si habia algun alumno seleccionado y limpio los campos cargados
            limpiarFormulario();
            dgvAlumnos.SelectedIndex = -1;
            btnEliminar.Enabled = false;
            btnRenovar.Enabled = false;

            dgvAlumnos.DataSource = listaFiltrada;
            dgvAlumnos.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                lblErrorDni.Visible = false;
                lblMensaje.Visible = false;

                Page.Validate();
                if (!Page.IsValid)
                    return;

                Alumno alumno = new Alumno();
                AlumnoNegocio negocio = new AlumnoNegocio();

                //carga de datos
                alumno.DNI = txtDni.Text;
                alumno.Nombre = txtNombre.Text;
                alumno.Apellido = txtApellido.Text;
                alumno.Email = txtEmail.Text;
                alumno.Telefono = txtTelefono.Text;

                if (!string.IsNullOrEmpty(txtFechaNacimiento.Text))
                    alumno.FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text);

                //plan
                alumno.Plan = new Plan();
                alumno.Plan.Id = int.Parse(ddlPlan.SelectedValue);

                //se ingresa manual o sino 30 dias por defecto
                if (!string.IsNullOrEmpty(txtFechaVencimiento.Text))
                    alumno.FechaVencimientoPlan = DateTime.Parse(txtFechaVencimiento.Text);
                else
                    alumno.FechaVencimientoPlan = DateTime.Today.AddDays(30);

                bool esAlta = (dgvAlumnos.SelectedIndex == -1);

                //seleccion de ALTA o MODIFICACION + control de DNI repetido
                if (esAlta)
                {
                    //control DNI
                    if (negocio.existeDni(alumno.DNI))
                    {
                        lblErrorDni.Text = "El DNI ingresado ya se encuentra registrado.";
                        lblErrorDni.Visible = true;
                        return;
                    }

                    negocio.agregar(alumno);
                }
                else
                {
                    alumno.Id = int.Parse(dgvAlumnos.SelectedDataKey.Value.ToString());

                    if (negocio.existeDni(alumno.DNI, alumno.Id))
                    {
                        lblErrorDni.Text = "El DNI ingresado ya pertenece a otro alumno.";
                        lblErrorDni.Visible = true;
                        return;
                    }

                    negocio.modificar(alumno);
                }

                //actualizo y limpio campos
                cargarGrilla();
                limpiarCampos();

                if (esAlta)
                    lblMensaje.Text = "Alumno agregado correctamente.";
                else
                    lblMensaje.Text = "Alumno modificado correctamente.";

                lblMensaje.Visible = true;
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
            dgvAlumnos.DataSource = (List<Alumno>)Session["listaAlumnos"];
            dgvAlumnos.DataBind();
        }

        private void limpiarFormulario()
        {
            txtDni.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtFechaNacimiento.Text = string.Empty;
            txtFechaVencimiento.Text = string.Empty;

            if (ddlPlan.Items.Count > 0)
                ddlPlan.SelectedIndex = 0;

            lblErrorDni.Text = string.Empty;
            lblErrorDni.Visible = false;
            lblMensaje.Text = string.Empty;
            lblMensaje.Visible = false;
        }

        private void limpiarCampos()
        {
            limpiarFormulario();
            txtFiltro.Text = string.Empty;

            if (ddlEstado.Items.Count > 0)
                ddlEstado.SelectedIndex = 0;

            //deshabilito botones de seleccion
            btnEliminar.Enabled = false;
            btnRenovar.Enabled = false;

            //vuelta al estado inicial de la grilla
            dgvAlumnos.SelectedIndex = -1;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            AlumnoNegocio negocio = new AlumnoNegocio();

            try
            {
                if (dgvAlumnos.SelectedIndex != -1)
                {
                    int id = int.Parse(dgvAlumnos.SelectedDataKey.Value.ToString());
                    negocio.eliminarLogico(id);

                    cargarGrilla();
                    limpiarCampos();

                    lblMensaje.Text = "Alumno dado de baja correctamente.";
                    lblMensaje.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                throw ex;
            }
        }

        protected void btnRenovar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvAlumnos.SelectedIndex != -1)
                {
                    int idSeleccionado = int.Parse(dgvAlumnos.SelectedDataKey.Value.ToString());

                    AlumnoNegocio alumnoNegocio = new AlumnoNegocio();
                    Alumno seleccionado = alumnoNegocio.listar().Find(a => a.Id == idSeleccionado);

                    if (seleccionado != null)
                    {
                        Pago nuevoPago = new Pago();
                        nuevoPago.Alumno = seleccionado;

                        PagoNegocio pagoNegocio = new PagoNegocio();
                        pagoNegocio.registrar(nuevoPago);

                        cargarGrilla();
                        limpiarCampos();

                        Alumno actualizado = ((List<Alumno>)Session["listaAlumnos"]).Find(a => a.Id == idSeleccionado);
                        lblMensaje.Text = "Pago registrado correctamente. Nuevo vencimiento: " + actualizado.FechaVencimientoPlan.ToString("dd/MM/yyyy");
                        lblMensaje.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                throw ex;
            }
        }
    }
}