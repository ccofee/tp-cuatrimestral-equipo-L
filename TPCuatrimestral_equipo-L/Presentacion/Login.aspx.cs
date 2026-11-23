using System;
using Dominio;
using Negocio;

namespace Presentacion
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            lblErrorLogin.Visible = false;

            Page.Validate();
            if (!Page.IsValid)
                return;

            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario usuario = negocio.BuscarPorEmail(txtEmail.Text.Trim());

            if (usuario == null || usuario.Clave != txtClave.Text)
            {
                lblErrorLogin.Text = "Email o contraseña incorrectos.";
                lblErrorLogin.Visible = true;
                return;
            }

            if (usuario.DebeCambiarClave)
            {
                Session.Remove("UsuarioLogueado");
                Session["UsuarioPendienteCambioClave"] = usuario;
                Response.Redirect("CambiarClave.aspx");
                return;
            }

            Session["UsuarioLogueado"] = usuario;
            Response.Redirect("Default.aspx");
        }
    }
}
