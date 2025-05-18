namespace INICIO_Forms.OPERATIVO
{
    partial class CHECKOUT
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
            this.listClientes = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.listHotelesCheck = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboCiudadesCheck = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.iconButton1 = new FontAwesome.Sharp.IconButton();
            this.iconButton2 = new FontAwesome.Sharp.IconButton();
            this.iconButton3 = new FontAwesome.Sharp.IconButton();
            this.SuspendLayout();
            // 
            // listClientes
            // 
            this.listClientes.FormattingEnabled = true;
            this.listClientes.Location = new System.Drawing.Point(484, 99);
            this.listClientes.Name = "listClientes";
            this.listClientes.ScrollAlwaysVisible = true;
            this.listClientes.Size = new System.Drawing.Size(487, 446);
            this.listClientes.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(12, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 20);
            this.label4.TabIndex = 20;
            this.label4.Text = "Seleccione Hotel";
            this.label4.UseWaitCursor = true;
            // 
            // listHotelesCheck
            // 
            this.listHotelesCheck.FormattingEnabled = true;
            this.listHotelesCheck.Location = new System.Drawing.Point(16, 111);
            this.listHotelesCheck.Name = "listHotelesCheck";
            this.listHotelesCheck.ScrollAlwaysVisible = true;
            this.listHotelesCheck.Size = new System.Drawing.Size(326, 446);
            this.listHotelesCheck.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(12, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 20);
            this.label3.TabIndex = 18;
            this.label3.Text = "Seleccione Ciudad";
            this.label3.UseWaitCursor = true;
            // 
            // comboCiudadesCheck
            // 
            this.comboCiudadesCheck.FormattingEnabled = true;
            this.comboCiudadesCheck.Location = new System.Drawing.Point(16, 41);
            this.comboCiudadesCheck.Name = "comboCiudadesCheck";
            this.comboCiudadesCheck.Size = new System.Drawing.Size(274, 21);
            this.comboCiudadesCheck.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(800, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 20);
            this.label1.TabIndex = 21;
            this.label1.Text = "Clientes a Salir";
            this.label1.UseWaitCursor = true;
            // 
            // iconButton1
            // 
            this.iconButton1.BackColor = System.Drawing.Color.LightGreen;
            this.iconButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.iconButton1.IconChar = FontAwesome.Sharp.IconChar.CheckDouble;
            this.iconButton1.IconColor = System.Drawing.Color.Black;
            this.iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton1.Location = new System.Drawing.Point(740, 574);
            this.iconButton1.Name = "iconButton1";
            this.iconButton1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.iconButton1.Size = new System.Drawing.Size(208, 73);
            this.iconButton1.TabIndex = 25;
            this.iconButton1.Text = "Generar Factura";
            this.iconButton1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.iconButton1.UseVisualStyleBackColor = false;
            this.iconButton1.Click += new System.EventHandler(this.iconButton1_Click);
            // 
            // iconButton2
            // 
            this.iconButton2.BackColor = System.Drawing.Color.Yellow;
            this.iconButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.iconButton2.IconChar = FontAwesome.Sharp.IconChar.Hotel;
            this.iconButton2.IconColor = System.Drawing.Color.Black;
            this.iconButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton2.Location = new System.Drawing.Point(322, 41);
            this.iconButton2.Name = "iconButton2";
            this.iconButton2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.iconButton2.Size = new System.Drawing.Size(184, 31);
            this.iconButton2.TabIndex = 26;
            this.iconButton2.Text = "Generar Hoteles";
            this.iconButton2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.iconButton2.UseVisualStyleBackColor = false;
            this.iconButton2.Click += new System.EventHandler(this.iconButton2_Click);
            // 
            // iconButton3
            // 
            this.iconButton3.BackColor = System.Drawing.Color.Lime;
            this.iconButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.iconButton3.IconChar = FontAwesome.Sharp.IconChar.Hotel;
            this.iconButton3.IconColor = System.Drawing.Color.Black;
            this.iconButton3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton3.Location = new System.Drawing.Point(430, 574);
            this.iconButton3.Name = "iconButton3";
            this.iconButton3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.iconButton3.Size = new System.Drawing.Size(264, 60);
            this.iconButton3.TabIndex = 27;
            this.iconButton3.Text = "Generar Clientes para salir";
            this.iconButton3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.iconButton3.UseVisualStyleBackColor = false;
            this.iconButton3.Click += new System.EventHandler(this.iconButton3_Click);
            // 
            // CHECKOUT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MistyRose;
            this.ClientSize = new System.Drawing.Size(983, 680);
            this.Controls.Add(this.iconButton3);
            this.Controls.Add(this.iconButton2);
            this.Controls.Add(this.iconButton1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listHotelesCheck);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboCiudadesCheck);
            this.Controls.Add(this.listClientes);
            this.Name = "CHECKOUT";
            this.Text = "CHECKOUT";
            this.Load += new System.EventHandler(this.CHECKOUT_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listClientes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox listHotelesCheck;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboCiudadesCheck;
        private System.Windows.Forms.Label label1;
        private FontAwesome.Sharp.IconButton iconButton1;
        private FontAwesome.Sharp.IconButton iconButton2;
        private FontAwesome.Sharp.IconButton iconButton3;
    }
}