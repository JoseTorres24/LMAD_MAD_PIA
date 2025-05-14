namespace INICIO_Forms.ADMINISTRATIVO
{
    partial class CancelacionReservacion
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
            this.iconButton2 = new FontAwesome.Sharp.IconButton();
            this.iconButton3 = new FontAwesome.Sharp.IconButton();
            this.MostrarCodigo = new FontAwesome.Sharp.IconButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listHotelesCheck = new System.Windows.Forms.ListBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // listReservaciones
            // 
            this.listReservaciones.FormattingEnabled = true;
            this.listReservaciones.Location = new System.Drawing.Point(502, 59);
            this.listReservaciones.Name = "listReservaciones";
            this.listReservaciones.ScrollAlwaysVisible = true;
            this.listReservaciones.Size = new System.Drawing.Size(411, 485);
            this.listReservaciones.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(586, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(224, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Reservaciones En espera";
            // 
            // iconButton2
            // 
            this.iconButton2.BackColor = System.Drawing.Color.Tomato;
            this.iconButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.iconButton2.IconChar = FontAwesome.Sharp.IconChar.Hotel;
            this.iconButton2.IconColor = System.Drawing.Color.Black;
            this.iconButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton2.Location = new System.Drawing.Point(599, 550);
            this.iconButton2.Name = "iconButton2";
            this.iconButton2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.iconButton2.Size = new System.Drawing.Size(247, 57);
            this.iconButton2.TabIndex = 20;
            this.iconButton2.Text = "Generar Reservaciones";
            this.iconButton2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.iconButton2.UseVisualStyleBackColor = false;
            this.iconButton2.Click += new System.EventHandler(this.iconButton2_Click);
            // 
            // iconButton3
            // 
            this.iconButton3.BackColor = System.Drawing.Color.Yellow;
            this.iconButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.iconButton3.IconChar = FontAwesome.Sharp.IconChar.Hotel;
            this.iconButton3.IconColor = System.Drawing.Color.Black;
            this.iconButton3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton3.Location = new System.Drawing.Point(229, 84);
            this.iconButton3.Name = "iconButton3";
            this.iconButton3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.iconButton3.Size = new System.Drawing.Size(184, 31);
            this.iconButton3.TabIndex = 26;
            this.iconButton3.Text = "Generar Hoteles";
            this.iconButton3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.iconButton3.UseVisualStyleBackColor = false;
            this.iconButton3.Click += new System.EventHandler(this.iconButton3_Click);
            // 
            // MostrarCodigo
            // 
            this.MostrarCodigo.BackColor = System.Drawing.Color.LightGreen;
            this.MostrarCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.MostrarCodigo.IconChar = FontAwesome.Sharp.IconChar.Ticket;
            this.MostrarCodigo.IconColor = System.Drawing.Color.Black;
            this.MostrarCodigo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.MostrarCodigo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.MostrarCodigo.Location = new System.Drawing.Point(100, 543);
            this.MostrarCodigo.Name = "MostrarCodigo";
            this.MostrarCodigo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.MostrarCodigo.Size = new System.Drawing.Size(237, 71);
            this.MostrarCodigo.TabIndex = 25;
            this.MostrarCodigo.Text = "Cancelar Reservacion";
            this.MostrarCodigo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.MostrarCodigo.UseVisualStyleBackColor = false;
            this.MostrarCodigo.Click += new System.EventHandler(this.MostrarCodigo_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(59, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 20);
            this.label4.TabIndex = 24;
            this.label4.Text = "Seleccione Hotel";
            this.label4.UseWaitCursor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(59, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 20);
            this.label2.TabIndex = 22;
            this.label2.Text = "Seleccione Ciudad";
            this.label2.UseWaitCursor = true;
            // 
            // listHotelesCheck
            // 
            this.listHotelesCheck.FormattingEnabled = true;
            this.listHotelesCheck.Location = new System.Drawing.Point(63, 121);
            this.listHotelesCheck.Name = "listHotelesCheck";
            this.listHotelesCheck.ScrollAlwaysVisible = true;
            this.listHotelesCheck.Size = new System.Drawing.Size(350, 381);
            this.listHotelesCheck.TabIndex = 23;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(63, 35);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(274, 21);
            this.comboBox1.TabIndex = 21;
            // 
            // CancelacionReservacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 651);
            this.Controls.Add(this.iconButton3);
            this.Controls.Add(this.MostrarCodigo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listHotelesCheck);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.iconButton2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listReservaciones);
            this.Name = "CancelacionReservacion";
            this.Text = "CancelacionReservacion";
            this.Load += new System.EventHandler(this.CancelacionReservacion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listReservaciones;
        private System.Windows.Forms.Label label1;
        private FontAwesome.Sharp.IconButton iconButton2;
        private FontAwesome.Sharp.IconButton iconButton3;
        private FontAwesome.Sharp.IconButton MostrarCodigo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listHotelesCheck;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}