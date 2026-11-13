using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class UsuarioNegocio
    {
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT Id, Email, Rol, Nombre, Apellido, Activo FROM USUARIOS WHERE Activo = 1 ORDER BY Apellido, Nombre");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.Id = (int)datos.Lector["Id"];
                    usuario.Email = (string)datos.Lector["Email"];
                    usuario.Rol = (int)datos.Lector["Rol"];
                    usuario.Nombre = (string)datos.Lector["Nombre"];
                    usuario.Apellido = (string)datos.Lector["Apellido"];
                    usuario.Activo = (bool)datos.Lector["Activo"];

                    lista.Add(usuario);
                }

                return lista;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Agregar(Usuario nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("INSERT INTO USUARIOS (Email, Clave, Rol, Nombre, Apellido) VALUES (@email, @clave, @rol, @nombre, @apellido)");
                datos.SetearParametro("@email", nuevo.Email);
                datos.SetearParametro("@clave", nuevo.Clave);
                datos.SetearParametro("@rol", nuevo.Rol);
                datos.SetearParametro("@nombre", nuevo.Nombre);
                datos.SetearParametro("@apellido", nuevo.Apellido);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Modificar(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE USUARIOS SET Email = @email, Rol = @rol, Nombre = @nombre, Apellido = @apellido WHERE Id = @id");
                datos.SetearParametro("@email", usuario.Email);
                datos.SetearParametro("@rol", usuario.Rol);
                datos.SetearParametro("@nombre", usuario.Nombre);
                datos.SetearParametro("@apellido", usuario.Apellido);
                datos.SetearParametro("@id", usuario.Id);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void CambiarClave(int id, string nuevaClave)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE USUARIOS SET Clave = @clave WHERE Id = @id");
                datos.SetearParametro("@clave", nuevaClave);
                datos.SetearParametro("@id", id);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void BajaLogica(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE USUARIOS SET Activo = 0 WHERE Id = @id");
                datos.SetearParametro("@id", id);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public Usuario BuscarPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT Id, Email, Rol, Nombre, Apellido, Activo FROM USUARIOS WHERE Id = @id");
                datos.SetearParametro("@id", id);
                datos.EjecutarLectura();

                if (!datos.Lector.Read())
                    return null;

                Usuario usuario = new Usuario();
                usuario.Id = (int)datos.Lector["Id"];
                usuario.Email = (string)datos.Lector["Email"];
                usuario.Rol = (int)datos.Lector["Rol"];
                usuario.Nombre = (string)datos.Lector["Nombre"];
                usuario.Apellido = (string)datos.Lector["Apellido"];
                usuario.Activo = (bool)datos.Lector["Activo"];

                return usuario;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public bool ExisteEmail(string email)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT Id FROM USUARIOS WHERE Email = @email");
                datos.SetearParametro("@email", email);
                datos.EjecutarLectura();

                return datos.Lector.Read();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}
