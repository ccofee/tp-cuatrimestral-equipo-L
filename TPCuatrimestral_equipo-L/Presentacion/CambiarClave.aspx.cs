using System;
using System.Web.UI;
using Dominio;
using Negocio;

namespace Presentacion
{
    public partial class CambiarClave : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuarioPendiente = Session["UsuarioPendienteCambioClave"] as Usuario;

            if (usuarioPendiente == null)
                Response.Redirect("Login.aspx");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;

            Page.Validate();
            if (!Page.IsValid)
                return;

            Usuario usuarioPendiente = Session["UsuarioPendienteCambioClave"] as Usuario;

            if (usuarioPendiente == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            UsuarioNegocio negocio = new UsuarioNegocio();
            negocio.CambiarClavePropia(usuarioPendiente.Id, txtNuevaClave.Text);

            usuarioPendiente.DebeCambiarClave = false;
            Session.Remove("UsuarioPendienteCambioClave");
            Session["UsuarioLogueado"] = usuarioPendiente;

            Response.Redirect("Default.aspx");
        }
    }
}
