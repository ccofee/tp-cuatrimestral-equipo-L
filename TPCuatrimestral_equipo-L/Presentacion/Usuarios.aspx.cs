using System;
using System.Globalization;
using System.Web.UI;
using Dominio;
using Negocio;

namespace Presentacion
{
    public partial class Usuarios : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarGrilla();
        }

        protected void dgvUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idSeleccionado = int.Parse(dgvUsuarios.SelectedDataKey.Value.ToString());

            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario seleccionado = negocio.BuscarPorId(idSeleccionado);

            if (seleccionado != null)
            {
                txtEmail.Text = seleccionado.Email;
                txtClave.Text = "";
                txtNombre.Text = seleccionado.Nombre;
                txtApellido.Text = seleccionado.Apellido;
                ddlRol.SelectedValue = seleccionado.Rol.ToString(CultureInfo.InvariantCulture);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            lblErrorUsuario.Visible = false;

            Page.Validate();
            if (!Page.IsValid)
                return;

            bool esAlta = dgvUsuarios.SelectedIndex == -1;

            if (esAlta && string.IsNullOrWhiteSpace(txtClave.Text))
            {
                lblErrorUsuario.Text = "La contraseña es obligatoria para un usuario nuevo.";
                lblErrorUsuario.Visible = true;
                return;
            }

            Usuario usuario = new Usuario();
            usuario.Email = txtEmail.Text.Trim();
            usuario.Nombre = txtNombre.Text.Trim();
            usuario.Apellido = txtApellido.Text.Trim();
            usuario.Rol = int.Parse(ddlRol.SelectedValue, CultureInfo.InvariantCulture);

            UsuarioNegocio negocio = new UsuarioNegocio();

            try
            {
                if (esAlta)
                {
                    if (negocio.ExisteEmail(usuario.Email))
                    {
                        lblErrorUsuario.Text = "El email ingresado ya está registrado.";
                        lblErrorUsuario.Visible = true;
                        return;
                    }

                    usuario.Clave = txtClave.Text;
                    negocio.Agregar(usuario);
                }
                else
                {
                    usuario.Id = int.Parse(dgvUsuarios.SelectedDataKey.Value.ToString());
                    negocio.Modificar(usuario);

                    if (!string.IsNullOrWhiteSpace(txtClave.Text))
                        negocio.CambiarClave(usuario.Id, txtClave.Text);
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                throw;
            }

            LimpiarFormulario();
            CargarGrilla();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            CargarGrilla();
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedIndex == -1)
                return;

            int id = int.Parse(dgvUsuarios.SelectedDataKey.Value.ToString());
            UsuarioNegocio negocio = new UsuarioNegocio();

            try
            {
                negocio.BajaLogica(id);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                throw;
            }

            LimpiarFormulario();
            CargarGrilla();
        }

        private void CargarGrilla()
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            dgvUsuarios.DataSource = negocio.Listar();
            dgvUsuarios.DataBind();
        }

        private void LimpiarFormulario()
        {
            txtEmail.Text = "";
            txtClave.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            ddlRol.SelectedIndex = 0;

            lblErrorUsuario.Text = "";
            lblErrorUsuario.Visible = false;

            dgvUsuarios.SelectedIndex = -1;
        }
    }
}
