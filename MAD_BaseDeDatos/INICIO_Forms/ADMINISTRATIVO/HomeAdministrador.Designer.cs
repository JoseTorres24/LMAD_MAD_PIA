namespace INICIO_Forms.ADMINISTRATIVO
{
    partial class HomeAdministrador
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
            this.menuAdmin = new System.Windows.Forms.MenuStrip();
            this.menuAdminTitulo = new System.Windows.Forms.MenuStrip();
            this.label1 = new System.Windows.Forms.Label();
            this.Hoteles = new FontAwesome.Sharp.IconMenuItem();
            this.ProcesoHotel = new System.Windows.Forms.ToolStripMenuItem();
            this.RegistroUsuario = new FontAwesome.Sharp.IconMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.ReporteHotel = new FontAwesome.Sharp.IconMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.ReporteClientes = new FontAwesome.Sharp.IconMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.ReporteDeVentas = new FontAwesome.Sharp.IconMenuItem();
            this.VentasReporte = new System.Windows.Forms.ToolStripMenuItem();
            this.SalirMenu = new FontAwesome.Sharp.IconMenuItem();
            this.Salir = new FontAwesome.Sharp.IconMenuItem();
            this.menuAdmin.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuAdmin
            // 
            this.menuAdmin.BackColor = System.Drawing.Color.LightBlue;
            this.menuAdmin.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Hoteles,
            this.RegistroUsuario,
            this.ReporteHotel,
            this.ReporteClientes,
            this.ReporteDeVentas,
            this.SalirMenu});
            this.menuAdmin.Location = new System.Drawing.Point(0, 88);
            this.menuAdmin.Name = "menuAdmin";
            this.menuAdmin.Size = new System.Drawing.Size(861, 71);
            this.menuAdmin.TabIndex = 0;
            this.menuAdmin.Text = "menuStrip1";
            // 
            // menuAdminTitulo
            // 
            this.menuAdminTitulo.AutoSize = false;
            this.menuAdminTitulo.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.menuAdminTitulo.Location = new System.Drawing.Point(0, 0);
            this.menuAdminTitulo.Name = "menuAdminTitulo";
            this.menuAdminTitulo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menuAdminTitulo.Size = new System.Drawing.Size(861, 88);
            this.menuAdminTitulo.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.label1.Font = new System.Drawing.Font("Franklin Gothic Demi", 19F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(34, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 33);
            this.label1.TabIndex = 3;
            this.label1.Text = "Control de Hoteles";
            // 
            // Hoteles
            // 
            this.Hoteles.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProcesoHotel});
            this.Hoteles.IconChar = FontAwesome.Sharp.IconChar.Building;
            this.Hoteles.IconColor = System.Drawing.Color.Black;
            this.Hoteles.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Hoteles.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Hoteles.Name = "Hoteles";
            this.Hoteles.Size = new System.Drawing.Size(60, 67);
            this.Hoteles.Text = "Hoteles";
            this.Hoteles.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // ProcesoHotel
            // 
            this.ProcesoHotel.Name = "ProcesoHotel";
            this.ProcesoHotel.Size = new System.Drawing.Size(175, 22);
            this.ProcesoHotel.Text = "Proceso de Hoteles";
            this.ProcesoHotel.Click += new System.EventHandler(this.ProcesoHotel_Click);
            // 
            // RegistroUsuario
            // 
            this.RegistroUsuario.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3});
            this.RegistroUsuario.IconChar = FontAwesome.Sharp.IconChar.UserGear;
            this.RegistroUsuario.IconColor = System.Drawing.Color.Black;
            this.RegistroUsuario.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.RegistroUsuario.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.RegistroUsuario.Name = "RegistroUsuario";
            this.RegistroUsuario.Size = new System.Drawing.Size(176, 67);
            this.RegistroUsuario.Text = "Registro de Usuario Operativo";
            this.RegistroUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.RegistroUsuario.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.RegistroUsuario.Click += new System.EventHandler(this.RegistroUsuario_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(200, 22);
            this.toolStripMenuItem3.Text = "Registro de Usuario Ops";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // ReporteHotel
            // 
            this.ReporteHotel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem4});
            this.ReporteHotel.IconChar = FontAwesome.Sharp.IconChar.BuildingWheat;
            this.ReporteHotel.IconColor = System.Drawing.Color.Black;
            this.ReporteHotel.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.ReporteHotel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ReporteHotel.Name = "ReporteHotel";
            this.ReporteHotel.Size = new System.Drawing.Size(120, 67);
            this.ReporteHotel.Text = "Reporte De Hoteles";
            this.ReporteHotel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ReporteHotel.Click += new System.EventHandler(this.ReporteHotel_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem4.Text = "Reporte de Hoteles";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // ReporteClientes
            // 
            this.ReporteClientes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem5});
            this.ReporteClientes.IconChar = FontAwesome.Sharp.IconChar.UserGroup;
            this.ReporteClientes.IconColor = System.Drawing.Color.Black;
            this.ReporteClientes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.ReporteClientes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ReporteClientes.Name = "ReporteClientes";
            this.ReporteClientes.Size = new System.Drawing.Size(119, 67);
            this.ReporteClientes.Text = "Reporte de clientes";
            this.ReporteClientes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ReporteClientes.Click += new System.EventHandler(this.ReporteClientes_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem5.Text = "Reporte de clientes";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // ReporteDeVentas
            // 
            this.ReporteDeVentas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.VentasReporte});
            this.ReporteDeVentas.IconChar = FontAwesome.Sharp.IconChar.Road;
            this.ReporteDeVentas.IconColor = System.Drawing.Color.Black;
            this.ReporteDeVentas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.ReporteDeVentas.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ReporteDeVentas.Name = "ReporteDeVentas";
            this.ReporteDeVentas.Size = new System.Drawing.Size(113, 67);
            this.ReporteDeVentas.Text = "Reporte de Ventas";
            this.ReporteDeVentas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // VentasReporte
            // 
            this.VentasReporte.Name = "VentasReporte";
            this.VentasReporte.Size = new System.Drawing.Size(180, 22);
            this.VentasReporte.Text = "Reporte de Ventas";
            this.VentasReporte.Click += new System.EventHandler(this.VentasReporte_Click);
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
            this.SalirMenu.Click += new System.EventHandler(this.SalirMenu_Click);
            // 
            // Salir
            // 
            this.Salir.IconChar = FontAwesome.Sharp.IconChar.None;
            this.Salir.IconColor = System.Drawing.Color.Black;
            this.Salir.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Salir.Name = "Salir";
            this.Salir.Size = new System.Drawing.Size(159, 22);
            this.Salir.Text = "Salir del Sistema";
            this.Salir.Click += new System.EventHandler(this.Salir_Click);
            // 
            // HomeAdministrador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 572);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuAdmin);
            this.Controls.Add(this.menuAdminTitulo);
            this.MainMenuStrip = this.menuAdmin;
            this.Name = "HomeAdministrador";
            this.Text = "HomeAdministrador";
            this.Load += new System.EventHandler(this.HomeAdministrador_Load);
            this.menuAdmin.ResumeLayout(false);
            this.menuAdmin.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuAdmin;
        private System.Windows.Forms.MenuStrip menuAdminTitulo;
        private FontAwesome.Sharp.IconMenuItem Hoteles;
        private FontAwesome.Sharp.IconMenuItem RegistroUsuario;
        private FontAwesome.Sharp.IconMenuItem ReporteHotel;
        private FontAwesome.Sharp.IconMenuItem ReporteClientes;
        private System.Windows.Forms.Label label1;
        private FontAwesome.Sharp.IconMenuItem SalirMenu;
        private FontAwesome.Sharp.IconMenuItem Salir;
        private System.Windows.Forms.ToolStripMenuItem ProcesoHotel;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private FontAwesome.Sharp.IconMenuItem ReporteDeVentas;
        private System.Windows.Forms.ToolStripMenuItem VentasReporte;
    }
}