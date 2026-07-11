using Entidad_BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Acceso_DAL
{
    public class MP_HistorialCambios
    {
        AccesoDatos conexDB = new AccesoDatos();

        public void RegistrarCambio(CambioBE cambio)
        {
            SqlParameter[] parametros = new SqlParameter[6];
            parametros[0] = new SqlParameter("@EntidadId", cambio.entidadId);
            parametros[1] = new SqlParameter("@NombreCampo", cambio.campo);
            parametros[2] = new SqlParameter("@ValorAnterior", cambio.valorAnterior);
            parametros[3] = new SqlParameter("@ValorNuevo", cambio.valorNuevo);
            parametros[4] = new SqlParameter("@Usuario", cambio.usuario);
            parametros[5] = new SqlParameter("@Fecha", cambio.fecha);

            conexDB.Escribir("SP_RegistrarCambio", parametros);
        }

        public List<CambioBE> ListarCambios(int entidadId)
        {
            List<CambioBE> cambios = new List<CambioBE>();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@EntidadId", entidadId);
            DataTable dt = conexDB.LeerTabla("SP_ExtHistorialCambios", param);
            foreach (DataRow dr in dt.Rows)
            {
                CambioBE cambio = new CambioBE();
                cambio.id = Convert.ToInt32(dr[0].ToString());
                cambio.entidadId = Convert.ToInt32(dr[1].ToString());
                cambio.campo = dr[2].ToString();
                cambio.valorAnterior = dr[3].ToString();
                cambio.valorNuevo = dr[4].ToString();
                cambio.usuario = dr[5].ToString();
                cambio.fecha = Convert.ToDateTime(dr[6].ToString());
                cambios.Add(cambio);
            }
            return cambios;
        }
    }
}
