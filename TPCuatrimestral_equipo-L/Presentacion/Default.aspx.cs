using System;
using System.Linq;
using System.Web.UI;
using Dominio;
using Negocio;

namespace Presentacion
{
    public partial class Default : Page
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
            {
                if (usuarioLogueado.Rol == 1)
                {
                    phAdmin.Visible = true;
                    CargarVistaAdmin();
                }
                else
                {
                    phAlumno.Visible = true;
                    CargarPanelAlumno(usuarioLogueado);
                }
            }
        }

        protected void ddlVista_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarVistaAdmin();
        }

        private void CargarVistaAdmin()
        {
            AlumnoNegocio negocio = new AlumnoNegocio();
            var todos = negocio.listar();

            litSociosActivos.Text = todos.Count.ToString();
            litCuotasAlDia.Text = todos.Count(a => !a.Atrasado).ToString();
            litCuotasAtrasadas.Text = todos.Count(a => a.Atrasado).ToString();

            var vista = todos;

            switch (ddlVista.SelectedValue)
            {
                case "proximos":
                    vista = todos.Where(a => !a.Atrasado && a.FechaVencimientoPlan <= DateTime.Today.AddDays(5)).ToList();
                    break;
                case "fidelizacion":
                    vista = todos.Where(a => a.AntiguedadMeses >= 6).ToList();
                    break;
                default:
                    vista = todos.Where(a => a.Atrasado).ToList();
                    break;
            }

            dgvDashboard.DataSource = vista;
            dgvDashboard.DataBind();
        }

        private void CargarPanelAlumno(Usuario usuarioLogueado)
        {
            litNombreAlumno.Text = usuarioLogueado.Nombre;

            AlumnoNegocio negocio = new AlumnoNegocio();
            Alumno alumno = negocio.buscarPorIdUsuario(usuarioLogueado.Id);

            if (alumno == null)
            {
                lblSinFicha.Visible = true;
                return;
            }

            phDatosAlumno.Visible = true;
            litPlanAlumno.Text = alumno.Plan.Descripcion;
            litVencimientoAlumno.Text = alumno.FechaVencimientoPlan.ToString("dd/MM/yyyy");
            litEstadoAlumno.Text = alumno.Atrasado
                ? "<span class=\"badge bg-danger\">Cuota atrasada</span>"
                : "<span class=\"badge bg-success\">Cuota al día</span>";
        }
    }
}
