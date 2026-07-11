using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad_BE
{
    public class UsuarioBE : IVerificable, IOriginadorCambios
    {
        private int _cod;

        public int cod
        {
            get { return _cod; }
            set { _cod = value; }
        }

        private string _nomb;

        public string nomb
        {
            get { return _nomb; }
            set { _nomb = value; }
        }

        private string _ape;

        public string ape
        {
            get { return _ape; }
            set { _ape = value; }
        }

        private int _dni;

        public int dni
        {
            get { return _dni; }
            set { _dni = value; }
        }

        private string _user;

        public string user
        {
            get { return _user; }
            set { _user = value; }
        }

        private string _pass;

        public string pass
        {
            get { return _pass; }
            set { _pass = value; }
        }

        private string _rol;

        public string rol
        {
            get { return _rol; }
            set { _rol = value; }
        }

        private string _dir;

        public string dir
        {
            get { return _dir; }
            set { _dir = value; }
        }

        private string _tel;

        public string tel
        {
            get { return _tel; }
            set { _tel = value; }
        }

        private string _email;

        public string email
        {
            get { return _email; }
            set { _email = value; }
        }

        private Boolean _estado;

        public Boolean estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        private Boolean _bloq;

        public Boolean bloq
        {
            get { return _bloq; }
            set { _bloq = value; }
        }

        private string _dvh;

        public string dvh
        {
            get { return _dvh; }
            set { _dvh = value; }
        }

        public string digito => _dvh;

        public UsuarioBE() { }

        public UsuarioBE CrearUsuario(string[] datos)
        {
            this.cod = Convert.ToInt32(datos[0]);
            this.dni = Convert.ToInt32(datos[3]);
            this.nomb = datos[1];
            this.ape = datos[2];
            this.user = datos[4];
            this.rol = datos[6];
            this.pass = datos[5];
            this.dir = datos[7];
            this.tel = (datos[8]);
            this.email = datos[9];
            this.estado = Convert.ToBoolean(datos[10]);
            this.bloq = Convert.ToBoolean(datos[11]);
            this.dvh = datos[12];
            return this;
        }

        public string ObtenerCamposDV()
        {
            return string.Join("|",
                cod,
                dni,
                nomb,
                ape,
                user,
                rol,
                pass,
                dir,
                tel,
                email,
                estado,
                bloq);
        }

        #region Patron Memento - Control de Cambios (T06b)

        //Se fotografian solo los campos de negocio auditables:
        //cod es la identidad (no cambia) y dvh es un campo tecnico derivado
        public MementoBE CrearMemento()
        {
            return new MementoBE(new Dictionary<string, string>
            {
                { "dni",    dni.ToString() },
                { "nomb",   nomb },
                { "ape",    ape },
                { "user",   user },
                { "rol",    rol },
                { "pass",   pass },
                { "dir",    dir },
                { "tel",    tel },
                { "email",  email },
                { "estado", estado.ToString() },
                { "bloq",   bloq.ToString() }
            });
        }

        public void RestaurarMemento(MementoBE memento)
        {
            Dictionary<string, string> datos = memento.ObtenerEstado();
            this.dni = Convert.ToInt32(datos["dni"]);
            this.nomb = datos["nomb"];
            this.ape = datos["ape"];
            this.user = datos["user"];
            this.rol = datos["rol"];
            this.pass = datos["pass"];
            this.dir = datos["dir"];
            this.tel = datos["tel"];
            this.email = datos["email"];
            this.estado = Convert.ToBoolean(datos["estado"]);
            this.bloq = Convert.ToBoolean(datos["bloq"]);
        }

        #endregion
    }
}
