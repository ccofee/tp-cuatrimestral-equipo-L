using System;
using System.Web.UI;

namespace Presentacion
{
    public partial class Error : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["error"] != null)
            {
                litDetalle.Text = "<pre class=\"small text-muted\">" + Server.HtmlEncode(Session["error"].ToString()) + "</pre>";
                Session.Remove("error");
            }
        }
    }
}
