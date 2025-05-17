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
            CargarCiudades();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        // Ademas se tiene que asignar el guid 
        private void buttonGuardarReservacion_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Validar habitación seleccionada
                if (checkedListBoxHabitaciones.CheckedItems.Count == 0)
                {
                    MessageBox.Show("Selecciona una habitación primero.");
                    return;
                }

                // 2. Validar que se haya calculado el total
                if (costoTotal <= 0)
                {
                    MessageBox.Show("Genera el costo total antes de guardar la reservación.");
                    return;
                }

                // 3. Validar anticipo mínimo del 10%
                if (!decimal.TryParse(textAnticipo.Text, out decimal anticipoDecimal))
                {
                    MessageBox.Show("El anticipo ingresado no es válido.");
                    return;
                }

                decimal minimoAnticipo = costoTotal * 0.1m;
                if (anticipoDecimal < minimoAnticipo)
                {
                    MessageBox.Show($"El anticipo debe ser al menos el 10% del total (${minimoAnticipo:N2}).");
                    return;
                }

                // 4. Validar que la cantidad de noches coincida con los días entre fechas
                int dias = (dateCheckOut.Value.Date - dateCheckIn.Value.Date).Days;
                if (dias <= 0)
                {
                    MessageBox.Show("La fecha de salida debe ser posterior a la de entrada.");
                    return;
                }

                if (dias != (int)numericNoches.Value)
                {
                    MessageBox.Show($"El número de noches no coincide con las fechas seleccionadas ({dias} noches).");
                    return;
                }

                // 5. Obtener la habitación seleccionada
                int indexHabitacion = checkedListBoxHabitaciones.CheckedIndices[0];
                int idHabitacion = habitacionesDisponibles[indexHabitacion].ID_Habitacion;

                // 6. Crear objeto reservación
                var reservacion = new Reservacion
                {
                    CodigoReservacion = Guid.NewGuid().ToString(),
                    RFC_Cliente = ((Cliente)comboReservacion.SelectedItem).RFC,
                    ID_Hotel = ((Hoteles)listHoteles.SelectedItem).ID_Hotel,
                    ID_Habitacion = idHabitacion,
                    FechaInicio = dateCheckIn.Value,
                    FechaFin = dateCheckOut.Value,
                    Anticipo = (float)anticipoDecimal,
                    Total = (float)costoTotal,
                    UsuarioRegistro = Sesion.ID_Usuario
                };

                // 7. Guardar en base de datos
                if (BD_Reservacion.CrearReservacion(reservacion))
                {
                    MessageBox.Show("Reservación creada exitosamente.");
                    // Aquí podrías limpiar el formulario o hacer otra acción
                }
                else
                {
                    MessageBox.Show("No se pudo crear la reservación.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear reservación: {ex.Message}");
            }
            this.Close(); // Porque pues el registro de reservacion es unico y nose puede repetir
            homeOperativo.Show();
        }

        private void btnBuscarHabitaciones_Click(object sender, EventArgs e)
        {
            try
            {
                // Resetear variables de costo
                costoTotal = 0;
                costoHabitacion = 0;
                costoServicios = 0;
                LabelDeCosto.Text = "Total: $0";
                textAnticipo.Text = "";


                // 1. Validaciones básicas
                if (listHoteles.SelectedItem == null)
                {
                    MessageBox.Show("Selecciona un hotel primero", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }



                // 2. Obtener parámetros
                int idHotel = ((Hoteles)listHoteles.SelectedItem).ID_Hotel;
                string tipoHabitacion = comboHabitacionNivel.SelectedItem?.ToString() ?? "";
                string vista = comboHabitacionVista.SelectedItem?.ToString() ?? "";
                string tipoCama = comboTipoCama.SelectedItem?.ToString() ?? "";
                int numeroCamas = (int)numericCamas.Value;
                int personas = (int)numericPersonas.Value;

                // 3. Buscar habitaciones
                habitacionesDisponibles = BD_Reservacion.ObtenerHabitacionesDisponibles(
                    idHotel,
                    dateCheckIn.Value,
                    dateCheckOut.Value,
                    tipoHabitacion,
                    vista,
                    tipoCama,
                    personas,
                    numeroCamas
                );

                // 4. Mostrar resultados
                checkedListBoxHabitaciones.Items.Clear();
                if (habitacionesDisponibles.Count == 0)
                {
                    checkedListBoxHabitaciones.Items.Add("No hay habitaciones disponibles con estos filtros");
                }
                else
                {
                    foreach (var habitacion in habitacionesDisponibles)
                    {
                        checkedListBoxHabitaciones.Items.Add(
                            $"{habitacion.NumeroHabitacion} - " +
                            $"Piso {habitacion.PisoHabitacion} - " +
                            $"{habitacion.TipoHabitacion} - " +
                            $"{habitacion.VistaHabitacion} - " +
                            $"{habitacion.NumeroCamas} cama(s)");
                    }
                }

                // 5. Cargar servicios
                serviciosDisponibles = BD_Reservacion.ObtenerServiciosPorHotel(idHotel);
                checkedListServicios.Items.Clear();
                foreach (var servicio in serviciosDisponibles)
                {
                    checkedListServicios.Items.Add($"{servicio.NombreServicio} (${servicio.Costo})", false);
                }
                CalcularCostoTotal(); // Forzar cálculo inicial
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar habitaciones: {ex.Message}\n\nDetalle:\n{ex.StackTrace}",
                              "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void checkedListBoxHabitaciones_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            BeginInvoke((Action)(() =>
            {
                Console.WriteLine($"✅ Se cambió selección de habitación: índice {e.Index}");

                for (int i = 0; i < checkedListBoxHabitaciones.Items.Count; i++)
                {
                    if (i != e.Index && checkedListBoxHabitaciones.GetItemChecked(i))
                    {
                        checkedListBoxHabitaciones.SetItemChecked(i, false);
                    }
                }

                CalcularCostoTotal();
            }));
        }

        private void checkedListServicios_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Esperar a que el cambio se complete antes de calcular
            BeginInvoke((Action)(() => CalcularCostoTotal()));
        }
        //Funciones de costos y precios
        private void CalcularCostoTotal()
        {
            Console.WriteLine($"📦 habitacionesDisponibles.Count = {habitacionesDisponibles?.Count}");

            try
            {
                costoHabitacion = 0;
                costoServicios = 0;

                // 1. Calcular costo de habitación
                if (checkedListBoxHabitaciones.CheckedItems.Count > 0 &&
                    habitacionesDisponibles != null &&
                    habitacionesDisponibles.Count > 0)
                {
                    int selectedIndex = checkedListBoxHabitaciones.CheckedIndices[0];
                    if (selectedIndex >= 0 && selectedIndex < habitacionesDisponibles.Count)
                    {
                        var habitacion = habitacionesDisponibles[selectedIndex];

                        // Precios según tipo de habitación
                        switch (habitacion.TipoHabitacion)
                        {
                            case "Sencilla": costoHabitacion = 1500; break;
                            case "Lujo": costoHabitacion = 3000; break;
                            case "Suite": costoHabitacion = 5000; break;
                            default: costoHabitacion = 0; break;
                        }

                        // Multiplicar por número de noches (mínimo 1)
                        int noches = Math.Max(1, (int)numericNoches.Value);
                        costoHabitacion *= noches;
                    }
                }

                // 2. Calcular costo de servicios
                if (checkedListServicios.CheckedItems.Count > 0 &&
                    serviciosDisponibles != null &&
                    serviciosDisponibles.Count > 0)
                {
                    foreach (int index in checkedListServicios.CheckedIndices)
                    {
                        if (index >= 0 && index < serviciosDisponibles.Count)
                        {
                            costoServicios += (decimal)serviciosDisponibles[index].Costo;
                        }
                    }
                }

                // 3. Calcular total
                costoTotal = costoHabitacion + costoServicios;
                LabelDeCosto.Text = $"Total: ${costoTotal:N2}";

                // 4. Actualizar anticipo mínimo si ya hay un valor
                if (!string.IsNullOrWhiteSpace(textAnticipo.Text) && decimal.TryParse(textAnticipo.Text, out decimal anticipo))
                {
                    decimal minimo = costoTotal * 0.1m;
                    if (anticipo < minimo)
                    {
                        textAnticipo.BackColor = Color.LightPink;
                    }
                    else
                    {
                        textAnticipo.BackColor = SystemColors.Window;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CalcularCostoTotal: {ex.Message}");
            }
        }

        private void numericNoches_ValueChanged(object sender, EventArgs e)
        {
            // Actualizar fecha de salida
            dateCheckOut.Value = dateCheckIn.Value.AddDays((double)numericNoches.Value);

            // Recalcular costo
            CalcularCostoTotal();
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
        public  void checkedListBoxHabitaciones_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void CargarCiudades()
        {
            try
            {
                comboCiudades.DataSource = BD_Reservacion.ObtenerCiudades();
                comboCiudades.SelectedIndex = 0; // Sin selección inicial
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar ciudades: " + ex.Message);
            }
        }
        //Aqui se tendria que cargar las hoteles al momento de seleccionar la ciudad no
        private void comboCiudades_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Limpiar controles dependientes
                listHoteles.DataSource = null;
                checkedListBoxHabitaciones.Items.Clear();
                checkedListServicios.Items.Clear();
                costoTotal = 0;
                costoHabitacion = 0;
                costoServicios = 0;
                LabelDeCosto.Text = "Total: $0";
                textAnticipo.Text = "";

                // Validar selección
                if (comboCiudades.SelectedIndex < 0) return;

                string ciudadSeleccionada = comboCiudades.SelectedItem.ToString();

                // Mostrar carga mientras se consulta
                Cursor.Current = Cursors.WaitCursor;
                listHoteles.DataSource = null;
                listHoteles.Items.Add("Cargando hoteles...");

                // Usar Task para no bloquear la interfaz
                Task.Run(() =>
                {
                    List<Hoteles> hoteles = BD_Reservacion.ObtenerHotelesPorCiudad(ciudadSeleccionada);

                    // Actualizar UI en el hilo principal
                    this.Invoke((MethodInvoker)delegate
                    {
                        Cursor.Current = Cursors.Default;

                        if (hoteles == null || hoteles.Count == 0)
                        {
                            listHoteles.DataSource = null;
                            listHoteles.Items.Add("No se encontraron hoteles en " + ciudadSeleccionada);
                            Timer timer = new Timer();
                            timer.Interval = 6000; // 2 segundos
                            listHoteles.Items.Clear();
                            return;
                        }

                        listHoteles.DataSource = hoteles;
                        listHoteles.DisplayMember = "NombreHotel";
                        listHoteles.ValueMember = "ID_Hotel";

                        // Seleccionar el primer hotel por defecto si hay resultados
                        if (hoteles.Count > 0)
                        {
                            listHoteles.SelectedIndex = 0;
                        }
                    });
                });
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show($"Error al cargar hoteles para {comboCiudades.SelectedItem}: {ex.Message}",
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboReservacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboReservacion.SelectedItem is Cliente clienteSeleccionado)
            {
                textCliente.Text = clienteSeleccionado.NombreCompleto;
            }
            else
            {
                textCliente.Text = string.Empty;
            }
        }

        private void btnGenerarTotal_Click(object sender, EventArgs e)
        {
            try
            {
                costoHabitacion = 0;
                costoServicios = 0;

                // Verificar selección de habitación
                if (checkedListBoxHabitaciones.CheckedItems.Count > 0 &&
                    habitacionesDisponibles != null &&
                    habitacionesDisponibles.Count > 0)
                {
                    int selectedIndex = checkedListBoxHabitaciones.CheckedIndices[0];
                    if (selectedIndex >= 0 && selectedIndex < habitacionesDisponibles.Count)
                    {
                        var habitacion = habitacionesDisponibles[selectedIndex];

                        // Precio según tipo
                        switch (habitacion.TipoHabitacion.Trim().ToLower())
                        {
                            case "sencilla":
                                costoHabitacion = 1500;
                                break;
                            case "lujo":
                                costoHabitacion = 3000;
                                break;
                            case "suite":
                                costoHabitacion = 5000;
                                break;
                            default:
                                costoHabitacion = 0;
                                break;
                        }

                        int noches = Math.Max(1, (int)numericNoches.Value);
                        costoHabitacion *= noches;
                    }
                }

                // Calcular costo de servicios
                if (checkedListServicios.CheckedItems.Count > 0 &&
                    serviciosDisponibles != null &&
                    serviciosDisponibles.Count > 0)
                {
                    foreach (int index in checkedListServicios.CheckedIndices)
                    {
                        if (index >= 0 && index < serviciosDisponibles.Count)
                        {
                            costoServicios += (decimal)serviciosDisponibles[index].Costo;
                        }
                    }
                }

                // Sumar total
                costoTotal = costoHabitacion + costoServicios;
                LabelDeCosto.Text = $"Total: ${costoTotal:N2}";

                // Validar anticipo
                if (!string.IsNullOrWhiteSpace(textAnticipo.Text) &&
                    decimal.TryParse(textAnticipo.Text, out decimal anticipo))
                {
                    decimal minimo = costoTotal * 0.1m;
                    textAnticipo.BackColor = anticipo < minimo ? Color.LightPink : SystemColors.Window;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al calcular el total: {ex.Message}");
            }
        }

        private void listHoteles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
