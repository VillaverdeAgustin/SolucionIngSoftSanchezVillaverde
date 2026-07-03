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
                    SessionManager.GetInstance.Login(us);
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
            if ((AuthOK != LoginResult.LoginOK) || (AuthOK != LoginResult.ExisteSesion)) { maxIntentos--; }
            if (maxIntentos == 0)
            {
                usuario = new UsuarioBE();
                usuario.user = us.user;
                usuario.pass = " ";
                mpUsuario.ActualizarBloqueo(usuario, true);
                AuthOK = LoginResult.FinIntentos;
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
                    SessionManager.GetInstance.UsuarioActual().pass = encPass;
                    mpUsuario.CambiarPass(SessionManager.GetInstance.UsuarioActual());
                    maxIntentos = 3;

                    return 2;//Cambio de pass ok
                }
                maxIntentos--;
                if (maxIntentos == 0)
                {
                    if (tipo == 0)//Verificar Actual
                    {
                        us.user = SessionManager.GetInstance.UsuarioActual().user;
                        us.pass = " ";
                        mpUsuario.ActualizarBloqueo(us, true);
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
            mpUsuario.ActualizarBloqueo(us, false);
        }

        public void CrearUsuario(UsuarioBE us)
        {
            usuario = us;
            us.pass = Encriptador.EncriptarIrrev(us.pass);
            mpUsuario.CrearUsuario(us);
        }

        public void EliminarUs(UsuarioBE us)
        {
            mpUsuario.EliminarUsuario(us);
        }

        public void ActualizarUsuario(UsuarioBE us)
        {
            mpUsuario.ActualizarUsuario(us);
        }

        public string GenerarPass(string ape, string dni)
        {
            string pass = ape.Substring(0, 3) + dni.Substring(0, 3);
            return pass;
        }
    }
}
