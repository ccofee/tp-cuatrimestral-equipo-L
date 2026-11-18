using System;

namespace Presentacion
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception excepcion = Server.GetLastError();
            Session["error"] = excepcion.ToString();

            Server.ClearError();
            Response.Redirect("Error.aspx");
        }
    }
}
