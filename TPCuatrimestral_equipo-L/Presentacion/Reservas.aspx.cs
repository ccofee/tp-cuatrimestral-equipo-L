using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class Reservas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Reserva> reservas = new List<Reserva>();

                reservas.Add(new Reserva
                {
                    Id = 1,
                    FechaClase = DateTime.Today.AddDays(1),
                    Estado = "Reservada",
                    FechaAlta = DateTime.Now
                });

                reservas.Add(new Reserva
                {
                    Id = 2,
                    FechaClase = DateTime.Today.AddDays(3),
                    Estado = "Reservada",
                    FechaAlta = DateTime.Now
                });

                dgvReservas.DataSource = reservas;
                dgvReservas.DataBind();
            }
        }
    }
}