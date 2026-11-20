using System;
using System.Web.UI;
using Dominio;
using Negocio;

namespace Presentacion
{
    public partial class Perfil : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuarioLogueado = Session["UsuarioLogueado"] as Usuario;

            if (usuarioLogueado == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
                CargarDatos(usuarioLogueado);
        }

        private void CargarDatos(Usuario usuarioLogueado)
        {
            litNombre.Text = usuarioLogueado.Nombre + " " + usuarioLogueado.Apellido;
            litEmail.Text = usuarioLogueado.Email;

            phCambiarClave.Visible = usuarioLogueado.Rol != 1;

            if (usuarioLogueado.Rol == 2)
            {
                AlumnoNegocio negocio = new AlumnoNegocio();
                Alumno alumno = negocio.buscarPorIdUsuario(usuarioLogueado.Id);

                if (alumno != null)
                {
                    phDatosAlumno.Visible = true;
                    litDni.Text = alumno.DNI;
                    litPlan.Text = alumno.Plan.Descripcion;
                    litVencimiento.Text = alumno.FechaVencimientoPlan.ToString("dd/MM/yyyy");
                    litEstado.Text = alumno.Atrasado
                        ? "<span class=\"badge bg-danger\">Cuota atrasada</span>"
                        : "<span class=\"badge bg-success\">Cuota al día</span>";
                }
            }
        }

        protected void btnCambiarClave_Click(object sender, EventArgs e)
        {
            lblMensaje.Visible = false;

            Page.Validate();
            if (!Page.IsValid)
                return;

            Usuario usuarioLogueado = Session["UsuarioLogueado"] as Usuario;

            if (usuarioLogueado == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (usuarioLogueado.Rol == 1)
                return;

            UsuarioNegocio negocio = new UsuarioNegocio();
            negocio.CambiarClavePropia(usuarioLogueado.Id, txtNuevaClave.Text);

            txtNuevaClave.Text = "";
            txtConfirmarClave.Text = "";

            lblMensaje.CssClass = "d-block mt-2 text-success";
            lblMensaje.Text = "Contraseña actualizada correctamente.";
            lblMensaje.Visible = true;
        }
    }
}
