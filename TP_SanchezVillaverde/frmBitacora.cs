using Negocio_BLL;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace TP_SanchezVillaverde
{
    public partial class frmBitacora : Form
    {
        public frmBitacora()
        {
            InitializeComponent();
        }

        BitacoraBLL bitacora = new BitacoraBLL();

        private void frmBitacora_Load(object sender, EventArgs e)
        {
            dgvBitacora.DataSource = bitacora.ListarBitacora();
            dgvBitacora.Columns[0].HeaderText = "ID Registro";
            dgvBitacora.Columns[1].HeaderText = "Usuario";
            dgvBitacora.Columns[2].HeaderText = "Accion";
            dgvBitacora.Columns[3].HeaderText = "Fecha";
            dgvBitacora.EnableHeadersVisualStyles = false;
            dgvBitacora.ColumnHeadersDefaultCellStyle.Font = new Font(dgvBitacora.Font, FontStyle.Bold);
            dgvBitacora.ReadOnly = true;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Control control = btnSalir.Parent;
            frmMenu.opcActivo.BackColor = Color.WhiteSmoke;
            this.Close();
        }
    }
}
