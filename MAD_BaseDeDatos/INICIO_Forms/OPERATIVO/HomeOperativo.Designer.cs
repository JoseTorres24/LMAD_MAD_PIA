namespace INICIO_Forms
{
    partial class HomeOperativo
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
            this.menuTitutlo = new System.Windows.Forms.MenuStrip();
            this.ReservacionesMenu = new FontAwesome.Sharp.IconMenuItem();
            this.CheckInMenu = new FontAwesome.Sharp.IconMenuItem();
            this.CheckOutMenu = new FontAwesome.Sharp.IconMenuItem();
            this.SalirMenu = new FontAwesome.Sharp.IconMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.label1 = new System.Windows.Forms.Label();
            this.frmReservacion = new FontAwesome.Sharp.IconMenuItem();
            this.frmCheckIn = new FontAwesome.Sharp.IconMenuItem();
            this.frmCheckOut = new FontAwesome.Sharp.IconMenuItem();
            this.Salir = new FontAwesome.Sharp.IconMenuItem();
            this.menuTitutlo.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuTitutlo
            // 
            this.menuTitutlo.BackColor = System.Drawing.Color.Snow;
            this.menuTitutlo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ReservacionesMenu,
            this.CheckInMenu,
            this.CheckOutMenu,
            this.SalirMenu});
            this.menuTitutlo.Location = new System.Drawing.Point(0, 77);
            this.menuTitutlo.Name = "menuTitutlo";
            this.menuTitutlo.Size = new System.Drawing.Size(1182, 71);
            this.menuTitutlo.TabIndex = 0;
            this.menuTitutlo.Text = "menuStrip1";
            // 
            // ReservacionesMenu
            // 
            this.ReservacionesMenu.AutoSize = false;
            this.ReservacionesMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.frmReservacion});
            this.ReservacionesMenu.IconChar = FontAwesome.Sharp.IconChar.Receipt;
            this.ReservacionesMenu.IconColor = System.Drawing.Color.Black;
            this.ReservacionesMenu.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.ReservacionesMenu.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ReservacionesMenu.Name = "ReservacionesMenu";
            this.ReservacionesMenu.Size = new System.Drawing.Size(93, 67);
            this.ReservacionesMenu.Text = "Reservaciones";
            this.ReservacionesMenu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // CheckInMenu
            // 
            this.CheckInMenu.AutoSize = false;
            this.CheckInMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.frmCheckIn});
            this.CheckInMenu.IconChar = FontAwesome.Sharp.IconChar.DoorOpen;
            this.CheckInMenu.IconColor = System.Drawing.Color.Black;
            this.CheckInMenu.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.CheckInMenu.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.CheckInMenu.Name = "CheckInMenu";
            this.CheckInMenu.Size = new System.Drawing.Size(122, 67);
            this.CheckInMenu.Text = "Check In";
            this.CheckInMenu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // CheckOutMenu
            // 
            this.CheckOutMenu.AutoSize = false;
            this.CheckOutMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.frmCheckOut});
            this.CheckOutMenu.IconChar = FontAwesome.Sharp.IconChar.DoorClosed;
            this.CheckOutMenu.IconColor = System.Drawing.Color.Black;
            this.CheckOutMenu.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.CheckOutMenu.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.CheckOutMenu.Name = "CheckOutMenu";
            this.CheckOutMenu.Size = new System.Drawing.Size(93, 67);
            this.CheckOutMenu.Text = "Check Out";
            this.CheckOutMenu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // SalirMenu
            // 
            this.SalirMenu.AutoSize = false;
            this.SalirMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Salir});
            this.SalirMenu.IconChar = FontAwesome.Sharp.IconChar.Running;
            this.SalirMenu.IconColor = System.Drawing.Color.Black;
            this.SalirMenu.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.SalirMenu.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.SalirMenu.Name = "SalirMenu";
            this.SalirMenu.Size = new System.Drawing.Size(122, 67);
            this.SalirMenu.Text = "Salir";
            this.SalirMenu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.BackColor = System.Drawing.Color.Salmon;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menuStrip1.Size = new System.Drawing.Size(1182, 77);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Salmon;
            this.label1.Font = new System.Drawing.Font("Franklin Gothic Demi", 19F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Snow;
            this.label1.Location = new System.Drawing.Point(59, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 33);
            this.label1.TabIndex = 2;
            this.label1.Text = "Control de Hoteles";
            // 
            // frmReservacion
            // 
            this.frmReservacion.IconChar = FontAwesome.Sharp.IconChar.None;
            this.frmReservacion.IconColor = System.Drawing.Color.Black;
            this.frmReservacion.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.frmReservacion.Name = "frmReservacion";
            this.frmReservacion.Size = new System.Drawing.Size(181, 22);
            this.frmReservacion.Text = "Generar Reservacion";
            this.frmReservacion.Click += new System.EventHandler(this.iconMenuItem1_Click);
            // 
            // frmCheckIn
            // 
            this.frmCheckIn.IconChar = FontAwesome.Sharp.IconChar.None;
            this.frmCheckIn.IconColor = System.Drawing.Color.Black;
            this.frmCheckIn.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.frmCheckIn.Name = "frmCheckIn";
            this.frmCheckIn.Size = new System.Drawing.Size(180, 22);
            this.frmCheckIn.Text = "Realizar Check In";
            this.frmCheckIn.Click += new System.EventHandler(this.iconMenuItem2_Click);
            // 
            // frmCheckOut
            // 
            this.frmCheckOut.IconChar = FontAwesome.Sharp.IconChar.None;
            this.frmCheckOut.IconColor = System.Drawing.Color.Black;
            this.frmCheckOut.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.frmCheckOut.Name = "frmCheckOut";
            this.frmCheckOut.Size = new System.Drawing.Size(180, 22);
            this.frmCheckOut.Text = "Realizar Check out";
            this.frmCheckOut.Click += new System.EventHandler(this.iconMenuItem3_Click);
            // 
            // Salir
            // 
            this.Salir.IconChar = FontAwesome.Sharp.IconChar.None;
            this.Salir.IconColor = System.Drawing.Color.Black;
            this.Salir.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Salir.Name = "Salir";
            this.Salir.Size = new System.Drawing.Size(180, 22);
            this.Salir.Text = "Salir del Sistema";
            this.Salir.Click += new System.EventHandler(this.iconMenuItem4_Click);
            // 
            // HomeOperativo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MistyRose;
            this.ClientSize = new System.Drawing.Size(1182, 652);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuTitutlo);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuTitutlo;
            this.Name = "HomeOperativo";
            this.Text = "HomeOperativo";
            this.Load += new System.EventHandler(this.HomeOperativo_Load);
            this.menuTitutlo.ResumeLayout(false);
            this.menuTitutlo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuTitutlo;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Label label1;
        private FontAwesome.Sharp.IconMenuItem ReservacionesMenu;
        private FontAwesome.Sharp.IconMenuItem CheckInMenu;
        private FontAwesome.Sharp.IconMenuItem CheckOutMenu;
        private FontAwesome.Sharp.IconMenuItem SalirMenu;
        private FontAwesome.Sharp.IconMenuItem frmReservacion;
        private FontAwesome.Sharp.IconMenuItem frmCheckIn;
        private FontAwesome.Sharp.IconMenuItem frmCheckOut;
        private FontAwesome.Sharp.IconMenuItem Salir;
    }
}