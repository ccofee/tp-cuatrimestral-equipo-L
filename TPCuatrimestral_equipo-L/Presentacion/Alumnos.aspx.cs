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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Alumno nuevo = new Alumno();
            AlumnoNegocio negocio = new AlumnoNegocio();

            try
            {
                //carga de datos
                nuevo.DNI = txtDni.Text;
                nuevo.Nombre = txtNombre.Text;
                nuevo.Apellido = txtApellido.Text;
                nuevo.Email = txtEmail.Text;
                nuevo.Telefono = txtTelefono.Text;

                if (!string.IsNullOrEmpty(txtFechaNacimiento.Text))
                {
                    nuevo.FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text);
                }

                nuevo.Plan = new Plan();
                nuevo.Plan.Id = int.Parse(ddlPlan.SelectedValue);


                if (!string.IsNullOrEmpty(txtFechaVencimiento.Text))
                {
                    nuevo.FechaVencimientoPlan = DateTime.Parse(txtFechaVencimiento.Text);
                }


                negocio.agregar(nuevo);

                //refresco y limpieza de campos
                cargarGrilla();
                limpiarCampos();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                throw ex;
            }
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
        }


    }
}