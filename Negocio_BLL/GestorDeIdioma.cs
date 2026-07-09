using Acceso_DAL;
using Entidad_BE;
using Servicios;
using System.Collections.Generic;

namespace Negocio_BLL
{
    /// <summary>
    /// Sujeto observable del patron Observer de idiomas (Singleton).
    /// Mantiene el idioma activo, el diccionario de traducciones cargado
    /// desde la base de datos y la lista de observadores suscriptos.
    /// Al cambiar el idioma notifica a todos los observadores para que
    /// refresquen sus textos sin reiniciar la aplicacion.
    /// </summary>
    public class GestorDeIdioma
    {
        private static GestorDeIdioma _gestor = null;
        private static object _lock = new object();//Bloquear acceso multihilo

        public const string IdiomaPorDefecto = "ES";

        private readonly List<IObservadorIdioma> _observadores = new List<IObservadorIdioma>();
        private Dictionary<string, string> _traducciones = new Dictionary<string, string>();
        private IdiomaBE _idiomaActual = null;
        private IdiomaDAL idiomaDAL = new IdiomaDAL();

        private GestorDeIdioma() { }

        public static GestorDeIdioma GetInstance
        {
            get
            {
                if (_gestor == null)
                {
                    lock (_lock)
                    {
                        if (_gestor == null)
                        {
                            _gestor = new GestorDeIdioma();
                        }
                    }
                }
                return _gestor;
            }
        }

        public IdiomaBE IdiomaActual
        {
            get
            {
                AsegurarIdiomaCargado();
                return _idiomaActual;
            }
        }

        public List<IdiomaBE> ObtenerIdiomas()
        {
            return idiomaDAL.ObtenerIdiomas();
        }

        public void Suscribir(IObservadorIdioma observador)
        {
            if (!_observadores.Contains(observador))
            {
                _observadores.Add(observador);
            }
            //El nuevo observador arranca alineado con el idioma vigente
            AsegurarIdiomaCargado();
            if (_idiomaActual != null)
            {
                observador.ActualizarTextos();
            }
        }

        public void Desuscribir(IObservadorIdioma observador)
        {
            _observadores.Remove(observador);
        }

        public void CambiarIdioma(string codigo)
        {
            foreach (IdiomaBE idioma in idiomaDAL.ObtenerIdiomas())
            {
                if (idioma.Codigo == codigo)
                {
                    _traducciones = idiomaDAL.ObtenerTraducciones(idioma.Id);
                    _idiomaActual = idioma;
                    Notificar();
                    return;
                }
            }
            throw new KeyNotFoundException($"El idioma '{codigo}' no se encuentra registrado en el sistema");
        }

        public string Traducir(string clave)
        {
            AsegurarIdiomaCargado();
            string texto;
            if (_traducciones.TryGetValue(clave, out texto))
            {
                return texto;
            }
            return clave;//Si falta la traduccion se muestra la clave para detectarla facil
        }

        private void Notificar()
        {
            //Copia defensiva: un observador puede desuscribirse durante la notificacion
            foreach (IObservadorIdioma observador in _observadores.ToArray())
            {
                observador.ActualizarTextos();
            }
        }

        private void AsegurarIdiomaCargado()
        {
            if (_idiomaActual == null)
            {
                try
                {
                    CambiarIdioma(IdiomaPorDefecto);
                }
                catch
                {
                    //Sin conexion o sin migracion de idiomas: la UI conserva
                    //los textos del disenio y Traducir devuelve las claves
                }
            }
        }
    }
}
