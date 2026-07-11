namespace TP_SanchezVillaverde
{
    partial class frmHistorialCambios
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pHistorial = new System.Windows.Forms.Panel();
            this.btnSalir = new System.Windows.Forms.Button();
            this.gbDetalle = new System.Windows.Forms.GroupBox();
            this.txtDetalle = new System.Windows.Forms.TextBox();
            this.dgvHistorial = new System.Windows.Forms.DataGridView();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pHistorial.SuspendLayout();
            this.gbDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).BeginInit();
            this.SuspendLayout();
            //
            // pHistorial
            //
            this.pHistorial.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pHistorial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pHistorial.Controls.Add(this.btnSalir);
            this.pHistorial.Controls.Add(this.gbDetalle);
            this.pHistorial.Controls.Add(this.dgvHistorial);
            this.pHistorial.Controls.Add(this.lblUsuario);
            this.pHistorial.Controls.Add(this.lblTitulo);
            this.pHistorial.Location = new System.Drawing.Point(12, 12);
            this.pHistorial.Name = "pHistorial";
            this.pHistorial.Size = new System.Drawing.Size(760, 537);
            this.pHistorial.TabIndex = 0;
            //
            // btnSalir
            //
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnSalir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnSalir.Location = new System.Drawing.Point(335, 490);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(88, 34);
            this.btnSalir.TabIndex = 4;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            //
            // gbDetalle
            //
            this.gbDetalle.Controls.Add(this.txtDetalle);
            this.gbDetalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.gbDetalle.Location = new System.Drawing.Point(15, 388);
            this.gbDetalle.Name = "gbDetalle";
            this.gbDetalle.Size = new System.Drawing.Size(728, 90);
            this.gbDetalle.TabIndex = 3;
            this.gbDetalle.TabStop = false;
            this.gbDetalle.Text = "Detalle del Cambio";
            //
            // txtDetalle
            //
            this.txtDetalle.BackColor = System.Drawing.Color.White;
            this.txtDetalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtDetalle.Location = new System.Drawing.Point(11, 24);
            this.txtDetalle.Multiline = true;
            this.txtDetalle.Name = "txtDetalle";
            this.txtDetalle.ReadOnly = true;
            this.txtDetalle.Size = new System.Drawing.Size(706, 54);
            this.txtDetalle.TabIndex = 0;
            //
            // dgvHistorial
            //
            this.dgvHistorial.AllowUserToAddRows = false;
            this.dgvHistorial.AllowUserToDeleteRows = false;
            this.dgvHistorial.AllowUserToResizeRows = false;
            this.dgvHistorial.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHistorial.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvHistorial.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHistorial.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvHistorial.ColumnHeadersHeight = 40;
            this.dgvHistorial.EnableHeadersVisualStyles = false;
            this.dgvHistorial.Location = new System.Drawing.Point(15, 82);
            this.dgvHistorial.MultiSelect = false;
            this.dgvHistorial.Name = "dgvHistorial";
            this.dgvHistorial.ReadOnly = true;
            this.dgvHistorial.RowHeadersVisible = false;
            this.dgvHistorial.RowHeadersWidth = 51;
            this.dgvHistorial.RowTemplate.Height = 24;
            this.dgvHistorial.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHistorial.Size = new System.Drawing.Size(728, 294);
            this.dgvHistorial.TabIndex = 2;
            this.dgvHistorial.SelectionChanged += new System.EventHandler(this.dgvHistorial_SelectionChanged);
            //
            // lblUsuario
            //
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblUsuario.Location = new System.Drawing.Point(12, 52);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(180, 20);
            this.lblUsuario.TabIndex = 1;
            this.lblUsuario.Text = "Cambios del usuario: ";
            //
            // lblTitulo
            //
            this.lblTitulo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Verdana", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lblTitulo.Location = new System.Drawing.Point(159, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(440, 36);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "HISTORIAL DE CAMBIOS";
            //
            // frmHistorialCambios
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.pHistorial);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmHistorialCambios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Historial de Cambios";
            this.Load += new System.EventHandler(this.frmHistorialCambios_Load);
            this.pHistorial.ResumeLayout(false);
            this.pHistorial.PerformLayout();
            this.gbDetalle.ResumeLayout(false);
            this.gbDetalle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pHistorial;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.DataGridView dgvHistorial;
        private System.Windows.Forms.GroupBox gbDetalle;
        private System.Windows.Forms.TextBox txtDetalle;
        private System.Windows.Forms.Button btnSalir;
    }
}
