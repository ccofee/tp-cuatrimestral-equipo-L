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
    public partial class Pagos : System.Web.UI.Page
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
            }
        }

        private void cargarGrilla()
        {
            PagoNegocio negocio = new PagoNegocio();

            try
            {
                List<Pago> lista = negocio.listar();
                Session.Add("listaPagos", lista);
                enlazarGrillaYResumen(lista);
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

        protected void ddlPeriodo_SelectedIndexChanged(object sender, EventArgs e)
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
            List<Pago> lista = (List<Pago>)Session["listaPagos"];
            string filtro = txtFiltro.Text.ToUpper();
            string periodo = ddlPeriodo.SelectedValue;

            List<Pago> listaFiltrada = lista.FindAll(x =>
                (x.AlumnoDNI.ToUpper().Contains(filtro) ||
                 x.AlumnoNombre.ToUpper().Contains(filtro)) &&
                (periodo == "Todos" ||
                 (periodo == "Hoy" && x.FechaPago.Date == DateTime.Today) ||
                 (periodo == "7Dias" && x.FechaPago.Date >= DateTime.Today.AddDays(-7)) ||
                 (periodo == "MesActual" && x.FechaPago.Month == DateTime.Today.Month && x.FechaPago.Year == DateTime.Today.Year))
            );

            enlazarGrillaYResumen(listaFiltrada);
        }

        private void enlazarGrillaYResumen(List<Pago> lista)
        {
            dgvPagos.DataSource = lista;
            dgvPagos.DataBind();

            decimal total = 0;
            foreach (Pago item in lista)
            {
                total += item.Monto;
            }

            litCantidadPagos.Text = lista.Count.ToString();
            litTotalRecaudado.Text = total.ToString("C");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtFiltro.Text = string.Empty;
            ddlPeriodo.SelectedIndex = 0;

            List<Pago> lista = (List<Pago>)Session["listaPagos"];
            enlazarGrillaYResumen(lista);
        }
    }
}