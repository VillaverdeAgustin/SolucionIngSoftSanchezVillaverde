namespace TP_SanchezVillaverde
{
    partial class frmGestionPerfiles
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
            this.labelTitulo = new System.Windows.Forms.Label();
            this.labelRol = new System.Windows.Forms.Label();
            this.labelNombre = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.rBFamilia = new System.Windows.Forms.RadioButton();
            this.rBRol = new System.Windows.Forms.RadioButton();
            this.button5 = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.comboFamilias = new System.Windows.Forms.ComboBox();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelTitulo
            // 
            this.labelTitulo.AutoSize = true;
            this.labelTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitulo.Location = new System.Drawing.Point(13, 13);
            this.labelTitulo.Name = "labelTitulo";
            this.labelTitulo.Size = new System.Drawing.Size(161, 24);
            this.labelTitulo.TabIndex = 0;
            this.labelTitulo.Text = "Gestión de Perfiles";
            // 
            // labelRol
            // 
            this.labelRol.AutoSize = true;
            this.labelRol.Location = new System.Drawing.Point(13, 60);
            this.labelRol.Name = "labelRol";
            this.labelRol.Size = new System.Drawing.Size(81, 13);
            this.labelRol.TabIndex = 1;
            this.labelRol.Text = "Seleccionar Rol:";
            // 
            // labelNombre
            // 
            this.labelNombre.AutoSize = true;
            this.labelNombre.Location = new System.Drawing.Point(13, 100);
            this.labelNombre.Name = "labelNombre";
            this.labelNombre.Size = new System.Drawing.Size(47, 13);
            this.labelNombre.TabIndex = 2;
            this.labelNombre.Text = "Nombre:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(16, 120);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(200, 20);
            this.textBox2.TabIndex = 3;
            // 
            // rBFamilia
            // 
            this.rBFamilia.AutoSize = true;
            this.rBFamilia.Location = new System.Drawing.Point(16, 150);
            this.rBFamilia.Name = "rBFamilia";
            this.rBFamilia.Size = new System.Drawing.Size(60, 17);
            this.rBFamilia.TabIndex = 4;
            this.rBFamilia.TabStop = true;
            this.rBFamilia.Text = "Familia";
            this.rBFamilia.UseVisualStyleBackColor = true;
            // 
            // rBRol
            // 
            this.rBRol.AutoSize = true;
            this.rBRol.Location = new System.Drawing.Point(100, 150);
            this.rBRol.Name = "rBRol";
            this.rBRol.Size = new System.Drawing.Size(41, 17);
            this.rBRol.TabStop = true;
            this.rBRol.Text = "Rol";
            this.rBRol.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(16, 180);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 5;
            this.button5.Text = "Crear";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(250, 60);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(300, 250);
            this.treeView1.TabIndex = 6;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Location = new System.Drawing.Point(580, 60);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(300, 250);
            this.checkedListBox1.TabIndex = 7;
            // 
            // comboFamilias
            // 
            this.comboFamilias.Location = new System.Drawing.Point(16, 75);
            this.comboFamilias.Name = "comboFamilias";
            this.comboFamilias.Size = new System.Drawing.Size(200, 21);
            this.comboFamilias.TabIndex = 8;
            this.comboFamilias.SelectedIndexChanged += new System.EventHandler(this.comboFamilias_SelectedIndexChanged);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(16, 210);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 23);
            this.btnEliminar.TabIndex = 9;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(100, 180);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 10;
            this.button7.Text = "Guardar";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(100, 210);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Cancelar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmGestionPerfiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 350);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.comboFamilias);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.rBRol);
            this.Controls.Add(this.rBFamilia);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.labelNombre);
            this.Controls.Add(this.labelRol);
            this.Controls.Add(this.labelTitulo);
            this.Name = "frmGestionPerfiles";
            this.Text = "Gestión de Perfiles";
            this.Load += new System.EventHandler(this.frmGestionPerfiles_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitulo;
        private System.Windows.Forms.Label labelRol;
        private System.Windows.Forms.Label labelNombre;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.RadioButton rBFamilia;
        private System.Windows.Forms.RadioButton rBRol;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.ComboBox comboFamilias;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button1;
    }
}
