using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class Alumnos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    AlumnoNegocio negocio = new AlumnoNegocio();
                    dgvAlumnos.DataSource = negocio.listar();
                    dgvAlumnos.DataBind();
                }
                catch (Exception ex)
                {
                    Session.Add("error", ex);
                    // Response.Redirect("Error.aspx");

                    throw ex;
                }
            }
        }
    }
}