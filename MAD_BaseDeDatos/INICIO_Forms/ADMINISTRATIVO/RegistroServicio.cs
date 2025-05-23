﻿using ClasesData;
using ClasesData.BD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace INICIO_Forms.ADMINISTRATIVO
{
    public partial class RegistroServicio: Form
    {
        public RegistroServicio()
        {
            InitializeComponent();
        }

        private void RegistroServicio_Load(object sender, EventArgs e)
        {
            BD_HotelesHabitacionesServicios.CargarHotelesEnListBox(listHotelesRegistados);
            

        }
        private int ObtenerIDHotelSeleccionado()
        {
            if (listHotelesRegistados.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un hotel de la lista.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }

            // Convertir la selección a cadena y asegurarse de que no es null ni vacía
            string seleccionado = listHotelesRegistados.SelectedItem.ToString().Trim();

            if (string.IsNullOrEmpty(seleccionado))
            {
                MessageBox.Show("El hotel seleccionado no contiene información válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }

            // Obtener el nombre del hotel y consultar la base de datos
            string nombreHotelSeleccionado = seleccionado;
            int idHotel = BD_HotelesHabitacionesServicios.ObtenerIDHotelPorNombre(nombreHotelSeleccionado);

            return idHotel;
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            int idHotelSeleccionado = ObtenerIDHotelSeleccionado();
            if (idHotelSeleccionado <= 0)
            {
                MessageBox.Show("Debe seleccionar un hotel antes de registrar servicios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(textServicio.Text))
            {
                MessageBox.Show("El nombre del servicio no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(textCosto.Text, out int costoServicio))
            {
                MessageBox.Show("El costo del servicio debe ser un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Servicios nuevoServicio = new Servicios
            {
                NombreServicio = textServicio.Text,
                Costo = costoServicio
            };

            int idServicio = BD_HotelesHabitacionesServicios.GuardarServicio(nuevoServicio);
            if (idServicio <= 0)
            {
                MessageBox.Show("Error al guardar el servicio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            HotelServicios relacion = new HotelServicios
            {
                ID_Hotel = idHotelSeleccionado,
                ID_Servicio = idServicio
            };

            BD_HotelesHabitacionesServicios.AsociarServicioAHotel(relacion);

            BD_HotelesHabitacionesServicios.CargarServiciosEnListBox(listServicios);
            MessageBox.Show("Servicio registrado y asociado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //No sirve para nada
        private void listServicios_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listHotelesRegistados_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listHotelesRegistados.SelectedItem == null)
            {
                return;
            }
            else
            {
                BD_HotelesHabitacionesServicios.CargarServiciosEnListBox(listServicios);
            }
        }
    }
}
