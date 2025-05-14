using ClasesData;
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

namespace INICIO_Forms.OPERATIVO
{
    public partial class Reservaciones : Form
    {
        //VARIABLES
        private List<Servicios> serviciosDisponibles = new List<Servicios>();
        private List<Habitaciones> habitacionesDisponibles = new List<Habitaciones>();
        private decimal costoTotal = 0;
        private decimal costoHabitacion = 0;
        private decimal costoServicios = 0;


        private HomeOperativo homeOperativo;
        public Reservaciones(HomeOperativo homeOperativo)
        {
            InitializeComponent();
            this.homeOperativo = homeOperativo;
            this.FormClosed += new FormClosedEventHandler(Reservaciones_FormClosed);

        }
        // Cuando el formulario se cierre, mostramos HomeOperativo
        private void Reservaciones_FormClosed(object sender, FormClosedEventArgs e)
        {
            homeOperativo.Show();
        }

        private void Reservaciones_Load(object sender, EventArgs e)
        {
            CargarDatosIniciales();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        // Ademas se tiene que asignar el guid 
        private void buttonGuardarReservacion_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (checkedListBoxHabitaciones.CheckedItems.Count == 0)
            {
                MessageBox.Show("Selecciona una habitación", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(textAnticipo.Text, out decimal anticipo) || anticipo < (costoTotal * 0.1m))
            {
                MessageBox.Show("Ingresa un anticipo válido (mínimo 10%)", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Crear reservación
            var reservacion = new Reservacion
            {
                CodigoReservacion = Guid.NewGuid().ToString(),
                RFC_Cliente = ((Cliente)comboReservacion.SelectedItem).RFC,
                ID_Hotel = ((Hoteles)listHoteles.SelectedItem).ID_Hotel,
                FechaInicio = dateCheckIn.Value,
                FechaFin = dateCheckOut.Value,
                Anticipo = (float)anticipo,
                UsuarioRegistro = Sesion.ID_Usuario // Asume que tienes una clase con el usuario actual
            };

            // Guardar reservación
            if (BD_Reservacion.CrearReservacion(reservacion))
            {
                // Guardar servicios seleccionados
                foreach (int index in checkedListServicios.CheckedIndices)
                {
                    var servicioReservacion = new ServiciosReservacion
                    {
                        ID_Reservacion = reservacion.CodigoReservacion,
                        ID_Servicio = serviciosDisponibles[index].ID_Servicio
                    };
                    BD_Reservacion.AgregarServicioReservacion(servicioReservacion);
                }

                MessageBox.Show("Reservación creada exitosamente", "Éxito",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Error al crear la reservación", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscarHabitaciones_Click(object sender, EventArgs e)
        {
            if (listHoteles.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un hotel primero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idHotel = ((Hoteles)listHoteles.SelectedItem).ID_Hotel;
            string nivel = comboHabitacionNivel.SelectedItem.ToString();
            string vista = comboHabitacionVista.SelectedItem.ToString();
            string tipoCama = comboTipoCama.SelectedItem.ToString();

            habitacionesDisponibles = BD_Reservacion.ObtenerHabitacionesDisponibles(
                idHotel,
                dateCheckIn.Value,
                dateCheckOut.Value,
                nivel,
                vista,
                tipoCama,
                (int)numericCamas.Value
            );

            checkedListBoxHabitaciones.Items.Clear();
            foreach (var habitacion in habitacionesDisponibles)
            {
                checkedListBoxHabitaciones.Items.Add(
                    $"Habitación {habitacion.NumeroHabitacion} - Piso {habitacion.PisoHabitacion} - {habitacion.NivelHabitacion}"
                );
            }

            // Cargar servicios disponibles para el hotel
            serviciosDisponibles = BD_Reservacion.ObtenerServiciosPorHotel(idHotel);
            checkedListServicios.Items.Clear();
            foreach (var servicio in serviciosDisponibles)
            {
                checkedListServicios.Items.Add($"{servicio.NombreServicio} - ${servicio.Costo}", false);
            }
        }

        private void checkedListBoxHabitaciones_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Solo permitir una selección
            if (e.NewValue == CheckState.Checked)
            {
                for (int i = 0; i < checkedListBoxHabitaciones.Items.Count; i++)
                {
                    if (i != e.Index)
                    {
                        checkedListBoxHabitaciones.SetItemChecked(i, false);
                    }
                }
            }

            CalcularCostoTotal();
        }

        private void checkedListServicios_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CalcularCostoTotal();
        }


        //Funciones de costos y precios
        private void CalcularCostoTotal()
        {
            costoHabitacion = 0;
            costoServicios = 0;

            // Calcular costo de habitación
            if (checkedListBoxHabitaciones.CheckedItems.Count > 0)
            {
                int index = checkedListBoxHabitaciones.CheckedIndices[0];
                var habitacion = habitacionesDisponibles[index];

                // Precios según nivel
                switch (habitacion.NivelHabitacion)
                {
                    case "Sencilla": costoHabitacion = 1500; break;
                    case "Lujo": costoHabitacion = 3000; break;
                    case "Suite": costoHabitacion = 5000; break;
                }

                // Multiplicar por noches
                costoHabitacion *= numericNoches.Value;
            }

            // Calcular costo de servicios
            foreach (int index in checkedListServicios.CheckedIndices)
            {
                costoServicios += (decimal)serviciosDisponibles[index].Costo;
            }

            costoTotal = costoHabitacion + costoServicios;
            LabelDeCosto.Text = $"Total: ${costoTotal}";
        }
        private void textAnticipo_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(textAnticipo.Text, out decimal anticipo))
            {
                decimal minimo = costoTotal * 0.1m;
                if (anticipo < minimo)
                {
                    MessageBox.Show($"El anticipo mínimo es el 10%: ${minimo}", "Advertencia",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        //Funciones de lectura(CARGA PA QUE ENTIENDAN) puro Tama Impala
        private void CargarDatosIniciales()
        {
            // Cargar clientes
            comboReservacion.DataSource = BD_Reservacion.ObtenerClientes();
            comboReservacion.DisplayMember = "RFC";
            comboReservacion.ValueMember = "RFC";

            // Cargar ciudades
            comboCiudades.DataSource = BD_Reservacion.ObtenerCiudades();

            // Cargar tipos de camas, vistas y niveles (como en tu código)
            comboTipoCama.Items.AddRange(new string[] { "Cama sencilla", "Cama doble", "Cama matrimonial" });
            comboHabitacionVista.Items.AddRange(new string[] { "Frente al mar", "No frente al mar" });
            comboHabitacionNivel.Items.AddRange(new string[] { "Sencilla", "Lujo", "Suite" });

            comboTipoCama.SelectedIndex = 0;
            comboHabitacionVista.SelectedIndex = 0;
            comboHabitacionNivel.SelectedIndex = 0;

            // Configurar fechas
            dateCheckIn.Value = DateTime.Today;
            dateCheckOut.Value = DateTime.Today.AddDays(1);
        }
        private void checkedListBoxHabitaciones_SelectedIndexChanged(object sender, EventArgs e)
        {

        }




    }
}
