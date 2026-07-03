using System.Collections.Generic;
using System.Linq;
using Acceso_DAL;
using Entidad_BE;

namespace Negocio_BLL
{
    public class PerfilBLL
    {
        private PerfilDAL perfilDAL = new PerfilDAL();

        public List<Permiso> ListaPermisos(string pTipo = "")
        {
            return perfilDAL.ListaPermisos(pTipo);
        }

        public bool ValidarNombre(string pNombre)
        {
            if (perfilDAL.ListaPermisos("Compuesto").Exists(x => x.Nombre == pNombre))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool PerfilEnUso(string nombre)
        {
            return perfilDAL.PerfilEnUso(nombre);
        }

        public void EliminarFamilia(Familia pFamilia)
        {
            perfilDAL.EliminarFamilia(pFamilia);
        }

        public List<Permiso> ListaPermisosEnArbol()
        {
            return perfilDAL.ListaPermisosEnArbol();
        }

        public void AgregarFamilia(Familia pFamilia)
        {
            perfilDAL.AgregarFamilia(pFamilia);
        }

        public void ModificarFamilia(Familia pFamilia, List<string> permisos)
        {
            perfilDAL.ModificarFamilia(pFamilia, permisos);
        }

        public void AgregarPermisoFamilia(string pNombreFamilia, List<string> pNombrePermisos)
        {
            foreach (var s in pNombrePermisos)
            {
                perfilDAL.AgregarPermisoAFamilia(pNombreFamilia, s);
            }
        }

        public List<Permiso> ObtenerHijosDeFamilia(string nombreFamilia)
        {
            List<Permiso> permisosEnArbol = ListaPermisosEnArbol();

            // Verificar si permisosEnArbol no es nulo
            if (permisosEnArbol == null)
            {
                return new List<Permiso>();
            }

            // Encontrar la familia con el nombre dado
            Familia familia = permisosEnArbol.OfType<Familia>().FirstOrDefault(f => f.Nombre == nombreFamilia);

            // Devolver los hijos si se encuentra la familia, de lo contrario una lista vacía
            return familia?.RetornarListaHijos() ?? new List<Permiso>();
        }

        public void EliminarPermisoDeFamilia(string familia, string permiso)
        {
            perfilDAL.EliminarPermisoDeFamilia(familia, permiso);
        }
    }
}
