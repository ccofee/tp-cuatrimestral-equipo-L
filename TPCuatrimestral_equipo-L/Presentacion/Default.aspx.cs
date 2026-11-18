using System;
using System.Web.UI;
using Dominio;

namespace Presentacion
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuarioLogueado = Session["UsuarioLogueado"] as Usuario;

            if (usuarioLogueado == null)
                Response.Redirect("Login.aspx");
        }
    }
}
