using System;
using System.Drawing;
using System.Windows.Forms;
using Entidad_BE;
using Negocio_BLL;

namespace TP_SanchezVillaverde
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        UsuarioBLL usuario = new UsuarioBLL();
        BitacoraBLL bitacora = new BitacoraBLL();

        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtContra.boton = this.btnIniciar;
            lblError.Text = "";
        }

        private void txtContra_Load(object sender, EventArgs e)
        {
            txtContra.Hide(true);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Esta seguro que desea cerrar la aplicacion?", "Atencion",
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
                MessageBox.Show("Los datos ingresados no cumplen con el formato requerido.", "Datos Invalidos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                                lblError.Text = "El usuario ingresado no existe";
                                break;
                            case LoginResult.UserBloqueado:
                                bitacora.RegistrarBitacora(user.user, TipoAccion.LoginFail);
                                lblError.Text = "El usuario se encuentra bloqueado. Contacte al administrador";
                                break;
                            case LoginResult.PassIncorrecta:
                                bitacora.RegistrarBitacora(user.user, TipoAccion.LoginFail);
                                lblError.Text = "La contraseña ingresada es incorrecta";
                                break;
                            case LoginResult.UserInactivo:
                                bitacora.RegistrarBitacora(user.user, TipoAccion.LoginFail);
                                MessageBox.Show($"El usuario -->{user.user}<-- no esta disponible. Contacte al administrador.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                break;
                            case LoginResult.FinIntentos:
                                bitacora.RegistrarBitacora(user.user, TipoAccion.BloqueoUsuario);
                                MessageBox.Show("Cantidad de intentos superado, se bloqueo el usuario. Cerrando la aplicacion.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Application.Exit();
                                break;
                            case LoginResult.SesionIniciada:
                                bitacora.RegistrarBitacora(user.user, TipoAccion.Login);
                                MessageBox.Show($"El usuario -->{user.user}<-- ya tiene la sesion iniciada.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                frmMenu frm = new frmMenu();
                                frm.Show();
                                this.Hide();

                                frm.FormClosing += frm_closing;
                                break;
                            case LoginResult.ExisteSesion:
                                if (MessageBox.Show("Ya existe una sesion iniciada, desea finalizarla?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    bitacora.RegistrarBitacora(user.user, TipoAccion.Logout);
                                    usuario.Logout();
                                    MessageBox.Show("Sesion cerrada correctamente. Intente nuevamente con su usuario", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else { MessageBox.Show("Intente nuevamente con el usuario actualmente en uso. Si no es el suyo por favor cierre la sesion", " ", MessageBoxButtons.OK, MessageBoxIcon.Hand); }
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error de comunicacion con la Base de Datos: " + ex.Message);
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
