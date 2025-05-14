namespace INICIO_Forms.OPERATIVO
{
    partial class CHEKIN
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
            this.listReservaciones = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.listHotelesCheck = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboCiudadesCheck = new System.Windows.Forms.ComboBox();
            this.MostrarCodigo = new FontAwesome.Sharp.IconButton();
            this.SuspendLayout();
            // 
            // listReservaciones
            // 
            this.listReservaciones.FormattingEnabled = true;
            this.listReservaciones.Location = new System.Drawing.Point(495, 58);
            this.listReservaciones.Name = "listReservaciones";
            this.listReservaciones.ScrollAlwaysVisible = true;
            this.listReservaciones.Size = new System.Drawing.Size(359, 485);
            this.listReservaciones.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(491, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(224, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Reservaciones En espera";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(19, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 20);
            this.label4.TabIndex = 16;
            this.label4.Text = "Seleccione Hotel";
            this.label4.UseWaitCursor = true;
            // 
            // listHotelesCheck
            // 
            this.listHotelesCheck.FormattingEnabled = true;
            this.listHotelesCheck.Location = new System.Drawing.Point(23, 113);
            this.listHotelesCheck.Name = "listHotelesCheck";
            this.listHotelesCheck.ScrollAlwaysVisible = true;
            this.listHotelesCheck.Size = new System.Drawing.Size(350, 381);
            this.listHotelesCheck.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(19, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 20);
            this.label3.TabIndex = 14;
            this.label3.Text = "Seleccione Ciudad";
            this.label3.UseWaitCursor = true;
            // 
            // comboCiudadesCheck
            // 
            this.comboCiudadesCheck.FormattingEnabled = true;
            this.comboCiudadesCheck.Location = new System.Drawing.Point(23, 40);
            this.comboCiudadesCheck.Name = "comboCiudadesCheck";
            this.comboCiudadesCheck.Size = new System.Drawing.Size(274, 21);
            this.comboCiudadesCheck.TabIndex = 13;
            // 
            // MostrarCodigo
            // 
            this.MostrarCodigo.BackColor = System.Drawing.Color.LightGreen;
            this.MostrarCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.MostrarCodigo.IconChar = FontAwesome.Sharp.IconChar.Ticket;
            this.MostrarCodigo.IconColor = System.Drawing.Color.Black;
            this.MostrarCodigo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.MostrarCodigo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.MostrarCodigo.Location = new System.Drawing.Point(60, 513);
            this.MostrarCodigo.Name = "MostrarCodigo";
            this.MostrarCodigo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.MostrarCodigo.Size = new System.Drawing.Size(190, 71);
            this.MostrarCodigo.TabIndex = 17;
            this.MostrarCodigo.Text = "Generar Codigo";
            this.MostrarCodigo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.MostrarCodigo.UseVisualStyleBackColor = false;
            this.MostrarCodigo.Click += new System.EventHandler(this.MostrarCodigo_Click);
            // 
            // CHEKIN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MistyRose;
            this.ClientSize = new System.Drawing.Size(983, 639);
            this.Controls.Add(this.MostrarCodigo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listHotelesCheck);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboCiudadesCheck);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listReservaciones);
            this.Name = "CHEKIN";
            this.Text = "CHECKIN";
            this.Load += new System.EventHandler(this.CHEKIN_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listReservaciones;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox listHotelesCheck;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboCiudadesCheck;
        private FontAwesome.Sharp.IconButton MostrarCodigo;
    }
}