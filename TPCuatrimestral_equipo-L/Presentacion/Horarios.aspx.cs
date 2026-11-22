using System;
using System.Globalization;
using System.Web.UI;
using Dominio;
using Negocio;

namespace Presentacion
{
    public partial class Horarios : Page
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
                CargarActividades();
                CargarGrilla();
            }
        }

        protected void dgvHorarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            //precarga del formulario para modificar
            int idSeleccionado = int.Parse(dgvHorarios.SelectedDataKey.Value.ToString());

            HorarioNegocio negocio = new HorarioNegocio();
            Horario seleccionado = negocio.BuscarPorId(idSeleccionado);

            if (seleccionado != null)
            {
                ddlActividad.SelectedValue = seleccionado.Actividad.Id.ToString();
                ddlDiaSemana.SelectedValue = seleccionado.DiaSemana.ToString(CultureInfo.InvariantCulture);
                txtHoraInicio.Text = seleccionado.HoraInicio.ToString(@"hh\:mm");
                txtHoraFin.Text = seleccionado.HoraFin.ToString(@"hh\:mm");
                txtCupoMaximo.Text = seleccionado.CupoMaximo.ToString(CultureInfo.InvariantCulture);
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            lblErrorHorario.Visible = false;

            Page.Validate();
            if (!Page.IsValid)
                return;

            Horario horario = ArmarHorario();
            if (horario == null)
                return;

            HorarioNegocio negocio = new HorarioNegocio();

            try
            {
                negocio.Agregar(horario);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                throw;
            }

            LimpiarFormulario();
            CargarGrilla();
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            lblErrorHorario.Visible = false;

            if (dgvHorarios.SelectedIndex == -1)
            {
                lblErrorHorario.Text = "Seleccioná un horario de la grilla para modificar.";
                lblErrorHorario.Visible = true;
                return;
            }

            Page.Validate();
            if (!Page.IsValid)
                return;

            Horario horario = ArmarHorario();
            if (horario == null)
                return;

            horario.Id = int.Parse(dgvHorarios.SelectedDataKey.Value.ToString());
            HorarioNegocio negocio = new HorarioNegocio();

            try
            {
                negocio.Modificar(horario);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                throw;
            }

            LimpiarFormulario();
            CargarGrilla();
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            //baja logica de un horario
            lblErrorHorario.Visible = false;

            if (dgvHorarios.SelectedIndex == -1)
            {
                lblErrorHorario.Text = "Seleccioná un horario de la grilla para eliminar.";
                lblErrorHorario.Visible = true;
                return;
            }

            int id = int.Parse(dgvHorarios.SelectedDataKey.Value.ToString());
            HorarioNegocio negocio = new HorarioNegocio();

            try
            {
                negocio.BajaLogica(id);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                throw;
            }

            LimpiarFormulario();
            CargarGrilla();
        }

        private Horario ArmarHorario()
        {
            TimeSpan horaInicio;
            TimeSpan horaFin;
            int cupoMaximo;

            try
            {
                horaInicio = TimeSpan.Parse(txtHoraInicio.Text, CultureInfo.InvariantCulture);
                horaFin = TimeSpan.Parse(txtHoraFin.Text, CultureInfo.InvariantCulture);
                cupoMaximo = int.Parse(txtCupoMaximo.Text, CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                lblErrorHorario.Text = "Revisá el formato de los horarios y el cupo.";
                lblErrorHorario.Visible = true;
                return null;
            }

            if (horaFin <= horaInicio)
            {
                lblErrorHorario.Text = "La hora de fin debe ser posterior a la hora de inicio.";
                lblErrorHorario.Visible = true;
                return null;
            }

            if (cupoMaximo <= 0)
            {
                lblErrorHorario.Text = "El cupo máximo debe ser mayor a 0.";
                lblErrorHorario.Visible = true;
                return null;
            }

            Horario horario = new Horario();
            horario.Actividad = new Actividad();
            horario.Actividad.Id = int.Parse(ddlActividad.SelectedValue);
            horario.DiaSemana = int.Parse(ddlDiaSemana.SelectedValue, CultureInfo.InvariantCulture);
            horario.HoraInicio = horaInicio;
            horario.HoraFin = horaFin;
            horario.CupoMaximo = cupoMaximo;

            return horario;
        }

        private void CargarGrilla()
        {
            //listado de horarios
            HorarioNegocio negocio = new HorarioNegocio();
            dgvHorarios.DataSource = negocio.Listar();
            dgvHorarios.DataBind();
        }
        protected string ObtenerNombreDia(int dia)
        {
            switch (dia)
            {
                case 1:
                    return "Lunes";
                case 2:
                    return "Martes";
                case 3:
                    return "Miercoles";
                case 4:
                    return "Jueves";
                case 5:
                    return "Viernes";
                case 6:
                    return "Sabado";
                case 7:
                    return "Domingo";
                default:
                    return "";
            }
        }
        private void LimpiarFormulario()
        {
            ddlActividad.SelectedIndex = 0;
            ddlDiaSemana.SelectedIndex = 0;
            txtHoraInicio.Text = "";
            txtHoraFin.Text = "";
            txtCupoMaximo.Text = "";

            lblErrorHorario.Text = "";
            lblErrorHorario.Visible = false;

            dgvHorarios.SelectedIndex = -1;
        }
        private void CargarActividades()
        {
            ActividadNegocio negocio = new ActividadNegocio();

            ddlActividad.DataSource = negocio.listar();
            ddlActividad.DataTextField = "Nombre";
            ddlActividad.DataValueField = "Id";
            ddlActividad.DataBind();
        }
    }
}
