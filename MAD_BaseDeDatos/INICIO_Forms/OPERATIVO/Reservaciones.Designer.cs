﻿namespace INICIO_Forms.OPERATIVO
{
    partial class Reservaciones
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
            this.comboReservacion = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonGuardarReservacion = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textCliente = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboCiudades = new System.Windows.Forms.ComboBox();
            this.listHoteles = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboHabitacionNivel = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.checkedListBoxHabitaciones = new System.Windows.Forms.CheckedListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboHabitacionVista = new System.Windows.Forms.ComboBox();
            this.numericCamas = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.numericPersonas = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.numericNoches = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.dateCheckIn = new System.Windows.Forms.DateTimePicker();
            this.textAnticipo = new System.Windows.Forms.TextBox();
            this.LabelDeCosto = new System.Windows.Forms.Label();
            this.Anticipo = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.dateReservacion = new System.Windows.Forms.DateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericCamas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPersonas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericNoches)).BeginInit();
            this.SuspendLayout();
            // 
            // comboReservacion
            // 
            this.comboReservacion.FormattingEnabled = true;
            this.comboReservacion.Location = new System.Drawing.Point(55, 72);
            this.comboReservacion.Name = "comboReservacion";
            this.comboReservacion.Size = new System.Drawing.Size(274, 21);
            this.comboReservacion.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(51, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Busqueda por RFC ";
            this.label1.UseWaitCursor = true;
            // 
            // buttonGuardarReservacion
            // 
            this.buttonGuardarReservacion.BackColor = System.Drawing.Color.LightGreen;
            this.buttonGuardarReservacion.Location = new System.Drawing.Point(833, 573);
            this.buttonGuardarReservacion.Name = "buttonGuardarReservacion";
            this.buttonGuardarReservacion.Size = new System.Drawing.Size(162, 65);
            this.buttonGuardarReservacion.TabIndex = 6;
            this.buttonGuardarReservacion.Text = "Guardar Reservacion";
            this.buttonGuardarReservacion.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(51, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(170, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Nombre del Cliente";
            this.label2.UseWaitCursor = true;
            // 
            // textCliente
            // 
            this.textCliente.Enabled = false;
            this.textCliente.Location = new System.Drawing.Point(55, 130);
            this.textCliente.Multiline = true;
            this.textCliente.Name = "textCliente";
            this.textCliente.Size = new System.Drawing.Size(274, 27);
            this.textCliente.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(51, 175);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "Seleccione Ciudad";
            this.label3.UseWaitCursor = true;
            // 
            // comboCiudades
            // 
            this.comboCiudades.FormattingEnabled = true;
            this.comboCiudades.Location = new System.Drawing.Point(55, 198);
            this.comboCiudades.Name = "comboCiudades";
            this.comboCiudades.Size = new System.Drawing.Size(274, 21);
            this.comboCiudades.TabIndex = 9;
            // 
            // listHoteles
            // 
            this.listHoteles.FormattingEnabled = true;
            this.listHoteles.Location = new System.Drawing.Point(44, 257);
            this.listHoteles.Name = "listHoteles";
            this.listHoteles.ScrollAlwaysVisible = true;
            this.listHoteles.Size = new System.Drawing.Size(350, 381);
            this.listHoteles.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(28, 234);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 20);
            this.label4.TabIndex = 12;
            this.label4.Text = "Seleccione Hotel";
            this.label4.UseWaitCursor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(12, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(235, 25);
            this.label5.TabIndex = 13;
            this.label5.Text = "Generar Reservacion";
            this.label5.UseWaitCursor = true;
            // 
            // comboHabitacionNivel
            // 
            this.comboHabitacionNivel.FormattingEnabled = true;
            this.comboHabitacionNivel.Location = new System.Drawing.Point(474, 39);
            this.comboHabitacionNivel.Name = "comboHabitacionNivel";
            this.comboHabitacionNivel.Size = new System.Drawing.Size(274, 21);
            this.comboHabitacionNivel.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(470, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(271, 20);
            this.label6.TabIndex = 15;
            this.label6.Text = "Seleccione Nivel de Habitacion";
            this.label6.UseWaitCursor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(826, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(175, 20);
            this.label7.TabIndex = 16;
            this.label7.Text = "Cantidad de noches";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // checkedListBoxHabitaciones
            // 
            this.checkedListBoxHabitaciones.FormattingEnabled = true;
            this.checkedListBoxHabitaciones.Location = new System.Drawing.Point(474, 322);
            this.checkedListBoxHabitaciones.Name = "checkedListBoxHabitaciones";
            this.checkedListBoxHabitaciones.Size = new System.Drawing.Size(274, 334);
            this.checkedListBoxHabitaciones.TabIndex = 20;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(470, 82);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(293, 20);
            this.label8.TabIndex = 21;
            this.label8.Text = "Seleccione Vista de la Habitacion";
            this.label8.UseWaitCursor = true;
            // 
            // comboHabitacionVista
            // 
            this.comboHabitacionVista.FormattingEnabled = true;
            this.comboHabitacionVista.Location = new System.Drawing.Point(474, 118);
            this.comboHabitacionVista.Name = "comboHabitacionVista";
            this.comboHabitacionVista.Size = new System.Drawing.Size(274, 21);
            this.comboHabitacionVista.TabIndex = 22;
            // 
            // numericCamas
            // 
            this.numericCamas.Location = new System.Drawing.Point(490, 197);
            this.numericCamas.Name = "numericCamas";
            this.numericCamas.Size = new System.Drawing.Size(120, 20);
            this.numericCamas.TabIndex = 23;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(470, 163);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(170, 20);
            this.label9.TabIndex = 24;
            this.label9.Text = "Cantidad de camas";
            this.label9.UseWaitCursor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(474, 258);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(274, 21);
            this.comboBox1.TabIndex = 26;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(486, 229);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(135, 20);
            this.label10.TabIndex = 25;
            this.label10.Text = "Tipo de Camas";
            this.label10.UseWaitCursor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(820, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(323, 20);
            this.label11.TabIndex = 28;
            this.label11.Text = "Cantidad de Personas por Habitacion";
            this.label11.UseWaitCursor = true;
            // 
            // numericPersonas
            // 
            this.numericPersonas.Location = new System.Drawing.Point(830, 52);
            this.numericPersonas.Name = "numericPersonas";
            this.numericPersonas.Size = new System.Drawing.Size(120, 20);
            this.numericPersonas.TabIndex = 27;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(835, 344);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(106, 20);
            this.label12.TabIndex = 29;
            this.label12.Text = "Costo Total";
            // 
            // numericNoches
            // 
            this.numericNoches.Location = new System.Drawing.Point(830, 131);
            this.numericNoches.Name = "numericNoches";
            this.numericNoches.Size = new System.Drawing.Size(120, 20);
            this.numericNoches.TabIndex = 30;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold);
            this.label13.Location = new System.Drawing.Point(829, 175);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(146, 20);
            this.label13.TabIndex = 31;
            this.label13.Text = "Dia de Check IN";
            // 
            // dateCheckIn
            // 
            this.dateCheckIn.Location = new System.Drawing.Point(824, 211);
            this.dateCheckIn.Name = "dateCheckIn";
            this.dateCheckIn.Size = new System.Drawing.Size(200, 20);
            this.dateCheckIn.TabIndex = 32;
            // 
            // textAnticipo
            // 
            this.textAnticipo.Location = new System.Drawing.Point(839, 484);
            this.textAnticipo.Multiline = true;
            this.textAnticipo.Name = "textAnticipo";
            this.textAnticipo.Size = new System.Drawing.Size(230, 44);
            this.textAnticipo.TabIndex = 33;
            // 
            // LabelDeCosto
            // 
            this.LabelDeCosto.AutoSize = true;
            this.LabelDeCosto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold);
            this.LabelDeCosto.Location = new System.Drawing.Point(835, 379);
            this.LabelDeCosto.Name = "LabelDeCosto";
            this.LabelDeCosto.Size = new System.Drawing.Size(93, 20);
            this.LabelDeCosto.TabIndex = 34;
            this.LabelDeCosto.Text = "Resultado";
            // 
            // Anticipo
            // 
            this.Anticipo.AutoSize = true;
            this.Anticipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold);
            this.Anticipo.Location = new System.Drawing.Point(835, 448);
            this.Anticipo.Name = "Anticipo";
            this.Anticipo.Size = new System.Drawing.Size(77, 20);
            this.Anticipo.TabIndex = 35;
            this.Anticipo.Text = "Anticipo";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold);
            this.label14.Location = new System.Drawing.Point(470, 290);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(217, 20);
            this.label14.TabIndex = 36;
            this.label14.Text = "Seleccione Habitaciones";
            this.label14.UseWaitCursor = true;
            // 
            // dateReservacion
            // 
            this.dateReservacion.Location = new System.Drawing.Point(824, 291);
            this.dateReservacion.Name = "dateReservacion";
            this.dateReservacion.Size = new System.Drawing.Size(200, 20);
            this.dateReservacion.TabIndex = 38;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold);
            this.label15.Location = new System.Drawing.Point(829, 257);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(174, 20);
            this.label15.TabIndex = 37;
            this.label15.Text = "Dia de Reservacion";
            // 
            // Reservaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MistyRose;
            this.ClientSize = new System.Drawing.Size(1167, 714);
            this.Controls.Add(this.dateReservacion);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.Anticipo);
            this.Controls.Add(this.LabelDeCosto);
            this.Controls.Add(this.textAnticipo);
            this.Controls.Add(this.dateCheckIn);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.numericNoches);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.numericPersonas);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.numericCamas);
            this.Controls.Add(this.comboHabitacionVista);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.checkedListBoxHabitaciones);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboHabitacionNivel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listHoteles);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboCiudades);
            this.Controls.Add(this.textCliente);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonGuardarReservacion);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboReservacion);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Reservaciones";
            this.Text = "Reservaciones";
            this.Load += new System.EventHandler(this.Reservaciones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericCamas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPersonas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericNoches)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboReservacion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonGuardarReservacion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textCliente;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboCiudades;
        private System.Windows.Forms.ListBox listHoteles;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboHabitacionNivel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckedListBox checkedListBoxHabitaciones;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboHabitacionVista;
        private System.Windows.Forms.NumericUpDown numericCamas;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown numericPersonas;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown numericNoches;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DateTimePicker dateCheckIn;
        private System.Windows.Forms.TextBox textAnticipo;
        private System.Windows.Forms.Label LabelDeCosto;
        private System.Windows.Forms.Label Anticipo;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DateTimePicker dateReservacion;
        private System.Windows.Forms.Label label15;
    }
}