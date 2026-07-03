using Acceso_DAL;
using Entidad_BE;
using Servicios;
using System.Collections.Generic;

namespace Negocio_BLL
{
    public class BitacoraBLL
    {
        private MP_Bitacora mpBitacora = new MP_Bitacora();

        public List<EventoBE> ListarBitacora()
        {
            return mpBitacora.ListarEventos();
        }

        public void RegistrarBitacora(string us, TipoAccion acc)
        {
            mpBitacora.RegistrarEvento(Bitacora.RegistrarEvento(us, acc.ToString()));
        }

        public void RegistrarBitacora(string us, string acc)
        {
            mpBitacora.RegistrarEvento(Bitacora.RegistrarEvento(us, acc));
        }
    }
}
