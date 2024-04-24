namespace QuieroImprimirChino
{
    partial class frmEtiquetasIsrael
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEtiquetasIsrael));
            this.comboMercaderias = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.comboImpresoras = new System.Windows.Forms.ComboBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.lblMercaderia = new System.Windows.Forms.Label();
            this.lblImpresora = new System.Windows.Forms.Label();
            this.numericCopias = new System.Windows.Forms.NumericUpDown();
            this.lblCopias = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAcercaDe = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.Lbl_Version = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericCopias)).BeginInit();
            this.SuspendLayout();
            // 
            // comboMercaderias
            // 
            this.comboMercaderias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboMercaderias.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboMercaderias.FormattingEnabled = true;
            this.comboMercaderias.Location = new System.Drawing.Point(238, 179);
            this.comboMercaderias.Name = "comboMercaderias";
            this.comboMercaderias.Size = new System.Drawing.Size(364, 27);
            this.comboMercaderias.TabIndex = 2;
            this.comboMercaderias.SelectedIndexChanged += new System.EventHandler(this.comboMercaderias_SelectedIndexChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Location = new System.Drawing.Point(238, 111);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(364, 27);
            this.dateTimePicker1.TabIndex = 4;
            // 
            // btnImprimir
            // 
            this.btnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.Location = new System.Drawing.Point(238, 373);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(136, 35);
            this.btnImprimir.TabIndex = 5;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // comboImpresoras
            // 
            this.comboImpresoras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboImpresoras.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboImpresoras.FormattingEnabled = true;
            this.comboImpresoras.Location = new System.Drawing.Point(238, 243);
            this.comboImpresoras.Name = "comboImpresoras";
            this.comboImpresoras.Size = new System.Drawing.Size(364, 27);
            this.comboImpresoras.TabIndex = 10;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.lblFecha.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblFecha.Location = new System.Drawing.Point(48, 115);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(160, 19);
            this.lblFecha.TabIndex = 11;
            this.lblFecha.Text = "Fecha de producción:";
            // 
            // lblMercaderia
            // 
            this.lblMercaderia.AutoSize = true;
            this.lblMercaderia.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.lblMercaderia.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMercaderia.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMercaderia.Location = new System.Drawing.Point(48, 179);
            this.lblMercaderia.Name = "lblMercaderia";
            this.lblMercaderia.Size = new System.Drawing.Size(78, 19);
            this.lblMercaderia.TabIndex = 12;
            this.lblMercaderia.Text = "Producto:";
            // 
            // lblImpresora
            // 
            this.lblImpresora.AutoSize = true;
            this.lblImpresora.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.lblImpresora.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImpresora.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblImpresora.Location = new System.Drawing.Point(48, 243);
            this.lblImpresora.Name = "lblImpresora";
            this.lblImpresora.Size = new System.Drawing.Size(88, 19);
            this.lblImpresora.TabIndex = 13;
            this.lblImpresora.Text = "Impresora:";
            // 
            // numericCopias
            // 
            this.numericCopias.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericCopias.Location = new System.Drawing.Point(238, 306);
            this.numericCopias.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericCopias.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericCopias.Name = "numericCopias";
            this.numericCopias.Size = new System.Drawing.Size(76, 40);
            this.numericCopias.TabIndex = 14;
            this.numericCopias.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblCopias
            // 
            this.lblCopias.AutoSize = true;
            this.lblCopias.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.lblCopias.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopias.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCopias.Location = new System.Drawing.Point(48, 307);
            this.lblCopias.Name = "lblCopias";
            this.lblCopias.Size = new System.Drawing.Size(62, 19);
            this.lblCopias.TabIndex = 15;
            this.lblCopias.Text = "Copias:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label1.Font = new System.Drawing.Font("Franklin Gothic Demi", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(579, 44);
            this.label1.TabIndex = 16;
            this.label1.Text = "Etiquetas alto impacto PREMIUM";
            // 
            // btnAcercaDe
            // 
            this.btnAcercaDe.FlatAppearance.BorderSize = 0;
            this.btnAcercaDe.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnAcercaDe.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnAcercaDe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAcercaDe.Image = ((System.Drawing.Image)(resources.GetObject("btnAcercaDe.Image")));
            this.btnAcercaDe.Location = new System.Drawing.Point(3, 390);
            this.btnAcercaDe.Name = "btnAcercaDe";
            this.btnAcercaDe.Size = new System.Drawing.Size(43, 43);
            this.btnAcercaDe.TabIndex = 17;
            this.btnAcercaDe.UseVisualStyleBackColor = true;
            this.btnAcercaDe.Click += new System.EventHandler(this.btnAcercaDe_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(467, 300);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 26);
            this.button1.TabIndex = 18;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(467, 346);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(135, 30);
            this.button2.TabIndex = 19;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Lbl_Version
            // 
            this.Lbl_Version.AutoSize = true;
            this.Lbl_Version.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Lbl_Version.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Version.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Lbl_Version.Location = new System.Drawing.Point(52, 418);
            this.Lbl_Version.Name = "Lbl_Version";
            this.Lbl_Version.Size = new System.Drawing.Size(17, 19);
            this.Lbl_Version.TabIndex = 20;
            this.Lbl_Version.Text = "v";
            // 
            // frmEtiquetasIsrael
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(631, 436);
            this.Controls.Add(this.Lbl_Version);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnAcercaDe);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblCopias);
            this.Controls.Add(this.numericCopias);
            this.Controls.Add(this.lblImpresora);
            this.Controls.Add(this.lblMercaderia);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.comboImpresoras);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.comboMercaderias);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmEtiquetasIsrael";
            this.Text = "OFFAL EXP SA - Impresion de etiquetas PREMIUM";
            this.Load += new System.EventHandler(this.frmEtiquetasIsrael_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericCopias)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboMercaderias;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.ComboBox comboImpresoras;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label lblMercaderia;
        private System.Windows.Forms.Label lblImpresora;
        private System.Windows.Forms.NumericUpDown numericCopias;
        private System.Windows.Forms.Label lblCopias;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAcercaDe;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label Lbl_Version;
    }
}

