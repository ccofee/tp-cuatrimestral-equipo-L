using System;
using Negocio;

namespace Presentacion
{
    public partial class OlvideContrasena : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
                return;

            UsuarioNegocio negocio = new UsuarioNegocio();
            negocio.RestablecerClave(txtEmail.Text.Trim());

            lblMensaje.Text = "Si el email está registrado, te enviamos una contraseña provisoria.";
            lblMensaje.Visible = true;

            btnEnviar.Enabled = false;
            txtEmail.Enabled = false;
        }
    }
}
