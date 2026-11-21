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
        protected void btnReservar_Click(object sender, EventArgs e)
        {
            Usuario usuarioLogueado = Session["UsuarioLogueado"] as Usuario;

            if (usuarioLogueado == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (ddlActividad.SelectedValue == "0")
                return;

            if (ddlHorario.SelectedValue == "0")
                return;

            DateTime fechaClase;

            if (!DateTime.TryParse(txtFechaClase.Text, out fechaClase))
                return;

            AlumnoNegocio alumnoNegocio = new AlumnoNegocio();
            Alumno alumno = alumnoNegocio.buscarPorIdUsuario(usuarioLogueado.Id);

            if (alumno == null)
                return;

            // Regla 1: tiene membresia vigente 
            if (alumno.FechaVencimientoPlan < DateTime.Today ||
                fechaClase > alumno.FechaVencimientoPlan)
            {
                return;
            }

            // Regla 2: el plan debe incluir clases
            if (!alumno.Plan.IncluyeClases)
            {
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
                return;
            }

            // Valida cupo
            int cantidadHorario = negocioReserva.CantidadReservasHorario(
                idHorario,
                fechaClase
            );

            if (cantidadHorario >= horario.CupoMaximo)
            {
                return;
            }
            // Crear la reserva
            Reserva nueva = new Reserva();

            nueva.Alumno = alumno;
            nueva.Horario = horario;
            nueva.FechaClase = fechaClase;
            nueva.Estado = "Reservada";

            int idReserva = negocioReserva.Agregar(nueva);

            // Actualizar la grilla
            CargarReservas();
        }
        protected void btnCancelarReserva_Click(object sender, EventArgs e)
        {
            // valida que haya una reserva seleccionada
            if (dgvReservas.SelectedIndex < 0)
                return;

            int idReserva = Convert.ToInt32(
                dgvReservas.DataKeys[dgvReservas.SelectedIndex].Value
            );

            ReservaNegocio reservaNegocio = new ReservaNegocio();

            // busca la reserva seleccionada
            Reserva reserva = reservaNegocio.BuscarPorId(idReserva);

            if (reserva == null)
                return;

            // evita cancelar una reserva ya cancelada
            if (reserva.Estado == "Cancelada")
                return;

            Usuario usuarioLogueado = Session["UsuarioLogueado"] as Usuario;

            if (usuarioLogueado == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            AlumnoNegocio alumnoNegocio = new AlumnoNegocio();
            Alumno alumno = alumnoNegocio.buscarPorIdUsuario(usuarioLogueado.Id);

            if (alumno == null)
                return;

            // verifica que la reserva sea del alumno
            if (reserva.Alumno.Id != alumno.Id)
                return;

            // arma la fecha y hora de la clase
            DateTime fechaHoraClase =
                reserva.FechaClase.Date + reserva.Horario.HoraInicio;

            // valida que falten al menos 2 horas para cancelar
            if (fechaHoraClase <= DateTime.Now.AddHours(2))
                return;

            // cancela la reserva
            reservaNegocio.Cancelar(idReserva);

            // actualiza la grilla
            CargarReservas();

            dgvReservas.SelectedIndex = -1;
        }
    }
}