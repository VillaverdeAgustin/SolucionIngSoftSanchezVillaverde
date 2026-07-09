using Entidad_BE;
using Negocio_BLL;
using Servicios;
using System.Drawing;
using System.Windows.Forms;

namespace TP_SanchezVillaverde
{
    public partial class frmMenu : Form, IObservadorIdioma
    {
        public frmMenu()
        {
            InitializeComponent();
        }

        public static ToolStripMenuItem opcActivo = null;
        public static Form formActivo = null;
        UsuarioBLL usuario = new UsuarioBLL();
        BitacoraBLL bitacora = new BitacoraBLL();
        PerfilBLL perfilBLL = new PerfilBLL();
        GestorDeIdioma gestorIdioma = GestorDeIdioma.GetInstance;

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
            CargarMenuIdiomas();
            gestorIdioma.Suscribir(this);
            this.FormClosed += frmMenu_FormClosed;
        }

        private void frmMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            gestorIdioma.Desuscribir(this);
        }

        #region Patron Observer - Idiomas

        public void ActualizarTextos()
        {
            tsInicio.Text = gestorIdioma.Traducir("MENU_TS_INICIO");
            iniciarSesionToolStripMenuItem.Text = gestorIdioma.Traducir("MENU_INICIAR_SESION");
            cerrarSesionToolStripMenuItem.Text = gestorIdioma.Traducir("MENU_CERRAR_SESION");
            salirToolStripMenuItem.Text = gestorIdioma.Traducir("COMUN_SALIR");
            tsAdmin.Text = gestorIdioma.Traducir("MENU_TS_ADMIN");
            gestionDeUsuariosToolStripMenuItem.Text = gestorIdioma.Traducir("MENU_GESTION_USUARIOS");
            perfilesToolStripMenuItem.Text = gestorIdioma.Traducir("MENU_PERFILES");
            idiomasToolStripMenuItem.Text = gestorIdioma.Traducir("MENU_IDIOMAS");
            backupToolStripMenuItem.Text = gestorIdioma.Traducir("MENU_BACKUP");
            restoreToolStripMenuItem.Text = gestorIdioma.Traducir("MENU_RESTORE");
            bitacoraToolStripMenuItem.Text = gestorIdioma.Traducir("MENU_BITACORA");
            tsUsuario.Text = gestorIdioma.Traducir("COMUN_USUARIO");
            cambiarClaveToolStripMenuItem.Text = gestorIdioma.Traducir("MENU_CAMBIAR_CLAVE");
            cambiarIdiomaToolStripMenuItem.Text = gestorIdioma.Traducir("MENU_CAMBIAR_IDIOMA");
            tsVentas.Text = gestorIdioma.Traducir("MENU_TS_VENTAS");
            llenarCarritoToolStripMenuItem.Text = gestorIdioma.Traducir("MENU_LLENAR_CARRITO");
            realizarVentaToolStripMenuItem.Text = gestorIdioma.Traducir("MENU_REALIZAR_VENTA");
            tsGestion.Text = gestorIdioma.Traducir("MENU_TS_GESTION");
            clienteToolStripMenuItem.Text = gestorIdioma.Traducir("MENU_CLIENTE");
            productosToolStripMenuItem.Text = gestorIdioma.Traducir("MENU_PRODUCTOS");
            proveedoresToolStripMenuItem.Text = gestorIdioma.Traducir("MENU_PROVEEDORES");
            tsReportes.Text = gestorIdioma.Traducir("MENU_TS_REPORTES");
            tsAyuda.Text = gestorIdioma.Traducir("MENU_TS_AYUDA");

            if (SessionManager.GetInstance.logged && SessionManager.GetInstance.UsuarioActual() != null)
            {
                lblUsuario.Text = gestorIdioma.Traducir("MENU_LBL_USUARIO") + SessionManager.GetInstance.UsuarioActual().user;
            }
            else
            {
                lblUsuario.Text = gestorIdioma.Traducir("MENU_LBL_SIN_CONEXION");
            }
            MarcarIdiomaActivo();
        }

        private void CargarMenuIdiomas()
        {
            try
            {
                cambiarIdiomaToolStripMenuItem.DropDownItems.Clear();
                foreach (IdiomaBE idioma in gestorIdioma.ObtenerIdiomas())
                {
                    ToolStripMenuItem item = new ToolStripMenuItem(idioma.Nombre);
                    item.Tag = idioma.Codigo;
                    item.Click += itemIdioma_Click;
                    cambiarIdiomaToolStripMenuItem.DropDownItems.Add(item);
                }
            }
            catch { }//Sin conexion a la BD no se ofrece el cambio de idioma
        }

        private void MarcarIdiomaActivo()
        {
            foreach (ToolStripMenuItem item in cambiarIdiomaToolStripMenuItem.DropDownItems)
            {
                item.Checked = gestorIdioma.IdiomaActual != null
                    && item.Tag.ToString() == gestorIdioma.IdiomaActual.Codigo;
            }
        }

        private void itemIdioma_Click(object sender, System.EventArgs e)
        {
            try
            {
                gestorIdioma.CambiarIdioma((sender as ToolStripMenuItem).Tag.ToString());
                string user = SessionManager.GetInstance.logged ? SessionManager.GetInstance.UsuarioActual().user : "null";
                bitacora.RegistrarBitacora(user, TipoAccion.CambioIdioma);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(gestorIdioma.Traducir("COMUN_ERROR_BD") + ex.Message);
            }
        }

        #endregion

        #region Set Formulario
        public void FormConectado()
        {
            UsuarioBE usActual = SessionManager.GetInstance.UsuarioActual();
            lblUsuario.Text = gestorIdioma.Traducir("MENU_LBL_USUARIO") + usActual.user;
            iniciarSesionToolStripMenuItem.Enabled = false;
            cerrarSesionToolStripMenuItem.Enabled = true;
            tsAdmin.Enabled = true;
            tsReportes.Enabled = true;
            tsGestion.Enabled = true;
            cambiarClaveToolStripMenuItem.Enabled = true;

            AplicarPermisos(usActual.rol);
        }

        private void AplicarPermisos(string rol)
        {
            gestionDeUsuariosToolStripMenuItem.Enabled = perfilBLL.TienePermiso(rol, TipoPermiso.GestionUsuarios.ToString());
            perfilesToolStripMenuItem.Enabled = perfilBLL.TienePermiso(rol, TipoPermiso.GestionPerfiles.ToString());
            backupToolStripMenuItem.Enabled = perfilBLL.TienePermiso(rol, TipoPermiso.GestionBackup.ToString());
            restoreToolStripMenuItem.Enabled = perfilBLL.TienePermiso(rol, TipoPermiso.GestionBackup.ToString());
            bitacoraToolStripMenuItem.Enabled = perfilBLL.TienePermiso(rol, TipoPermiso.GestionBitacora.ToString());
            llenarCarritoToolStripMenuItem.Enabled = perfilBLL.TienePermiso(rol, TipoPermiso.LlenarCarrito.ToString());
            realizarVentaToolStripMenuItem.Enabled = perfilBLL.TienePermiso(rol, TipoPermiso.RealizarCobro.ToString());
            clienteToolStripMenuItem.Enabled = perfilBLL.TienePermiso(rol, TipoPermiso.RegistrarCliente.ToString());
            productosToolStripMenuItem.Enabled = perfilBLL.TienePermiso(rol, TipoPermiso.GestionProductos.ToString());
        }

        public void FormDesconectado()
        {
            lblUsuario.Text = gestorIdioma.Traducir("MENU_LBL_SIN_CONEXION");
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
            bitacora.RegistrarBitacora(SessionManager.GetInstance.UsuarioActual().user, TipoAccion.GestionUsuariosAbierta.ToString());
            OpenForm(tsAdmin, new frmUsuario());
        }

        private void salirToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show(gestorIdioma.Traducir("COMUN_CONFIRMA_SALIR_APP"), gestorIdioma.Traducir("COMUN_ATENCION"),
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bitacora.RegistrarBitacora(SessionManager.GetInstance.UsuarioActual().user, TipoAccion.AppClose.ToString());
                SessionManager.GetInstance.Logout();
                Application.Exit();
            }
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show(gestorIdioma.Traducir("COMUN_CONFIRMA_CERRAR_SESION"), gestorIdioma.Traducir("COMUN_ATENCION"),
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bitacora.RegistrarBitacora(SessionManager.GetInstance.UsuarioActual().user, TipoAccion.Logout.ToString());
                UsuarioBLL usuario = new UsuarioBLL();
                usuario.Logout();

                frmLogin frm = new frmLogin();
                frm.Show();
                this.Close();
            }
        }

        private void iniciarSesionToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            frmLogin frm = new frmLogin();
            frm.Show();
            this.Close();
        }

        private void cambiarClaveToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            frmModClave frmClave = new frmModClave();
            frmClave.ShowDialog();
        }

        private void bitacoraToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            bitacora.RegistrarBitacora(SessionManager.GetInstance.UsuarioActual().user, TipoAccion.BitacoraAbierta.ToString());
            OpenForm(tsAdmin, new frmBitacora());
        }

        private void perfilesToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            frmGestionPerfiles frmperfil = new frmGestionPerfiles();
            frmperfil.ShowDialog();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
