namespace INICIO_Forms.ADMINISTRATIVO
{
    partial class RegistroServicio
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
            this.textCosto = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.listServicios = new System.Windows.Forms.ListBox();
            this.textServicio = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.listHotelesRegistados = new System.Windows.Forms.ListBox();
            this.buttonGuardar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textCosto
            // 
            this.textCosto.Location = new System.Drawing.Point(593, 143);
            this.textCosto.Multiline = true;
            this.textCosto.Name = "textCosto";
            this.textCosto.Size = new System.Drawing.Size(209, 24);
            this.textCosto.TabIndex = 53;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label18.Location = new System.Drawing.Point(593, 108);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(205, 20);
            this.label18.TabIndex = 52;
            this.label18.Text = "Ingresar Costo de servicio";
            // 
            // listServicios
            // 
            this.listServicios.FormattingEnabled = true;
            this.listServicios.Location = new System.Drawing.Point(356, 33);
            this.listServicios.Name = "listServicios";
            this.listServicios.Size = new System.Drawing.Size(198, 342);
            this.listServicios.TabIndex = 51;
            this.listServicios.SelectedIndexChanged += new System.EventHandler(this.listServicios_SelectedIndexChanged);
            // 
            // textServicio
            // 
            this.textServicio.Location = new System.Drawing.Point(594, 72);
            this.textServicio.Multiline = true;
            this.textServicio.Name = "textServicio";
            this.textServicio.Size = new System.Drawing.Size(209, 24);
            this.textServicio.TabIndex = 50;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label8.Location = new System.Drawing.Point(591, 39);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(203, 20);
            this.label8.TabIndex = 49;
            this.label8.Text = "Ingresar Servicio de Hotel";
            // 
            // listHotelesRegistados
            // 
            this.listHotelesRegistados.FormattingEnabled = true;
            this.listHotelesRegistados.Location = new System.Drawing.Point(12, 33);
            this.listHotelesRegistados.Name = "listHotelesRegistados";
            this.listHotelesRegistados.Size = new System.Drawing.Size(319, 498);
            this.listHotelesRegistados.TabIndex = 54;
            this.listHotelesRegistados.SelectedIndexChanged += new System.EventHandler(this.listHotelesRegistados_SelectedIndexChanged);
            // 
            // buttonGuardar
            // 
            this.buttonGuardar.Location = new System.Drawing.Point(356, 470);
            this.buttonGuardar.Name = "buttonGuardar";
            this.buttonGuardar.Size = new System.Drawing.Size(153, 43);
            this.buttonGuardar.TabIndex = 55;
            this.buttonGuardar.Text = "Guardar Servicio";
            this.buttonGuardar.UseVisualStyleBackColor = true;
            this.buttonGuardar.Click += new System.EventHandler(this.buttonGuardar_Click);
            // 
            // RegistroServicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(826, 553);
            this.Controls.Add(this.buttonGuardar);
            this.Controls.Add(this.listHotelesRegistados);
            this.Controls.Add(this.textCosto);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.listServicios);
            this.Controls.Add(this.textServicio);
            this.Controls.Add(this.label8);
            this.Name = "RegistroServicio";
            this.Text = "RegistroServicio";
            this.Load += new System.EventHandler(this.RegistroServicio_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textCosto;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ListBox listServicios;
        private System.Windows.Forms.TextBox textServicio;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox listHotelesRegistados;
        private System.Windows.Forms.Button buttonGuardar;
    }
}