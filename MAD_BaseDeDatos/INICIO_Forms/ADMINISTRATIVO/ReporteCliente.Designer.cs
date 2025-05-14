namespace INICIO_Forms.ADMINISTRATIVO
{
    partial class ReporteCliente
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboReservacion = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textAño = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(32, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Busqueda por RFC ";
            this.label1.UseWaitCursor = true;
            // 
            // comboReservacion
            // 
            this.comboReservacion.FormattingEnabled = true;
            this.comboReservacion.Location = new System.Drawing.Point(36, 58);
            this.comboReservacion.Name = "comboReservacion";
            this.comboReservacion.Size = new System.Drawing.Size(274, 21);
            this.comboReservacion.TabIndex = 6;
            this.comboReservacion.SelectedIndexChanged += new System.EventHandler(this.comboReservacion_SelectedIndexChanged_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(32, 86);
            this.label3.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Año";
            // 
            // textAño
            // 
            this.textAño.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.textAño.Location = new System.Drawing.Point(36, 107);
            this.textAño.Margin = new System.Windows.Forms.Padding(1);
            this.textAño.Multiline = true;
            this.textAño.Name = "textAño";
            this.textAño.Size = new System.Drawing.Size(185, 21);
            this.textAño.TabIndex = 8;
            this.textAño.TextChanged += new System.EventHandler(this.textAño_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(309, 566);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(152, 44);
            this.button1.TabIndex = 11;
            this.button1.Text = "Exportar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(53, 566);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(168, 58);
            this.button2.TabIndex = 12;
            this.button2.Text = "Agregar al reporte";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(36, 158);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(583, 381);
            this.listBox1.TabIndex = 13;
            // 
            // ReporteCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(660, 649);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textAño);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboReservacion);
            this.Name = "ReporteCliente";
            this.Text = "ReporteCliente";
            this.Load += new System.EventHandler(this.ReporteCliente_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboReservacion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textAño;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox listBox1;
    }
}