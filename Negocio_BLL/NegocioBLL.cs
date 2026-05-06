using Acceso_DAL;
using Entidad_BE;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio_BLL
{
    public class NegocioBLL
    {
        private MP_Usuario mpUsuario = new MP_Usuario();
        private MP_Bitacora mpBitacora = new MP_Bitacora();

        public List<UsuarioBE> ListarUsuarios()
        {
            return mpUsuario.ListarUsuarios();
        }

        public void RegistrarBitacora(string us, string acc)
        {
            mpBitacora.RegistrarEvento(Bitacora.getInstance().RegistrarEvento(us,acc));
        }
    }
}
