using System;
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

        public bool Agregar(Usuario nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("INSERT INTO USUARIOS (Email, Clave, Rol, Nombre, Apellido, DebeCambiarClave) VALUES (@email, @clave, @rol, @nombre, @apellido, 1)");
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

            //si es alumno, vincula el usuario recien creado con su registro en ALUMNOS (FK va de ALUMNOS hacia USUARIOS)
            if (nuevo.Rol == 2)
            {
                int idUsuarioGenerado = BuscarIdUsuarioPorEmail(nuevo.Email);

                if (idUsuarioGenerado > 0)
                    return VincularAlumno(idUsuarioGenerado, nuevo.Email);

                return false;
            }

            return true;
        }

        private int BuscarIdUsuarioPorEmail(string email)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT Id FROM USUARIOS WHERE Email = @email");
                datos.SetearParametro("@email", email);
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                    return (int)datos.Lector["Id"];

                return 0;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        private bool VincularAlumno(int idUsuario, string email)
        {
            AccesoDatos consulta = new AccesoDatos();
            bool existeAlumnoSinVincular;

            try
            {
                consulta.SetearConsulta("SELECT Id FROM ALUMNOS WHERE Email = @email AND Activo = 1 AND IdUsuario IS NULL");
                consulta.SetearParametro("@email", email);
                consulta.EjecutarLectura();
                existeAlumnoSinVincular = consulta.Lector.Read();
            }
            finally
            {
                consulta.CerrarConexion();
            }

            if (!existeAlumnoSinVincular)
                return false;

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE ALUMNOS SET IdUsuario = @idUsuario WHERE Email = @email AND Activo = 1 AND IdUsuario IS NULL");
                datos.SetearParametro("@idUsuario", idUsuario);
                datos.SetearParametro("@email", email);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }

            return true;
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
                datos.SetearConsulta("UPDATE USUARIOS SET Clave = @clave, DebeCambiarClave = 1 WHERE Id = @id");
                datos.SetearParametro("@clave", nuevaClave);
                datos.SetearParametro("@id", id);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void CambiarClavePropia(int id, string nuevaClave)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE USUARIOS SET Clave = @clave, DebeCambiarClave = 0 WHERE Id = @id");
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

            //desvincula al alumno (si lo hubiera) para que pueda vincularse a un usuario nuevo mas adelante
            AccesoDatos desvincular = new AccesoDatos();

            try
            {
                desvincular.SetearConsulta("UPDATE ALUMNOS SET IdUsuario = NULL WHERE IdUsuario = @id");
                desvincular.SetearParametro("@id", id);
                desvincular.EjecutarAccion();
            }
            finally
            {
                desvincular.CerrarConexion();
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

        public Usuario BuscarPorEmail(string email)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT Id, Email, Clave, Rol, Nombre, Apellido, Activo, DebeCambiarClave FROM USUARIOS WHERE Email = @email AND Activo = 1");
                datos.SetearParametro("@email", email);
                datos.EjecutarLectura();

                if (!datos.Lector.Read())
                    return null;

                Usuario usuario = new Usuario();
                usuario.Id = (int)datos.Lector["Id"];
                usuario.Email = (string)datos.Lector["Email"];
                usuario.Clave = (string)datos.Lector["Clave"];
                usuario.Rol = (int)datos.Lector["Rol"];
                usuario.Nombre = (string)datos.Lector["Nombre"];
                usuario.Apellido = (string)datos.Lector["Apellido"];
                usuario.Activo = (bool)datos.Lector["Activo"];
                usuario.DebeCambiarClave = (bool)datos.Lector["DebeCambiarClave"];

                return usuario;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public bool RestablecerClave(string email)
        {
            Usuario usuario = BuscarPorEmail(email);

            if (usuario == null)
                return false;

            string claveProvisoria = GenerarClaveProvisoria();

            EmailService servicioMail = new EmailService();
            servicioMail.EnviarMail(usuario.Email, "Tu nueva contraseña provisoria - GymConnect",
                "Hola " + usuario.Nombre + ",\n\nTu nueva contraseña provisoria es: " + claveProvisoria +
                "\n\nIngresá con ella desde Login y te recomendamos cambiarla apenas puedas.");

            CambiarClave(usuario.Id, claveProvisoria);

            return true;
        }

        private string GenerarClaveProvisoria()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 8);
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

        public bool ExisteEmail(string email, int idExcluir)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT Id FROM USUARIOS WHERE Email = @email AND Id != @id");
                datos.SetearParametro("@email", email);
                datos.SetearParametro("@id", idExcluir);
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
