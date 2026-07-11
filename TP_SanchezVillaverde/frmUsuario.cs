using Entidad_BE;
using Negocio_BLL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TP_SanchezVillaverde
{
    public partial class frmUsuario : Form, IObservadorIdioma
    {
        public frmUsuario()
        {
            InitializeComponent();
        }

        UsuarioBE auxUsuario;
        BitacoraBLL bitacora = new BitacoraBLL();
        UsuarioBLL usuarioBLL = new UsuarioBLL();
        PerfilBLL perfilBLL = new PerfilBLL();
        GestorDeIdioma gestorIdioma = GestorDeIdioma.GetInstance;
        int varMod = 0; //Indica si la modificacion esta activa
        List<string> roles = new List<string>();

        #region Patron Observer - Idiomas

        public void ActualizarTextos()
        {
            this.Text = gestorIdioma.Traducir("USU_TITULO");
            label1.Text = gestorIdioma.Traducir("USU_LBL_TITULO");
            label2.Text = gestorIdioma.Traducir("USU_LBL_OPCIONES");
            gbDatos.Text = gestorIdioma.Traducir("USU_GB_DATOS");
            g.Text = gestorIdioma.Traducir("USU_GB_MENSAJES");
            label3.Text = gestorIdioma.Traducir("USU_LBL_DNI");
            label4.Text = gestorIdioma.Traducir("COMUN_NOMBRE");
            label5.Text = gestorIdioma.Traducir("COMUN_APELLIDO");
            label6.Text = gestorIdioma.Traducir("COMUN_USUARIO");
            label7.Text = gestorIdioma.Traducir("COMUN_ROL");
            label8.Text = gestorIdioma.Traducir("USU_LBL_DIRECCION");
            label9.Text = gestorIdioma.Traducir("USU_LBL_TELEFONO");
            label10.Text = gestorIdioma.Traducir("USU_LBL_EMAIL");
            chkActivo.Text = gestorIdioma.Traducir("USU_CHK_ACTIVO");
            chkBloqueado.Text = gestorIdioma.Traducir("USU_CHK_BLOQUEADO");
            btnCrearUs.Text = gestorIdioma.Traducir("USU_BTN_CREAR");
            btnModUs.Text = gestorIdioma.Traducir("USU_BTN_MODIFICAR");
            btnElimUs.Text = gestorIdioma.Traducir("USU_BTN_ELIMINAR");
            btnDesbloquear.Text = gestorIdioma.Traducir("USU_BTN_DESBLOQUEAR");
            btnHistorial.Text = gestorIdioma.Traducir("USU_BTN_HISTORIAL");
            btnGuardar.Text = gestorIdioma.Traducir("COMUN_GUARDAR");
            btnCancelar.Text = gestorIdioma.Traducir("COMUN_CANCELAR");
            btnSalir.Text = gestorIdioma.Traducir("COMUN_SALIR");
            TraducirColumnas();
        }

        private void TraducirColumnas()
        {
            if (dgvUsuarios.Columns.Count == 0) { return; }
            dgvUsuarios.Columns[1].HeaderText = gestorIdioma.Traducir("USU_LBL_DNI");
            dgvUsuarios.Columns[2].HeaderText = gestorIdioma.Traducir("COMUN_NOMBRE");
            dgvUsuarios.Columns[3].HeaderText = gestorIdioma.Traducir("COMUN_APELLIDO");
            dgvUsuarios.Columns[4].HeaderText = gestorIdioma.Traducir("COMUN_USUARIO");
            dgvUsuarios.Columns[5].HeaderText = gestorIdioma.Traducir("COMUN_ROL");
            dgvUsuarios.Columns[10].HeaderText = gestorIdioma.Traducir("USU_CHK_ACTIVO");
            dgvUsuarios.Columns[11].HeaderText = gestorIdioma.Traducir("USU_CHK_BLOQUEADO");
        }

        #endregion

        #region Funciones

        private void ConfigDefaultForm()
        {
            HabilitarBtn(btnGuardar, false);
            HabilitarBtn(btnCancelar, false);
            cmbRol.Enabled = false;
            HabilitarBtn(btnCrearUs, true);
            HabilitarBtn(btnModUs, false);
            HabilitarBtn(btnElimUs, false);
            HabilitarBtn(btnDesbloquear, false);
            HabilitarBtn(btnHistorial, false);
            dgvUsuarios.Enabled = true;
            chkActivo.Enabled = false;
            varMod = 0;
            foreach (Control c in pUsuario.Controls)
            {
                if (c is Button btn)
                {
                    if (btn.Enabled) { btn.Font = new Font(btn.Font.Name, btn.Font.Size, FontStyle.Bold); btn.ForeColor = Color.Black; }
                    else { btn.Font = new Font(btn.Font.Name, btn.Font.Size, FontStyle.Regular); }
                    c.BackColor = Color.FromArgb(255, 192, 192);
                }
            }
            LimpiarDatos();

            foreach (Control control in gbDatos.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Enabled = false;
                }
            }
        }

        private void HabilitarBtn(Button btn, bool tipo)
        {
            if (tipo)
            {
                btn.Enabled = tipo;
                btn.Font = new Font(btn.Font.Name, btnGuardar.Font.Size, FontStyle.Bold);
                btn.ForeColor = Color.Black;
            }
            else
            {
                btn.Font = new Font(btn.Font.Name, btnGuardar.Font.Size, FontStyle.Regular);
                btn.Enabled = tipo;
            }
        }

        private Button ultimoBoton = null;

        private void ClickBoton(object sender, EventArgs e)
        {
            if (ultimoBoton != null)
            {
                ultimoBoton.BackColor = Color.FromArgb(255, 192, 192);
            }

            //Se compara contra el boton y no contra su texto porque el texto cambia segun el idioma activo
            if (!ReferenceEquals(sender, btnCancelar))
            {
                (sender as Button).BackColor = Color.LightCoral;
            }
            ultimoBoton = (sender as Button);
        }

        private void HabilitarCampos()
        {
            foreach (Control control in gbDatos.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Enabled = true;
                    textBox.BackColor = Color.White;
                }
                cmbRol.Enabled = true;
            }
        }

        private void LimpiarDatos()
        {
            foreach (Control control in gbDatos.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Text = "";
                    textBox.BackColor = Color.Gainsboro;
                }
            }
            cmbRol.SelectedItem = null;
            chkBloqueado.Checked = false;
            chkActivo.Checked = false;
            txtDoc.Focus();
        }

        private void ConfigDGV(List<UsuarioBE> usuarios)
        {
            dgvUsuarios.DataSource = usuarios;
            dgvUsuarios.Columns["dir"].Visible = false;
            dgvUsuarios.Columns["tel"].Visible = false;
            dgvUsuarios.Columns["email"].Visible = false;
            dgvUsuarios.Columns["cod"].Visible = false;
            dgvUsuarios.Columns["pass"].Visible = false;
            dgvUsuarios.Columns["dvh"].Visible = false;
            TraducirColumnas();
            dgvUsuarios.ReadOnly = true;
        }

        private void ActualizarDGV()
        {
            dgvUsuarios.DataSource = usuarioBLL.ListarUsuarios();
            ConfigDefaultForm();
        }

        private UsuarioBE CargaUsuario()
        {
            if (ValCampos())//Validacion caracteres
            {
                auxUsuario = new UsuarioBE();
                auxUsuario.dni = Convert.ToInt32(txtDoc.Text);
                auxUsuario.nomb = txtNom.Text;
                auxUsuario.ape = txtApe.Text;
                auxUsuario.rol = cmbRol.SelectedValue.ToString();
                auxUsuario.user = txtUsu.Text;
                auxUsuario.estado = chkActivo.Checked;
                auxUsuario.bloq = chkBloqueado.Checked;
                if (txtDir.Text == string.Empty) { auxUsuario.dir = " "; }
                else { auxUsuario.dir = txtDir.Text; }
                if (txtTel.Text == string.Empty) { auxUsuario.tel = " "; }
                else { auxUsuario.tel = txtTel.Text; }
                if (txtMail.Text == string.Empty) { auxUsuario.email = " "; }
                else { auxUsuario.email = txtMail.Text; }

                return auxUsuario;
            }
            return null;
        }

        private void GuardarEnabled()
        {
            if (txtNom.Text != "" & txtApe.Text != "" & txtDoc.Text != ""
                    & txtUsu.Text != "" & cmbRol.SelectedValue != null)
            {
                HabilitarBtn(btnGuardar, true);
            }
            else
            {
                HabilitarBtn(btnGuardar, false);
            }
        }

        private void CampoError(string campo)
        //Marcar en rojo los campos con validacion no ok
        {
            foreach (Control t in gbDatos.Controls)
            {
                if (t is TextBox & (t.Name == campo))
                {
                    t.BackColor = Color.LightCoral;
                    break;
                }
            }
        }

        private UsuarioBE ExtraerDatos(DataGridViewRow fila)
        {
            string[] datos = new string[fila.Cells.Count];

            for (int i = 0; i < fila.Cells.Count; i++)
            {
                datos[i] = fila.Cells[i].Value.ToString();
            }
            auxUsuario = new UsuarioBE();
            return auxUsuario.CrearUsuario(datos);
        }

        private void LlenarMensaje(string linea)
        {
            txtMensaje.Text = linea + Environment.NewLine + Environment.NewLine + txtMensaje.Text;
        }

        #endregion

        #region ValidacionCampos
        private void txtDoc_TextChanged(object sender, EventArgs e)
        {
            txtDoc.BackColor = Color.White;
            txtDoc.BackColor = Color.White;
            GuardarEnabled();
        }

        private void txtNom_TextChanged(object sender, EventArgs e)
        {
            txtNom.BackColor = Color.White;
            GuardarEnabled();
        }

        private void txtApe_TextChanged(object sender, EventArgs e)
        {
            txtApe.BackColor = Color.White;
            GuardarEnabled();
        }

        private void txtUsu_TextChanged(object sender, EventArgs e)
        {
            txtUsu.BackColor = Color.White;
            GuardarEnabled();
        }

        private void txtDir_TextChanged(object sender, EventArgs e)
        {
            txtDir.BackColor = Color.White;
            GuardarEnabled();
        }

        private void txtTel_TextChanged(object sender, EventArgs e)
        {
            txtTel.BackColor = Color.White;
            GuardarEnabled();
        }

        private void txtMail_TextChanged(object sender, EventArgs e)
        {
            txtMail.BackColor = Color.White;
            GuardarEnabled();
        }

        private void cmbRol_SelectedValueChanged(object sender, EventArgs e)
        {
            GuardarEnabled();
        }

        private bool ValCampos()
        {
            string patronT = @"^[a-zA-Z\s]+$";
            string patronD = @"^[A-Za-z0-9\s]+$";
            string patronN = "^[0-9]+$";
            string patronS = "^[A-Za-z0-9]+$";
            string patronM = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";
            bool txtOK, numOK, sensOK, dirOK, mailOK;
            txtOK = numOK = sensOK = dirOK = mailOK = true;

            foreach (Control t in gbDatos.Controls)
            {
                if (t is TextBox)
                {
                    //Validacion Nombre y Apellido
                    if (t.Name == "txtNom" || t.Name == "txtApe")
                    {
                        if (!Regex.IsMatch(t.Text, patronT))
                        {
                            CampoError(t.Name);
                            txtOK = false;
                        }
                    }
                    else
                    {
                        //Validacion Direccion
                        if (t.Name == "txtDir")
                        {
                            if (!Regex.IsMatch(t.Text, patronD) & t.Text != string.Empty)
                            {
                                CampoError(t.Name);
                                dirOK = false;
                            }
                        }
                        else
                        {
                            //Validacion User y Pass
                            if (t.Name == "txtUsu")
                            {
                                if (!Regex.IsMatch(t.Text, patronS))
                                {
                                    CampoError(t.Name);
                                    sensOK = false;
                                }
                            }
                            else
                            {
                                //Validacion DNI y Telefono
                                if ((t.Name == "txtTel" & t.Text != string.Empty) || t.Name == "txtDoc")
                                {
                                    if (!Regex.IsMatch(t.Text, patronN))
                                    {
                                        CampoError(t.Name);
                                        numOK = false;
                                    }
                                }
                                else
                                {
                                    //Validacion Mail
                                    if (t.Name == "txtMail" & t.Text != string.Empty)
                                    {
                                        if (!Regex.IsMatch(t.Text, patronM))
                                        {
                                            CampoError(t.Name);
                                            mailOK = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #region Texto de error en Campo Mensajes
            if (!txtOK)
            {
                LlenarMensaje(gestorIdioma.Traducir("USU_VAL_NOMBRE"));
            }

            if (!numOK)
            {
                LlenarMensaje(gestorIdioma.Traducir("USU_VAL_NUMEROS"));
            }

            if (!dirOK)
            {
                LlenarMensaje(gestorIdioma.Traducir("USU_VAL_DIRECCION"));
            }

            if (!sensOK)
            {
                LlenarMensaje(gestorIdioma.Traducir("USU_VAL_USUARIO"));
            }

            if (!mailOK)
            {
                LlenarMensaje(gestorIdioma.Traducir("USU_VAL_MAIL"));
            }
            #endregion

            if (!txtOK || !sensOK || !dirOK || !numOK || !mailOK)
            {
                txtDoc.Focus();
                return false;
            }
            else { return true; }
        }
        #endregion

        private void frmUsuario_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigDGV(usuarioBLL.ListarUsuarios());

                // Cargar roles dinámicamente desde el sistema de permisos
                try
                {
                    List<Permiso> permisosRol = perfilBLL.ListaPermisos("Rol");
                    roles.Clear();
                    foreach (var permiso in permisosRol)
                    {
                        roles.Add(permiso.Nombre);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(gestorIdioma.Traducir("USU_ERR_ROLES") + ex.Message);
                    roles = new List<string>() { "Cajero", "Vendedor", "Admin" };
                }

                cmbRol.DataSource = new BindingSource(roles, null);
                ConfigDefaultForm();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            gestorIdioma.Suscribir(this);
            this.FormClosed += frmUsuario_FormClosed;
        }

        private void frmUsuario_FormClosed(object sender, FormClosedEventArgs e)
        {
            gestorIdioma.Desuscribir(this);
        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Carga de datos de fila seleccionada a textbox
            if (e.RowIndex >= 0)
            {

                DataGridViewRow fila = dgvUsuarios.Rows[e.RowIndex];

                txtDoc.Text = fila.Cells["dni"].Value.ToString();
                txtNom.Text = fila.Cells["nomb"].Value.ToString();
                txtApe.Text = fila.Cells["ape"].Value.ToString();
                cmbRol.SelectedIndex = cmbRol.Items.IndexOf(Convert.ToString(fila.Cells["rol"].Value));
                txtUsu.Text = fila.Cells["user"].Value.ToString();
                txtDir.Text = fila.Cells["dir"].Value.ToString();
                txtTel.Text = fila.Cells["tel"].Value.ToString();
                txtMail.Text = fila.Cells["email"].Value.ToString();
                if (Convert.ToBoolean(fila.Cells["estado"].Value) == true)
                {
                    chkActivo.Checked = true;
                    HabilitarBtn(btnElimUs, true);
                }
                else
                {
                    chkActivo.Checked = false;
                    HabilitarBtn(btnElimUs, false);
                }
                if (Convert.ToBoolean(fila.Cells["bloq"].Value) == true)
                {
                    chkBloqueado.Checked = true;
                    HabilitarBtn(btnDesbloquear, true);
                }
                else
                {
                    chkBloqueado.Checked = false;
                    HabilitarBtn(btnDesbloquear, false);
                }
                HabilitarBtn(btnCancelar, true);
                HabilitarBtn(btnModUs, true);
                HabilitarBtn(btnHistorial, true);
                HabilitarBtn(btnGuardar, false);
            }
            else { ConfigDefaultForm(); }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            auxUsuario = null;
            Control control = btnSalir.Parent;
            frmMenu.opcActivo.BackColor = Color.WhiteSmoke;
            this.Close();
        }

        private void btnCrearUs_Click(object sender, EventArgs e)
        {
            HabilitarBtn(btnCancelar, true);
            HabilitarBtn(btnDesbloquear, false);
            HabilitarBtn(btnHistorial, false);
            HabilitarBtn(btnCrearUs, false);
            HabilitarBtn(btnElimUs, false);
            HabilitarBtn(btnModUs, false);
            dgvUsuarios.Enabled = false;
            LimpiarDatos();
            HabilitarCampos();
            chkActivo.Checked = true;
            ClickBoton(sender, e);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ConfigDefaultForm();
            ClickBoton(sender, e);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            UsuarioBE us = CargaUsuario();

            if (us != null)
            {
                int error = 0;

                if (varMod == 0) //Guardar Usuario Nuevo
                {
                    foreach (DataGridViewRow fila in dgvUsuarios.Rows)
                    {
                        if (Convert.ToInt32(fila.Cells[3].Value) == us.dni)
                        {
                            LlenarMensaje(gestorIdioma.Traducir("USU_MSG_DNI_EXISTE"));
                            ConfigDefaultForm();
                            error = 1;
                            break;
                        }
                        else
                        {
                            if (fila.Cells[4].Value.ToString() == us.user)
                            {
                                LlenarMensaje(gestorIdioma.Traducir("USU_MSG_USER_EXISTE"));
                                txtUsu.Text = "";
                                error = 1;
                                break;
                            }
                        }
                    }
                    if (error == 0)
                    {
                        try
                        {
                            us.pass = usuarioBLL.GenerarPass(us.ape, us.dni.ToString());
                            usuarioBLL.CrearUsuario(us);
                            ActualizarDGV();
                            LlenarMensaje(string.Format(gestorIdioma.Traducir("USU_MSG_CREADO"), us.user));
                            bitacora.RegistrarBitacora(SessionManager.GetInstance.UsuarioActual().user, TipoAccion.AltaUsuario.ToString() + " " + us.user);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(gestorIdioma.Traducir("USU_ERR_CREAR") + ex.Message);
                            ConfigDefaultForm();
                        }
                    }
                }
                else //Guardar Usuario Modificado
                {
                    try
                    {
                        us.cod = Convert.ToInt32(dgvUsuarios.SelectedRows[0].Cells[0].Value);
                        usuarioBLL.ActualizarUsuario(us);
                        ActualizarDGV();
                        LlenarMensaje(string.Format(gestorIdioma.Traducir("USU_MSG_ACTUALIZADO"), us.user));
                        bitacora.RegistrarBitacora(SessionManager.GetInstance.UsuarioActual().user, TipoAccion.ModificacionUsuario.ToString() + " " + us.user);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(gestorIdioma.Traducir("USU_ERR_ACTUALIZAR") + ex.Message);
                        ConfigDefaultForm();
                    }
                }
            }
        }

        private void btnModUs_Click(object sender, EventArgs e)
        {
            varMod = 1;
            HabilitarBtn(btnCancelar, true);
            HabilitarBtn(btnDesbloquear, false);
            HabilitarBtn(btnHistorial, false);
            HabilitarBtn(btnCrearUs, false);
            HabilitarBtn(btnElimUs, false);
            HabilitarBtn(btnModUs, false);
            dgvUsuarios.Enabled = false;
            chkActivo.Enabled = true;
            HabilitarBtn(btnGuardar, true);
            HabilitarCampos();
            if (txtTel.Text == " ") { txtTel.Clear(); }
            if (txtDir.Text == " ") { txtDir.Clear(); }
            if (txtMail.Text == " ") { txtMail.Clear(); }
        }

        private void btnElimUs_Click(object sender, EventArgs e)
        {
            try
            {
                UsuarioBE us = ExtraerDatos(dgvUsuarios.SelectedRows[0]);
                if (MessageBox.Show(string.Format(gestorIdioma.Traducir("USU_MSG_CONFIRMA_ELIMINAR"), us.user), gestorIdioma.Traducir("USU_BTN_ELIMINAR"),
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    usuarioBLL.EliminarUs(us);
                    ActualizarDGV();
                    LlenarMensaje(string.Format(gestorIdioma.Traducir("USU_MSG_BAJA_OK"), us.user));
                    bitacora.RegistrarBitacora(SessionManager.GetInstance.UsuarioActual().user, TipoAccion.BajaUsuario.ToString() + " " + us.user);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(gestorIdioma.Traducir("USU_ERR_ELIMINAR") + ex.Message);
                ConfigDefaultForm();
            }
        }

        private void btnDesbloquear_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(gestorIdioma.Traducir("USU_MSG_CONFIRMA_DESBLOQUEO"), gestorIdioma.Traducir("USU_TIT_DESBLOQUEAR"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    UsuarioBE aux = new UsuarioBE();
                    aux = ExtraerDatos(dgvUsuarios.SelectedRows[0]);
                    aux.pass = usuarioBLL.GenerarPass(aux.ape, aux.dni.ToString());
                    usuarioBLL.DesbloquearUS(aux);
                    LlenarMensaje(gestorIdioma.Traducir("USU_MSG_DESBLOQUEO_OK"));
                    bitacora.RegistrarBitacora(SessionManager.GetInstance.UsuarioActual().user, TipoAccion.DesbloqueoUsuario.ToString() + " " + aux.user);
                    ActualizarDGV();
                }
            }
            catch (Exception ex) { MessageBox.Show(gestorIdioma.Traducir("COMUN_ERROR_BD") + ex.Message); }
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvUsuarios.SelectedRows.Count == 0) { return; }
                int cod = Convert.ToInt32(dgvUsuarios.SelectedRows[0].Cells["cod"].Value);
                string user = dgvUsuarios.SelectedRows[0].Cells["user"].Value.ToString();
                bitacora.RegistrarBitacora(SessionManager.GetInstance.UsuarioActual().user, TipoAccion.ConsultaHistorial.ToString() + " " + user);
                frmHistorialCambios frm = new frmHistorialCambios(cod, user);
                frm.ShowDialog();
            }
            catch (Exception ex) { MessageBox.Show(gestorIdioma.Traducir("COMUN_ERROR_BD") + ex.Message); }
        }

        private void pUsuario_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
