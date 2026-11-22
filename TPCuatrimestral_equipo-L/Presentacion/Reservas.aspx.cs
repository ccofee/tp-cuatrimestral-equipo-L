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
            Usuario usuarioLogueado = Session["UsuarioLogueado"] as Usuario;

            if (usuarioLogueado == null)
                return;

            AlumnoNegocio alumnoNegocio = new AlumnoNegocio();
            Alumno alumno = alumnoNegocio.buscarPorIdUsuario(usuarioLogueado.Id);

            if (alumno == null)
                return;

            ReservaNegocio negocio = new ReservaNegocio();
            List<Reserva> reservas = negocio.Listar();

            dgvReservas.DataSource = reservas.Where(x => x.Alumno.Id == alumno.Id).ToList();
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
        protected void btnReservar_Click(object sender, EventArgs e)
        {
            lblMensajeReserva.Visible = false;
            lblMensajeReserva.Text = "";
            Usuario usuarioLogueado = Session["UsuarioLogueado"] as Usuario;

            if (usuarioLogueado == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (ddlActividad.SelectedValue == "0")
            {
                lblMensajeReserva.Text = "Selecciona una actividad.";
                lblMensajeReserva.CssClass = "d-block mt-3 text-danger";
                lblMensajeReserva.Visible = true;
                return;
            }
            if (ddlHorario.SelectedValue == "0")
            {
                lblMensajeReserva.Text = "Selecciona un horario.";
                lblMensajeReserva.CssClass = "d-block mt-3 text-danger";
                lblMensajeReserva.Visible = true;
                return;
            }
            
            DateTime fechaClase;

            if (!DateTime.TryParse(txtFechaClase.Text, out fechaClase))
            {
                lblMensajeReserva.Text = "Selecciona una fecha valida.";
                lblMensajeReserva.CssClass = "d-block mt-3 text-danger";
                lblMensajeReserva.Visible = true;
                return;
            }

            AlumnoNegocio alumnoNegocio = new AlumnoNegocio();
            Alumno alumno = alumnoNegocio.buscarPorIdUsuario(usuarioLogueado.Id);

            if (alumno == null)
            {
                lblMensajeReserva.Text = "No se encontro un alumno asociado a este usuario.";
                lblMensajeReserva.CssClass = "d-block mt-3 text-danger";
                lblMensajeReserva.Visible = true;
                return;
            }

            // Regla 1: tiene membresia vigente 
            if (alumno.FechaVencimientoPlan < DateTime.Today ||
                fechaClase > alumno.FechaVencimientoPlan)
            {
                lblMensajeReserva.Text = "Tu plan no esta vigente para la fecha seleccionada.";
                lblMensajeReserva.CssClass = "d-block mt-3 text-danger";
                lblMensajeReserva.Visible = true;
                return;
            }

            // Regla 2: el plan debe incluir clases
            if (!alumno.Plan.IncluyeClases)
            {
                lblMensajeReserva.Text = "Tu plan no incluye clases.";
                lblMensajeReserva.CssClass = "d-block mt-3 text-danger";
                lblMensajeReserva.Visible = true;
                return;
            }
            // Regla 3: limite de las clases semanales
            if (alumno.Plan.ClasesSemanales > 0)
            {
                int diferencia = (7 + (fechaClase.DayOfWeek - DayOfWeek.Monday)) % 7;
                DateTime lunes = fechaClase.AddDays(-diferencia).Date;
                DateTime domingo = lunes.AddDays(6);

                ReservaNegocio reservaNegocio = new ReservaNegocio();

                int cantidadSemana = reservaNegocio.CantidadReservasSemana(
                    alumno.Id,
                    lunes,
                    domingo
                );

                if (cantidadSemana >= alumno.Plan.ClasesSemanales)
                {
                    lblMensajeReserva.Text = "Ya alcanzaste el limite de clases semanales de tu plan.";
                    lblMensajeReserva.CssClass = "d-block mt-3 text-danger";
                    lblMensajeReserva.Visible = true;
                    return;
                }
            }
            // Regla 4: cupo y reserva duplicada
            int idHorario = int.Parse(ddlHorario.SelectedValue);

            HorarioNegocio horarioNegocio = new HorarioNegocio();
            Horario horario = horarioNegocio.BuscarPorId(idHorario);

            if (horario == null)
                return;

            ReservaNegocio negocioReserva = new ReservaNegocio();

            // Evita reserva duplicada
            if (negocioReserva.ExisteReserva(alumno.Id, idHorario, fechaClase))
            {
                lblMensajeReserva.Text = "Ya tenes una reserva para ese horario y fecha.";
                lblMensajeReserva.CssClass = "d-block mt-3 text-danger";
                lblMensajeReserva.Visible = true;
                return;
            }

            // Valida cupo
            int cantidadHorario = negocioReserva.CantidadReservasHorario(
                idHorario,
                fechaClase
            );

            Reserva nueva = new Reserva();
           

            nueva.Alumno = alumno;
            nueva.Horario = horario;
            nueva.FechaClase = fechaClase;

            // si no hay cupo queda en espera
            if (cantidadHorario >= horario.CupoMaximo)
                nueva.Estado = "EnEspera";
            else
                nueva.Estado = "Reservada";

            int idReserva = negocioReserva.Agregar(nueva);
            if (nueva.Estado == "Reservada")
            {
                lblMensajeReserva.Text = "Reserva realizada correctamente.";
                lblMensajeReserva.CssClass = "d-block mt-3 text-success";
            }
            else
            {
                lblMensajeReserva.Text = "El horario esta completo. Quedaste en lista de espera.";
                lblMensajeReserva.CssClass = "d-block mt-3 text-warning";
            }

            lblMensajeReserva.Visible = true;
            // actualiza la grilla
            CargarReservas();
        }
        protected void btnCancelarReserva_Click(object sender, EventArgs e)
        {
            lblMensajeReserva.Visible = false;
            lblMensajeReserva.Text = "";
            // valida que haya una reserva seleccionada
            if (dgvReservas.SelectedIndex < 0)
            {
                lblMensajeReserva.Text = "Selecciona una reserva para cancelar.";
                lblMensajeReserva.CssClass = "d-block mt-3 text-danger";
                lblMensajeReserva.Visible = true;
                return;
            }

            int idReserva = Convert.ToInt32(
                dgvReservas.DataKeys[dgvReservas.SelectedIndex].Value
            );

            ReservaNegocio reservaNegocio = new ReservaNegocio();

            // busca la reserva seleccionada
            Reserva reserva = reservaNegocio.BuscarPorId(idReserva);

            if (reserva == null)
            {
                lblMensajeReserva.Text = "No se encontro la reserva seleccionada.";
                lblMensajeReserva.CssClass = "d-block mt-3 text-danger";
                lblMensajeReserva.Visible = true;
                return;
            }

            // evita cancelar una reserva ya cancelada
            if (reserva.Estado == "Cancelada")
            {
                lblMensajeReserva.Text = "La reserva ya esta cancelada.";
                lblMensajeReserva.CssClass = "d-block mt-3 text-danger";
                lblMensajeReserva.Visible = true;
                return;
            }

            Usuario usuarioLogueado = Session["UsuarioLogueado"] as Usuario;

            if (usuarioLogueado == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            AlumnoNegocio alumnoNegocio = new AlumnoNegocio();
            Alumno alumno = alumnoNegocio.buscarPorIdUsuario(usuarioLogueado.Id);

            if (alumno == null)
            {
                lblMensajeReserva.Text = "No se encontro un alumno asociado a este usuario.";
                lblMensajeReserva.CssClass = "d-block mt-3 text-danger";
                lblMensajeReserva.Visible = true;
                return;
            }

            // verifica que la reserva sea del alumno
            if (reserva.Alumno.Id != alumno.Id)
            {
                lblMensajeReserva.Text = "No podes cancelar una reserva de otro alumno.";
                lblMensajeReserva.CssClass = "d-block mt-3 text-danger";
                lblMensajeReserva.Visible = true;
                return;
            }

            // arma la fecha y hora de la clase
            DateTime fechaHoraClase =
                reserva.FechaClase.Date + reserva.Horario.HoraInicio;

            // valida que falten al menos 2 horas para cancelar
            if (fechaHoraClase <= DateTime.Now.AddHours(2))
            {
                lblMensajeReserva.Text = "La reserva solo se puede cancelar con al menos 2 horas de anticipacion.";
                lblMensajeReserva.CssClass = "d-block mt-3 text-danger";
                lblMensajeReserva.Visible = true;
                return;
            }

            // guarda si la reserva ocupaba un cupo
            bool liberaCupo = reserva.Estado == "Reservada";

            // cancela la reserva
            reservaNegocio.Cancelar(idReserva);


            // si libero un cupo busca al primero en espera
            if (liberaCupo)
            {
                reservaNegocio.PromoverPrimeroEnEspera(
                    reserva.Horario.Id,
                    reserva.FechaClase
                );
            }
           // mensaje
            lblMensajeReserva.Text = "Reserva cancelada correctamente.";
            lblMensajeReserva.CssClass = "d-block mt-3 text-success";
            lblMensajeReserva.Visible = true;

            // actualiza la grilla
            CargarReservas();

            dgvReservas.SelectedIndex = -1;
        }
    }
}