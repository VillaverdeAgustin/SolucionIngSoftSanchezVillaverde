using Entidad_BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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