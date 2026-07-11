using System;

namespace Entidad_BE
{
    /// <summary>
    /// Registro de auditoria del control de cambios (T06b).
    /// Cada instancia representa la modificacion de UN campo de la
    /// entidad controlada: valor anterior, valor nuevo, quien y cuando.
    /// </summary>
    public class CambioBE
    {
        private int _id;

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _campo;

        public string campo
        {
            get { return _campo; }
            set { _campo = value; }
        }

        private string _valorAnterior;

        public string valorAnterior
        {
            get { return _valorAnterior; }
            set { _valorAnterior = value; }
        }

        private string _valorNuevo;

        public string valorNuevo
        {
            get { return _valorNuevo; }
            set { _valorNuevo = value; }
        }

        private string _usuario;

        public string usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        private DateTime _fecha;

        public DateTime fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        private int _entidadId;

        public int entidadId
        {
            get { return _entidadId; }
            set { _entidadId = value; }
        }
    }
}
