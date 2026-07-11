namespace Entidad_BE
{
    /// <summary>
    /// Originador del patron Memento (T06b - Control de Cambios).
    /// Toda entidad auditable sabe fotografiar su estado en un memento
    /// y reconstruirse a partir de una foto anterior.
    /// </summary>
    public interface IOriginadorCambios
    {
        MementoBE CrearMemento();

        void RestaurarMemento(MementoBE memento);
    }
}
