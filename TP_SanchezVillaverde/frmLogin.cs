using System;
using System.Drawing;
using System.Windows.Forms;
using Entidad_BE;
using Negocio_BLL;
using Servicios;

namespace TP_SanchezVillaverde
{
    public partial class frmLogin : Form, IObservadorIdioma
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        UsuarioBLL usuario = new UsuarioBLL();
        BitacoraBLL bitacora = new BitacoraBLL();
        GestorDeIdioma gestorIdioma = GestorDeIdioma.GetInstance;

        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtContra.boton = this.btnIniciar;
            lblError.Text = "";
            CargarMenuIdiomas();
            gestorIdioma.Suscribir(this);
            this.FormClosed += frmLogin_FormClosed;
        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            gestorIdioma.Desuscribir(this);
        }

        #region Patron Observer - Idiomas

        public void ActualizarTextos()
        {
            this.Text = gestorIdioma.Traducir("LOGIN_TITULO");
            lblUsuario.Text = gestorIdioma.Traducir("COMUN_USUARIO");
            lblContra.Text = gestorIdioma.Traducir("COMUN_CONTRASENA");
            btnIniciar.Text = gestorIdioma.Traducir("LOGIN_BTN_INICIAR");
            btnCancelar.Text = gestorIdioma.Traducir("COMUN_CANCELAR");
            lblSinConexion.Text = gestorIdioma.Traducir("LOGIN_LNK_SIN_CONEXION");
            cambiarIdiomaToolStripMenuItem.Text = gestorIdioma.Traducir("MENU_CAMBIAR_IDIOMA");
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

        private void itemIdioma_Click(object sender, EventArgs e)
        {
            try
            {
                gestorIdioma.CambiarIdioma((sender as ToolStripMenuItem).Tag.ToString());
                bitacora.RegistrarBitacora("null", TipoAccion.CambioIdioma);
            }
            catch (Exception ex)
            {
                MessageBox.Show(gestorIdioma.Traducir("COMUN_ERROR_BD") + ex.Message);
            }
        }

        #endregion

        private void txtContra_Load(object sender, EventArgs e)
        {
            txtContra.Hide(true);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(gestorIdioma.Traducir("COMUN_CONFIRMA_SALIR_APP"), gestorIdioma.Traducir("COMUN_ATENCION"),
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bitacora.RegistrarBitacora("null", TipoAccion.AppClose);
                Application.Exit();
            }
            else txtUsuario.Enfocar();
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            UsuarioBE user;

            if (!txtUsuario.ok || !txtContra.ok)
            {
                MessageBox.Show(gestorIdioma.Traducir("LOGIN_MSG_DATOS_INVALIDOS"), gestorIdioma.Traducir("LOGIN_TIT_DATOS_INVALIDOS"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    user = new UsuarioBE();
                    user.user = txtUsuario.texto;
                    user.pass = txtContra.texto;

                    LoginResult authOK = usuario.Login(user);
                    if (authOK == LoginResult.LoginOK)
                    {
                        //bitacora.RegistrarBitacora(user.user, TipoAccion.Login);
                        frmMenu frm = new frmMenu();
                        frm.Show();
                        this.Hide();

                        frm.FormClosing += frm_closing;
                    }
                    else
                    {
                        //Mensaje de label de Error
                        switch (authOK)
                        {
                            case LoginResult.UserInexistente:
                                bitacora.RegistrarBitacora(user.user, TipoAccion.LoginFail);
                                lblError.Text = gestorIdioma.Traducir("LOGIN_ERR_USER_INEXISTENTE");
                                break;
                            case LoginResult.UserBloqueado:
                                bitacora.RegistrarBitacora(user.user, TipoAccion.LoginFail);
                                lblError.Text = gestorIdioma.Traducir("LOGIN_ERR_USER_BLOQUEADO");
                                break;
                            case LoginResult.PassIncorrecta:
                                bitacora.RegistrarBitacora(user.user, TipoAccion.LoginFail);
                                lblError.Text = gestorIdioma.Traducir("LOGIN_ERR_PASS_INCORRECTA");
                                break;
                            case LoginResult.UserInactivo:
                                bitacora.RegistrarBitacora(user.user, TipoAccion.LoginFail);
                                MessageBox.Show(string.Format(gestorIdioma.Traducir("LOGIN_MSG_USER_INACTIVO"), user.user), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                break;
                            case LoginResult.FinIntentos:
                                bitacora.RegistrarBitacora(user.user, TipoAccion.BloqueoUsuario);
                                MessageBox.Show(gestorIdioma.Traducir("LOGIN_MSG_FIN_INTENTOS"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Application.Exit();
                                break;
                            case LoginResult.SesionIniciada:
                                bitacora.RegistrarBitacora(user.user, TipoAccion.Login);
                                MessageBox.Show(string.Format(gestorIdioma.Traducir("LOGIN_MSG_SESION_INICIADA"), user.user), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                frmMenu frm = new frmMenu();
                                frm.Show();
                                this.Hide();

                                frm.FormClosing += frm_closing;
                                break;
                            case LoginResult.ExisteSesion:
                                if (MessageBox.Show(gestorIdioma.Traducir("LOGIN_MSG_EXISTE_SESION"), " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    bitacora.RegistrarBitacora(user.user, TipoAccion.Logout);
                                    usuario.Logout();
                                    MessageBox.Show(gestorIdioma.Traducir("LOGIN_MSG_SESION_CERRADA"), " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else { MessageBox.Show(gestorIdioma.Traducir("LOGIN_MSG_USUARIO_EN_USO"), " ", MessageBoxButtons.OK, MessageBoxIcon.Hand); }
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(gestorIdioma.Traducir("COMUN_ERROR_BD") + ex.Message);
                }
            }
            txtUsuario.Limpiar();
            txtContra.Limpiar();
            txtUsuario.Enfocar();
        }

        private void lblSinConexion_Click(object sender, EventArgs e)
        {
            bitacora.RegistrarBitacora("null", TipoAccion.NoSesion);
            usuario.Logout();
            frmMenu frm = new frmMenu();
            frm.Show();
            this.Hide();
        }

        private void lblSinConexion_MouseHover(object sender, EventArgs e)
        {
            var font = ((Label)sender).Font;

            ((Label)sender).Font = new Font(font, FontStyle.Bold);

            font.Dispose();
        }

        private void lblSinConexion_MouseLeave(object sender, EventArgs e)
        {
            var font = ((Label)sender).Font;

            ((Label)sender).Font = new Font(font, FontStyle.Regular);

            font.Dispose();
        }

        private void frm_closing(object sender, FormClosingEventArgs e)
        {
            txtContra.Limpiar();
            txtUsuario.Limpiar();
            this.Show();
            txtUsuario.Enfocar();
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            lblError.Text = "";
        }
    }
}
