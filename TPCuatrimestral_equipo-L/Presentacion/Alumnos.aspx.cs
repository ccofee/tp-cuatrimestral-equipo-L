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
            }
        }

        private void cargarGrilla()
        {
            AlumnoNegocio negocio = new AlumnoNegocio();
            try
            {
                dgvAlumnos.DataSource = negocio.listar();
                dgvAlumnos.DataBind();
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


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Alumno alumno = new Alumno();
            AlumnoNegocio negocio = new AlumnoNegocio();

            try
            {
                //carga de datos
                alumno.DNI = txtDni.Text;
                alumno.Nombre = txtNombre.Text;
                alumno.Apellido = txtApellido.Text;
                alumno.Email = txtEmail.Text;
                alumno.Telefono = txtTelefono.Text;

                if (!string.IsNullOrEmpty(txtFechaNacimiento.Text))
                    alumno.FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text);

                alumno.Plan = new Plan();
                alumno.Plan.Id = int.Parse(ddlPlan.SelectedValue);

                if (!string.IsNullOrEmpty(txtFechaVencimiento.Text))
                    alumno.FechaVencimientoPlan = DateTime.Parse(txtFechaVencimiento.Text);

                //seleccion de ALTA o MODIFICACION
                if (dgvAlumnos.SelectedIndex != -1)
                {
                    alumno.Id = int.Parse(dgvAlumnos.SelectedDataKey.Value.ToString());
                    negocio.modificar(alumno);
                }
                else
                {
                    negocio.agregar(alumno);
                }

                //actualizacion y limpieza de campos
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
            ddlPlan.SelectedValue = "0";

            //vuelta al estado inicial de la grillla
            dgvAlumnos.SelectedIndex = -1;
        }


    }
}