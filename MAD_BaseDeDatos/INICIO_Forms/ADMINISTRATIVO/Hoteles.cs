using ClasesData.BD;
using ClasesData;
using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace INICIO_Forms.ADMINISTRATIVO
{
    public partial class Hoteles: Form
    {
        public Hoteles()
        {
            InitializeComponent();
        }

        private void Hoteles_Load(object sender, EventArgs e)
        {
            //Deberia de cargarlos?
            BD_HotelesHabitacionesServicios.CargarHotelesEnListBox(listHotelesRegistados);
        }


        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            
            // Verificar que los valores no sean negativos
            if (numericPisos.Value <= 0)
            {
                MessageBox.Show("El número de pisos debe ser mayor a 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (numericHabitaciones.Value <= 0)
            {
                MessageBox.Show("El número de habitaciones debe ser mayor a 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Guardar hotel
            ClasesData.Hoteles nuevoHotel = new ClasesData.Hoteles
            {
                NombreHotel = textnombreHotel.Text,
                Pais = textPais.Text,
                Ciudad = textCiudad.Text,
                Domicilio = textDomicilio.Text,
                NumeroPisos = (int)numericPisos.Value,
                NumeroHabitaciones = (int)numericHabitaciones.Value,
                FechaInicioOperaciones = dateOperacion.Value,
                UsuarioRegistro = Sesion.ID_Usuario
            };

            if (BD_HotelesHabitacionesServicios.HotelExiste(nuevoHotel))
            {
                MessageBox.Show("Ya existe un hotel con el mismo nombre, país, ciudad y dirección.", "Registro duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BD_HotelesHabitacionesServicios.GuardarHotel(nuevoHotel);

            BD_HotelesHabitacionesServicios.CargarHotelesEnListBox(listHotelesRegistados);
            // Si se guardó correctamente, guardar habitaciones asociadas
            
        }
    }
}
