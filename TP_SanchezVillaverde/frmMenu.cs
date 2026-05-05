using Entidad_BE;
using Negocio_BLL;
using Servicios;
using System.Drawing;
using System.Windows.Forms;

namespace TP_SanchezVillaverde
{
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
        }

        public static ToolStripMenuItem opcActivo = null;
        public static Form formActivo = null;

        private void frmMenu_Load(object sender, System.EventArgs e)
        {
            if (SessionManager.GetInstance.logged == true)
            {
                FormConectado();
            }
            else
            {
                FormDesconectado();
            }
        }

        #region Set Formulario
        public void FormConectado()
        {
            UsuarioBE usActual = SessionManager.GetInstance.UsuarioActual();
            lblUsuario.Text = "Usuario: " + usActual.user;
            iniciarSesionToolStripMenuItem.Enabled = false;
            cerrarSesionToolStripMenuItem.Enabled = true;
            tsAdmin.Enabled = true;
            tsReportes.Enabled = true;
            tsGestion.Enabled = true;
            cambiarClaveToolStripMenuItem.Enabled = true;
        }

        public void FormDesconectado()
        {
            lblUsuario.Text = "Usuario: Sin Conexion";
            iniciarSesionToolStripMenuItem.Enabled = true;
            cerrarSesionToolStripMenuItem.Visible = false;
            tsAdmin.Visible = false;
            tsReportes.Visible = false;
            tsGestion.Visible = false;
            cambiarClaveToolStripMenuItem.Visible = false;
        }

        #endregion

        private void OpenForm(ToolStripMenuItem opc, Form frm)
        {
            if (opcActivo != null)
            {
                opcActivo.BackColor = Color.WhiteSmoke;
            }
            opc.BackColor = Color.Gainsboro;
            opcActivo = opc;

            if (formActivo != null) { formActivo.Close(); }
            formActivo = frm;
            frm.MdiParent = this;
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            frm.Show();
        }

        private void gestionDeUsuariosToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            OpenForm(tsAdmin, new frmUsuario());
        }

        private void salirToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("¿Esta seguro que desea cerrar la aplicacion?", "Atencion",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SessionManager.GetInstance.Logout();
                Application.Exit();
            }
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("¿Esta seguro que desea cerrar la sesion?", "Atencion",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                UsuarioBLL usuario = new UsuarioBLL();
                usuario.Logout();

                frmLogin frm = new frmLogin();
                frm.Show();
                this.Close();
            }
        }
    }
}
