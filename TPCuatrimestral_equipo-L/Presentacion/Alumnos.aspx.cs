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

            if (!IsPostBack)
            {
                cargarGrilla();
                cargarPlanes();
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
                List<Alumno> lista = (List<Alumno>)Session["listaAlumnos"];

                List<Alumno> listaFiltrada = lista.FindAll(x =>
                    x.DNI.ToUpper().Contains(txtFiltro.Text.ToUpper()) ||
                    x.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()) ||
                    x.Apellido.ToUpper().Contains(txtFiltro.Text.ToUpper())
                );

                //desmarco si habia algun alumno seleccionado
                dgvAlumnos.SelectedIndex = -1;
                dgvAlumnos.DataSource = listaFiltrada;
                dgvAlumnos.DataBind();
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
                lblErrorDni.Visible = false;

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

                //seleccion de ALTA o MODIFICACION + control de DNI repetido
                if (dgvAlumnos.SelectedIndex == -1)
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
                    negocio.modificar(alumno);    
                }

                 //actualizo y limpio campos
                 cargarGrilla();
                 limpiarCampos();

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

        private void limpiarCampos()
        {
            txtDni.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtFechaNacimiento.Text = string.Empty;
            txtFechaVencimiento.Text = string.Empty;
            txtFiltro.Text = string.Empty;

            if (ddlPlan.Items.Count > 0)
                ddlPlan.SelectedIndex = 0;

            //limpieza de DNI
            lblErrorDni.Text = string.Empty;
            lblErrorDni.Visible = false;

            //vuelta al estado inicial de la grillla
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

                    AlumnoNegocio negocio = new AlumnoNegocio();
                    Alumno seleccionado = negocio.listar().Find(a => a.Id == idSeleccionado);

                    if (seleccionado != null)
                    {
                        DateTime nuevoVencimiento;

                        //si vencio sumamos 30 dias desde hoy, si no vencio se suman 30 a la fecha de vencimiento actual
                        if (seleccionado.FechaVencimientoPlan > DateTime.Today)
                            nuevoVencimiento = seleccionado.FechaVencimientoPlan.AddDays(30);
                        else
                            nuevoVencimiento = DateTime.Today.AddDays(30);

                        negocio.renovarPlan(idSeleccionado, nuevoVencimiento);

                        cargarGrilla();
                        limpiarCampos();
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