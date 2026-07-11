using Acceso_DAL;
using Entidad_BE;
using Servicios;
using System.Collections.Generic;

namespace Negocio_BLL
{
    public class UsuarioBLL
    {
        public int maxIntentos = 3;
        private UsuarioBE usuario;
        private MP_Usuario mpUsuario = new MP_Usuario();
        private VerificadorIntegridadBLL integridad = new VerificadorIntegridadBLL();

        public List<UsuarioBE> ListarUsuarios()
        {
            return mpUsuario.ListarUsuarios();
        }

        public LoginResult Login(UsuarioBE us)
        {
            LoginResult AuthOK;
            us.pass = Encriptador.EncriptarIrrev(us.pass);
            if (SessionManager.GetInstance.logged == false)
            {
                AuthOK = mpUsuario.Login(us);
                if (AuthOK == LoginResult.LoginOK)
                {
                    UsuarioBE completo = mpUsuario.ExtraerUsuario(us.user);
                    SessionManager.GetInstance.Login(completo ?? us);
                    maxIntentos = 3;
                }
            }
            else
            {
                usuario = SessionManager.GetInstance.UsuarioActual();
                if ((usuario.user == us.user) && (usuario.pass == us.pass))
                {
                    AuthOK = LoginResult.SesionIniciada;
                    maxIntentos = 3;
                }
                else { AuthOK = LoginResult.ExisteSesion; }
            }
            if ((AuthOK != LoginResult.LoginOK) && (AuthOK != LoginResult.SesionIniciada))
            {
                if (AuthOK != LoginResult.ExisteSesion) { maxIntentos--; }
                if (maxIntentos == 0)
                {
                    usuario = mpUsuario.ExtraerUsuario(us.user);
                    if (usuario != null)
                    {
                        usuario.bloq = true;
                        usuario.pass = " ";
                        usuario.dvh = VerificadorIntegridad.CalcularDVH(usuario);
                        mpUsuario.ActualizarBloqueo(usuario, true);
                        integridad.ActualizarDVV();
                    }
                    maxIntentos = 3;
                    AuthOK = LoginResult.FinIntentos;
                }
            }
            return AuthOK;
        }

        public int VerifUsuario(UsuarioBE us, int tipo)//tipo 0 = Verificar Actual -- tipo == 1 Verificar Nueva
        {
            string encPass = Encriptador.EncriptarIrrev(us.pass);
            if (SessionManager.GetInstance.UsuarioActual().pass == encPass)
            {
                maxIntentos = 3;
                return 1;//Usuario Verificado OK -- Nueva  = Anterior
            }
            else
            {
                if (tipo == 1)//Verificar Nueva
                {
                    UsuarioBE aux = mpUsuario.ExtraerUsuario(SessionManager.GetInstance.UsuarioActual().user);
                    aux.pass = encPass;
                    aux.dvh = VerificadorIntegridad.CalcularDVH(aux);
                    mpUsuario.CambiarPass(aux);
                    integridad.ActualizarDVV();
                    SessionManager.GetInstance.UsuarioActual().pass = encPass;
                    maxIntentos = 3;

                    return 2;//Cambio de pass ok
                }
                maxIntentos--;
                if (maxIntentos == 0)
                {
                    if (tipo == 0)//Verificar Actual
                    {
                        UsuarioBE aux = mpUsuario.ExtraerUsuario(SessionManager.GetInstance.UsuarioActual().user);
                        aux.bloq = true;
                        aux.pass = " ";
                        aux.dvh = VerificadorIntegridad.CalcularDVH(aux);
                        mpUsuario.ActualizarBloqueo(aux, true);
                        integridad.ActualizarDVV();
                        return 2; //Falla Verificacion, Bloquea usuario
                    }
                    else
                    {
                        maxIntentos = 3;
                        return 3;
                    }
                }
                return 0; //Reintentar
            }
        }

        public void Logout()
        {
            SessionManager.GetInstance.Logout();
        }

        public void DesbloquearUS(UsuarioBE us)
        {
            usuario = us;
            us.pass = Encriptador.EncriptarIrrev(us.pass);
            us.bloq = false;
            us.dvh = VerificadorIntegridad.CalcularDVH(us);
            mpUsuario.ActualizarBloqueo(us, false);
            integridad.ActualizarDVV();
        }

        public void CrearUsuario(UsuarioBE us)
        {
            usuario = us;
            us.pass = Encriptador.EncriptarIrrev(us.pass);
            us.dvh = VerificadorIntegridad.CalcularDVH(us);
            mpUsuario.CrearUsuario(us);
            //El Codigo es IDENTITY: recien despues del INSERT se conoce el valor real,
            //por lo que el DVH debe recalcularse con el usuario ya persistido
            UsuarioBE creado = mpUsuario.ExtraerUsuario(us.user);
            if (creado != null)
            {
                creado.dvh = VerificadorIntegridad.CalcularDVH(creado);
                mpUsuario.ActualizarUsuario(creado);
            }
            integridad.ActualizarDVV();
        }

        public void EliminarUs(UsuarioBE us)
        {
            us.estado = false;
            us.dvh = VerificadorIntegridad.CalcularDVH(us);
            mpUsuario.EliminarUsuario(us);
            integridad.ActualizarDVV();
        }

        public void ActualizarUsuario(UsuarioBE us)
        {
            UsuarioBE actual = mpUsuario.ExtraerUsuario(us.user);
            us.pass = actual != null ? actual.pass : us.pass;
            us.dvh = VerificadorIntegridad.CalcularDVH(us);
            mpUsuario.ActualizarUsuario(us);
            integridad.ActualizarDVV();
        }

        public string GenerarPass(string ape, string dni)
        {
            string pass = ape.Substring(0, 3) + dni.Substring(0, 3);
            return pass;
        }
    }
}
