using System;
using System.Web.UI;
using Dominio;

namespace Presentacion
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuarioLogueado = Session["UsuarioLogueado"] as Usuario;

            if (usuarioLogueado != null)
            {
                phMenuLogueado.Visible = true;
                litNombreUsuario.Text = usuarioLogueado.Nombre;

                if (usuarioLogueado.Rol == 1)
                    phMenuAdmin.Visible = true;
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }
}
