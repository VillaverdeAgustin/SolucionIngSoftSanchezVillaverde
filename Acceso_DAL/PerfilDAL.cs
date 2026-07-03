using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad_BE;

namespace Acceso_DAL
{
    public class PerfilDAL
    {
        private AccesoDatos conexDB = new AccesoDatos();

        public List<Permiso> ListaPermisos(string tipo = "")
        {
            string command = "";
            if (tipo == "Compuesto" || tipo == "Simple")
            {
                command = $"select * from Permiso where Tipo = '{tipo}'";
            }
            else if (tipo == "Rol")
            {
                command = $"select * from Permiso where Rol = 1";
            }
            else
            {
                command = $"select * from Permiso";
            }

            conexDB.AbrirConexion();
            SqlCommand cmd = new SqlCommand(command, conexDB.conexion);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            conexDB.CerrarConexion();

            List<Permiso> listaPermiso = new List<Permiso>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr[1].ToString() == "Simple")
                {
                    listaPermiso.Add(new PermisoSimple(dr[0].ToString()));
                }
                else
                {
                    if (bool.Parse(dr[2].ToString()))
                    {
                        listaPermiso.Add(new Familia(dr[0].ToString(), true));
                    }
                    else
                    {
                        listaPermiso.Add(new Familia(dr[0].ToString(), false));
                    }
                }
            }

            return listaPermiso;
        }

        public bool PerfilEnUso(string nombre)
        {
            conexDB.AbrirConexion();
            string query = $"SELECT * FROM Usuario WHERE Rol = '{nombre}'";
            SqlCommand cmd = new SqlCommand(query, conexDB.conexion);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            conexDB.CerrarConexion();

            int count = ds.Tables[0].Rows.Count;
            return count > 0;
        }

        public List<Permiso> ListaPermisosEnArbol()
        {
            List<Permiso> listapermisosSimples = new List<Permiso>();
            List<Permiso> listapermisosCompuestos = new List<Permiso>();

            string command = "select * from Permiso";
            conexDB.AbrirConexion();
            SqlCommand cmd = new SqlCommand(command, conexDB.conexion);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            conexDB.CerrarConexion();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr[1].ToString() == "Simple")
                {
                    listapermisosSimples.Add(new PermisoSimple(dr[0].ToString()));
                }
                else
                {
                    Familia pfamilia;
                    if (bool.Parse(dr[2].ToString()))
                    {
                        pfamilia = new Familia(dr[0].ToString(), true);
                    }
                    else
                    {
                        pfamilia = new Familia(dr[0].ToString(), false);
                    }

                    listapermisosCompuestos.Add(pfamilia);
                    listapermisosSimples.Add(pfamilia);
                }
            }

            command = "Select * from PermisoPermisos";
            conexDB.AbrirConexion();
            cmd = new SqlCommand(command, conexDB.conexion);
            adapter = new SqlDataAdapter(cmd);
            ds = new DataSet();
            adapter.Fill(ds);
            conexDB.CerrarConexion();

            Familia familialeida;
            Permiso permisoleido;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                familialeida = (Familia)listapermisosCompuestos.Find(x => x.Nombre == dr[0].ToString());
                permisoleido = listapermisosSimples.Find(x => x.Nombre == dr[1].ToString());

                if (familialeida != null && permisoleido != null)
                {
                    familialeida.AgregarHijo(permisoleido);
                }
            }

            return listapermisosCompuestos;
        }

        public void AgregarFamilia(Familia pFamilia)
        {
            conexDB.AbrirConexion();
            string query = $"INSERT INTO Permiso (Nombre_Permiso, Tipo, Rol) VALUES ('{pFamilia.Nombre}','Compuesto', {(pFamilia.EsRol ? 1 : 0)})";
            SqlCommand cmd = new SqlCommand(query, conexDB.conexion);
            cmd.ExecuteNonQuery();
            conexDB.CerrarConexion();
        }

        public void EliminarFamilia(Familia pFamilia)
        {
            conexDB.AbrirConexion();

            string command = $"delete from PermisoPermisos where Nombre_PermisoCompuesto = '{pFamilia.Nombre}' Or Nombre_Permiso = '{pFamilia.Nombre}'";
            SqlCommand cmd = new SqlCommand(command, conexDB.conexion);
            cmd.ExecuteNonQuery();

            command = $"delete from Permiso where Nombre_Permiso = '{pFamilia.Nombre}'";
            cmd = new SqlCommand(command, conexDB.conexion);
            cmd.ExecuteNonQuery();

            conexDB.CerrarConexion();
        }

        public void ModificarFamilia(Familia pFamilia, List<string> permisos)
        {
            conexDB.AbrirConexion();

            string command = $"delete from PermisoPermisos where Nombre_PermisoCompuesto = '{pFamilia.Nombre}'";
            SqlCommand cmd = new SqlCommand(command, conexDB.conexion);
            cmd.ExecuteNonQuery();

            foreach (var p in permisos)
            {
                command = $"insert into PermisoPermisos (Nombre_PermisoCompuesto, Nombre_Permiso) values ('{pFamilia.Nombre}','{p}')";
                cmd = new SqlCommand(command, conexDB.conexion);
                cmd.ExecuteNonQuery();
            }

            conexDB.CerrarConexion();
        }

        public void AgregarPermisoAFamilia(string familia, string permiso)
        {
            conexDB.AbrirConexion();
            string command = $"insert into PermisoPermisos (Nombre_PermisoCompuesto, Nombre_Permiso) values ('{familia}','{permiso}')";
            SqlCommand cmd = new SqlCommand(command, conexDB.conexion);
            cmd.ExecuteNonQuery();
            conexDB.CerrarConexion();
        }

        public void EliminarPermisoDeFamilia(string familia, string permiso)
        {
            conexDB.AbrirConexion();
            string command = $"delete from PermisoPermisos where Nombre_PermisoCompuesto = '{familia}' AND Nombre_Permiso = '{permiso}'";
            SqlCommand cmd = new SqlCommand(command, conexDB.conexion);
            cmd.ExecuteNonQuery();
            conexDB.CerrarConexion();
        }
    }
}
