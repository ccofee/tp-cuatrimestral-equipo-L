using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace Presentacion
{
    public partial class Reservas : System.Web.UI.Page
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
                CargarActividades();
                CargarReservas();
            }
        }
        private void CargarActividades()
        {
            ActividadNegocio negocio = new ActividadNegocio();

            ddlActividad.DataSource = negocio.listar();
            ddlActividad.DataTextField = "Nombre";
            ddlActividad.DataValueField = "Id";
            ddlActividad.DataBind();
        }
        private void CargarReservas()
        {
            ReservaNegocio negocio = new ReservaNegocio();

            dgvReservas.DataSource = negocio.Listar();
            dgvReservas.DataBind();
        }
        protected void ddlActividad_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlHorario.Items.Clear();
            ddlHorario.Items.Add(new ListItem("-- Seleccione un horario --", "0"));

            if (ddlActividad.SelectedValue == "0")
                return;

            HorarioNegocio negocio = new HorarioNegocio();
            List<Horario> horarios = negocio.Listar();

            int idActividad = int.Parse(ddlActividad.SelectedValue);

            foreach (Horario horario in horarios)
            {
                if (horario.Actividad.Id == idActividad)
                {
                    ListItem item = new ListItem();

                    item.Text = horario.DiaSemana + " - " +
                                horario.HoraInicio.ToString(@"hh\:mm") + " a " +
                                horario.HoraFin.ToString(@"hh\:mm");

                    item.Value = horario.Id.ToString();

                    ddlHorario.Items.Add(item);
                }
            }
        }
    }
}