using Acceso_DAL;
using Entidad_BE;
using Servicios;
using System.Collections.Generic;

namespace Negocio_BLL
{
    /// <summary>
    /// Control de Cambios sobre la entidad Usuario (T06b).
    /// Recibe el memento previo a la modificacion y la entidad ya
    /// modificada, delega la comparacion campo a campo en el
    /// GestorDeAuditoria y persiste una fila por cada campo que cambio.
    /// </summary>
    public class HistorialCambiosBLL
    {
        private MP_HistorialCambios mpHistorial = new MP_HistorialCambios();

        public void RegistrarCambiosUsuario(MementoBE antes, UsuarioBE despues, string usuarioResponsable)
        {
            List<CambioBE> cambios = GestorDeAuditoria.CompararMementos(antes, despues.CrearMemento(), despues.cod, usuarioResponsable);
            foreach (CambioBE cambio in cambios)
            {
                mpHistorial.RegistrarCambio(cambio);
            }
        }

        public List<CambioBE> ListarCambiosUsuario(int codUsuario)
        {
            return mpHistorial.ListarCambios(codUsuario);
        }
    }
}
