using Entidad_BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acceso_DAL
{
    public class MP_Bitacora
    {
        AccesoDatos conexDB = new AccesoDatos();

        public List<EventoBE> ListarEventos()
        {
            List<EventoBE> eventos = new List<EventoBE>();
            DataTable dt = conexDB.LeerTabla("SP_ExtBitacora", null);
            foreach (DataRow dr in dt.Rows)
            {
                EventoBE ev = new EventoBE();
                ev.registro = Convert.ToInt32(dr[0].ToString());
                ev.usuario = dr[1].ToString();
                ev.accion = dr[2].ToString();
                ev.fecha = Convert.ToDateTime(dr[3].ToString());
                eventos.Add(ev);
            }
            return eventos;
        }

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
