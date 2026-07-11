using Entidad_BE;
using Negocio_BLL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TP_SanchezVillaverde
{
    public partial class frmHistorialCambios : Form, IObservadorIdioma
    {
        private readonly int codUsuario;
        private readonly string nombreUsuario;
        HistorialCambiosBLL historialBLL = new HistorialCambiosBLL();
        GestorDeIdioma gestorIdioma = GestorDeIdioma.GetInstance;

        public frmHistorialCambios(int cod, string user)
        {
            InitializeComponent();
            codUsuario = cod;
            nombreUsuario = user;
        }

        private void frmHistorialCambios_Load(object sender, EventArgs e)
        {
            try
            {
                List<CambioBE> cambios = historialBLL.ListarCambiosUsuario(codUsuario);
                dgvHistorial.DataSource = cambios;
                dgvHistorial.Columns["entidadId"].Visible = false;
                dgvHistorial.ReadOnly = true;
                dgvHistorial.EnableHeadersVisualStyles = false;
                dgvHistorial.ColumnHeadersDefaultCellStyle.Font = new Font(dgvHistorial.Font, FontStyle.Bold);
                TraducirColumnas();
                if (cambios.Count == 0)
                {
                    txtDetalle.Text = gestorIdioma.Traducir("HIST_SIN_CAMBIOS");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(gestorIdioma.Traducir("COMUN_ERROR_BD") + ex.Message);
            }

            gestorIdioma.Suscribir(this);
            this.FormClosed += frmHistorialCambios_FormClosed;
        }

        private void frmHistorialCambios_FormClosed(object sender, FormClosedEventArgs e)
        {
            gestorIdioma.Desuscribir(this);
        }

        #region Patron Observer - Idiomas

        public void ActualizarTextos()
        {
            this.Text = gestorIdioma.Traducir("HIST_TITULO");
            lblTitulo.Text = gestorIdioma.Traducir("HIST_LBL_TITULO");
            lblUsuario.Text = gestorIdioma.Traducir("HIST_LBL_USUARIO") + nombreUsuario;
            gbDetalle.Text = gestorIdioma.Traducir("HIST_GB_DETALLE");
            btnSalir.Text = gestorIdioma.Traducir("COMUN_SALIR");
            TraducirColumnas();
            MostrarDetalle();
        }

        private void TraducirColumnas()
        {
            if (dgvHistorial.Columns.Count == 0) { return; }
            dgvHistorial.Columns["id"].HeaderText = gestorIdioma.Traducir("BIT_COL_REGISTRO");
            dgvHistorial.Columns["campo"].HeaderText = gestorIdioma.Traducir("HIST_COL_CAMPO");
            dgvHistorial.Columns["valorAnterior"].HeaderText = gestorIdioma.Traducir("HIST_COL_ANTERIOR");
            dgvHistorial.Columns["valorNuevo"].HeaderText = gestorIdioma.Traducir("HIST_COL_NUEVO");
            dgvHistorial.Columns["usuario"].HeaderText = gestorIdioma.Traducir("HIST_COL_RESPONSABLE");
            dgvHistorial.Columns["fecha"].HeaderText = gestorIdioma.Traducir("COMUN_FECHA");
        }

        #endregion

        private void dgvHistorial_SelectionChanged(object sender, EventArgs e)
        {
            MostrarDetalle();
        }

        private void MostrarDetalle()
        {
            if (dgvHistorial.SelectedRows.Count == 0) { return; }

            CambioBE cambio = dgvHistorial.SelectedRows[0].DataBoundItem as CambioBE;
            if (cambio == null) { return; }

            txtDetalle.Text = string.Format(gestorIdioma.Traducir("HIST_DETALLE_FMT"),
                cambio.fecha, cambio.usuario, cambio.campo, cambio.valorAnterior, cambio.valorNuevo);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
