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
            Usuario usuarioLogueado = Session["UsuarioLogueado"] as Usuario;

            if (usuarioLogueado == null)
                Response.Redirect("Login.aspx");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;

            Page.Validate();
            if (!Page.IsValid)
                return;

            Usuario usuarioLogueado = Session["UsuarioLogueado"] as Usuario;

            if (usuarioLogueado == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            UsuarioNegocio negocio = new UsuarioNegocio();
            negocio.CambiarClavePropia(usuarioLogueado.Id, txtNuevaClave.Text);

            usuarioLogueado.DebeCambiarClave = false;
            Session["UsuarioLogueado"] = usuarioLogueado;

            Response.Redirect("Default.aspx");
        }
    }
}
