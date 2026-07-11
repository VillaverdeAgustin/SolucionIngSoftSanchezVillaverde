using Entidad_BE;
using System;
using System.Collections.Generic;

namespace Servicios
{
    /// <summary>
    /// Cuidador (caretaker) del patron Memento aplicado a auditoria (T06b).
    /// Compara dos mementos de la misma entidad y genera un registro de
    /// cambio por cada campo modificado, sin repetir la logica de
    /// comparacion en cada formulario. Los campos sensibles (contraseña)
    /// se enmascaran: se audita QUE cambiaron, nunca su valor.
    /// </summary>
    public class GestorDeAuditoria
    {
        private const string Mascara = "****";
        private static readonly List<string> camposEnmascarados = new List<string> { "pass" };

        public static List<CambioBE> CompararMementos(MementoBE antes, MementoBE despues, int entidadId, string usuarioResponsable)
        {
            List<CambioBE> cambios = new List<CambioBE>();
            Dictionary<string, string> estadoAntes = antes.ObtenerEstado();

            foreach (KeyValuePair<string, string> campo in despues.ObtenerEstado())
            {
                string valorAnterior;
                estadoAntes.TryGetValue(campo.Key, out valorAnterior);
                if (valorAnterior == campo.Value) { continue; }

                bool enmascarar = camposEnmascarados.Contains(campo.Key);
                CambioBE cambio = new CambioBE();
                cambio.entidadId = entidadId;
                cambio.campo = campo.Key;
                cambio.valorAnterior = enmascarar ? Mascara : (valorAnterior ?? "");
                cambio.valorNuevo = enmascarar ? Mascara : (campo.Value ?? "");
                cambio.usuario = usuarioResponsable;
                cambio.fecha = DateTime.Now;
                cambios.Add(cambio);
            }
            return cambios;
        }
    }
}
