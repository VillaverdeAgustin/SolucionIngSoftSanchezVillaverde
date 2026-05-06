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
        private static Bitacora instancia = null;

        private Bitacora() { }

        public static Bitacora getInstance() 
        {
            if(instancia == null)
            {
                return instancia = new Bitacora();
            }
            else { return instancia; }
        }

        public EventoBE RegistrarEvento(string us, string accion)
        {
            EventoBE evento = new EventoBE();
            evento.usuario = us;
            evento.accion = accion;
            evento.fecha = DateTime.Now;
            return evento;
        }
    }
}