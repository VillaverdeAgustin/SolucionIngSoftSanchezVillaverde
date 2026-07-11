using System;
using System.Collections.Generic;

namespace Entidad_BE
{
    /// <summary>
    /// Memento del patron Memento (T06b - Control de Cambios).
    /// Guarda una foto inmutable del estado de una entidad como pares
    /// campo/valor, sin exponer la estructura interna del originador.
    /// </summary>
    public class MementoBE
    {
        private readonly Dictionary<string, string> _estado;
        private readonly DateTime _fechaCaptura;

        public DateTime fechaCaptura
        {
            get { return _fechaCaptura; }
        }

        public MementoBE(Dictionary<string, string> estado)
        {
            _estado = new Dictionary<string, string>(estado);
            _fechaCaptura = DateTime.Now;
        }

        public Dictionary<string, string> ObtenerEstado()
        {
            //Copia defensiva: el estado capturado no puede modificarse desde afuera
            return new Dictionary<string, string>(_estado);
        }
    }
}
