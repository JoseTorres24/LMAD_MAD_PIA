using ClasesData;
using ClasesData.BD;
using System;
using System.Windows.Forms;

namespace INICIO_Forms.ADMINISTRATIVO
{
    public partial class RegistroHabitacion: Form
    {
        public RegistroHabitacion()
        {
            InitializeComponent();
        }

        private void RegistroHabitacion_Load(object sender, EventArgs e)
        {
            BD_HotelesHabitacionesServicios.CargarHotelesEnListBox(listHotelesRegistados);
            BD_HotelesHabitacionesServicios.CargarHabitacionesEnListBox(listHabitacionesListas);

            //LISTO PARA LA CARGA
            // Poblar comboTipoCamas
            comboTipoCamas.Items.Clear();
            comboTipoCamas.Items.Add("Cama sencilla");
            comboTipoCamas.Items.Add("Cama doble");
            comboTipoCamas.Items.Add("Cama matrimonial");

            // Poblar comboHabitacionVista
            comboHabitacionVista.Items.Clear();
            comboHabitacionVista.Items.Add("Frente al mar");
            comboHabitacionVista.Items.Add("No frente al mar");

            // Poblar comboHabitacionNivel
            comboHabitacionNivel.Items.Clear();
            comboHabitacionNivel.Items.Add("Sencilla");
            comboHabitacionNivel.Items.Add("Lujo");
            comboHabitacionNivel.Items.Add("Suite");

            // Opcional: Seleccionar el primer elemento por defecto
            comboTipoCamas.SelectedIndex = 0;
            comboHabitacionVista.SelectedIndex = 0;
            comboHabitacionNivel.SelectedIndex = 0;


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
            // Verificar que haya un hotel seleccionado en la lista
            if (listHotelesRegistados.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un hotel de la lista antes de agregar habitaciones o servicios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener el ID del hotel seleccionado
            int idHotelSeleccionado = ObtenerIDHotelSeleccionado();

            if (idHotelSeleccionado <= 0)
            {
                MessageBox.Show("Error al obtener el hotel seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Si se seleccionó un hotel, proceder con la inserción de habitaciones
            foreach (var habitacion in comboHabitacion.Items)
            {
                int numeroHabitacion = int.Parse(habitacion.ToString().Replace("Habitación ", ""));
                int nivelPiso = int.Parse(comboPiso.SelectedItem.ToString().Replace("Piso ", ""));

                // Validación para evitar números negativos
                if (numeroHabitacion <= 0 || nivelPiso <= 0)
                {
                    MessageBox.Show("Los números de habitación y nivel de piso deben ser mayores a 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Habitaciones nuevaHabitacion = new Habitaciones
                {
                    ID_Hotel = idHotelSeleccionado,
                    NumeroHabitacion = numeroHabitacion,
                    PisoHabitacion = nivelPiso,

                    NivelHabitacion = comboHabitacionNivel.SelectedItem.ToString(),
                    VistaHabitacion = comboHabitacionVista.SelectedItem.ToString(),
                    NumeroCamas = (int)numericCamas.Value,
                    Capacidad = (int)numericHabitacionGente.Value,
                    Estado = "Disponible"
                };

                BD_HotelesHabitacionesServicios.GuardarHabitacion(nuevaHabitacion);
                BD_HotelesHabitacionesServicios.CargarHabitacionesEnListBox(listHabitacionesListas);
            }
        }

        private void listHotelesRegistrados_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listHotelesRegistados.SelectedItem == null)
                return;

            int idHotelSeleccionado = ObtenerIDHotelSeleccionado();

            ClasesData.Hoteles hotel = BD_HotelesHabitacionesServicios.ObtenerHotelPorID(idHotelSeleccionado);

            if (hotel != null)
            {
                // Poblar ComboBox de Pisos
                comboPiso.Items.Clear();
                for (int i = 1; i <= hotel.NumeroPisos; i++)
                {
                    comboPiso.Items.Add($"Piso {i}");
                }

                // Poblar ComboBox de Habitaciones
                comboHabitacion.Items.Clear();
                for (int i = 1; i <= hotel.NumeroHabitaciones; i++)
                {
                    comboHabitacion.Items.Add($"Habitación {i}");
                }
            }
        }
      
        //boton de modificar

        private void listHabitacionesListas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
