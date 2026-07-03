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
            evento.accion = accion.ToString();
            evento.fecha = DateTime.Now;
            return evento;
        }
    }
}