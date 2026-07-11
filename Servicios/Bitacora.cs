using Entidad_BE;
using System;

namespace Servicios
{
    public class Bitacora
    {
        public static EventoBE RegistrarEvento(string us, string accion)
        {
            EventoBE evento = new EventoBE();
            evento.usuario = us;
            //La accion puede llegar con detalle ("AltaUsuario pepe"): solo el primer token es el enum
            evento.accion = (TipoAccion)Enum.Parse(typeof(TipoAccion), accion.Split(' ')[0]);
            evento.fecha = DateTime.Now;
            return evento;
        }
    }
}