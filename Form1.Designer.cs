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
            this.labelFechaFaena = new System.Windows.Forms.Label();
            this.dateTimePickerFaena = new System.Windows.Forms.DateTimePicker();
            this.CategoriaCombo = new System.Windows.Forms.ComboBox();
            this.ActualizarMercaderiaBoton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericCopias)).BeginInit();
            this.SuspendLayout();
            // 
            // comboMercaderias
            // 
            this.comboMercaderias.BackColor = System.Drawing.SystemColors.Window;
            this.comboMercaderias.DropDownHeight = 150;
            this.comboMercaderias.Font = new System.Drawing.Font("Tahoma", 16F);
            this.comboMercaderias.FormattingEnabled = true;
            this.comboMercaderias.IntegralHeight = false;
            this.comboMercaderias.ItemHeight = 25;
            this.comboMercaderias.Location = new System.Drawing.Point(191, 196);
            this.comboMercaderias.Name = "comboMercaderias";
            this.comboMercaderias.Size = new System.Drawing.Size(496, 33);
            this.comboMercaderias.TabIndex = 3;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Tahoma", 18F);
            this.dateTimePicker1.Location = new System.Drawing.Point(191, 103);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(312, 36);
            this.dateTimePicker1.TabIndex = 1;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.Location = new System.Drawing.Point(238, 373);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(136, 35);
            this.btnImprimir.TabIndex = 6;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // comboImpresoras
            // 
            this.comboImpresoras.Font = new System.Drawing.Font("Tahoma", 14F);
            this.comboImpresoras.FormattingEnabled = true;
            this.comboImpresoras.Location = new System.Drawing.Point(191, 249);
            this.comboImpresoras.Name = "comboImpresoras";
            this.comboImpresoras.Size = new System.Drawing.Size(364, 31);
            this.comboImpresoras.TabIndex = 4;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.lblFecha.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblFecha.Location = new System.Drawing.Point(12, 111);
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
            this.lblMercaderia.Location = new System.Drawing.Point(12, 207);
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
            this.lblImpresora.Location = new System.Drawing.Point(12, 255);
            this.lblImpresora.Name = "lblImpresora";
            this.lblImpresora.Size = new System.Drawing.Size(88, 19);
            this.lblImpresora.TabIndex = 13;
            this.lblImpresora.Text = "Impresora:";
            // 
            // numericCopias
            // 
            this.numericCopias.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericCopias.Location = new System.Drawing.Point(238, 303);
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
            this.numericCopias.TabIndex = 5;
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
            this.lblCopias.Location = new System.Drawing.Point(12, 303);
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
            this.label1.Location = new System.Drawing.Point(102, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(499, 44);
            this.label1.TabIndex = 16;
            this.label1.Text = "Etiquetas de caja para Israel";
            // 
            // labelFechaFaena
            // 
            this.labelFechaFaena.AutoSize = true;
            this.labelFechaFaena.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.labelFechaFaena.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFechaFaena.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelFechaFaena.Location = new System.Drawing.Point(12, 159);
            this.labelFechaFaena.Name = "labelFechaFaena";
            this.labelFechaFaena.Size = new System.Drawing.Size(120, 19);
            this.labelFechaFaena.TabIndex = 18;
            this.labelFechaFaena.Text = "Fecha de faena:";
            // 
            // dateTimePickerFaena
            // 
            this.dateTimePickerFaena.Font = new System.Drawing.Font("Tahoma", 18F);
            this.dateTimePickerFaena.Location = new System.Drawing.Point(191, 145);
            this.dateTimePickerFaena.Name = "dateTimePickerFaena";
            this.dateTimePickerFaena.Size = new System.Drawing.Size(312, 36);
            this.dateTimePickerFaena.TabIndex = 2;
            // 
            // CategoriaCombo
            // 
            this.CategoriaCombo.BackColor = System.Drawing.Color.Lime;
            this.CategoriaCombo.Font = new System.Drawing.Font("Tahoma", 16F);
            this.CategoriaCombo.FormattingEnabled = true;
            this.CategoriaCombo.IntegralHeight = false;
            this.CategoriaCombo.ItemHeight = 25;
            this.CategoriaCombo.Location = new System.Drawing.Point(462, 327);
            this.CategoriaCombo.Name = "CategoriaCombo";
            this.CategoriaCombo.Size = new System.Drawing.Size(225, 33);
            this.CategoriaCombo.TabIndex = 21;
            // 
            // ActualizarMercaderiaBoton
            // 
            this.ActualizarMercaderiaBoton.Location = new System.Drawing.Point(536, 384);
            this.ActualizarMercaderiaBoton.Name = "ActualizarMercaderiaBoton";
            this.ActualizarMercaderiaBoton.Size = new System.Drawing.Size(75, 36);
            this.ActualizarMercaderiaBoton.TabIndex = 22;
            this.ActualizarMercaderiaBoton.Text = "Buscar";
            this.ActualizarMercaderiaBoton.UseVisualStyleBackColor = true;
            this.ActualizarMercaderiaBoton.Click += new System.EventHandler(this.ActualizarMercaderiaBoton_Click);
            // 
            // frmEtiquetasIsrael
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(699, 432);
            this.Controls.Add(this.ActualizarMercaderiaBoton);
            this.Controls.Add(this.CategoriaCombo);
            this.Controls.Add(this.labelFechaFaena);
            this.Controls.Add(this.dateTimePickerFaena);
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
            this.MaximizeBox = false;
            this.Name = "frmEtiquetasIsrael";
            this.Text = "Runfo SA - Impresion de etiquetas ISRAEL";
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
        private System.Windows.Forms.Label labelFechaFaena;
        private System.Windows.Forms.DateTimePicker dateTimePickerFaena;
        private System.Windows.Forms.ComboBox CategoriaCombo;
        private System.Windows.Forms.Button ActualizarMercaderiaBoton;
    }
}

