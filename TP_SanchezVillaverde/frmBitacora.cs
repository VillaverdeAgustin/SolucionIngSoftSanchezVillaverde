using Negocio_BLL;
using Servicios;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace TP_SanchezVillaverde
{
    public partial class frmBitacora : Form, IObservadorIdioma
    {
        public frmBitacora()
        {
            InitializeComponent();
        }

        BitacoraBLL bitacora = new BitacoraBLL();
        GestorDeIdioma gestorIdioma = GestorDeIdioma.GetInstance;

        private void frmBitacora_Load(object sender, EventArgs e)
        {
            dgvBitacora.DataSource = bitacora.ListarBitacora();
            TraducirColumnas();
            dgvBitacora.EnableHeadersVisualStyles = false;
            dgvBitacora.ColumnHeadersDefaultCellStyle.Font = new Font(dgvBitacora.Font, FontStyle.Bold);
            dgvBitacora.ReadOnly = true;
            gestorIdioma.Suscribir(this);
            this.FormClosed += frmBitacora_FormClosed;
        }

        private void frmBitacora_FormClosed(object sender, FormClosedEventArgs e)
        {
            gestorIdioma.Desuscribir(this);
        }

        #region Patron Observer - Idiomas

        public void ActualizarTextos()
        {
            this.Text = gestorIdioma.Traducir("BIT_TITULO");
            label1.Text = gestorIdioma.Traducir("BIT_LBL_TITULO");
            btnSalir.Text = gestorIdioma.Traducir("COMUN_SALIR");
            TraducirColumnas();
        }

        private void TraducirColumnas()
        {
            if (dgvBitacora.Columns.Count == 0) { return; }
            dgvBitacora.Columns[0].HeaderText = gestorIdioma.Traducir("BIT_COL_REGISTRO");
            dgvBitacora.Columns[1].HeaderText = gestorIdioma.Traducir("COMUN_USUARIO");
            dgvBitacora.Columns[2].HeaderText = gestorIdioma.Traducir("COMUN_ACCION");
            dgvBitacora.Columns[3].HeaderText = gestorIdioma.Traducir("COMUN_FECHA");
        }

        #endregion

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Control control = btnSalir.Parent;
            frmMenu.opcActivo.BackColor = Color.WhiteSmoke;
            this.Close();
        }
    }
}
