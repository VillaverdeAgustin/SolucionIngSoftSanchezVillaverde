using Entidad_BE;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acceso_DAL
{
    public class MP_Bitacora
    {
        AccesoDatos conexDB = new AccesoDatos();
        public void RegistrarEvento(EventoBE ev)
        {
            SqlParameter[] parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter("@usuario", ev.usuario);
            parametros[1] = new SqlParameter("@accion", ev.accion);
            parametros[2] = new SqlParameter("fecha", ev.fecha);

            conexDB.Escribir("SP_RegistrarEvento", parametros);
        }
    }
}
