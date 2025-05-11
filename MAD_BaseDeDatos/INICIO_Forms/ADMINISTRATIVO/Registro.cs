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
using static ClasesData.Utilidades;

namespace INICIO_Forms.ADMINISTRATIVO
{
    public partial class Registro: Form
    {
        public Registro()
        {
            InitializeComponent();
        }

        private void Registro_Load(object sender, EventArgs e)
        {
            CargarUsuariosEnListBox();
        }


        private void CargarUsuariosEnListBox()
        {
            listBox1.Items.Clear();
            var usuarios = BD_Usuario.ObtenerUsuariosOperativos();
            foreach (var (id, nombre) in usuarios)
            {
                listBox1.Items.Add(new ListItem(nombre, id));
            }
        }

        private void CrearCuenta_Click(object sender, EventArgs e)
        {
            string correo = textCorreo.Text;
            string contraseña = textContra.Text;
            string nombre = textNombre.Text;
            string numeroNomina = textNomina.Text;
            DateTime fechaNacimiento = FechaNacimiento.Value;
            string telefonoCasa = textPhoneCasa.Text;
            string telefonoCelular = textPhone.Text;


            if (!ValidarCorreo(correo))
            {
                MessageBox.Show("El correo debe ser @outlook.com, @gmail.com o @hotmail.com", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidarContraseña(contraseña))
            {
                MessageBox.Show("La contraseña debe tener al menos 8 caracteres, una mayúscula, una minúscula y un carácter especial.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidarNombre(nombre))
            {
                MessageBox.Show("El nombre solo debe contener letras y espacios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidarNumeroNomina(numeroNomina))
            {
                MessageBox.Show("El número de nómina debe tener exactamente 11 dígitos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar que el usuario tenga al menos 21 años
            DateTime fecha = FechaNacimiento.Value;
            int edad = DateTime.Now.Year - fechaNacimiento.Year;

            // Ajustar si aún no ha cumplido años este año
            if (fecha > DateTime.Now.AddYears(-edad))
            {
                edad--;
            }

            if (edad < 21)
            {
                MessageBox.Show("Debes tener al menos 21 años.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (!ValidarTelefono(telefonoCasa) || !ValidarTelefono(telefonoCelular))
            {
                MessageBox.Show("Los teléfonos deben contener solo números y tener al menos 10 dígitos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Usuario nuevo = new Usuario
            {
                CorreoElectronico = textCorreo.Text,
                NombreCompleto = textNombre.Text,
                NumeroNomina = long.Parse(textNomina.Text),
                FechaNacimiento = FechaNacimiento.Value,
                TelefonoCasa = long.Parse(textPhoneCasa.Text),
                TelefonoCelular = long.Parse(textPhone.Text),
                FechaRegistro = DateTime.Now,
                ID_UsuarioRegistro = Sesion.ID_Usuario
            };

            string contrasena = textContra.Text;

            BD_Usuario.CrearUsuarioOperativo(nuevo, contrasena);
            CargarUsuariosEnListBox();

        }
        private void iconButton2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un usuario para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ListItem selectedItem = (ListItem)listBox1.SelectedItem;
            int idUsuario = selectedItem.ID;

            var confirmacion = MessageBox.Show($"¿Estás seguro que deseas eliminar a {selectedItem.Nombre}?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacion == DialogResult.Yes)
            {
                BD_Usuario.EliminarUsuarioOperativo(idUsuario);
                MessageBox.Show("Usuario eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarUsuariosEnListBox();
            }
        }
        private void iconButton1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un usuario para modificar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ListItem selectedItem = (ListItem)listBox1.SelectedItem;
            int idUsuario = selectedItem.ID;

            string nuevoCorreo = textCorreo.Text;
            string nuevoNombre = textNombre.Text;
            string nuevaNomina = textNomina.Text;
            DateTime nuevaFechaNacimiento = FechaNacimiento.Value;
            string nuevoTelCasa = textPhoneCasa.Text;
            string nuevoTelCel = textPhone.Text;
            string nuevaContrasena = textContra.Text; // Nueva contraseña

            // Validaciones
            if (!ValidarCorreo(nuevoCorreo) || !ValidarNombre(nuevoNombre) || !ValidarNumeroNomina(nuevaNomina) ||
                !ValidarTelefono(nuevoTelCasa) || !ValidarTelefono(nuevoTelCel)|| !ValidarContraseña(nuevaContrasena))
            {
                MessageBox.Show("Verifica que todos los datos ingresados sean correctos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Usuario usuarioActualizado = new Usuario
            {
                ID_Usuario = idUsuario,
                CorreoElectronico = nuevoCorreo,
                NombreCompleto = nuevoNombre,
                NumeroNomina = long.Parse(nuevaNomina),
                FechaNacimiento = nuevaFechaNacimiento,
                TelefonoCasa = long.Parse(nuevoTelCasa),
                TelefonoCelular = long.Parse(nuevoTelCel),
                //Es para saber si podemos saber quien modifico
                FechaRegistro = DateTime.Now,
                ID_UsuarioRegistro = Sesion.ID_Usuario

            };

            BD_Usuario.ModificarUsuarioOperativo(usuarioActualizado, nuevaContrasena);

            MessageBox.Show("Usuario modificado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarUsuariosEnListBox();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
                return;

            // Obtener el usuario seleccionado
            ListItem selectedItem = (ListItem)listBox1.SelectedItem;
            int idUsuario = selectedItem.ID;

            // Obtener los datos completos del usuario
            Usuario usuario = BD_Usuario.ObtenerUsuarioPorID(idUsuario);

            if (usuario != null)
            {
                textCorreo.Text = usuario.CorreoElectronico;
                textNombre.Text = usuario.NombreCompleto;
                textNomina.Text = usuario.NumeroNomina.ToString();
                FechaNacimiento.Value = usuario.FechaNacimiento;
                textPhoneCasa.Text = usuario.TelefonoCasa.ToString();
                textPhone.Text = usuario.TelefonoCelular.ToString();

                // Obtener la contraseña (opcional, dependiendo de seguridad), pero como creo que se tiene que cambiar todo ahi lo dejo por 
                textContra.Text = BD_Usuario.ObtenerContraseñaPorID(usuario.ID_Usuario);
            }
        }
    }
}
