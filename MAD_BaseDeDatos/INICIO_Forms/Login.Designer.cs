using System.Drawing;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace INICIO_Forms
{
    partial class Login
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textCorreo = new System.Windows.Forms.TextBox();
            this.textContraseña = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CrearCuentaAdmin_btn = new FontAwesome.Sharp.IconButton();
            this.Salir_btn = new FontAwesome.Sharp.IconButton();
            this.IniciarSesion_btn = new FontAwesome.Sharp.IconButton();
            this.iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(207, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(270, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Bienvenido A Sistema Gestion ";
            this.label1.UseWaitCursor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(273, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Hoteles TUotaku";
            // 
            // textCorreo
            // 
            this.textCorreo.Location = new System.Drawing.Point(211, 104);
            this.textCorreo.Multiline = true;
            this.textCorreo.Name = "textCorreo";
            this.textCorreo.Size = new System.Drawing.Size(280, 25);
            this.textCorreo.TabIndex = 6;
            // 
            // textContraseña
            // 
            this.textContraseña.Location = new System.Drawing.Point(211, 151);
            this.textContraseña.Multiline = true;
            this.textContraseña.Name = "textContraseña";
            this.textContraseña.PasswordChar = '*';
            this.textContraseña.Size = new System.Drawing.Size(280, 27);
            this.textContraseña.TabIndex = 7;
            this.textContraseña.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(221, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Contraseña";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(221, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "Correo Electronico";
            // 
            // CrearCuentaAdmin_btn
            // 
            this.CrearCuentaAdmin_btn.BackColor = System.Drawing.Color.Plum;
            this.CrearCuentaAdmin_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.549999F, System.Drawing.FontStyle.Bold);
            this.CrearCuentaAdmin_btn.ForeColor = System.Drawing.Color.Black;
            this.CrearCuentaAdmin_btn.IconChar = FontAwesome.Sharp.IconChar.UserAlt;
            this.CrearCuentaAdmin_btn.IconColor = System.Drawing.Color.Black;
            this.CrearCuentaAdmin_btn.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.CrearCuentaAdmin_btn.IconSize = 35;
            this.CrearCuentaAdmin_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CrearCuentaAdmin_btn.Location = new System.Drawing.Point(239, 286);
            this.CrearCuentaAdmin_btn.Name = "CrearCuentaAdmin_btn";
            this.CrearCuentaAdmin_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.CrearCuentaAdmin_btn.Size = new System.Drawing.Size(151, 47);
            this.CrearCuentaAdmin_btn.TabIndex = 12;
            this.CrearCuentaAdmin_btn.Text = "Crear Cuenta";
            this.CrearCuentaAdmin_btn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CrearCuentaAdmin_btn.UseVisualStyleBackColor = false;
            this.CrearCuentaAdmin_btn.Click += new System.EventHandler(this.CrearCuentaAdmin_btn_Click);
            // 
            // Salir_btn
            // 
            this.Salir_btn.BackColor = System.Drawing.Color.OrangeRed;
            this.Salir_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.549999F, System.Drawing.FontStyle.Bold);
            this.Salir_btn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Salir_btn.IconChar = FontAwesome.Sharp.IconChar.DoorClosed;
            this.Salir_btn.IconColor = System.Drawing.Color.Black;
            this.Salir_btn.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Salir_btn.IconSize = 35;
            this.Salir_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Salir_btn.Location = new System.Drawing.Point(339, 217);
            this.Salir_btn.Name = "Salir_btn";
            this.Salir_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Salir_btn.Size = new System.Drawing.Size(141, 51);
            this.Salir_btn.TabIndex = 11;
            this.Salir_btn.Text = "Salir";
            this.Salir_btn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Salir_btn.UseVisualStyleBackColor = false;
            // 
            // IniciarSesion_btn
            // 
            this.IniciarSesion_btn.BackColor = System.Drawing.Color.LightSkyBlue;
            this.IniciarSesion_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.549999F, System.Drawing.FontStyle.Bold);
            this.IniciarSesion_btn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.IniciarSesion_btn.IconChar = FontAwesome.Sharp.IconChar.Key;
            this.IniciarSesion_btn.IconColor = System.Drawing.Color.Black;
            this.IniciarSesion_btn.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.IniciarSesion_btn.IconSize = 35;
            this.IniciarSesion_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.IniciarSesion_btn.Location = new System.Drawing.Point(130, 217);
            this.IniciarSesion_btn.Name = "IniciarSesion_btn";
            this.IniciarSesion_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.IniciarSesion_btn.Size = new System.Drawing.Size(166, 51);
            this.IniciarSesion_btn.TabIndex = 10;
            this.IniciarSesion_btn.Text = "Iniciar Sesion";
            this.IniciarSesion_btn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.IniciarSesion_btn.UseVisualStyleBackColor = false;
            this.IniciarSesion_btn.Click += new System.EventHandler(this.iconButton1_Click);
            // 
            // iconPictureBox1
            // 
            this.iconPictureBox1.BackColor = System.Drawing.Color.MistyRose;
            this.iconPictureBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconPictureBox1.IconColor = System.Drawing.SystemColors.ControlText;
            this.iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconPictureBox1.IconSize = 164;
            this.iconPictureBox1.ImageLocation = "https://res.cloudinary.com/jnto/image/upload/w_240,h_210,c_fill,f_auto,fl_lossy,q" +
    "_60/v1/media/filer_public/eb/b5/ebb57bef-2c33-4457-a52f-d9226ff91e84/pokemon-roo" +
    "m-apartment-hotel-mimaru_z14739";
            this.iconPictureBox1.InitialImage = global::INICIO_Forms.Properties.Resources.FotoInicio;
            this.iconPictureBox1.Location = new System.Drawing.Point(12, 32);
            this.iconPictureBox1.Name = "iconPictureBox1";
            this.iconPictureBox1.Size = new System.Drawing.Size(177, 164);
            this.iconPictureBox1.TabIndex = 13;
            this.iconPictureBox1.TabStop = false;
            this.iconPictureBox1.WaitOnLoad = true;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MistyRose;
            this.ClientSize = new System.Drawing.Size(559, 364);
            this.Controls.Add(this.iconPictureBox1);
            this.Controls.Add(this.CrearCuentaAdmin_btn);
            this.Controls.Add(this.Salir_btn);
            this.Controls.Add(this.IniciarSesion_btn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textContraseña);
            this.Controls.Add(this.textCorreo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.Name = "Login";
            this.Text = "Inicio Sesion";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textCorreo;
        private System.Windows.Forms.TextBox textContraseña;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private FontAwesome.Sharp.IconButton IniciarSesion_btn;
        private FontAwesome.Sharp.IconButton Salir_btn;
        private FontAwesome.Sharp.IconButton CrearCuentaAdmin_btn;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
    }
}

