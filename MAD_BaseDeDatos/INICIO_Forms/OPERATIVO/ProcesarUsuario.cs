using ClasesData;
using ClasesData.BD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.TextFormatting;


namespace INICIO_Forms.OPERATIVO
{
    public partial class ProcesarUsuario : Form
    {
        private HomeOperativo home;
        public ProcesarUsuario(HomeOperativo home)
        {
            InitializeComponent();
            this.home = home;
            this.FormClosed += new FormClosedEventHandler(ProcesarUsuario_FormClosed);
        }
        private void ProcesarUsuario_FormClosed(object sender, EventArgs e)
        {
            home.Show();
        }

        private void ProcesarUsuario_Load(object sender, EventArgs e)
        {

            CargarClientesEnListBox();
            comboBox1.Items.Add("Casad@");
            comboBox1.Items.Add("Solter@");

        }

        private void CargarClientesEnListBox()
        {
            // Obtén la lista de clientes.
            var clientes = BD_Cliente.ObtenerClientes();
            // Asigna la lista completa al DataSource del ListBox.
            listBox1.DataSource = clientes;
            // Establece la propiedad que se mostrará en el ListBox.
            listBox1.DisplayMember = "DisplayInfo";
        }
        //Crear cuenta
        private void button1_Click(object sender, EventArgs e)
        {
            DateTime fechaNacimiento = dateTimePicker1.Value;

            // Validar datos
            if (!Utilidades.ValidarRFC(textBox6.Text)||
                !Utilidades.ValidarEdadMinima(fechaNacimiento)
                || !Utilidades.ValidarNombre(textBox1.Text)
                || !Utilidades.ValidarCorreo(textBox4.Text)
                || !Utilidades.ValidarTelefono(textBox5.Text)
                || !Utilidades.ValidarTelefono(textBox8.Text))
            {
                MessageBox.Show("Verifica que todos los datos sean correctos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string rfcCliente = (textBox6.Text);

            // Validar que el cliente no exista
            if (BD_Cliente.ClienteExiste(rfcCliente))
            {
                MessageBox.Show("Ya existe un cliente con este RFC.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            // Crear cliente
            Cliente nuevoCliente = new Cliente
            {
                RFC = (rfcCliente),
                NombreCompleto = textBox1.Text,
                CorreoElectronico = textBox4.Text,
                TelefonoCasa = long.Parse(textBox5.Text),  // ✅ Usa long.Parse()
                TelefonoCelular = long.Parse(textBox8.Text),  // ✅ Usa long.Parse()
                EstadoCivil = comboBox1.SelectedItem.ToString(),
                FechaNacimiento = dateTimePicker1.Value,
                Ciudad = textBox3.Text,
                Estado = textBox7.Text,
                Pais = textBox2.Text
            };

            BD_Cliente.CrearCliente(nuevoCliente);
            CargarClientesEnListBox();
            LimpiarCampos();
        }
        // Método para modificar un cliente
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Validar selección de cliente
                if (listBox1.SelectedItem == null)
                {
                    MessageBox.Show("Selecciona un cliente para modificar.", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Obtener datos del formulario
                string seleccionado = listBox1.SelectedItem.ToString();
                string originalRFC = seleccionado.Split('-')[0].Trim();
                string nuevoRFC = textBox6.Text.Trim();
                DateTime fechaNacimiento = dateTimePicker1.Value;

                // 3. Validaciones secuenciales con mensajes específicos
                if (!Utilidades.ValidarRFC(nuevoRFC))
                {
                    MessageBox.Show("RFC no válido. Debe tener formato:\n" +
                                  "- 4 letras + 6 dígitos + 3 caracteres (Personas Morales)\n" +
                                  "- 3 letras + 6 dígitos + 3 caracteres (Personas Físicas)",
                                  "Error en RFC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox6.Focus();
                    return;
                }

                if (!Utilidades.ValidarNombre(textBox1.Text))
                {
                    MessageBox.Show("Nombre no válido. Solo debe contener letras y espacios.",
                                  "Error en Nombre", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox1.Focus();
                    return;
                }

                if (!Utilidades.ValidarCorreo(textBox4.Text))
                {
                    MessageBox.Show("Correo no válido. Debe ser de dominio:\n" +
                                  "outlook.com, gmail.com o hotmail.com",
                                  "Error en Correo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox4.Focus();
                    return;
                }

                if (!Utilidades.ValidarTelefono(textBox5.Text))
                {
                    MessageBox.Show("Teléfono de casa no válido. Debe tener 10 dígitos.",
                                  "Error en Teléfono", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox5.Focus();
                    return;
                }

                if (!Utilidades.ValidarTelefono(textBox8.Text))
                {
                    MessageBox.Show("Teléfono celular no válido. Debe tener 10 dígitos.",
                                  "Error en Celular", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox8.Focus();
                    return;
                }

                if (comboBox1.SelectedItem == null)
                {
                    MessageBox.Show("Selecciona un estado civil válido.",
                                  "Error en Estado Civil", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    comboBox1.Focus();
                    return;
                }

                if (!Utilidades.ValidarEdadMinima(fechaNacimiento))
                {
                    MessageBox.Show("La edad mínima requerida es 21 años.",
                                  "Error en Fecha de Nacimiento", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dateTimePicker1.Focus();
                    return;
                }

                // Validación adicional para campos de ubicación
                if (string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text) ||
                    string.IsNullOrWhiteSpace(textBox7.Text))
                {
                    MessageBox.Show("Todos los campos de ubicación (País, Ciudad, Estado) son obligatorios.",
                                  "Error en Ubicación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                // 5. Crear objeto Cliente con los datos validados
                Cliente clienteModificado = new Cliente
                {
                    RFC = nuevoRFC,
                    NombreCompleto = textBox1.Text,
                    CorreoElectronico = textBox4.Text,
                    TelefonoCasa = long.Parse(textBox5.Text),
                    TelefonoCelular = long.Parse(textBox8.Text),
                    EstadoCivil = comboBox1.SelectedItem.ToString(),
                    FechaNacimiento = fechaNacimiento,
                    Ciudad = textBox3.Text,
                    Estado = textBox7.Text,
                    Pais = textBox2.Text
                };

                // 6. Actualizar en base de datos
                BD_Cliente.ModificarCliente(clienteModificado);


                // 7. Actualizar UI y notificar
                MessageBox.Show("Cliente modificado exitosamente.", "Éxito",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarClientesEnListBox();
                LimpiarCampos();
            }
            catch (FormatException)
            {
                MessageBox.Show("Formato numérico inválido en campos de teléfono.",
                              "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Error de base de datos: {ex.Message}",
                              "Error de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}",
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un cliente para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Se asume que el formato del texto es "RFC - Nombre"
            string seleccionado = listBox1.SelectedItem.ToString();
            // Extrae el RFC tomando la parte ubicada antes del guion y eliminando espacios.
            string rfcCliente = seleccionado.Split('-')[0].Trim();

            // Opcional: Agregar una confirmación antes de eliminar
            DialogResult confirmacion = MessageBox.Show("¿Estás seguro de eliminar el cliente con RFC " + rfcCliente + "?",
                                                          "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacion == DialogResult.Yes)
            {
                BD_Cliente.EliminarCliente(rfcCliente);
                CargarClientesEnListBox();
                LimpiarCampos();
                MessageBox.Show("Cliente eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Asegurarse de que haya un elemento seleccionado.
            if (listBox1.SelectedItem == null)
                return;

            // Obtiene el objeto Cliente seleccionado.
            Cliente clienteSeleccionado = (Cliente)listBox1.SelectedItem;

            // Asigna cada propiedad del cliente a su respectivo control.
            textBox6.Text = clienteSeleccionado.RFC;
            
            textBox1.Text = clienteSeleccionado.NombreCompleto;
            textBox4.Text = clienteSeleccionado.CorreoElectronico;
            
            textBox5.Text = clienteSeleccionado.TelefonoCasa.ToString();
            textBox8.Text = clienteSeleccionado.TelefonoCelular.ToString();
            comboBox1.SelectedItem = clienteSeleccionado.EstadoCivil;
            dateTimePicker1.Value = clienteSeleccionado.FechaNacimiento;
            textBox3.Text = clienteSeleccionado.Ciudad;
            textBox7.Text = clienteSeleccionado.Estado;
            textBox2.Text = clienteSeleccionado.Pais;
        

        }

        private void LimpiarCampos()
        {
            // Limpia los TextBox correspondientes a los campos del cliente
            textBox6.Clear(); // RFC
            textBox1.Clear(); // Nombre Completo
            textBox4.Clear(); // Correo Electrónico
            textBox5.Clear(); // Teléfono Casa
            textBox8.Clear(); // Teléfono Celular
            textBox3.Clear(); // Ciudad
            textBox7.Clear(); // Estado
            textBox2.Clear(); // País

            // Resetea el ComboBox (Estado Civil) a ninguna opción seleccionada
            comboBox1.SelectedIndex = -1;

            // Restablece el DateTimePicker a la fecha actual
            dateTimePicker1.Value = DateTime.Today;
        }



    }
}
